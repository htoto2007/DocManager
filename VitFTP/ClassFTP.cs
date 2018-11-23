using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
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
        vitProgressStatus.FormProgressStatus formProgressStatus;

        public ClassFTP()
        {
            uri = "ftp://" + VitSettings.Properties.FTPSettings.Default.host + ":" + VitSettings.Properties.FTPSettings.Default.port + "/";
            userName = ClassUsers.getThisUser().login;
            password = ClassUsers.getThisUser().password;

            sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Sftp,
                HostName = VitSettings.Properties.FTPSettings.Default.host,
                UserName = ClassUsers.getThisUser().login,
                Password = ClassUsers.getThisUser().password,
                GiveUpSecurityAndAcceptAnySshHostKey = false,
            };
            new Thread(() => { formProgressStatus = new vitProgressStatus.FormProgressStatus(); }).Start();
        }

        private void session_FileTransferProgress(object sender, WinSCP.FileTransferProgressEventArgs e)
        {
            if (e.FileProgress > 0)
            {
                ProgressFormAsync(Convert.ToInt32(e.OverallProgress), Convert.ToInt32(e.FileProgress), e.FileName);
            }
            else
            {
                formProgressStatus.Hide();
            }
        }

        private void ProgressFormAsync(int max, int value, string fileName)
        {   
            formProgressStatus.label1.Invoke((Action)(() =>
            {
                formProgressStatus.label1.Text = fileName;
            }));
            formProgressStatus.progressBar1.Invoke((Action)(() => {
                
                formProgressStatus.progressBar1.Minimum = 0;
                formProgressStatus.progressBar1.Maximum = max;
                formProgressStatus.progressBar1.Step = 1;
                formProgressStatus.progressBar1.Value = value;
            }));

        }

        private void ProgressFormCloseAsync()
        {
            
                formProgressStatus.Invoke((Action)(() => {
                    formProgressStatus.Hide();
                }));
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
                    await Task.Run(() => session.PutFiles(VitSettings.Properties.FTPSettings.Default.pathTnp + "\\" + Path.GetFileName(sourcePath), targetPath + "/"));
                }
                await Task.Run(() => EraseDirectory(VitSettings.Properties.FTPSettings.Default.pathTnp));
                session.Close();
            }
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

        private void EraseDirectory(string directoryPath)
        {
            foreach (string path in Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories))
            {
                getIconFile(path);
                File.Delete(path);
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

        public async Task<bool> Upload2Async(string localPath, string remotePath)
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

                TransferOptions transferOptions = new TransferOptions
                {
                    TransferMode = TransferMode.Automatic,
                    OverwriteMode = OverwriteMode.Resume
                };
                res = await Task.Run<bool>(() => session.PutFiles(localPath, remotePath, false, transferOptions).IsSuccess);
                session.Close();
            }
            return res;
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

        public bool FileExist(string path)
        {
            bool res = false;
            using (Session session = new Session())
            {
                // Connect
                session.Open(sessionOptions);
                res = session.FileExists(path);
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
                if(iterator == 0)
                {
                    formProgressStatus.Show();
                    //session.FileTransferProgress += new FileTransferProgressEventHandler(session_FileTransferProgress);
                    ProgressFormAsync(localPaths.GetLength(0), iterator, "");
                }
                foreach (string localPath in localPaths)
                {
                    if (Path.GetExtension(localPath) == "") continue;
                    if (!formProgressStatus.IsDisposed)
                        formProgressStatus.progressBar1.PerformStep();
                    else
                        break;

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
                    if (res == false) continue;
                    arrComplete.Add(remotePath + Path.GetFileName(localPath));
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