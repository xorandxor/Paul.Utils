using System;

namespace Paul.Utils
{
    public static class Config
    {
        public static string DBConn => AppSettings.ReadSetting("DBCONN");
        public static string Logfile => AppSettings.ReadSetting("LOGFILE");
        public static string ApiPrivateKey => AppSettings.ReadSetting("API_PRIVATE_KEY");
        public static string ApiPublicKey => AppSettings.ReadSetting("API_PUBLIC_KEY");

        public static int DataImportIntervalMIN => Convert.ToInt32(AppSettings.ReadSetting("DATA_IMPORT_INTERVAL_MIN"));



        public static bool EMAIL_ALERT_DB_DOWN => Convert.ToBoolean(AppSettings.ReadSetting("EMAIL_ALERT_DB_DOWN"));
        public static string ADMIN_EMAIL => Convert.ToString(AppSettings.ReadSetting("ADMIN_EMAIL"));

        public static bool EMAIL_FUNCTIONAL = false;

        public static string EMAIL_HOST => Convert.ToString(AppSettings.ReadSetting("EMAIL_HOST"));
        public static int EMAIL_PORT => Convert.ToInt16(AppSettings.ReadSetting("EMAIL_PORT"));
        public static string EMAIL_USERNAME => Convert.ToString(AppSettings.ReadSetting("EMAIL_USERNAME"));
        public static string EMAIL_PASSWORD => Convert.ToString(AppSettings.ReadSetting("EMAIL_PASSWORD"));
        public static bool EMAIL_USE_SSL => Convert.ToBoolean(AppSettings.ReadSetting("ADMIN_USE_SSL"));
    }
}