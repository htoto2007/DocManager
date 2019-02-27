using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VitFTP;
using VitMysql;
using VitNotifyMessage;
using vitProgressStatus;
using VitRelationsUsersToFiles;
using VitUsers;

namespace VitFiles
{
    public class ClassFiles
    {
        private readonly ClassMysql classMysql = new ClassMysql();
        private readonly ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
        private FormProgressStatus formProgressStatus = null;
        ClassRelationsUsersToFile classRelationsUsersToFile = new ClassRelationsUsersToFile();

        /// <summary>
        /// Генерирует новое имя файла не похожее на другие
        /// </summary>
        /// <param name="sourceFile">Путь к файлу</param>
        /// <param name="targetPath">путь назначения</param>
        /// <param name="classFTP">Экземпляр класс FTP</param>
        /// <returns></returns>
        public string newFileNameGenerator(string sourceFile, string targetPath, ClassFTP classFTP)
        {
            int i = 0;
            while (true)
            {
                string fileName = Path.GetFileNameWithoutExtension(sourceFile) + " - копия(" + i.ToString() + ")";
                string fileExtension = Path.GetExtension(sourceFile);
                string newFilePath = targetPath + "/" + fileName + fileExtension;
                if (!classFTP.FileExist(newFilePath))
                {
                    //classFTP.sessionClose();
                    return newFilePath.Replace("\\", "/");
                }

                if(i > 100000)
                {
                    classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.USER_ERROR, "Количество индексов копий привышает системное ограничение в 1000000");
                    break;
                }
                i++;
            }
            return "";
        }

        public string[] Copy(string[] arrPath, string targetPath)
        {
            List<string> filesOk = new List<string>();
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            classFTP.SessionOpen();
            foreach (string sourcePath in arrPath)
            {
                // Задаем новое имя для копии файла
                targetPath = newFileNameGenerator(sourcePath, targetPath, classFTP);

                // производим копию самого файла на сервере
                int res = classFTP.copy(sourcePath, targetPath.Replace("\\", "/"));
                if (res != 0)
                {
                    Console.WriteLine("Ошибка копирования: " + res);
                    Console.WriteLine(sourcePath);
                    continue;
                }

                int targetIdFile = createData(targetPath);
                int sourceIdFile = getInfoByFilePath(sourcePath).id;
                //ClassCardPropValue classCardPropsValue = new ClassCardPropValue();
                //classCardPropsValue.CopyByIdFile(sourceIdFile, targetIdFile);
                filesOk.Add("/" + targetPath);
            }
            classFTP.sessionClose();
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
                "   path = '" + path + "' ");
            ClassUsers classUsers = new ClassUsers();
            ClassRelationsUsersToFile classRelationsUsersToFile = new ClassRelationsUsersToFile();
            classRelationsUsersToFile.add(classUsers.getThisUser().id, id, "Создание файла");
            return id;
        }

        /// <summary>
        /// Обновляет информациюо пути у файлу в базе
        /// </summary>
        /// <param name="id">Номер файла</param>
        /// <param name="path">Новый путь</param>
        private void UpdateById(int id, string path)
        {
            classMysql.UpdateOrDelete("" +
                "UPDATE tb_files " +
                "SET " +
                "   path = '" + path + "' " +
                "WHERE " +
                "   id = '" + id + "' ");
        }

        /// <summary>
        /// Удаляет информацию о файле из базы по пути на сервере
        /// </summary>
        /// <param name="path"></param>
        private void DeleteDataByPath(string path)
        {
            classMysql.UpdateOrDelete("" +
                "DELETE " +
                "FROM tb_files " +
                "WHERE " + 
                "   path = '" + path + "' ");
        }


        /// <summary>
        /// Производит поиск дубликатов файлов из выбранного списка с файлами в выбранной директории на сервере
        /// </summary>
        /// <param name="arrPath">Список выбранных файлов на клиенте</param>
        /// <param name="remotePath">Директория назначения на сервере</param>
        /// <returns></returns>
        private async Task<bool> DuplicateSearchAsync(string[] arrPath, string remotePath)
        {
            formProgressStatus = new FormProgressStatus(0, arrPath.GetLength(0));
            formProgressStatus.buttonCancel.Enabled = false;
            List<string> arrSourceFileNames = new List<string>();
            List<string> arrTargetFileNames = new List<string>();
            remotePath = remotePath.Replace("\\", "/");

            // получаем список файлов из директории, в которую будем осуществлять отправку файлов
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            classFTP.SessionOpen();
            string[] directoryList = classFTP.ListDirectory(remotePath);
            classFTP.sessionClose();
            
            int iterator = 0;
            // производим сравнение списка отправляемых файлов со списком из директории назначения
            foreach (string path in arrPath)
            {
                formProgressStatus.Iterator(
                 iterator,
                 path,
                 remotePath,
                 iterator.ToString() + "/" + arrPath.GetLength(0).ToString(),
                 "Проверка на наличие дубликатов");

                string targetFileName = remotePath + "/" + Path.GetFileName(path);
                foreach (string dir in directoryList)
                {
                    if (dir.Equals(targetFileName))
                    {
                        arrSourceFileNames.Add(path);
                        arrTargetFileNames.Add(targetFileName);
                        continue;
                    }
                }
                iterator++;
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
        /// Загружает файл на сервер не создавая его карточку. 
        /// В результате выдает массив локальных путей успешно загруженных файлов
        /// </summary>
        /// <param name="arrPath">Массив локальных путей к фалам</param>
        /// <param name="remotePath">Директория назначения "/directory/"</param>
        /// <returns></returns>
        public async Task<string[]> CreateFileAsync(string[] arrPath, string remotePath)
        {
            // делаем поиск дубликатов
            if (await DuplicateSearchAsync(arrPath, remotePath) == false) return null;

            // грузим файлы на сервер
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            classFTP.SessionOpen();
            string[] uploadRemoteFiles = await classFTP.UploadAsync(arrPath, "/" + remotePath + "/" , true);
            classFTP.sessionClose();

            FormProgressStatus formProgressStatus = new FormProgressStatus(0, uploadRemoteFiles.GetLength(0));
            formProgressStatus.buttonCancel.Enabled = false;
            await Task.Run(() => { Thread.Sleep(1000); });
            int iterator = 0;
            // отправляем информацию о загруженых файлах в базу
            foreach (string localPath in uploadRemoteFiles)
            {
                formProgressStatus.Iterator(
                    iterator,
                    localPath,
                    "Запись в базу данных",
                    iterator.ToString() + "/" + uploadRemoteFiles.GetLength(0).ToString(),
                    "Регистрация файлов в базе");

                int idFile = createData(remotePath + "/" + Path.GetFileName(localPath));
                classRelationsUsersToFile.add(classUsers.getThisUser().id, idFile, "Создан файл без карточки");
                iterator++;
            }
            formProgressStatus.Close();
            formProgressStatus.Dispose();
            return uploadRemoteFiles;
        }

        /// <summary>
        /// Запускает методы для удаления файла или папки
        /// </summary>
        /// <param name="remotePathes">удаленный путь к файлу</param>
        /// <returns></returns>
        public async Task<string[]> DeleteFiles(string[] remotePathes)
        {
            formProgressStatus = new FormProgressStatus(0, remotePathes.GetLength(0));
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            // список для хранения путей файлов, которые были успешно удалены
            List<string> filesOk = new List<string>();

            classFTP.SessionOpen();
            for (int i = 0; i < remotePathes.GetLength(0); i++)
            {
                // проверяем отмену операции
                if (formProgressStatus.IsDisposed == true) return filesOk.ToArray();

                // итератор индикатора загрузки
                formProgressStatus.Iterator(
                    i, remotePathes[i], 
                    "вечность",
                    i.ToString() + "/" + remotePathes.GetLength(0).ToString(),
                    "Удаление файлов");
                // делаем запрос на удаление
                bool resDelete = classFTP.RemoveDirecroty(remotePathes[i]);
                // проверяем результаты
                if (!resDelete)
                {
                    Console.WriteLine("Не удалось удалить физическую копию " + remotePathes);
                    continue;
                }

                // удаляем информацию из базы данных
                await Task.Run(() => { DeleteDataByPath(remotePathes[i]); } );
                // делаем контрольный запрос
                FileCollection fileInfo = new FileCollection();
                await Task.Run(() => { fileInfo = getInfoByFilePath(remotePathes[i]); });
                // проверяем результаты
                if (fileInfo.path != "")
                {
                    Console.WriteLine("Не удалось удалить файл из базы " + fileInfo.path);
                    continue;
                }
                filesOk.Add(remotePathes[i]);
            }
            classFTP.sessionClose();
            formProgressStatus.Close();
            formProgressStatus.Dispose();
            return filesOk.ToArray();
        }

        public async Task<string[]> MoveFileAsync(string[] sourceArrPath, string targetPath)
        {
            // делаем поиск дубликатов
            if (await DuplicateSearchAsync(sourceArrPath, targetPath) == false) return null;

            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            classFTP.SessionOpen();
            // переносим файлы на сервере
            string[] movedFilePathOk = classFTP.move(sourceArrPath, targetPath);
            classFTP.sessionClose();
            
            // проверяем результат
            if (movedFilePathOk.GetLength(0) != sourceArrPath.GetLength(0))
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Не удалось переместить часть файлов.");

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
        /// Выдает информацию о файле по его пути
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
            return fileCollection;
        }

        /// <summary>
        /// Выдает информацию о файле по его номеру
        /// </summary>
        /// <param name="idFile">номер файла</param>
        /// <returns></returns>
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
            /// <summary>
            /// Дата создания файла
            /// </summary>
            public DateTime createDateTime;
            /// <summary>
            /// номер файла
            /// </summary>
            public int id;
            /// <summary>
            /// disk://directory/fileName.ext
            /// </summary>
            public string path;
        }
    }
}