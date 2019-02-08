using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VitNotifyMessage;
using WinSCP;

namespace VitFTP
{
    public class ClassFTPUsers
    {
        private readonly SessionOptions sessionOptions;

        public ClassFTPUsers(string userName, string password)
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
            if (xmlUser == null)
            {
                ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
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
                ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
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

        private bool getRepository()
        {
            using (Session session = new Session())
            {
                // Connect
                session.Open(sessionOptions);
                TransferOperationResult transferOperationResult = session.GetFiles("currentDirectory.txt", VitSettings.Properties.FTPSettings.Default.openFilePath + "\\currentDirectory.txt", false, null);
                session.Close();
                if (!transferOperationResult.IsSuccess)
                {
                    ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
                    classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Не удалось получить файл конфигурации с сервера!");
                    return false;
                }
                if (!File.Exists(VitSettings.Properties.FTPSettings.Default.openFilePath + "\\currentDirectory.txt")) return false;
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

        public bool AddUser(UserCollection userCollection)
        {
            ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
            if (getRepository() == false)
            {
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Не удалось получить файл путей сервера!");
                return false;
            }

            string repeositoryPath = File.ReadAllText(VitSettings.Properties.FTPSettings.Default.openFilePath + "\\currentDirectory.txt") + "\\Repository\\";

            if (getFileConfig() == false)
            {
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Не удалось получить файл настроек сервера!");
                return false;
            }

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
            userCollection.DirCollection.dir = repeositoryPath;
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

            if (sendFileConfig() == false) return false;
            return true;
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
    }
}
