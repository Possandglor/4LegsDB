using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4LegsDB
{
    internal class HttpUtil
    {
        private HttpClient _httpClient;

        public HttpUtil()
        {
            _httpClient = new HttpClient();
        }
        public async Task<string> SendPostRequest(string url, Dictionary<string,string> param)//string title, string datn, string _event)
        {

            var content = new FormUrlEncodedContent(param);
            

            HttpResponseMessage response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
            else
            {
                Console.WriteLine("Ошибка при выполнении запроса: " + response.StatusCode);
                return null;
            }
        }
        
        public void sendMessage()
        {
            // Информация о почтовом сервере
            string smtpServer = Parameters.settings.smtpServer;//"smtp-mail.outlook.com";
            int smtpPort = Parameters.settings.smtpPort;//587;
            string smtpUsername = Parameters.settings.smtpUsername; //"Possandglor@live.ru";
            string smtpPassword = Parameters.settings.smtpPassword;// "TARGETBOL90a!";

            // Отправитель, получатель и тема письма
            string from = Parameters.settings.from;// "Possandglor@live.ru";
            string to = Parameters.settings.to;// "possandglor@gmail.com";
            string subject = Parameters.settings.subject;// "Вложение файла";

            // Путь к файлу, который нужно отправить
            string filePath = Parameters.settings.filePath;// "db.json";

            // Создание объекта письма
            MailMessage mail = new MailMessage(from, to, subject, "");

            // Создание вложения из файла
            Attachment attachment = new Attachment(filePath);

            // Добавление вложения к письму
            mail.Attachments.Add(attachment);

            // Настройка клиента SMTP
            SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
            smtpClient.Credentials = new System.Net.NetworkCredential(smtpUsername, smtpPassword);
            smtpClient.EnableSsl = true;

            try
            {
                // Отправка письма
                smtpClient.Send(mail);
                MessageBox.Show("Письмо успешно отправлено.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при отправке письма: " + ex.Message);
            }
            finally
            {
                // Освобождение ресурсов
                mail.Dispose();
                attachment.Dispose();
                smtpClient.Dispose();
            }
        }
    }
}
