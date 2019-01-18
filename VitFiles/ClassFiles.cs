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

        public ClassFiles()
        {
            
            //Thread thread = new Thread(() => { formProgressStatus = new FormProgressStatus(); });
            //thread.Start();
        }

        public string[] Copy(string[] arrPath, string targetPath)
        {
            // делаем поиск дубликатов
            if (DuplicateSearch(arrPath, targetPath) == false) return null;

            List<string> filesOk = new List<string>();

            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            foreach (string sourcePath in arrPath)
            {
                classFTP.copyAsync(sourcePath, targetPath);
                ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
                if (classFTP.FileExist(targetPath))
                {
                    classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.USER_ERROR, "Не получилось скопироватиь файл. " + targetPath);
                    continue;
                }
                filesOk.Add(targetPath);
            }
            return filesOk.ToArray();
        }

        public string[] createFileWithCard(string[] arrPath, string remotePath)
        {
            // делаем поиск дубликатов
            if (DuplicateSearch(arrPath, remotePath) == false) return null;

            // создаем коллекцию для карты файла
            FormFileCard formFileCard = new FormFileCard();
            DialogResult dialogResult = formFileCard.ShowDialog();
            FormFileCard.CardPropCollection[] cardPropCollections = new FormFileCard.CardPropCollection[formFileCard.panelCardProps.Controls.Count / 2];

            formProgressStatus = new FormProgressStatus(0, arrPath.GetLength(0));
           
            // Если карточка подтверждена
            if (dialogResult == DialogResult.OK)
            {
                int iterator = 0;
                formProgressStatus = new FormProgressStatus(0, formFileCard.panelCardProps.Controls.Count);
                foreach (Control control in formFileCard.panelCardProps.Controls)
                {
                    
                    if (control.Name.Split('_')[0] == "tb")
                    {
                        cardPropCollections[iterator].idProp = formFileCard.getValueByControl(control).idProp;
                        cardPropCollections[iterator].text = formFileCard.getValueByControl(control).text;
                    }

                    if(control.Name.Split('_')[0] != "tb")
                    {
                        formProgressStatus.Iterator(iterator, "Собираем свойства карточки " + control.Text);
                        continue;
                    }
                    iterator++;
                }
                formProgressStatus.Close();
            }

            formProgressStatus = new FormProgressStatus(0, arrPath.GetLength(0));
            List<string> uploadRemoteFiles = new List<string>();
            for (int i = 0; i < arrPath.GetLength(0); i++)
            {
                formProgressStatus.Iterator(i, "Загрузка на сервер " + arrPath[i]);
                ClassCardPropsValue classCardPropsValue = new ClassCardPropsValue();
                foreach (var cardPropCollection in cardPropCollections)
                {
                    classCardPropsValue.createValue(cardPropCollection.idProp, cardPropCollection.text, "/" + remotePath + "/" + Path.GetFileName(arrPath[i]));
                }
                ClassUsers classUsers = new ClassUsers();
                ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
                classFTP.Upload2Async(arrPath[i], "/" + remotePath + "/" + Path.GetFileName(arrPath[i]), true);
                if(!CheckMatchPath("/" + remotePath + "/" + Path.GetFileName(arrPath[i]))){
                    Console.WriteLine("Не удалось загрузить файл! " + remotePath + "\\" + Path.GetFileName(arrPath[i]));
                }
                else
                {
                    uploadRemoteFiles.Add("/" + remotePath + "/" + Path.GetFileName(arrPath[i]));
                }
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
            List<string> fileNames = new List<string>();
            int iterator = 0;
            foreach (string path in arrPath)
            {
                iterator++;
                formProgressStatus.Iterator(iterator, "Проверка дубликатов " + path);
                if (CheckMatchPath("/" + remotePath + "/" + Path.GetFileName(path)) == true)
                {
                    fileNames.Add(path);
                }
            }
            formProgressStatus.Dispose();

            DialogResult dialogResult = DialogResult.None;
            if (fileNames.Count > 0)
            {
                FormDuplicateFileList formDuplicateFileList = new FormDuplicateFileList(fileNames.ToArray());
                dialogResult = formDuplicateFileList.ShowDialog();
            }

            if ((dialogResult != DialogResult.OK) && (dialogResult != DialogResult.None)) return false;
            return true;
        }

        public string[] createFileWithoutCard(string[] arrPath, string remotePath)
        {
            // делаем поиск дубликатов
            if (DuplicateSearch(arrPath, remotePath) == false) return null;

            // Инициализируем индикатор хода операции
            formProgressStatus = new FormProgressStatus(0, arrPath.GetLength(0));
            // В этот список будут записываться загруженные файлы. Файлы, которые не удалось загрузить в него не попадут
            List<string> uploadRemoteFiles = new List<string>();

            for (int i = 0; i < arrPath.GetLength(0); i++)
            {
                formProgressStatus.Iterator(i, "Загрузка на сервер " + arrPath[i]);
                ClassUsers classUsers = new ClassUsers();
                ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
                classFTP.Upload2Async(arrPath[0], "/" + remotePath + "/" + Path.GetFileName(arrPath[i]), true);
                if (!CheckMatchPath("/" + remotePath + "/" + Path.GetFileName(arrPath[i])))
                {
                    Console.WriteLine("Не удалось загрузить файл! " + arrPath[i] + " -> " + remotePath + "\\" + Path.GetFileName(arrPath[i]));
                }
                else
                {
                    uploadRemoteFiles.Add("/" + remotePath + "/" + Path.GetFileName(arrPath[i]));
                }
            }
            formProgressStatus.Close();
            return uploadRemoteFiles.ToArray();
        }

        public void DeleteFiles(string[] remotePathes)
        {
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            formProgressStatus = new FormProgressStatus(0, remotePathes.GetLength(0));
            for (int i = 0; i < remotePathes.GetLength(0); i++)
            {
                formProgressStatus.Iterator(i, "Удаление " + remotePathes[i]);
                classFTP.DeleteFile(remotePathes[i]);
            }
            formProgressStatus.Close();
            formProgressStatus.Dispose();
        }

        public bool CheckMatchPath(string remotePath)
        {
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            return classFTP.FileExist("/"+remotePath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">z://directory/fileName.ext</param>
        public void getInfoByName(string name)
        {
            
        }

        public struct FileCollection
        {
            public DateTime createDateTime;
            public int hashCode;
            public int id;
            public int idTypeCard;

            /// <summary>
            /// fileName.ext
            /// </summary>
            public string name;

            /// <summary>
            /// disk://directory/fileName.ext
            /// </summary>
            public string path;

            /// <summary>
            /// disk://directory/
            /// </summary>
            public string pathWithoutFileName;
        }
    }
}