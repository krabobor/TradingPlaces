using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI.WebControls;
using TradingPlaces.Classes;
using TradingPlaces.Models;

namespace TradingPlaces.Helpers
{
    public static class SendingEmail
    {
      
        /// <summary>
        /// Return true if message send succsesfully, false if message send with errors
        /// </summary>
        /// <param name="emailModel"></param>
        /// <returns></returns>
        public static bool SendEmail(Email emailModel)
        {
            string smtpStr = ConfigurationData.GetEmailServer();
            int portInt = ConfigurationData.GetEmailPort();
            string userStr = ConfigurationData.GetEmailUser();
            string passStr = ConfigurationData.GetEmailPass();
            string emailStr = ConfigurationData.GetAdminEmail();
            bool sslBool = ConfigurationData.GetEmailSsl();

            MailDefinition mailDefinition = new MailDefinition();
            mailDefinition.BodyFileName = "~/Content/tmp/TemplateEmail.html";
            mailDefinition.From = userStr;
            ListDictionary replacements = new ListDictionary();
            replacements.Add("<%Title%>", emailModel.Title);
            replacements.Add("<%Name%>", emailModel.Name);
            replacements.Add("<%PhoneNumber%>", emailModel.PhoneNumber);
            replacements.Add("<%Message%>", emailModel.Message);
            replacements.Add("<%BestTime%>", emailModel.ContactTime);

            string mailTo = string.Format("{0} <{1}>", "Administrator", emailStr);
            MailMessage mailMessage = mailDefinition.CreateMailMessage(mailTo, replacements, new System.Web.UI.Control());

            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = "Feedback";

            SmtpClient smtp = new SmtpClient();
            smtp.Host = smtpStr;
            smtp.Port = portInt;

            smtp.Credentials = new NetworkCredential(userStr, passStr);
            smtp.EnableSsl = sslBool;
            try
            {
                smtp.Send(mailMessage);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}