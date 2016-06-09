using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TradingPlaces.Classes
{
    /// <summary>
    /// Return email server data
    /// </summary>
    public static class ConfigurationData
    {
        public static string GetEmailServer()
        {
            return ConfigurationManager.AppSettings["smtpServer"];
        }

        public static int GetEmailPort()
        {
            string portStr = ConfigurationManager.AppSettings["smtpPort"];
            int portInt = 25;
            try
            {
                portInt = Int32.Parse(portStr);
            }
            catch {}
            return portInt;
        }

        public static string GetEmailUser()
        {
            return ConfigurationManager.AppSettings["smtpUser"];
        }

        public static string GetEmailPass()
        {
            return ConfigurationManager.AppSettings["smtpPass"];
        }

        public static string GetAdminEmail()
        {
            return ConfigurationManager.AppSettings["adminEmail"];
        }

        public static bool GetEmailSsl()
        {
            string sslStr = ConfigurationManager.AppSettings["EnableSsl"];
            bool sslBool = false;
            try
            {
                sslBool = Boolean.Parse(sslStr);
            }
            catch { }

            return sslBool;
        }
         
        
    }
}