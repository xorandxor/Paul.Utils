using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System;

namespace Paul.Utils
{
    public class MailTool
    {
        /// <summary>
        /// Ssnd alert emails to warn the operator of various issues
        /// (financial loss, database server offline, API unavailable)
        /// </summary>
        /// <param name="from_email"></param>
        /// <param name="to_email"></param>
        /// <param name="eml_subject"></param>
        /// <param name="eml_body"></param>
        /// <param name="smtp_server"></param>
        /// <param name="smtp_user"></param>
        /// <param name="smtp_pass"></param>
        /// <exception cref="ApplicationException"></exception>
        public static void SendEmail(
            string from_email,
            string to_email,
            string eml_subject,
            string eml_body,
            string smtp_server,
            string smtp_user,
            string smtp_pass)
        {
            try
            {
                if (Config.EMAIL_FUNCTIONAL)
                {
                    SmtpClient mySmtpClient = new SmtpClient(smtp_server);

                    // set smtp-client with basicAuthentication
                    mySmtpClient.UseDefaultCredentials = false;
                    System.Net.NetworkCredential basicAuthenticationInfo = new
                       System.Net.NetworkCredential(smtp_user, smtp_pass);
                    mySmtpClient.Credentials = basicAuthenticationInfo;

                    // add from,to mailaddresses
                    MailAddress from = new MailAddress(from_email, from_email);
                    MailAddress to = new MailAddress(to_email, to_email);
                    MailMessage myMail = new System.Net.Mail.MailMessage(from, to);

                    // add ReplyTo
                    //MailAddress replyTo = new MailAddress("reply@example.com");
                    //myMail.ReplyToList.Add(replyTo);

                    // set subject and encoding
                    myMail.Subject = eml_subject;
                    myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                    // set body-message and encoding
                    myMail.Body = eml_body;
                    myMail.BodyEncoding = System.Text.Encoding.UTF8;
                    // text or html
                    myMail.IsBodyHtml = true;

                    mySmtpClient.Send(myMail);
                }
                else
                {
                    try
                    {
                        Logging.LogDB("Email:: Not functional - logging here instead:");
                        Logging.LogDB("Email: " + eml_subject + " \n" + eml_body);
                    }
                    catch 
                    {
                        Logging.Log("Nothing fucking works in this hellhole! " + eml_subject + "\n" + eml_body);
                    }
                }
            }
            catch (SmtpException ex)
            {
                throw new ApplicationException
                  ("SmtpException has occured: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Alert_Admin(string subject, string message)
        {
            try
            {
                if (Config.EMAIL_FUNCTIONAL)
                {
                    SmtpClient mySmtpClient = new SmtpClient(Config.EMAIL_HOST);

                    // set smtp-client with basicAuthentication
                    mySmtpClient.UseDefaultCredentials = false;
                    System.Net.NetworkCredential basicAuthenticationInfo = new
                       System.Net.NetworkCredential(Config.EMAIL_USERNAME, Config.EMAIL_PASSWORD);
                    mySmtpClient.Credentials = basicAuthenticationInfo;
                    mySmtpClient.EnableSsl = true;
                    mySmtpClient.Port = Config.EMAIL_PORT;

                    // add from,to mailaddresses
                    MailAddress from = new MailAddress(Config.ADMIN_EMAIL, Config.ADMIN_EMAIL);
                    MailAddress to = new MailAddress(Config.ADMIN_EMAIL, Config.ADMIN_EMAIL);
                    MailMessage myMail = new System.Net.Mail.MailMessage(from, to);

                    // add ReplyTo
                    //MailAddress replyTo = new MailAddress("reply@example.com");
                    //myMail.ReplyToList.Add(replyTo);

                    // set subject and encoding
                    myMail.Subject = subject;
                    myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                    // set body-message and encoding
                    myMail.Body = message;
                    myMail.BodyEncoding = System.Text.Encoding.UTF8;
                    // text or html
                    myMail.IsBodyHtml = true;

                    mySmtpClient.Send(myMail);
                }
            }
            catch (SmtpException ex)
            {
                throw new ApplicationException("SmtpException has occured: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      

    }
}
