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

        public void createFile(string path, string hashCode, int idTypeCard)
        {
            
        }

        public void createFile(string[] arrPath, string remotePath)
        {
            formProgressStatus = new FormProgressStatus(0, arrPath.GetLength(0));
            int iterator = 0;
            List<string> fileNames = new List<string>();
            // делаем поиск дубликатов
            foreach (string path in arrPath)
            {
                iterator++;
                formProgressStatus.Iterator(iterator, "Проверка дубликатов "+path);
                if(checkMatchPath(remotePath + "\\" + Path.GetFileName(path)) == true)
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

            if ((dialogResult != DialogResult.OK) && (dialogResult != DialogResult.None)) return;

            // создаем коллекцию для карты файла
            FormFileCard formFileCard = new FormFileCard();
            dialogResult = formFileCard.ShowDialog();
            FormFileCard.CardPropCollection[] cardPropCollections = new FormFileCard.CardPropCollection();
            return;
            if (dialogResult == DialogResult.OK)
            {
                int i = 0;
                foreach (Control control in formFileCard.panelCardProps.Controls)
                {
                    cardPropCollections[i].idProp = formFileCard.getValueByControl(control).idProp;
                    cardPropCollections[i].text = formFileCard.getValueByControl(control).text;
                    i++;
                }
            }

            foreach (string path in arrPath) {
                ClassCardPropsValue classCardPropsValue = new ClassCardPropsValue();
                classCardPropsValue.createValue(cardPropCollections[i].idPro, cardPropCollections[i].text, remotePath + "\\" + Path.GetFileName(path));
            }
            return;
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