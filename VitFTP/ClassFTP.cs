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

        /// <summary>
        /// Позволяет изменять параметры пользователя на FTP сервере
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="optionName"></param>
        /// <param name="value"></param>
        public void changeUserProperties(string userName, string optionName, string value)
        {
            string xmlFileName = "FileZilla Server.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFileName);
            XmlElement xRoot = xmlDoc.DocumentElement;

            // получили список польщ=зователей
            XmlElement xmlUsers = getXmlUsers(xRoot);
            // получили пользователя
            XmlElement xmlUser = getXmlUserByName(xmlUsers, userName);
            
            // проверяем 
            if(xmlUser == null)
            {
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Пользовател не найден!");
                return;
            }

            XmlNodeList xmlNodeList = xmlUser.GetElementsByTagName("Option");
            for (int i = 0; i < xmlNodeList.Count; i++)
            {
                if (xmlNodeList[i].Name == optionName)
                {
                    xmlNodeList[i].Value = value;
                    xmlUser.ReplaceChild(xmlNodeList[i], xmlUser.GetElementsByTagName("Option")[i]);
                    break;
                }
            }
            xmlUsers.ReplaceChild(xmlUser, getXmlUserByName(xmlUsers, userName));
            xRoot.ReplaceChild(xmlUsers, getXmlUsers(xRoot));
            return;
        }

        /// <summary>
        /// позволяет изменять доступ к удаленным каталогам на сервере
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="optionName"></param>
        /// <param name="value"></param>
        public void changeUserPermissions(string userName, string optionName, string value)
        {
            string xmlFileName = "FileZilla Server.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFileName);
            XmlElement xRoot = xmlDoc.DocumentElement;

            // получили список польщ=зователей
            XmlElement xmlUsers = getXmlUsers(xRoot);
            // получили пользователя
            XmlElement xmlUser = getXmlUserByName(xmlUsers, userName);

            // проверяем 
            if (xmlUser == null)
            {
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Пользовател не найден!");
                return;
            }

            XmlNodeList xmlNodeList = xmlUser.GetElementsByTagName("oermissions");
            foreach (XmlDocument xmlElem in xmlNodeList)
            {
                if (xmlElem.Name == optionName)
                {
                    xmlElem.Value = value;
                    break;
                }
            }
            return;
        }

        

        private XmlElement getXmlUsers(XmlElement xRoot)
        {
            XmlElement xmlUsers = null;
            // получаем елемент с пользователями
            foreach (XmlElement xmlElem in xRoot.ChildNodes)
            {
                if (xmlElem.Name == "Users")
                    xmlUsers = xmlElem;
            }
            return xmlUsers;
        }

        /// <summary>
        /// Получает узел пользователя подпадаюзего под имя
        /// </summary>
        /// <param name="xmlUsers">Узел списка пользователей</param>
        /// <returns></returns>
        private XmlElement getXmlUserByName(XmlElement xmlUsers, string userName)
        {
            XmlElement xmlUser = null;
            foreach (XmlElement xmlElem in xmlUsers.ChildNodes)
            {
                if (xmlElem.Name == userName) xmlUser = xmlElem;
            }
            return xmlUser;
        }

        public struct Permissions
        {
            public const string FILEREAD = "FileRead";
            public const string FILEWRITE = "FileWrite";
            public const string FILEDELETE = "FileDelete";
            public const string FILEAPPEND = "FileAppend";
            public const string DIRCREATE = "DirCreate";
            public const string DIRDELETE = "DirDelete";
            public const string DIRLIST = "DirList";
            public const string DIRSUBDIRS = "DirSubdirs";
            public const string ISHOME = "IsHome";
            public const string AUTOCREATE = "AutoCreate";
        }

        public struct UserOptionName
        {
            
            public const string NAME = "Name";

            public const string PASS = "Pass";
            /// <summary>
            /// Активирует или дизактивирует пользователя
            /// </summary>
            public const string ENABLE = "Enabled";
            public const string SALT = "Salt";
            /// <summary>
            /// Группа доступа присвоеная пользоватклю
            /// </summary>
            public const string GROUP = "Group";
            public const string BYPASSSERVERUSERLIMIT = "Bypass server userlimit";
            /// <summary>
            /// Лимит подключившихся пользователей с такими же ключами доступа
            /// </summary>
            public const string USERLIMIT = "User Limit";
            /// <summary>
            /// Лимит подключений с одного IP адреса
            /// </summary>
            public const string IPLIMIT = "IP Limit";
            /// <summary>
            /// Комментарий к акаунту пользователя на FTP сервере
            /// </summary>
            public const string COMMENTS = "Comments";
            public const string FORCESSL = "ForceSsl";
        }

        /// <summary>
        /// Представление объекта пользователя
        /// </summary>
        public struct UserCollection
        {
            /// <summary>
            /// Login пользователя для входа на FTP сервер
            /// </summary>
            public string name;
            /// <summary>
            /// Проль для входа на FTP сервер
            /// </summary>
            public string pass;
            /// <summary>
            /// Активирует или дизактивирует пользователя
            /// </summary>
            public bool enable;
            public string salt;
            /// <summary>
            /// Группа доступа присвоеная пользоватклю
            /// </summary>
            public string group;
            public bool bypassServerUserlimit;
            /// <summary>
            /// Лимит подключившихся пользователей с такими же ключами доступа
            /// </summary>
            public int userLimit;
            /// <summary>
            /// Лимит подключений с одного IP адреса
            /// </summary>
            public int ipLimit;
            /// <summary>
            /// Комментарий к акаунту пользователя на FTP сервере
            /// </summary>
            public string comments;
            public bool forceSsl;
            /// <summary>
            /// Содержит коллекцию файлов и папок с определенными правами доступа этого пользователя
            /// </summary>
            public DirCollection DirCollection;
        }

        /// <summary>
        /// Содержит коллекцию файлов и папок с определенными правами доступа этого пользователя
        /// </summary>
        public struct DirCollection
        {
            /// <summary>
            /// Путь до папки доступной пользователю
            /// </summary>
            public string dir;
            /// <summary>
            /// Запись файла
            /// </summary>
            public bool FileWrite;
            /// <summary>
            /// Удаление файла
            /// </summary>
            public bool FileDelete;
            /// <summary>
            /// Добавление в конец файла
            /// </summary>
            public bool FileAppend;
            /// <summary>
            /// Создание папки
            /// </summary>
            public bool DirCreate;
            /// <summary>
            /// Удаление папки
            /// </summary>
            public bool DirDelete;
            /// <summary>
            /// Получение списка файлов и папок
            /// </summary>
            public bool DirList;
            public bool DirSubdirs;
            /// <summary>
            /// Домашняя папка (может быть только одна)
            /// </summary>
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

        /// <summary>
        /// Определяет наличие файла по указанному пути сервере. 
        /// </summary>
        /// <param name="path">"directoey/fileName.ext" Можнт принемать пути файлов USNIX и WINSOWS</param>
        /// <returns></returns>
        public bool FileExist(string path)
        {
            bool res = false;
            path = path.Replace("\\", "/");
            using (Session session = new Session())
            {
                // Connect
                session.Open(sessionOptions);
                res = session.FileExists(path);
                session.Close();
            }
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
                    fileList.Add(file.FullName.Replace("\\", "/"));
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
            using (Session session = new Session())
            {
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
            }
            formProgressStatus.Close();
            formProgressStatus.Dispose();
            return filesOk.ToArray();
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
                    formProgressStatus.Iterator(
                        iterator, 
                        localPath, 
                        remotePath, 
                        iterator + "/" + localPaths.GetLength(0), 
                        "Загрузка файлов на сервер");
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