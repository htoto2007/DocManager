using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using VitUsers;
using WinSCP;

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
        private readonly string userName;
        private ClassUsers ClassUsers = new ClassUsers();
        private readonly SessionOptions sessionOptions;
        private string uri;

        public ClassFTP()
        {
            uri = "ftp://" + VitSettings.Properties.FTPSettings.Default.host + ":" + VitSettings.Properties.FTPSettings.Default.port + "/";
            userName = ClassUsers.getThisUser().login;
            password = ClassUsers.getThisUser().password;

            sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Ftp,
                HostName = VitSettings.Properties.FTPSettings.Default.host,
                UserName = ClassUsers.getThisUser().login,
                Password = ClassUsers.getThisUser().password,
            };
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
                await Task.Run(() => session.DuplicateFile(sourcePath, targetPath));
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

        public string PrintWorkingDirectory()
        {
            FtpWebRequest request = createRequest(WebRequestMethods.Ftp.PrintWorkingDirectory);

            return getStatusDescription(request);
        }

        public bool RemoveDirecroty2(string directoryName)
        {
            bool res = false;
            using (Session session = new Session())
            {
                // Connect
                session.Open(sessionOptions);

                res = session.RemoveFiles("/" + directoryName).IsSuccess;
                session.Close();
            }
            return res;
        }

        public string RemoveDirectory(string directoryName)
        {
            FtpWebRequest request = createRequest(combine(uri, directoryName), WebRequestMethods.Ftp.RemoveDirectory);

            return getStatusDescription(request);
        }

        public string Rename(string currentName, string newName)
        {
            FtpWebRequest request = createRequest(combine(uri, currentName), WebRequestMethods.Ftp.Rename);

            request.RenameTo = newName;

            return getStatusDescription(request);
        }

        public bool Upload2(string localPath, string remotePath)
        {
            SessionOptions sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Ftp,
                HostName = VitSettings.Properties.FTPSettings.Default.host,
                UserName = ClassUsers.getThisUser().login,
                Password = ClassUsers.getThisUser().password,
            };
            bool res = false;
            using (Session session = new Session())
            {
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

                TransferOptions transferOptions = new TransferOptions
                {
                    TransferMode = TransferMode.Automatic,
                    OverwriteMode = OverwriteMode.Resume
                };
                res = session.PutFiles(localPath, remotePath, false, transferOptions).IsSuccess;
                session.Close();
            }
            return res;
        }

        public bool Upload2(string[] localPaths, string remotePath)
        {
            bool res = false;
            using (Session session = new Session())
            {
                // Connect
                session.Open(sessionOptions);
                foreach (string localPath in localPaths)
                {
                    if (session.FileExists(remotePath + Path.GetFileName(localPath)))
                    {
                        VitNotifyMessage.ClassNotifyMessage classNotifyMessage = new VitNotifyMessage.ClassNotifyMessage();
                        if (classNotifyMessage.showDialog(VitNotifyMessage.ClassNotifyMessage.TypeMessage.QUESTION, remotePath + Path.GetFileName(localPath) + " уже существует. Заменить его?") != System.Windows.Forms.DialogResult.Yes)
                        {
                            return false;
                        }
                    }

                    TransferOptions transferOptions = new TransferOptions
                    {
                        TransferMode = TransferMode.Automatic,
                        OverwriteMode = OverwriteMode.Resume
                    };
                    res = session.PutFiles(localPath, remotePath, false, transferOptions).IsSuccess;
                }
                session.Close();
            }
            return res;
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