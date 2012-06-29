using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace NNI.PayerPortal.Domain.Concrete
{
    public static class MailSettings
    {
        // Get Settings
        private static bool useSsl = Boolean.Parse(ConfigurationManager.AppSettings["Email.UseSSl"]);
        private static string serverName = ConfigurationManager.AppSettings["Email.ServerName"];
        private static int serverPort = Int32.Parse(ConfigurationManager.AppSettings["Email.ServerPort"]);
        private static bool writeAsFile = Boolean.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"]);
        private static bool sendAsSmtp = Boolean.Parse(ConfigurationManager.AppSettings["Email.SendAsSmtp"]);
        private static string fileLocation = ConfigurationManager.AppSettings["Email.FileLocation"];
        private static string username = ConfigurationManager.AppSettings["Email.Username"];
        private static string password = ConfigurationManager.AppSettings["Email.Password"];

        // Set Settings
        public static bool UseSsl
        {
            get
            {
                return useSsl;
            }
        }
        public static string ServerName
        {
            get
            {
                return serverName;
            }
        }
        public static int ServerPort
        {
            get
            {
                return serverPort;
            }
        }
        public static bool WriteAsFile
        {
            get
            {
                return writeAsFile;
            }
        }
        public static bool SendAsSmtp
        {
            get
            {
                return sendAsSmtp;
            }
        }
        public static string FileLocation
        {
            get
            {
                return fileLocation;
            }
        }
        public static string Username
        {
            get
            {
                return username;
            }
        }
        public static string Password
        {
            get
            {
                return password;
            }
        }
    }
}
