using System.Net;
using System.Net.Mail;

namespace VitMail
{
    public class ClassMail
    {
        public void Send(string mailto, string caption, string message, string attachFile = null)
        {
            string smtpServer = "smtp.yandex.ru";
            int port = 587;
            string mailFrom = "mail@vitden.ru";
            string password = "1a9a9a2a";

            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress(mailFrom, "VIT");
            // кому отправляем
            MailAddress to = new MailAddress(mailto);
            // создаем объект сообщения
            MailMessage m = new MailMessage(mailFrom, mailto)
            {
                // тема письма
                Subject = caption,
                // текст письма
                Body = "<h2>" + message + "</h2>",
                // письмо представляет код html
                IsBodyHtml = true
            };
            m.Attachments.Add(new Attachment(attachFile));
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient(smtpServer, port)
            {
                // логин и пароль
                Credentials = new NetworkCredential(mailFrom, password),
                EnableSsl = true,
                Timeout = 9999999
            };

            smtp.SendMailAsync(m);
        }
    }
}