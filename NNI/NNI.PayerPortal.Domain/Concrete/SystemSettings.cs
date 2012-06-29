using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace NNI.PayerPortal.Domain.Concrete
{
    public static class SystemSettings
    {
        // Password - GET
        private static string defaultPassword = ConfigurationManager.AppSettings["SystemSettings.DefaultPassword"];
        private static string passwordRegex = ConfigurationManager.AppSettings["SystemSettings.PasswordRegex"];
        // Email - GET
        private static string emailNotificationsFrom = ConfigurationManager.AppSettings["SystemSettings.EmailNotificationsFrom"];
        private static string emailNotificationsTo = ConfigurationManager.AppSettings["SystemSettings.EmailNotificationsTo"];

        // Password - RETURN
        public static string DefaultPassword
        {
            get
            {
                return defaultPassword;
            }
        }
        public static string PasswordRegex
        {
            get
            {
                return passwordRegex;
            }
        }
        // Email - RETURN
        public static string EmailNotificationsFrom
        {
            get
            {
                return emailNotificationsFrom;
            }
        }
        public static string EmailNotificationsTo
        {
            get
            {
                return emailNotificationsTo;
            }
        }
    }
}
