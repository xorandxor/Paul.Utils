using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace Paul.Utils
{
    public static class Logging
    {
        #region Public Methods

        /// <summary>
        /// Log messages to the database
        /// </summary>
        /// <param name="message">The message to be written</param>
        public static void LogDB(string message)
        {
            try
            {
                SqlParameter[] p = new SqlParameter[1];// (SqlParameter"message", SqlDbType.NVarChar, 255);
                p[0] = new SqlParameter();
                p[0].ParameterName = "@message";
                p[0].DbType = DbType.String;
                p[0].Value = message;
                SqlHelper.ExecuteNonQuery(Config.DBConn, CommandType.StoredProcedure, "LOG_INSERT", p);
            }
            catch (Exception ex) // if sql server is down then write an error to the system log
            {
                Logging.WriteToEventLog("Application", "Paul.Utils", "Exception Occurred: " + ex.ToString(), EventLogEntryType.Error);
            }
        }

        /// <summary>
        /// Simple logging function to log entries to a text file for debugging
        /// </summary>
        /// <param name="file"> c:\users\bob\mylog.log </param>
        /// <param name="message"> string of text you want logged </param>
        /// <param name="timestamp"> if true time is in the entry </param>
        public static void Log(string file, string message, bool timestamp)
        {
            string fileName = file;
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    if (timestamp)
                    {
                        writer.WriteLine(DateTime.Now.ToString() + " -- " + message);
                    }
                    else
                    {
                        writer.WriteLine(message);
                    }
                    writer.Close();
                    writer.Dispose();
                }
            }
            catch (Exception exp)
            {
                Console.Write(exp.Message);
            }
        }

        /// <summary>
        /// simple overload with timestamp using default logfile
        /// </summary>
        /// <param name="message"> </param>
        public static void Log(string message)
        {
            Log(Config.Logfile, message, true);
        }

        /// <summary>
        /// Simple logging function to log entries to a text file for debugging Since filename is
        /// not specificed we call config.logfile and hope its listed in the app.config
        /// </summary>
        /// <param name="message"> string of text you want logged </param>
        /// <param name="timestamp">
        /// bool indicating if you want the datetime.meow prefixed to the message
        /// </param>
        public static void Log(string message, bool timestamp)
        {
            string fileName = Config.Logfile;

            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    if (timestamp)
                    {
                        writer.WriteLine(DateTime.Now.ToString() + " -- " + message);
                    }
                    else
                    {
                        writer.WriteLine(message);
                    }
                    writer.Close();
                    writer.Dispose();
                }
            }
            catch (Exception exp)
            {
                Console.Write(exp.Message);
            }
        }

        public static void WriteToEventLog(string sLog, string sSource, string message, EventLogEntryType level)
        {
            if (!EventLog.SourceExists(sSource)) EventLog.CreateEventSource(sSource, sLog);

            EventLog.WriteEntry(sSource, message, level);
        }

        #endregion Public Methods
    }
}