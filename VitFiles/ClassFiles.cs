using System;
using System.IO;
using System.Threading.Tasks;
using VitFTP;
using VitMysql;
using VitNotifyMessage;
using vitProgressStatus;
using VitTextStringMask;
using VitTypeCard;
using VitUsers;

namespace VitFiles
{
    public class ClassFiles
    {

        private readonly ClassMysql classMysql = new ClassMysql();
        private readonly ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
        

        public ClassFiles()
        {
            
        }

        public void createFile(string path, string hashCode, int idTypeCard)
        {
            
        }

        public void createFile(string[] arrPath, string remotePath)
        {
            FormProgressStatus formProgressStatus = new FormProgressStatus(0, arrPath.GetLength(0));
            int iterator = 0;
            foreach (string path in arrPath)
            {
                formProgressStatus.iterator(iterator, path);
                checkMatchPath(remotePath + "\\" + Path.GetFileName(path));
                iterator++;
            }
        }

        public async Task createFileAsync(string[] arrPath, string remotePath)
        {
            await Task.Run(() => createFile(arrPath, remotePath));
        }

        public bool checkMatchPath(string remotePath)
        {
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            return classFTP.FileExist(remotePath);
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