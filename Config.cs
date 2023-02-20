namespace Paul.Utils
{
    public static class Config
    {
        public static string DBConn => AppSettings.ReadSetting("DBCONN");
        public static string Logfile => AppSettings.ReadSetting("LOGFILE");
        public static string ApiPrivateKey => AppSettings.ReadSetting("API_PRIVATE_KEY");
        public static string ApiPublicKey => AppSettings.ReadSetting("API_PUBLIC_KEY");
    }
}