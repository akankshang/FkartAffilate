using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace OldAPI.Models
{
    public class NotificationHelper
    {
        public static void SendRegistrationEmail(string Email, string UserName)
        {
            string path = "<h3>Welcome! #Username#, <br/>Your registeration is completed, Please use this Email :#Email#  to login. <br /> Thank you <h3/>";// HttpContext.Current.Server.MapPath(@"~\Content\Emails\registration.html");


            if (File.Exists(path))
            {
                string message = File.ReadAllText(path);

                message = message.Replace("#Email#", Email);
                message = message.Replace("#Username#", UserName);

                EmailHelper.SendMail(Email, "User Registration", message, true);

            }
        }

        internal static void SendForgetPasswordEmail(string email, string pass)
        {
            string path = "<h3>Email :#email# <br /> code: <pre>#password#</pre>  </h3>";// HttpContext.Current.Server.MapPath(@"~\Content\Emails\forgotpassword.html");


            if (File.Exists(path))
            {
                string message = File.ReadAllText(path);

                message = message.Replace("#email#", email);
                message = message.Replace("#password#", pass);

                EmailHelper.SendMail(email, "Reset Password", message, true);

            }
        }
               
        
    }

    public class EmailConfiguration
    {
        public static string EditorEmail = "akshaymishra.babu@gmail.com";
        public static string EmailBlindCopy = "";
        public static string EmailCopy = "";
        public static string Get_LogoUrl = "";
        public static bool IsApplicationUnderDevelopment = false;
        public static object SmtpEnableSsl = true;

        public static bool UseDefaultCredentials = false;
    }
    public class EmailHelper
    {


        #region Email Helper

        public static bool SendMail(string toAddress, string subject, string mailContent, bool IsBodyHtml)
        {
            mailContent = SetLogo(mailContent);
            bool result = false;

            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();


            try
            {
                smtpClient = GetSMTPClientObject();
                MailAddress senderAddress = new MailAddress(GetSenderAddress());
                message.From = new MailAddress("\"" + "Affililate URL Builder" + "\" <" + senderAddress + ">");
                message = GetEmails(toAddress, message);

                message.Subject = "[Affililate URL Builder] " + subject;
                message.IsBodyHtml = IsBodyHtml;
                message.Body = mailContent;



                if (!String.IsNullOrEmpty(toAddress))
                {
                    smtpClient.Send(message);
                }
                else
                {
                    // GNF.LogFailedEmail(toAddress);
                    return false;

                }

                result = true;
                // GNF.LogSentEmail(toAddress);

            }
            catch (Exception ex)
            {  
                    result = false;
                
            }

            return result;
        }

        private static string SetLogo(string mailContent)
        {
            try
            {
                string msg = mailContent.Replace("#LogoUrl#", EmailConfiguration.Get_LogoUrl.ToString());
                return msg;
            }
            catch (Exception eX)
            {
                return mailContent;
            }
        }

        public static bool SendMailWithMultipleAttachment(string toAddress, string subject, string mailContent, bool IsBodyHtml, List<System.Net.Mail.Attachment> lstAttachment)
        {
            mailContent = SetLogo(mailContent);
            bool result = false;

            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();

            try
            {
                smtpClient = GetSMTPClientObject();
                MailAddress senderAddress = new MailAddress(GetSenderAddress());
                message.From = new MailAddress("\"" + "[Affililate URL Builder]" + "\" <" + senderAddress + ">");
                message = GetEmails(toAddress, message);

                message.Subject = "[Affililate URL Builder] " + subject;
                message.IsBodyHtml = IsBodyHtml;
                message.Body = mailContent;

                foreach (Attachment a in lstAttachment)
                {
                    message.Attachments.Add(a);

                }


                if (!String.IsNullOrEmpty(toAddress))
                {
                    smtpClient.Send(message);
                }
                else
                {
                    // GNF.LogFailedEmail(toAddress);
                    return false;

                }

                result = true;
                // GNF.LogSentEmail(toAddress);

            }
            catch (Exception ex)
            {  
                    result = false;
               
            }

            return result;



        }

        #endregion


        #region Configuration
        private static string GetSenderAddress()
        {

            return "nowgray@gmail.com";//

        }

        private static SmtpClient GetSMTPClientObject()
        {

            SmtpClient smtpClient = new SmtpClient();

            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = EmailConfiguration.UseDefaultCredentials;
            smtpClient.Timeout = 60000;
            smtpClient.Credentials = new NetworkCredential("nowgray@gmail.com", "sxmhunopzzloywyp"); 
            smtpClient.EnableSsl = Convert.ToBoolean(EmailConfiguration.SmtpEnableSsl);



            return smtpClient;
        }

       

        internal static System.Net.Mail.MailMessage GetEmails(string toAddress, System.Net.Mail.MailMessage message)
        {
            message.To.Add(toAddress);
            string CCEmail = EmailConfiguration.EmailCopy;
            if (CCEmail.Trim() != "")
            {
                message.CC.Add(CCEmail);
            }
            string BCCEmail = EmailConfiguration.EmailBlindCopy;
            if (BCCEmail.Trim() != "")
            {
                message.Bcc.Add(BCCEmail);


            }

            return message;
        }

       


        #endregion
    }
}