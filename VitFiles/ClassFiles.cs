using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VitCardPropsValue;
using VitFTP;
using VitMysql;
using VitNotifyMessage;
using vitProgressStatus;
using VitRelationsUsersToFiles;
using VitTextStringMask;
using VitTypeCard;
using vitTypeCardProps;
using VitUsers;

namespace VitFiles
{
    public class ClassFiles
    {
        private readonly ClassMysql classMysql = new ClassMysql();
        private readonly ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
        private FormProgressStatus formProgressStatus = null;
        ClassRelationsUsersToFile classRelationsUsersToFile = new ClassRelationsUsersToFile();

        public string newFileNameGenerator(string sourceFile, string targetPath)
        {
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            int i = 0;
            while (true)
            {
                string fileName = Path.GetFileNameWithoutExtension(sourceFile) + " - копия(" + i.ToString() + ")";
                string fileExtension = Path.GetExtension(sourceFile);
                string newFilePath = targetPath + "/" + fileName + fileExtension;
                if (!classFTP.FileExist(newFilePath))
                    return newFilePath.Replace("\\", "/");

                if(i > 100000)
                {
                    classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.USER_ERROR, "Количество индексов копий привышает системное ограничение в 1000000");
                    return "";
                }
                i++;
            }
        }

        public string[] Copy(string[] arrPath, string targetPath)
        {
            List<string> filesOk = new List<string>();
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            foreach (string sourcePath in arrPath)
            {
                // Задаем новое имя для копии файла
                targetPath = newFileNameGenerator(sourcePath, targetPath);

                // производим копию самого файла на сервере
                classFTP.copyAsync(sourcePath, targetPath.Replace("\\", "/"));
                if (classFTP.FileExist(targetPath))
                {
                    ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
                    classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.USER_ERROR, "Не получилось скопироватиь файл. " + targetPath);
                    continue;
                }

                int targetIdFile = createData(targetPath);
                int sourceIdFile = getInfoByFilePath(sourcePath).id;
                ClassCardPropsValue classCardPropsValue = new ClassCardPropsValue();
                classCardPropsValue.CopyByIdFile(sourceIdFile, targetIdFile);
                filesOk.Add("/" + targetPath);
            }
            return filesOk.ToArray();
        }

        /// <summary>
        /// Создает сущность файла в базе и по завершению выдает его id
        /// </summary>
        /// <param name="path">Удаленный путь к файлу "/directory/fileName.ext"</param>
        private int createData(string path)
        {
            int id = classMysql.Insert("" +
                "INSERT INTO tb_files " +
                "SET " +
                "   path = '" + path + "'");
            ClassUsers classUsers = new ClassUsers();
            ClassRelationsUsersToFile classRelationsUsersToFile = new ClassRelationsUsersToFile();
            classRelationsUsersToFile.add(classUsers.getThisUser().id, id, "Создание файла");
            return id;
        }

        private void UpdateById(int id, string path)
        {
            classMysql.UpdateOrDelete("" +
                "UPDATE tb_files " +
                "SET " +
                "   path = '" + path + "' " +
                "WHERE " +
                "   id = '" + id + "' ");
        }

        private void DeleteDataByPath(string path)
        {
            classMysql.UpdateOrDelete("" +
                "DELETE " +
                "FROM tb_files " +
                "WHERE " + 
                "   path = '" + path + "' ");
        }

        /// <summary>
        /// Создает файл(ы) с карточкой
        /// </summary>
        /// <param name="arrPath">Массив локальных путей к фалам</param>
        /// <param name="remotePath">Директория назначения "/directory/"</param>
        /// <returns></returns>
        public async Task<string[]> CreateFileWithCardAsync(string[] arrPath, string remotePath)
        {
            // делаем поиск дубликатов
            if (await Task.Run(() => ( DuplicateSearch(arrPath, remotePath))) == false) return null;

            // создаем коллекцию для карты файла
            FormFileCard formFileCard = new FormFileCard();
            DialogResult dialogResult = formFileCard.ShowDialog();
            // Если карточка не подтверждена
            if (dialogResult != DialogResult.OK) return null;
            
            FormFileCard.CardPropCollection[] cardPropCollections = new FormFileCard.CardPropCollection[formFileCard.panelCardProps.Controls.Count / 2];

            // собирем значения полей формы в коллекуию карточки документа
            int iterator = 0;
            formProgressStatus = new FormProgressStatus(0, formFileCard.panelCardProps.Controls.Count);
            foreach (Control control in formFileCard.panelCardProps.Controls)
            {
                if (control.Name.Split('_')[0] == "tb")
                {
                    cardPropCollections[iterator].idProp = formFileCard.getValueByControl(control).idProp;
                    cardPropCollections[iterator].text = formFileCard.getValueByControl(control).text;
                }else{
                    formProgressStatus.Iterator(
                        iterator, 
                        control.Text, 
                        "базу данных", 
                        iterator + "/" + formFileCard.panelCardProps.Controls.Count.ToString(),
                        "Сбор данных карточек");
                    continue;
                }
                iterator++;
            }
            formProgressStatus.Close();

            // производим загрузку файлов
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            string[] filesOk = await classFTP.Upload2Async(arrPath, "/" + remotePath + "/", true);

            // производим згрузку данных в базу
            formProgressStatus = new FormProgressStatus(0, filesOk.GetLength(0));
            List<string> uploadRemoteFiles = new List<string>();
            for (int i = 0; i < filesOk.GetLength(0); i++)
            {
                formProgressStatus.Iterator(
                    i,
                    filesOk[i], 
                    "базу данных", 
                    i.ToString() + "/" + filesOk.GetLength(0).ToString(), 
                    "Загрузка файлов на сервер");
                
                int idFile = createData("/" + remotePath + "/" + Path.GetFileName(filesOk[i]));
                classRelationsUsersToFile.add(classUsers.getThisUser().id, idFile, "Создан файл без карточки");
                ClassCardPropsValue classCardPropsValue = new ClassCardPropsValue();
                foreach (var cardPropCollection in cardPropCollections)
                    classCardPropsValue.createValue(cardPropCollection.idProp, cardPropCollection.text, idFile);
                
                
            }
            formProgressStatus.Close();
            formProgressStatus.Dispose();
            return uploadRemoteFiles.ToArray();
        }


        /// <summary>
        /// Производит поиск дубликатов файлов из выбранного списка с файлами в выбранной директории на сервере
        /// </summary>
        /// <param name="arrPath">Список выбранных файлов на клиенте</param>
        /// <param name="remotePath">Директория назначения на сервере</param>
        /// <returns></returns>
        private bool DuplicateSearch(string[] arrPath, string remotePath)
        {
            formProgressStatus = new FormProgressStatus(0, arrPath.GetLength(0));
            List<string> arrSourceFileNames = new List<string>();
            List<string> arrTargetFileNames = new List<string>();
            remotePath = remotePath.Replace("\\", "/");
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            string[] directoryList = classFTP.ListDirectory(remotePath);
            
            
            int iterator = 0;
            foreach (string path in arrPath)
            {
                iterator++;
                formProgressStatus.Iterator(
                    iterator, 
                    path, 
                    remotePath, 
                    iterator.ToString() + "/" + arrPath.GetLength(0).ToString(),
                    "Проверка на наличие дубликатов");
                string targetFileName = "/" + remotePath + "/" + Path.GetFileName(path);
                foreach (string dir in directoryList)
                    if (dir.Equals(targetFileName))
                    {
                        arrSourceFileNames.Add(path);
                        arrTargetFileNames.Add(targetFileName);
                    }
            }
            formProgressStatus.Dispose();

            DialogResult dialogResult = DialogResult.None;
            if (arrSourceFileNames.Count > 0)
            {
                FormDuplicateFileList formDuplicateFileList = new FormDuplicateFileList(arrSourceFileNames.ToArray(), arrTargetFileNames.ToArray());
                dialogResult = formDuplicateFileList.ShowDialog();
            }

            if ((dialogResult != DialogResult.OK) && (dialogResult != DialogResult.None)) return false;
            return true;
        }

        /// <summary>
        /// Загружает файл на сервер не создавая его карточку
        /// </summary>
        /// <param name="arrPath">Массив локальных путей к фалам</param>
        /// <param name="remotePath">Директория назначения "/directory/"</param>
        /// <returns></returns>
        public async Task<string[]> CreateFileWithoutCardAsync(string[] arrPath, string remotePath)
        {
            // делаем поиск дубликатов
            if (await Task.Run(() => ( DuplicateSearch(arrPath, remotePath) )) == false) return null;
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            string[] uploadRemoteFiles = await classFTP.Upload2Async(arrPath, "/" + remotePath + "/" , true);

            
            foreach (string localPath in uploadRemoteFiles)
            {
                int idFile = createData("/" + remotePath + "/" + Path.GetFileName(localPath));
                classRelationsUsersToFile.add(classUsers.getThisUser().id, idFile, "Создан файл без карточки");
            }
            return uploadRemoteFiles;
        }

        public string[] DeleteFiles(string[] remotePathes)
        {
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            formProgressStatus = new FormProgressStatus(0, remotePathes.GetLength(0));
            List<string> filesOk = new List<string>();
            for (int i = 0; i < remotePathes.GetLength(0); i++)
            {
                Console.WriteLine("Удаляем " + remotePathes[i]);
                formProgressStatus.Iterator(
                    i, remotePathes[i], 
                    "вечность",
                    i.ToString() + "/" + remotePathes.GetLength(0).ToString(),
                    "Удаление файлов");
                if (classFTP.DeleteFile(remotePathes[i]) == "")
                {
                    Console.WriteLine("Не удалось удалить физическую копию " + remotePathes);
                    continue;
                }
                Console.WriteLine("Удаляем из базы " + remotePathes[i]);
                DeleteDataByPath(remotePathes[i]);
                var fileInfo = getInfoByFilePath(remotePathes[i]);
                if (fileInfo.path != "")
                {
                    Console.WriteLine("Не удалось удалить файл из базы " + fileInfo.path);
                    continue;
                }
                Console.WriteLine(remotePathes[i] + " удален успешно.");
                filesOk.Add(remotePathes[i]);
            }
            formProgressStatus.Close();
            formProgressStatus.Dispose();
            return filesOk.ToArray();
        }

        public async Task<string[]> MoveFileAsync(string[] sourceArrPath, string targetPath)
        {
            Console.WriteLine(targetPath);
            // делаем поиск дубликатов
            if (await Task.Run(() => ( DuplicateSearch(sourceArrPath, targetPath) )) == false) return null;

            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            
            // переносим файлы на сервере
            string[] movedFilePathOk = await classFTP.moveAsync(sourceArrPath, targetPath);

            // обновляем информацию сущностей файлов в базе
            ClassFiles classFiles = new ClassFiles();
            foreach (string path in movedFilePathOk)
            {
                int idFile = getInfoByFilePath(path).id;
                classFiles.UpdateById(idFile, path);
            }
            
            return movedFilePathOk;
        }

        /// <summary>
        /// Проверяет наличие файла на сервере
        /// </summary>
        /// <param name="remotePath">Удаленный путь к файлу "/directory/filename.ext"</param>
        /// <returns></returns>
        public bool CheckMatchPath(string remotePath)
        {
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            return classFTP.FileExist(remotePath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">"/directory/fileName.ext"</param>
        public FileCollection getInfoByFilePath(string filePath)
        {
            var rows = classMysql.getArrayByQuery("" +
                "SELECT * " +
                "FROM tb_files " +
                "WHERE " +
                "path = '" + filePath + "'");

            FileCollection fileCollection = new FileCollection
            {
                path = "",
                id = 0
            };
            if (rows.GetLength(0) < 1) return fileCollection;

            fileCollection.id = Convert.ToInt32( rows[0]["id"]);
            fileCollection.path = rows[0]["path"];
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            return fileCollection;
        }

        public FileCollection getInfoById(int idFile)
        {
            var rows = classMysql.getArrayByQuery("" +
                "SELECT * " +
                "FROM tb_files " +
                "WHERE " +
                "id = '" + idFile + "'");

            FileCollection fileCollection = new FileCollection
            {
                id = 0,
                path = ""
            };
            if (rows.GetLength(0) < 1) return fileCollection;

            fileCollection.id = Convert.ToInt32(rows[0]["id"]);
            fileCollection.path = rows[0]["path"];
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            return fileCollection;
        }

        public struct FileCollection
        {
            public DateTime createDateTime;
            public int id;
            /// <summary>
            /// disk://directory/fileName.ext
            /// </summary>
            public string path;
        }
    }
}