using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WinSCP;
using VitNotifyMessage;
using vitProgressStatus;

namespace VitFTP
{
    /// <summary>
    /// https://kbss.ru/blog/lang_c_sharp/107.html
    /// </summary>
    public class ClassFTP
    {
        /// <summary>
        /// Включает двоичный тип файлов при загрузке
        /// </summary>
        public bool Binary = true;
        /// <summary>
        /// Включает SSL протокол при подключении
        /// </summary>
        public bool EnableSsl = false;
        /// <summary>
        /// Включает генерацию hash
        /// </summary>
        public bool Hash = false;
        /// <summary>
        /// Отвечает за работу в пасивном режиме
        /// </summary>
        public bool Passive = true;
        private readonly int bufferSize = 1024;
        private readonly string password;
        private readonly SessionOptions sessionOptions;
        private readonly string userName;
        ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();

        private string uri;
        private FormProgressStatus formProgressStatus;



        private static void SessionFileTransferProgress(
        object sender, FileTransferProgressEventArgs e)
        {
            
            // Print transfer progress
            Console.Write("\r{0} ({1:P0})", e.FileName, e.FileProgress);
            
        }


        /// <summary>
        /// Конструктор класса. Инициализирует подключение после объявления экземпляоа класса.
        /// </summary>
        /// <param name="userName">имя пользователя FTP</param>
        /// <param name="password">Gfhjkm gjkmpjdfntkz АЕЗ</param>
        public ClassFTP(string userName, string password)
        {
            uri = "ftp://" + VitSettings.Properties.FTPSettings.Default.host + ":" + VitSettings.Properties.FTPSettings.Default.port + "/";
            this.userName = userName;
            this.password = password;

            try
            {
                sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Ftp,
                    HostName = VitSettings.Properties.FTPSettings.Default.host,
                    UserName = userName,
                    Password = password,
                    GiveUpSecurityAndAcceptAnySshHostKey = false,
                };
            }
            catch (Exception e)
            {
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, e.Message);
            }
        }

        /// <summary>
        /// выдает информацию о параметрах доступа
        /// </summary>
        /// <param name="path">путь к файлу у которого нужно узнать параметры доступа</param>
        /// <returns></returns>
        public AccessToFolder getAccess(string path)
        {
            AccessToFolder accessToFolder = new AccessToFolder();
            using (Session session = new Session())
            {
                // Connect
                session.Open(sessionOptions);
                RemoteFileInfo remoteFileInfo = session.GetFileInfo(path);
                accessToFolder.execute = remoteFileInfo.FilePermissions.UserExecute;
                accessToFolder.read = remoteFileInfo.FilePermissions.UserRead;
                accessToFolder.write = remoteFileInfo.FilePermissions.UserWrite;
                session.Close();
            }

            return accessToFolder;
        }

        /// <summary>
        /// коллекция параметров доступа
        /// </summary>
        public struct AccessToFolder
        {
            /// <summary>
            /// чтение
            /// </summary>
            public bool read;
            /// <summary>
            /// запись
            /// </summary>
            public bool write;
            /// <summary>
            /// запуск
            /// </summary>
            public bool execute;
        }

        public string ChangeWorkingDirectory(string path)
        {
            uri = combine(uri, path);

            return PrintWorkingDirectory();
        }

        /// <summary>
        /// Определяет тип файла (файл или папка)
        /// </summary>
        /// <param name="remoteFilePath">Путь к файлу</param>
        /// <returns>0 - информация отсутствует, 1 - является файлом, 2 - является папкой</returns>
        public int getFileType(string remoteFilePath)
        {
            RemoteFileInfo remoteFileInfo = null;
            using (Session session = new Session())
            {
                // Connect
                session.Open(sessionOptions);
                remoteFileInfo = session.GetFileInfo(remoteFilePath);
                session.Close();
            }
            if (remoteFileInfo == null) return 0;
            if (remoteFileInfo.IsDirectory) return 2;
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourcePath">sourceDirectory\fileName.ext</param>
        /// <param name="targetPath">targetDirectory\fileName.ext</param>
        /// <returns></returns>
        public async Task copyAsync(string sourcePath, string targetPath)
        {
            using (Session session = new Session())
            {
                // Connect
                session.Open(sessionOptions);
                await Task.Run(() => EraseDirectory(VitSettings.Properties.FTPSettings.Default.pathTnp));
                Console.WriteLine("sourcePath: " + sourcePath);
                Console.WriteLine("FTP file type " + getFileType(sourcePath.Replace("\\", "/")));
                if (getFileType(sourcePath.Replace("\\", "/")) == 2)
                {
                    await Task.Run(() => Directory.CreateDirectory(VitSettings.Properties.FTPSettings.Default.pathTnp + "\\" + Path.GetFileName(sourcePath)));
                    await Task.Run(() => session.GetFiles(sourcePath, VitSettings.Properties.FTPSettings.Default.pathTnp + "\\" + Path.GetFileName(sourcePath)));
                    await Task.Run(() => session.PutFiles(VitSettings.Properties.FTPSettings.Default.pathTnp + "\\" + Path.GetFileName(sourcePath), targetPath + "\\"));
                }
                else
                {
                    DownloadFile(sourcePath, VitSettings.Properties.FTPSettings.Default.pathTnp + "\\" + Path.GetFileName(sourcePath));
                    session.PutFiles(VitSettings.Properties.FTPSettings.Default.pathTnp + "\\" + Path.GetFileName(sourcePath), targetPath);
                }
                await Task.Run(() => EraseDirectory(VitSettings.Properties.FTPSettings.Default.pathTnp));
                session.Close();
            }
        }

        public bool CreateDirectory(string path)
        {
            if (FileExist(path))
            {
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.USER_ERROR, "Папака " + Path.GetFileName(path) + " уже существует.");
                return false;
            }
            using (Session session = new Session())
            {
                // Connect
                session.Open(sessionOptions);
                session.CreateDirectory(path);
                session.Close();
            }
            return FileExist(path);
        }

        

        public string DeleteFile(string fileName)
        {
            FtpWebRequest request = createRequest(combine(uri, fileName.TrimStart('/')), WebRequestMethods.Ftp.DeleteFile);

            return getStatusDescription(request);
        }

        public string DownloadFile(string source, string dest)
        {
            FtpWebRequest request = createRequest(combine(uri, source), WebRequestMethods.Ftp.DownloadFile);

            byte[] buffer = new byte[bufferSize];

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (FileStream fs = new FileStream(dest, FileMode.OpenOrCreate))
                    {
                        int readCount = stream.Read(buffer, 0, bufferSize);

                        while (readCount > 0)
                        {
                            if (Hash)
                            {
                                Console.Write("#");
                            }

                            fs.Write(buffer, 0, readCount);
                            readCount = stream.Read(buffer, 0, bufferSize);
                        }
                    }
                }

                return response.StatusDescription;
            }
        }

        /// <summary>
        /// Определяет наличие файла по указанному пути сервере. 
        /// </summary>
        /// <param name="path">"directoey/fileName.ext" Можнт принемать пути файлов USNIX и WINSOWS</param>
        /// <returns></returns>
        public bool FileExist(string path)
        {
            bool res = false;
            path = path.Replace("\\", "/");
            Session session = new Session();

            try
            {
                session.Open(sessionOptions);
            }
            catch (Exception e)
            {
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Не подключиться к сераеру. " + e.Message);
                return false;
            }
            res = session.FileExists(path);
            session.Close();

            return res;
        }

        public Icon getIconFile(string path)
        {
            Icon icon;

            // Connect
            icon = Icon.ExtractAssociatedIcon(path);
            string pathFileTypeIcons = VitSettings.Properties.GeneralsSettings.Default.fileTypeIcons;
            string extension = Path.GetExtension(path);
            string fileName = pathFileTypeIcons + "\\" + extension + ".png";
            if (!File.Exists(fileName))
            {
                icon.ToBitmap().Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
            }

            return icon;
        }

        public struct FileCollection
        {
            public string path;
            public bool isDirectory;
            public char fileType;
            public bool IsParentDirectory;
        }

        public FileCollection[] ListDirectoryDetail(string path)
        {
            Session session = new Session();
            try
            {
                session.Open(sessionOptions);
            }catch (Exception e)
            {
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Не подключиться к сераеру. " + e.Message);
                return null;
            }
            RemoteFileInfoCollection files = session.ListDirectory(path).Files;

            FileCollection[] fileCollection = new FileCollection[files.Count];
            for (int i = 0; i < files.Count; i++)
            {
                fileCollection[i].path = files[i].FullName;
                fileCollection[i].isDirectory = files[i].IsDirectory;
                fileCollection[i].fileType = files[i].FileType;
            }
            session.Close();
            return fileCollection;
        }

        public string[] ListDirectory(string path)
        {
            Session session = new Session();
            try
            {
                session.Open(sessionOptions);
            }
            catch (Exception e)
            {
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Не подключиться к сераеру. " + e.Message);
                return null;
            }
            RemoteFileInfoCollection files = session.ListDirectory(path).Files;
            List<string> fileList = new List<string>();
            
            for (int i = 0; i < files.Count; i++)
                fileList.Add(files[i].FullName);
            session.Close();
            return fileList.ToArray();
        }

        public string[] ListDirectoryWithotFiles(string path)
        {
            List<string> fileList = new List<string>();
            Session session = new Session();
            try
            {
                session.Open(sessionOptions);
            }
            catch (Exception e)
            {
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Не подключиться к сераеру. " + e.Message);
                return null;
            }
            RemoteFileInfoCollection files = session.ListDirectory(path).Files;

            foreach (RemoteFileInfo file in files)
                if(file.IsDirectory)
                    fileList.Add(file.FullName.Replace("\\", "/"));
            session.Close();

            return fileList.ToArray();
        }
        
        public string MakeDirectory(string directoryName)
        {
            FtpWebRequest request = createRequest(combine(uri, directoryName), WebRequestMethods.Ftp.MakeDirectory);
            return getStatusDescription(request);
        }

        /// <summary>
        /// Перемещает файлы на сервере и возвращает Массив изначальных путей
        /// </summary>
        /// <param name="sourceArrPath">начальный путь (пути к переносимым фалам) "/directory/fileName.ext"</param>
        /// <param name="targetPath">Путь к папке назначения "/directory/"</param>
        /// <returns></returns>
        public async Task<string[]> moveAsync(string[] sourceArrPath, string targetPath)
        {
            string pathMove = "";
            List<string> arrOkPathsMove = new List<string>();
            using (Session session = new Session())
            {
                // Connect
                session.Open(sessionOptions);
                foreach (string sourcePath in sourceArrPath)
                {
                    await Task.Run(() => EraseDirectory(VitSettings.Properties.FTPSettings.Default.pathTnp));
                    if (Path.GetExtension(sourcePath) == "")
                    {
                        await Task.Run(() => Directory.CreateDirectory(VitSettings.Properties.FTPSettings.Default.pathTnp + "\\" + Path.GetFileName(sourcePath)));
                        await Task.Run(() => session.GetFiles(sourcePath, VitSettings.Properties.FTPSettings.Default.pathTnp + "\\" + Path.GetFileName(sourcePath)));
                        await Task.Run(() => session.PutFiles(VitSettings.Properties.FTPSettings.Default.pathTnp + "\\" + Path.GetFileName(sourcePath), targetPath));
                        await Task.Run(() => session.RemoveFiles(sourcePath));
                    }
                    else
                    {
                        session.GetFiles(sourcePath, VitSettings.Properties.FTPSettings.Default.pathTnp + "\\" + Path.GetFileName(sourcePath));
                        session.PutFiles(VitSettings.Properties.FTPSettings.Default.pathTnp + "\\" + Path.GetFileName(sourcePath), targetPath);
                    }
                    await Task.Run(() => EraseDirectory(VitSettings.Properties.FTPSettings.Default.pathTnp));

                    pathMove = "/" + targetPath.Replace("\\", "/") + "/" + Path.GetFileName(sourcePath);
                    // проверяем результат переноса файла
                    if (FileExist(pathMove) == true)
                    {
                        arrOkPathsMove.Add(sourcePath);
                        await Task.Run(() => DeleteFile(sourcePath));
                    }
                }
                session.Close();
            }

            return arrOkPathsMove.ToArray();
        }

        public bool DownloadFile2(string source, string destination)
        {

            Session session = new Session();

            // Connect
            session.Open(sessionOptions);
            TransferOperationResult transferOperationResult = session.GetFiles(source, destination, false, null);
            session.Close();
            return transferOperationResult.IsSuccess;
        }

        public string PrintWorkingDirectory()
        {
            FtpWebRequest request = createRequest(WebRequestMethods.Ftp.PrintWorkingDirectory);

            return getStatusDescription(request);
        }

        public bool RemoveDirecroty(string directoryName)
        {
            RemovalOperationResult removalOperationResult;
            using (Session session = new Session())
            {
                // Connect
                session.Open(sessionOptions);
                Console.WriteLine("RemoveDirecroty " + directoryName.Replace("\\", "/"));
                removalOperationResult = session.RemoveFiles(directoryName);
                if (removalOperationResult.IsSuccess == false)
                {
                    SessionRemoteExceptionCollection sessionRemoteExceptions = removalOperationResult.Failures;
                    MessageBox.Show(sessionRemoteExceptions[0].Message);
                }
                session.Close();
            }
            Console.WriteLine(removalOperationResult.IsSuccess.ToString());
            return removalOperationResult.IsSuccess;
        }

        public FtpFileInfo getFileInfo(string filePath)
        {
            FtpFileInfo ftpFileInfo = new FtpFileInfo();
            using (Session session = new Session())
            {
                // Connect
                session.Open(sessionOptions);
                RemoteFileInfo remoteFileInfo = session.GetFileInfo(filePath);
                //ftpFileInfo.isExecure = remoteFileInfo.FilePermissions.UserExecute;
                //ftpFileInfo.isRead = remoteFileInfo.FilePermissions.UserRead;
                //ftpFileInfo.isWrite = remoteFileInfo.FilePermissions.UserWrite;
                ftpFileInfo.IsDirectory = remoteFileInfo.IsDirectory;
                ftpFileInfo.LastWriteTime = remoteFileInfo.LastWriteTime;
                ftpFileInfo.fullName = remoteFileInfo.FullName;
                session.Close();
            }

            return ftpFileInfo;
        }

        public struct FtpFileInfo
        {
            public bool isExecure;
            public bool isRead;
            public bool isWrite;
            public bool IsDirectory;
            public DateTime LastWriteTime;
            public string fullName;
        }

        public string Rename(string remotePath, string newName)
        {
            ChangeWorkingDirectory(Path.GetDirectoryName(remotePath));
            string currentName = Path.GetFileName(remotePath);
            FtpWebRequest request = createRequest(combine(uri, currentName), WebRequestMethods.Ftp.Rename);
            request.RenameTo = newName;
            return getStatusDescription(request);
        }

        /// <summary>
        /// Производит загрузку массива файлов. В качестве реультата выдает массив локальных путей успешно загруженых файлов.
        /// </summary>
        /// <param name="arrLocalPath">Массив загружаемых файлов string { "/localDirectory/fileName.ext" }</param>
        /// <param name="remotePath">Genm yfpyfxtybz "/directory/"</param>
        /// <param name="overwrite">Параметр перезаписи true/false</param>
        /// <returns>"/localDirectory/fileName.ext"</returns>
        public async Task<string[]> Upload2Async(string[] arrLocalPath, string remotePath, bool overwrite)
        {
            formProgressStatus = new FormProgressStatus(0, arrLocalPath.GetLength(0));
            List<string> filesOk = new List<string>();
            Session session = new Session();

                // Connect
                session.Open(sessionOptions);

                int i = 0;
                foreach (string localPath in arrLocalPath)
                {
                    if (formProgressStatus.IsDisposed == true)
                    {
                        Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!Форма загрузки закрыта!!!!!!!!!!!!!!!!!!!");
                        return filesOk.ToArray();
                    }
                    formProgressStatus.Iterator(
                        i, 
                        localPath, 
                        remotePath, 
                        i.ToString() + "/" + arrLocalPath.GetLength(0).ToString(),
                        "Загрузка файлов на сервер");

                    // пытаемся получить значек файла
                    if (Path.GetExtension(localPath) != "") getIconFile(localPath);

                    // определяем параметры загрузки файлов
                    TransferOptions transferOptions = new TransferOptions();
                    transferOptions.TransferMode = TransferMode.Automatic;
                    if (overwrite == false) transferOptions.OverwriteMode = OverwriteMode.Resume;
                    else transferOptions.OverwriteMode = OverwriteMode.Overwrite;

                    // загружаем файлы на сервер
                    bool res = await Task.Run<bool>(() => session.PutFiles(localPath, remotePath, false, transferOptions).IsSuccess);
                    if(res == true) filesOk.Add(localPath);
                    i++;
                }
                session.Close();
            
            formProgressStatus.Close();
            formProgressStatus.Dispose();
            return filesOk.ToArray();
        }

        public async Task<bool> Upload2Async(string localPath, string remotePath, bool overwrite)
        {
            if (Path.GetExtension(localPath) != "") getIconFile(localPath);

            bool res = false;
            using (Session session = new Session())
            {
                try
                {
                    session.Open(sessionOptions);
                }
                catch (Exception e)
                {
                    classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Невозможно подключиться к серверу. " + e.Message);
                    return false;
                }
                if (session.FileExists(remotePath + Path.GetFileName(localPath)))
                {
                    ClassNotifyMessage classNotifyMessage = new VitNotifyMessage.ClassNotifyMessage();
                    DialogResult dialogResult = classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.QUESTION, remotePath + Path.GetFileName(localPath) + " уже существует. Заменить его?");
                    if (dialogResult != DialogResult.Yes)  return false;
                }

                TransferOptions transferOptions = new TransferOptions();

                transferOptions.TransferMode = TransferMode.Automatic;
                if(overwrite == false) transferOptions.OverwriteMode = OverwriteMode.Resume;
                else transferOptions.OverwriteMode = OverwriteMode.Overwrite;
                
                res = await Task.Run<bool>(() => session.PutFiles(localPath, remotePath, false, transferOptions).IsSuccess);
                session.Close();
            }
            return res;
        }

        private string combine(string path1, string path2)
        {
            return Path.Combine(path1, path2).Replace("\\", "/");
        }

        private FtpWebRequest createRequest(string method)
        {
            return createRequest(uri, method);
        }

        private FtpWebRequest createRequest(string uri, string method)
        {
            FtpWebRequest r = (FtpWebRequest)WebRequest.Create(uri);

            r.Credentials = new NetworkCredential(userName, password);
            r.Timeout = VitSettings.Properties.FTPSettings.Default.timeout;
            r.Method = method;
            r.UseBinary = Binary;
            r.EnableSsl = EnableSsl;
            r.UsePassive = Passive;

            return r;
        }

        private void EraseDirectory(string directoryPath)
        {
            foreach (string path in Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories))
            {
                getIconFile(path);
                File.Delete(path);
            }
        }

        private string getStatusDescription(FtpWebRequest request)
        {
            try
            {
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    return response.StatusDescription;
                }
            }
            catch (System.Net.WebException e)
            {
                Console.WriteLine(e.Message + " " + e.InnerException);
                return "";
            }
        }
    }
}