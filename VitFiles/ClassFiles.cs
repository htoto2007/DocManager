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

        

        public string[] createFile(string[] arrPath, string remotePath)
        {
            formProgressStatus = new FormProgressStatus(0, arrPath.GetLength(0));
            
            int iterator = 0;
            List<string> fileNames = new List<string>();
            // делаем поиск дубликатов
            foreach (string path in arrPath)
            {
                iterator++;
                formProgressStatus.Iterator(iterator, "Проверка дубликатов "+path);
                if(checkMatchPath("/" + remotePath + "/" + Path.GetFileName(path)) == true)
                {
                    fileNames.Add(path);
                }
            }

            DialogResult dialogResult = DialogResult.None;
            if(fileNames.Count > 0)
            {
                FormDuplicateFileList formDuplicateFileList = new FormDuplicateFileList(fileNames.ToArray());
                dialogResult = formDuplicateFileList.ShowDialog();
            }

            if ((dialogResult != DialogResult.OK) && (dialogResult != DialogResult.None)) return null;

            // создаем коллекцию для карты файла
            FormFileCard formFileCard = new FormFileCard();
            dialogResult = formFileCard.ShowDialog();
            FormFileCard.CardPropCollection[] cardPropCollections = new FormFileCard.CardPropCollection[formFileCard.panelCardProps.Controls.Count / 2];
            
            // Если карточка подтверждена
            if (dialogResult == DialogResult.OK)
            {
                int i = 0;
                formProgressStatus = new FormProgressStatus(0, formFileCard.panelCardProps.Controls.Count);
                foreach (Control control in formFileCard.panelCardProps.Controls)
                {
                    
                    if (control.Name.Split('_')[0] == "tb")
                    {
                        cardPropCollections[i].idProp = formFileCard.getValueByControl(control).idProp;
                        cardPropCollections[i].text = formFileCard.getValueByControl(control).text;
                    }

                    if(control.Name.Split('_')[0] != "tb")
                    {
                        formProgressStatus.Iterator(i, "Собираем свойства карточки " + control.Text);
                        continue;
                    }
                    i++;
                }
                formProgressStatus.Close();
            }

            formProgressStatus = new FormProgressStatus(0, arrPath.GetLength(0));
            iterator = 0;
            string[] uploadRemoteFiles = new string[arrPath.GetLength(0)];
            foreach (string path in arrPath)
            {
                formProgressStatus.Iterator(iterator, "Загрузка на сервер " + path);
                ClassCardPropsValue classCardPropsValue = new ClassCardPropsValue();
                foreach (var cardPropCollection in cardPropCollections)
                {
                    classCardPropsValue.createValue(cardPropCollection.idProp, cardPropCollection.text, "/" + remotePath + "/" + Path.GetFileName(path));
                }
                ClassUsers classUsers = new ClassUsers();
                ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
                classFTP.Upload2Async(path, "/" + remotePath + "/" + Path.GetFileName(path), true);
                if(!checkMatchPath("/" + remotePath + "/" + Path.GetFileName(path))){
                    Console.WriteLine("Не удалось загрузить файл! " + remotePath + "\\" + Path.GetFileName(path));
                }
                else
                {
                    uploadRemoteFiles[iterator] = "/" + remotePath + "/" + Path.GetFileName(path);
                }
                iterator++;
            }
            formProgressStatus.Close();
            return uploadRemoteFiles;
        }

        public string[] createFileWithoutCard(string[] arrPath, string remotePath)
        {
            formProgressStatus = new FormProgressStatus(0, arrPath.GetLength(0));

            int iterator = 0;
            List<string> fileNames = new List<string>();
            // делаем поиск дубликатов
            foreach (string path in arrPath)
            {
                iterator++;
                formProgressStatus.Iterator(iterator, "Проверка дубликатов " + path);
                if (checkMatchPath("/" + remotePath + "/" + Path.GetFileName(path)) == true)
                {
                    fileNames.Add(path);
                }
            }

            DialogResult dialogResult = DialogResult.None;
            if (fileNames.Count > 0)
            {
                FormDuplicateFileList formDuplicateFileList = new FormDuplicateFileList(fileNames.ToArray());
                dialogResult = formDuplicateFileList.ShowDialog();
            }

            if ((dialogResult != DialogResult.OK) && (dialogResult != DialogResult.None)) return null;

            formProgressStatus = new FormProgressStatus(0, arrPath.GetLength(0));
            iterator = 0;
            string[] uploadRemoteFiles = new string[arrPath.GetLength(0)];
            foreach (string path in arrPath)
            {
                formProgressStatus.Iterator(iterator, "Загрузка на сервер " + path);
                
                ClassUsers classUsers = new ClassUsers();
                ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
                classFTP.Upload2Async(path, "/" + remotePath + "/" + Path.GetFileName(path), true);
                if (!checkMatchPath("/" + remotePath + "/" + Path.GetFileName(path)))
                {
                    Console.WriteLine("Не удалось загрузить файл! " + remotePath + "\\" + Path.GetFileName(path));
                }
                else
                {
                    uploadRemoteFiles[iterator] = "/" + remotePath + "/" + Path.GetFileName(path);
                }
                iterator++;
            }
            formProgressStatus.Close();
            return uploadRemoteFiles;
        }

        public void deleteFiles(string[] remotePathes)
        {
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            formProgressStatus = new FormProgressStatus(0, remotePathes.GetLength(0));
            int iterator = 0;
            foreach (string file in remotePathes)
            {
                iterator++;
                formProgressStatus.Iterator(iterator, "Удаление " + file);
                classFTP.DeleteFile(file);
            }
        }

        public bool checkMatchPath(string remotePath)
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