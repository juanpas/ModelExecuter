using BuildingBlock.Defs;
using BuildingBlock.Model;
using BuildingBlock.Repository.Contracts;
using SendGrid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BuildingBlock.Web.Utils
{
    public class Utils
    {
        private IMainUow Uow { get; set; }

        public Utils(IMainUow uow)
        {
            Uow = uow;
        }

        public Language CurrentRequestLocalizationLanguage
        {
            get
            {
                string languageName = "EN";
                HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[Settings.SelectedLanguageCookieName];
                if (cookie != null)
                    languageName = cookie.Value;

                return Uow.Languages.GetAll().First(a => a.Code == languageName);

            }
        }

        public bool DownloadRemoteImageFile(string uri, string fileName)
        {
            bool result = false;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // Check that the remote file was found. The ContentType
                // check is performed since a request for a non-existent
                // image file might be redirected to a 404-page, which would
                // yield the StatusCode "OK", even though the image was not
                // found.
                if ((response.StatusCode == HttpStatusCode.OK ||
                    response.StatusCode == HttpStatusCode.Moved ||
                    response.StatusCode == HttpStatusCode.Redirect) &&
                    response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
                {
                    // if the remote file was found, download it
                    using (Stream inputStream = response.GetResponseStream())
                    using (Stream outputStream = System.IO.File.OpenWrite(fileName))
                    {
                        byte[] buffer = new byte[4096];
                        int bytesRead;
                        do
                        {
                            bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                            outputStream.Write(buffer, 0, bytesRead);
                        } while (bytesRead != 0);
                    }

                    result = true;
                }
            }
            catch(Exception ex)
            {
                //Do nothing
            }

            return result;
        }


        public string GetNextIndexedFileName(string workingPath, string fileName, bool createFolder)
        {
            string extension = Path.GetExtension(fileName);
            string result = "0001" + extension;

            if (Directory.Exists(workingPath))
            {
                var directoryFiles = from file in Directory.GetFiles(workingPath)
                                     orderby file ascending
                                     select file;
                string lastFileName = directoryFiles.LastOrDefault();

                try
                {
                    int lastIndex = Convert.ToInt32(Path.GetFileNameWithoutExtension(lastFileName));

                    result = (lastIndex + 1).ToString().PadLeft(4, '0') + extension;
                }
                catch (Exception ex)
                {
                    //Do nothing
                }
            }
            else
            {
                if(createFolder)
                    Directory.CreateDirectory(workingPath);
            }

            return result;
        }

        public enum EmailMethod
        {
            SMTP = 1,
            SendGrid
        }

        public async Task<bool> SendEmail(EmailMethod method, string recipients, string subject, string body)
        {
            switch (method)
            {
                case EmailMethod.SendGrid:
                    return await SendEmail_SendGrid(recipients, subject, body);
                case EmailMethod.SMTP:
                default:
                    return await SendEmail_SMTP(recipients, subject, body);
            }
        }

        private async Task<bool> SendEmail_SMTP(string recipients, string subject, string body)
        {
            try
            {
                string fromAddress = Defs.Utils.ReadAppSetting<string>("EmailService_FromAddress", "pastranajc@gmail.com");
                string fromName = Defs.Utils.ReadAppSetting<string>("EmailService_FromName", "Solutek Enterprise Portal");
                string host = Defs.Utils.ReadAppSetting<string>("EmailService_Host", "smtp.gmail.com");
                int port = Defs.Utils.ReadAppSetting<int>("EmailService_Port", 587);
                string userName = Defs.Utils.ReadAppSetting<string>("EmailService_UserName", "pastranajc@gmail.com");
                string password = Defs.Utils.ReadAppSetting<string>("EmailService_Password", "Adivine123");


                var message = new MailMessage();
                message.IsBodyHtml = true;

                recipients.Split(';').ToList().ForEach(r => message.To.Add(new MailAddress(r.Trim())));

                message.From = new MailAddress(fromAddress);
                message.Subject = subject;
                message.Body = body;

                using (var smtp = new SmtpClient())
                {
                    smtp.EnableSsl = true;
                    var credential = new NetworkCredential
                    {
                        UserName = userName,
                        Password = password
                    };
                    smtp.Credentials = credential;
                    smtp.Host = host;
                    smtp.Port = port;

                    await smtp.SendMailAsync(message);
                }

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        private async Task<bool> SendEmail_SendGrid(string recipients, string subject, string body)
        {
            try
            {
                string fromAddress = Defs.Utils.ReadAppSetting<string>("EmailService_FromAddress", "pastranajc@gmail.com");
                string fromName = Defs.Utils.ReadAppSetting<string>("EmailService_FromName", "Solutek Enterprise Portal");
                string sendGridAPIKey = Defs.Utils.ReadAppSetting<string>("EmailService_SendGridAPIKey", "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");

                // Create the email object first, then add the properties.
                var message = new SendGridMessage();

                // Add the message properties.
                message.From = new MailAddress(fromAddress);

                // Add multiple addresses to the To field.
                List<String> recipientsList = recipients.Split(';').Select(s => s.Trim()).ToList();
                message.AddTo(recipientsList);

                message.Subject = subject;

                //Add the HTML and Text bodies
                message.Html = body;
                message.Text = body;

                // Create a Web transport, using API Key
                var transportWeb = new SendGrid.Web(sendGridAPIKey);

                // Send the email.
                await transportWeb.DeliverAsync(message);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}