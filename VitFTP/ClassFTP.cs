using BytesRoad.Net.Ftp;
using System;

namespace VitFTP
{
    /// <summary>
    /// https://kbss.ru/blog/lang_c_sharp/107.html
    /// </summary>
    public class ClassFTP
    {
        public string FTP_PASSWORD = "";

        public int FTP_PORT = 21;

        public string FTP_SERVER = "";

        public string FTP_USER = "";

        public int TimeoutFTP = 3000;

        //Сам клиент ФТП
        private FtpClient client = new FtpClient();

        public int connectToServer()
        {
            //Задаём параметры клиента.
            client.PassiveMode = true; //Включаем пассивный режим.
                                       //Подключаемся к FTP серверу.
            FtpResponse ftpResponse = client.Connect(TimeoutFTP, FTP_SERVER, FTP_PORT);
            client.Login(TimeoutFTP, FTP_USER, FTP_PASSWORD);
            client.UsedEncoding = System.Text.Encoding.UTF8;
            return ftpResponse.Code;
        }

        public void disconnect()
        {
            client.Disconnect(TimeoutFTP);
        }

        public void getDirectoryesAndFiles()
        {
            client.ChangeDirectory(TimeoutFTP, "новая папка");
            client.DataTransfered += new FtpClient.DataTransferedEventHandler(dataTrans);
            //Получает список содержимого текущего каталога с FTP.
            FtpItem[] ftpItems = client.GetDirectoryList(TimeoutFTP);
            foreach (FtpItem ftpItem in ftpItems)
            {
                Console.WriteLine(ftpItem.Name);
            }

            client.GetFile(TimeoutFTP, "d:\\2018.10.05.0.7z", "2018.10.05.0.7z");
            //client.GetFile(TimeoutFTP, "2018.10.05.0.7z", "d:\\2018.10.05.0.7z");
        }

        private void dataTrans(object sender, DataTransferedEventArgs e)
        {
            float persent = (100 / (float)e.LastTransfered) * ((float)e.WholeTransfered / 100);
            Console.WriteLine((int)persent);
        }
    }
}