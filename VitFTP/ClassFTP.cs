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

namespace VitFTP
{
    /// <summary>
    /// https://kbss.ru/blog/lang_c_sharp/107.html
    /// </summary>
    public class ClassFTP
    {
        public bool Binary = true;
        public bool EnableSsl = false;
        public bool Hash = false;
        public bool Passive = true;
        private readonly int bufferSize = 1024;
        private readonly string password;
        private readonly SessionOptions sessionOptions;
        private readonly string userName;
        ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();

        
        private string uri;

        public ClassFTP(string userName, string password)
        {
            uri = "ftp://" + VitSettings.Properties.FTPSettings.Default.host + ":" + VitSettings.Properties.FTPSettings.Default.port + "/";
            this.userName = userName;
            this.password = password;

            //MessageBox.Show(userName + " " + password);

            sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Ftp,
                HostName = VitSettings.Properties.FTPSettings.Default.host,
                UserName = userName,
                Password = password,
                GiveUpSecurityAndAcceptAnySshHostKey = false,
            };
            //new Thread(() => { formProgressStatus = new vitProgressStatus.FormProgressStatus(); }).Start();
        }

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

        public struct AccessToFolder
        {
            public bool read;
            public bool write;
            public bool execute;
        }

        public void changeUserProperties(string userName)
        {
            string xmlFileName = "FileZilla Server.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFileName);
            XmlElement xRoot = xmlDoc.DocumentElement;

            XmlElement xmlUsers = null;
            // получаем елемент с пользователями
            foreach (XmlElement xmlElem in xRoot.ChildNodes)
            {
                if (xmlElem.Name == "Users")
                    xmlUsers = xmlElem;
            }

            foreach (XmlElement xmlElem in xmlUsers.ChildNodes)
            {

            }
        }

        public struct UserCollection
        {
            public string name;
            public string pass;
            public bool enable;
            public string salt;
            public string group;
            public bool bypassServerUserlimit;
            public int userLimit;
            public int ipLimit;
            public string comments;
            public bool forceSsl;
            public DirCollection DirCollection;

        }

        public struct DirCollection
        {
            public string dir;
            public bool FileWrite;
            public bool FileDelete;
            public bool FileAppend;
            public bool DirCreate;
            public bool DirDelete;
            public bool DirList;
            public bool DirSubdirs;
            public bool IsHome;
            public bool AutoCreate;
        }

        private bool getFileConfig()
        {
            using (Session session = new Session())
            {
                // Connect
                session.Open(sessionOptions);
                TransferOperationResult transferOperationResult = session.GetFiles("FileZilla Server.xml", VitSettings.Properties.FTPSettings.Default.openFilePath + "\\FileZilla Server.xml", false, null);
                session.Close();
                if (!transferOperationResult.IsSuccess)
                {
                    VitNotifyMessage.ClassNotifyMessage classNotifyMessage = new VitNotifyMessage.ClassNotifyMessage();
                    classNotifyMessage.showDialog(VitNotifyMessage.ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Не удалось получить файл конфигурации с сервера!");
                    return false;
                }
                if (!File.Exists(VitSettings.Properties.FTPSettings.Default.openFilePath + "\\FileZilla Server.xml")) return false;
            }
            return true;
        }

        private bool sendFileConfig()
        {
            using (Session session = new Session())
            {
                // Connect
                session.Open(sessionOptions);
                TransferOperationResult transferOperationResult = session.PutFiles(VitSettings.Properties.FTPSettings.Default.openFilePath + "\\FileZilla Server.xml", "\\FileZilla Server.xml", true, null);
                session.Close();
                if (!transferOperationResult.IsSuccess)
                {
                    VitNotifyMessage.ClassNotifyMessage classNotifyMessage = new VitNotifyMessage.ClassNotifyMessage();
                    classNotifyMessage.showDialog(VitNotifyMessage.ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Не удалось отправить файл конфигурации на сервер!");
                    return false;
                }

            }
            return true;
        }

        public void deleteUserByLogin(string login)
        {
            if (getFileConfig() == false) return;
            string xmlFileName = VitSettings.Properties.FTPSettings.Default.openFilePath + "\\FileZilla Server.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFileName);
            XmlElement xRoot = xmlDoc.DocumentElement;

            XmlElement xmlUsers = null;
            // получаем елемент с пользователями
            foreach (XmlElement xmlElem in xRoot.ChildNodes)
            {
                if (xmlElem.Name == "Users")
                {
                    xmlUsers = xmlElem;
                    break;
                }
            }

            //XmlElement xmlUser = null;
            // получаем елемент "Пользователь по логину"
            foreach (XmlElement xmlUser in xmlUsers.ChildNodes)
            {
                Console.WriteLine(xmlUser.GetAttribute("Name") + " }{ " + login);
                if (xmlUser.GetAttribute("Name") == login)
                {
                    //xmlUser.RemoveAll();
                    xmlUsers.RemoveChild(xmlUser);
                    break;
                }
            }

            xmlDoc.Save(xmlFileName);
            if (sendFileConfig() == false) return;
        }

        public void AddUser(UserCollection userCollection)
        {
            if (getFileConfig() == false) return;

            string xmlFileName = VitSettings.Properties.FTPSettings.Default.openFilePath + "\\FileZilla Server.xml"; 
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFileName);
            XmlElement xRoot = xmlDoc.DocumentElement;

            XmlElement xmlUsers = null;
            // получаем елемент с пользователями
            foreach (XmlElement xmlElem in xRoot.ChildNodes)
            {
                if (xmlElem.Name == "Users")
                    xmlUsers = xmlElem;
                
            }

            XmlElement xmlUser = createNode(xmlDoc, "User", "", "Name", userCollection.name);

            XmlElement xmlOption = createNode(xmlDoc, "Option", "", "Name", "Pass");
            xmlUser.AppendChild(xmlOption);
            xmlOption = createNode(xmlDoc, "Option", Convert.ToInt32(userCollection.enable).ToString(), "Name", "Enabled");
            xmlUser.AppendChild(xmlOption);
            xmlOption = createNode(xmlDoc, "Option", userCollection.salt, "Name", "Salt");
            xmlUser.AppendChild(xmlOption);
            xmlOption = createNode(xmlDoc, "Option", userCollection.group, "Name", "Group");
            xmlUser.AppendChild(xmlOption);
            xmlOption = createNode(xmlDoc, "Option", Convert.ToInt32(userCollection.bypassServerUserlimit).ToString(), "Name", "Bypass server userlimit");
            xmlUser.AppendChild(xmlOption);
            xmlOption = createNode(xmlDoc, "Option", userCollection.userLimit.ToString(), "Name", "User Limit");
            xmlUser.AppendChild(xmlOption);
            xmlOption = createNode(xmlDoc, "Option", userCollection.ipLimit.ToString(), "Name", "IP Limit");
            xmlUser.AppendChild(xmlOption);
            xmlOption = createNode(xmlDoc, "Option", userCollection.comments, "Name", "Comments");
            xmlUser.AppendChild(xmlOption);
            xmlOption = createNode(xmlDoc, "Option", Convert.ToInt32(userCollection.forceSsl).ToString(), "Name", "ForceSsl");
            xmlUser.AppendChild(xmlOption);

            XmlElement xmlIpFilter = createNode(xmlDoc, "IpFilter", "", "", "");
            xmlUser.AppendChild(xmlIpFilter);
            XmlElement xmlIpFilterDisallowed = createNode(xmlDoc, "Disallowed", "", "", "");
            xmlIpFilter.AppendChild(xmlIpFilterDisallowed);
            XmlElement xmlIpFilterAllowed = createNode(xmlDoc, "Allowed", "", "", "");
            xmlIpFilter.AppendChild(xmlIpFilterAllowed);

            XmlElement xmlSpeedLimits = createNode(xmlDoc, "SpeedLimits", "", "DlType", "0");
            xmlUser.AppendChild(xmlSpeedLimits);

            XmlText xmlAttrText = xmlDoc.CreateTextNode("10");
            XmlAttribute xmlAttribute = xmlDoc.CreateAttribute("DlLimit");
            xmlAttribute.AppendChild(xmlAttrText);
            xmlSpeedLimits.Attributes.Append(xmlAttribute);

            xmlAttrText = xmlDoc.CreateTextNode("0");
            xmlAttribute = xmlDoc.CreateAttribute("ServerDlLimitBypass");
            xmlAttribute.AppendChild(xmlAttrText);
            xmlSpeedLimits.Attributes.Append(xmlAttribute);

            xmlAttrText = xmlDoc.CreateTextNode("0");
            xmlAttribute = xmlDoc.CreateAttribute("UlType");
            xmlAttribute.AppendChild(xmlAttrText);
            xmlSpeedLimits.Attributes.Append(xmlAttribute);

            xmlAttrText = xmlDoc.CreateTextNode("10");
            xmlAttribute = xmlDoc.CreateAttribute("UlLimit");
            xmlAttribute.AppendChild(xmlAttrText);
            xmlSpeedLimits.Attributes.Append(xmlAttribute);

            xmlAttrText = xmlDoc.CreateTextNode("0");
            xmlAttribute = xmlDoc.CreateAttribute("ServerUlLimitBypass");
            xmlAttribute.AppendChild(xmlAttrText);
            xmlSpeedLimits.Attributes.Append(xmlAttribute);

            XmlElement xmlUserDownload = createNode(xmlDoc, "Download", "", "", "");
            xmlSpeedLimits.AppendChild(xmlUserDownload);
            XmlElement xmlUserUpload = createNode(xmlDoc, "Upload", "", "", "");
            xmlSpeedLimits.AppendChild(xmlUserUpload);

            xmlUser.AppendChild(xmlSpeedLimits);

            XmlElement xmlUserPermissions = createNode(xmlDoc, "Permissions", "", "", "");

            XmlElement xmlUserPermission = createNode(xmlDoc, "Permission", "", "Dir", userCollection.DirCollection.dir);

            XmlElement xmlUserPermissionOption = createNode(xmlDoc, "Option", "1", "Name", "FileRead");
            xmlUserPermission.AppendChild(xmlUserPermissionOption);
            xmlUserPermissionOption = createNode(xmlDoc, "Option", Convert.ToInt32(userCollection.DirCollection.FileWrite).ToString(), "Name", "FileWrite");
            xmlUserPermission.AppendChild(xmlUserPermissionOption);
            xmlUserPermissionOption = createNode(xmlDoc, "Option", Convert.ToInt32(userCollection.DirCollection.FileDelete).ToString(), "Name", "FileDelete");
            xmlUserPermission.AppendChild(xmlUserPermissionOption);
            xmlUserPermissionOption = createNode(xmlDoc, "Option", Convert.ToInt32(userCollection.DirCollection.FileAppend).ToString(), "Name", "FileAppend");
            xmlUserPermission.AppendChild(xmlUserPermissionOption);
            xmlUserPermissionOption = createNode(xmlDoc, "Option", Convert.ToInt32(userCollection.DirCollection.DirCreate).ToString(), "Name", "DirCreate");
            xmlUserPermission.AppendChild(xmlUserPermissionOption);
            xmlUserPermissionOption = createNode(xmlDoc, "Option", Convert.ToInt32(userCollection.DirCollection.DirDelete).ToString(), "Name", "DirDelete");
            xmlUserPermission.AppendChild(xmlUserPermissionOption);
            xmlUserPermissionOption = createNode(xmlDoc, "Option", Convert.ToInt32(userCollection.DirCollection.DirList).ToString(), "Name", "DirList");
            xmlUserPermission.AppendChild(xmlUserPermissionOption);
            xmlUserPermissionOption = createNode(xmlDoc, "Option", Convert.ToInt32(userCollection.DirCollection.DirSubdirs).ToString(), "Name", "DirSubdirs");
            xmlUserPermission.AppendChild(xmlUserPermissionOption);
            xmlUserPermissionOption = createNode(xmlDoc, "Option", Convert.ToInt32(userCollection.DirCollection.IsHome).ToString(), "Name", "IsHome");
            xmlUserPermission.AppendChild(xmlUserPermissionOption);
            xmlUserPermissionOption = createNode(xmlDoc, "Option", Convert.ToInt32(userCollection.DirCollection.AutoCreate).ToString(), "Name", "AutoCreate");
            xmlUserPermission.AppendChild(xmlUserPermissionOption);

            xmlUserPermissions.AppendChild(xmlUserPermission);

            xmlUser.AppendChild(xmlUserPermissions);

            xmlUsers.AppendChild(xmlUser);

            xmlDoc.Save(xmlFileName);

            if(sendFileConfig() == false) return;
        }

        private XmlElement createNode(XmlDocument xmlDocument,
            string elemName,
            string elemValue,
            string attrName,
            string attrValue)
        {
            XmlElement xmlElement = xmlDocument.CreateElement(elemName);

            XmlText xmlAttrText = xmlDocument.CreateTextNode(attrValue);
            XmlText xmlText = xmlDocument.CreateTextNode(elemValue);
            if (attrName != "")
            {
                XmlAttribute xmlAttribute = xmlDocument.CreateAttribute(attrName);
                xmlAttribute.AppendChild(xmlAttrText);
                xmlElement.Attributes.Append(xmlAttribute);
            }
            xmlElement.AppendChild(xmlText);
            return xmlElement;
        }

        public string ChangeWorkingDirectory(string path)
        {
            uri = combine(uri, path);

            return PrintWorkingDirectory();
        }

        public string ChangeWorkingDirectoryByFullPath(string path)
        {
            string[] arrNames = path.Split('\\');
            string result = "";
            foreach (string name in arrNames)
            {
                //Console.WriteLine(name);
                result = ChangeWorkingDirectory(name);
            }
            return result;
        }

        public async Task copyAsync(string sourcePath, string targetPath)
        {
            using (Session session = new Session())
            {
                // Connect
                session.Open(sessionOptions);
                await Task.Run(() => EraseDirectory(VitSettings.Properties.FTPSettings.Default.pathTnp));
                if (Path.GetExtension(sourcePath) == "")
                {
                    await Task.Run(() => Directory.CreateDirectory(VitSettings.Properties.FTPSettings.Default.pathTnp + "\\" + Path.GetFileName(sourcePath)));
                    await Task.Run(() => session.GetFiles(sourcePath, VitSettings.Properties.FTPSettings.Default.pathTnp + "\\" + Path.GetFileName(sourcePath)));
                    await Task.Run(() => session.PutFiles(VitSettings.Properties.FTPSettings.Default.pathTnp + "\\" + Path.GetFileName(sourcePath), targetPath + "/"));
                }
                else
                {
                    await Task.Run(() => DownloadFile(sourcePath, VitSettings.Properties.FTPSettings.Default.pathTnp + "\\" + Path.GetFileName(sourcePath)));
                    await Task.Run(() => session.PutFiles(VitSettings.Properties.FTPSettings.Default.pathTnp + "\\" + Path.GetFileName(sourcePath), targetPath));
                }
                await Task.Run(() => EraseDirectory(VitSettings.Properties.FTPSettings.Default.pathTnp));
                session.Close();
            }
        }

        public void CreateDirectory(string path)
        {
            using (Session session = new Session())
            {
                // Connect
                session.Open(sessionOptions);
                session.CreateDirectory(path);
                session.Close();
            }
        }

        public string DeleteFile(string fileName)
        {
            FtpWebRequest request = createRequest(combine(uri, fileName), WebRequestMethods.Ftp.DeleteFile);

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

        public bool FileExist(string path)
        {
            bool res = false;
            path = path.Replace("\\", "/");
            Console.WriteLine(path);
            using (Session session = new Session())
            {
                // Connect
                session.Open(sessionOptions);
                res = session.FileExists(path);
                session.Close();
            }
            Console.WriteLine(res);
            return res;
        }

        public bool IsDirectory(string path)
        {
            bool res = false;
            using (Session session = new Session())
            {
                // Connect
                session.Open(sessionOptions);
                try
                {
                    RemoteFileInfo remoteFileInfo = session.GetFileInfo(path);
                    res = remoteFileInfo.IsDirectory;
                }
                catch (WinSCP.SessionRemoteException)
                {
                }
                session.Close();
            }
            return res;
        }

        public DateTime GetDateTimestamp(string fileName)
        {
            FtpWebRequest request = createRequest(combine(uri, fileName), WebRequestMethods.Ftp.GetDateTimestamp);

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                return response.LastModified;
            }
        }

        public long GetFileSize(string fileName)
        {
            FtpWebRequest request = createRequest(combine(uri, fileName), WebRequestMethods.Ftp.GetFileSize);

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                return response.ContentLength;
            }
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

        public string[] listDirectoryWithoutFiles()
        {
            string[] arrStr = ListDirectory();
            List<string> res = new List<string>();
            foreach(string str in arrStr)
            {
                using (Session session = new Session())
                {
                    // Connect
                    session.Open(sessionOptions);
                    if (session.GetFileInfo(str).IsDirectory)
                    {
                        res.Add(str);
                    }
                    session.Close();
                }
            }
            return res.ToArray();
        }

        public string getFileFullNAme(string path)
        {
            string fileFullName = "";
            using (Session session = new Session())
            {
                // Connect
                session.Open(sessionOptions);
                fileFullName = session.GetFileInfo(path).FullName;
                session.Close();
            }
            return fileFullName;
        }
        

        public string[] ListDirectory2(string path)
        {
            List<string> fileList = new List<string>();
            using (Session session = new Session())
            {
                // Connect
                session.Open(sessionOptions);
                RemoteFileInfoCollection files = session.ListDirectory(path).Files;
                foreach (RemoteFileInfo file in files)
                {
                    fileList.Add(file.FullName);
                }
                session.Close();
            }
            return fileList.ToArray();
        }

        public string[] ListDirectory()
        {
            List<string> list = new List<string>();

            FtpWebRequest request = createRequest(WebRequestMethods.Ftp.ListDirectory);

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, true))
                    {
                        while (!reader.EndOfStream)
                        {
                            list.Add(reader.ReadLine());
                        }
                    }
                }
            }

            return list.ToArray();
        }

        public string[] ListDirectoryDetails()
        {
            List<string> list = new List<string>();

            FtpWebRequest request = createRequest(WebRequestMethods.Ftp.ListDirectoryDetails);

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, true))
                    {
                        while (!reader.EndOfStream)
                        {
                            list.Add(reader.ReadLine());
                        }
                    }
                }
            }

            return list.ToArray();
        }

        public string MakeDirectory(string directoryName)
        {
            FtpWebRequest request = createRequest(combine(uri, directoryName), WebRequestMethods.Ftp.MakeDirectory);

            return getStatusDescription(request);
        }

        public async Task moveAsync(string sourcePath, string targetPath)
        {
            using (Session session = new Session())
            {
                // Connect
                session.Open(sessionOptions);
                await Task.Run(() => EraseDirectory(VitSettings.Properties.FTPSettings.Default.pathTnp));
                if (Path.GetExtension(sourcePath) == "")
                {
                    await Task.Run(() => Directory.CreateDirectory(VitSettings.Properties.FTPSettings.Default.pathTnp + "\\" + Path.GetFileName(sourcePath)));
                    await Task.Run(() => session.GetFiles(sourcePath, VitSettings.Properties.FTPSettings.Default.pathTnp + "\\" + Path.GetFileName(sourcePath)));
                    await Task.Run(() => session.PutFiles(VitSettings.Properties.FTPSettings.Default.pathTnp + "\\" + Path.GetFileName(sourcePath), targetPath + "/"));
                    await Task.Run(() => session.RemoveFiles(sourcePath));
                }
                else
                {
                    await Task.Run(() => DownloadFile(sourcePath, VitSettings.Properties.FTPSettings.Default.pathTnp + "\\" + Path.GetFileName(sourcePath)));
                    await Task.Run(() => session.PutFiles(VitSettings.Properties.FTPSettings.Default.pathTnp + "\\" + Path.GetFileName(sourcePath), targetPath + "/"));
                    await Task.Run(() => DeleteFile(sourcePath));
                }
                await Task.Run(() => EraseDirectory(VitSettings.Properties.FTPSettings.Default.pathTnp));
                session.Close();
            }
        }

        public string PrintWorkingDirectory()
        {
            FtpWebRequest request = createRequest(WebRequestMethods.Ftp.PrintWorkingDirectory);

            return getStatusDescription(request);
        }

        public bool RemoveDirecroty2(string directoryName)
        {
            RemovalOperationResult removalOperationResult;
            using (Session session = new Session())
            {
                // Connect
                session.Open(sessionOptions);
                removalOperationResult = session.RemoveFiles(directoryName.Replace("\\","/"));
                if(removalOperationResult.IsSuccess == false)
                {
                    SessionRemoteExceptionCollection sessionRemoteExceptions = removalOperationResult.Failures;
                    MessageBox.Show(sessionRemoteExceptions[0].Message);
                }
                session.Close();
            }
            return removalOperationResult.IsSuccess;
        }

        public string RemoveDirectory(string directoryName)
        {
            FtpWebRequest request = createRequest(combine(uri, directoryName), WebRequestMethods.Ftp.RemoveDirectory);

            return getStatusDescription(request);
        }

        public string Rename(string remotePath, string newName)
        {
            ChangeWorkingDirectory(Path.GetDirectoryName(remotePath));
            string currentName = Path.GetFileName(remotePath);
            FtpWebRequest request = createRequest(combine(uri, currentName), WebRequestMethods.Ftp.Rename);

            request.RenameTo = newName;

            return getStatusDescription(request);
        }

        

        public async Task<bool> Upload2Async(string localPath, string remotePath, bool overwrite)
        {
            if (Path.GetExtension(localPath) != "")
            {
                getIconFile(localPath);
            }

            bool res = false;
            using (Session session = new Session())
            {
                //session.FileTransferProgress += new FileTransferProgressEventHandler(session_FileTransferProgress);
                // Connect
                session.Open(sessionOptions);

                if (session.FileExists(remotePath + Path.GetFileName(localPath)))
                {
                    VitNotifyMessage.ClassNotifyMessage classNotifyMessage = new VitNotifyMessage.ClassNotifyMessage();
                    if (classNotifyMessage.showDialog(VitNotifyMessage.ClassNotifyMessage.TypeMessage.QUESTION, remotePath + Path.GetFileName(localPath) + " уже существует. Заменить его?") != System.Windows.Forms.DialogResult.Yes)
                    {
                        return false;
                    }
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

        public async Task<string[]> Upload2Async(string[] localPaths, string remotePath)
        {
            List<string> arrComplete = new List<string>();
            using (Session session = new Session())
            {
                //session.FileTransferProgress += new FileTransferProgressEventHandler(session_FileTransferProgress);
                // Connect
                session.Open(sessionOptions);

                int iterator = 0;
                vitProgressStatus.FormProgressStatus formProgressStatus = new vitProgressStatus.FormProgressStatus(0, localPaths.GetLength(0));
                    
                foreach (string localPath in localPaths)
                {
                    if (Path.GetExtension(localPath) == "")
                    {
                        continue;
                    }

                    

                    iterator++;
                    if (Path.GetExtension(localPath) != "")
                    {
                        getIconFile(localPath);
                    }

                    if (session.FileExists(remotePath + Path.GetFileName(localPath)))
                    {
                        VitNotifyMessage.ClassNotifyMessage classNotifyMessage = new VitNotifyMessage.ClassNotifyMessage();
                        if (classNotifyMessage.showDialog(VitNotifyMessage.ClassNotifyMessage.TypeMessage.QUESTION, remotePath + Path.GetFileName(localPath) + " уже существует. Заменить его?") != System.Windows.Forms.DialogResult.Yes)
                        {
                            return arrComplete.ToArray();
                        }
                    }

                    TransferOptions transferOptions = new TransferOptions
                    {
                        TransferMode = TransferMode.Automatic,
                        OverwriteMode = OverwriteMode.Resume
                    };
                    bool res = await Task.Run(() => session.PutFiles(localPath, remotePath, false, transferOptions).IsSuccess);
                    if (res == false)
                    {
                        continue;
                    }

                    arrComplete.Add(remotePath + Path.GetFileName(localPath));
                    formProgressStatus.Iterator(iterator, localPath);
                }
                session.Close();
                formProgressStatus.Hide();
            }

            return arrComplete.ToArray();
        }

        public string UploadFile(string source, string destination)
        {
            FtpWebRequest request = createRequest(combine(uri, destination), WebRequestMethods.Ftp.UploadFile);

            using (Stream stream = request.GetRequestStream())
            {
                using (FileStream fileStream = System.IO.File.Open(source, FileMode.Open))
                {
                    int num;

                    byte[] buffer = new byte[bufferSize];

                    while ((num = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        if (Hash)
                        {
                            Console.Write("#");
                        }

                        stream.Write(buffer, 0, num);
                    }
                }
            }

            return getStatusDescription(request);
        }

        public string UploadFileWithUniqueName(string source)
        {
            FtpWebRequest request = createRequest(WebRequestMethods.Ftp.UploadFileWithUniqueName);

            using (Stream stream = request.GetRequestStream())
            {
                using (FileStream fileStream = System.IO.File.Open(source, FileMode.Open))
                {
                    int num;

                    byte[] buffer = new byte[bufferSize];

                    while ((num = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        if (Hash)
                        {
                            Console.Write("#");
                        }

                        stream.Write(buffer, 0, num);
                    }
                }
            }

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                return Path.GetFileName(response.ResponseUri.ToString());
            }
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