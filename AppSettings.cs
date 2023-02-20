using System;
using System.Configuration;

namespace Paul.Utils
{
    /// <summary>
    /// Class to read and write to the app.config this is used by config.cs which only exposes
    /// predefined app settings to read from since there is no real need to write settings once the
    /// app.config is prepared nor is there functionality in the code to do so.
    /// as usual, this is recycled code from another project
    /// (the best code is the code you wrote 6 months ago)
    /// </summary>
    public class AppSettings
    {
        #region Public Methods

        /// <summary>
        /// read a configuration section from the app.config
        /// </summary>
        /// <param name="key">key 'name'</param>
        /// <returns>string 'value'</returns>
        public static string ReadSetting(string key)
        {
            string result = "";
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                result = appSettings[key] ?? "Not Found";
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
            return result;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// not used
        /// </summary>
        /// <param name="key"> foo </param>
        /// <param name="value"> brrr </param>
        private static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        /// <summary>
        /// not used
        /// </summary>
        private static void ReadAllSettings()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                if (appSettings.Count == 0)
                {
                    Console.WriteLine("AppSettings is empty.");
                }
                else
                {
                    foreach (var key in appSettings.AllKeys)
                    {
                        Console.WriteLine("Key: {0} Value: {1}", key, appSettings[key]);
                    }
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

        #endregion Private Methods
    }
}