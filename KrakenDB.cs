﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Paul.Utils
{
    public static class KrakenDB
    {
        /// <summary>
        /// gets the value is USD of an assetpair from the database
        /// </summary>
        /// <param name="assetpair">XXBTCZUSD for example</param>
        /// <returns>Price of assetpair in dollars</returns>
        public static double GetAssetValueUSD(string assetpair)
        {
            double price = 0;
            try
            {
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@assetpair", SqlDbType.NVarChar, 50) { Value = assetpair };
                price = Convert.ToDouble(SqlHelper.ExecuteScalar(Config.DBConn, CommandType.StoredProcedure, "TICKER_GET_CurrentPrice", p));
            }
            catch (Exception ex)
            {
                if (Config.OPTION_LOG_DB_ERRORS_TO_CONSOLE)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return price;
        }

        /// <summary>
        /// delete all records from accountbalance table
        /// </summary>
        public static void AccountBalanceClear()
        {
            try
            {
                SqlHelper.ExecuteNonQuery(Config.DBConn, System.Data.CommandType.StoredProcedure, "ACCOUNTBALANCE_DELETE_ALL");
            }
            catch (Exception ex)
            {
                if (Config.OPTION_LOG_DB_ERRORS_TO_CONSOLE)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        /// <summary>
        /// add a record to the accountbalance table
        /// </summary>
        /// <param name="bal"></param>
        public static void AccountBalanceInsert(BalanceObject bal)
        {
            #region reference

            //CREATE PROCEDURE[dbo].[AccountBalance_INSERT]
            // (
            // @usd money,
            // @btc numeric(18, 5),
            // @ltc numeric(18,5),
            // @eth numeric(18,5),
            // @xdg numeric(18,5),
            // @xmr numeric(18,5),
            // @dash numeric(18,5),
            // @zec numeric(18,5),
            // @rep numeric(18,5)

            #endregion reference

            SqlParameter[] p = new SqlParameter[9];
            p[0] = new SqlParameter("@usd", SqlDbType.Decimal) { Value = Convert.ToDecimal(bal.USD) };
            p[1] = new SqlParameter("@btc", SqlDbType.Decimal) { Value = Convert.ToDecimal(bal.BTC) };
            p[2] = new SqlParameter("@ltc", SqlDbType.Decimal) { Value = Convert.ToDecimal(bal.LTC) };
            p[3] = new SqlParameter("@eth", SqlDbType.Decimal) { Value = Convert.ToDecimal(bal.ETH) };
            p[4] = new SqlParameter("@xdg", SqlDbType.Decimal) { Value = Convert.ToDecimal(bal.DGE) };
            p[5] = new SqlParameter("@xmr", SqlDbType.Decimal) { Value = Convert.ToDecimal(bal.XMR) };
            p[6] = new SqlParameter("@dash", SqlDbType.Decimal) { Value = Convert.ToDecimal(bal.DASH) };
            p[7] = new SqlParameter("@zec", SqlDbType.Decimal) { Value = Convert.ToDecimal(bal.ZEC) };
            p[8] = new SqlParameter("@rep", SqlDbType.Decimal) { Value = Convert.ToDecimal(bal.REP) };
            SqlHelper.ExecuteNonQuery(Config.DBConn, CommandType.StoredProcedure, "AccountBalance_Insert", p);
        }

        public static double Get_BTC_Balance()
        {
            double _out = 0;
            try
            {
                _out = Convert.ToDouble(SqlHelper.ExecuteScalar(Config.DBConn, CommandType.StoredProcedure, "GET_BAL_BTC"));
            }
            catch (SqlException s_ex)
            {
                if (Config.OPTION_LOG_DB_ERRORS_TO_CONSOLE)
                {
                    Console.WriteLine(s_ex.ToString());
                }
            }
            catch (Exception ex)
            {
                if (Config.OPTION_LOG_DB_ERRORS_TO_CONSOLE)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return _out;
        }
        public static double Get_ETH_Balance()
        {
            double _out = 0;
            try
            {
                _out = Convert.ToDouble(SqlHelper.ExecuteScalar(
                    Config.DBConn, CommandType.StoredProcedure, "GET_BAL_ETH"));
            }
            catch (SqlException s_ex)
            {
                if (Config.OPTION_LOG_DB_ERRORS_TO_CONSOLE)
                {
                    Console.WriteLine(s_ex.ToString());
                }
            }
            catch (Exception ex)
            {
                if (Config.OPTION_LOG_DB_ERRORS_TO_CONSOLE)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return _out;
        }
        public static double Get_LTC_Balance()
        {
            double _out = 0;
            try
            {
                _out = Convert.ToDouble(SqlHelper.ExecuteScalar(
                    Config.DBConn, CommandType.StoredProcedure, "GET_BAL_LTC"));
            }
            catch (SqlException s_ex)
            {
                if (Config.OPTION_LOG_DB_ERRORS_TO_CONSOLE)
                {
                    Console.WriteLine(s_ex.ToString());
                }
            }
            catch (Exception ex)
            {
                if (Config.OPTION_LOG_DB_ERRORS_TO_CONSOLE)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return _out;
        }
        public static double Get_DGE_Balance()
        {
            double _out = 0;
            try
            {
                _out = Convert.ToDouble(SqlHelper.ExecuteScalar(
                    Config.DBConn, CommandType.StoredProcedure, "GET_BAL_DGE"));
            }
            catch (SqlException s_ex)
            {
                if (Config.OPTION_LOG_DB_ERRORS_TO_CONSOLE)
                {
                    Console.WriteLine(s_ex.ToString());
                }
            }
            catch (Exception ex)
            {
                if (Config.OPTION_LOG_DB_ERRORS_TO_CONSOLE)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return _out;
        }
        public static double Get_XMR_Balance()
        {
            double _out = 0;
            try
            {
                _out = Convert.ToDouble(SqlHelper.ExecuteScalar(
                    Config.DBConn, CommandType.StoredProcedure, "GET_BAL_XMR"));
            }
            catch (SqlException s_ex)
            {
                if (Config.OPTION_LOG_DB_ERRORS_TO_CONSOLE)
                {
                    Console.WriteLine(s_ex.ToString());
                }
            }
            catch (Exception ex)
            {
                if (Config.OPTION_LOG_DB_ERRORS_TO_CONSOLE)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return _out;
        }
        public static double Get_DASH_Balance()
        {
            double _out = 0;
            try
            {
                _out = Convert.ToDouble(SqlHelper.ExecuteScalar(
                    Config.DBConn, CommandType.StoredProcedure, "GET_BAL_DASH"));
            }
            catch (SqlException s_ex)
            {
                if (Config.OPTION_LOG_DB_ERRORS_TO_CONSOLE)
                {
                    Console.WriteLine(s_ex.ToString());
                }
            }
            catch (Exception ex)
            {
                if (Config.OPTION_LOG_DB_ERRORS_TO_CONSOLE)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return _out;
        }
        public static double Get_ZEC_Balance()
        {
            double _out = 0;
            try
            {
                _out = Convert.ToDouble(SqlHelper.ExecuteScalar(
                    Config.DBConn, CommandType.StoredProcedure, "GET_BAL_ZEC"));
            }
            catch (SqlException s_ex)
            {
                if (Config.OPTION_LOG_DB_ERRORS_TO_CONSOLE)
                {
                    Console.WriteLine(s_ex.ToString());
                }
            }
            catch (Exception ex)
            {
                if (Config.OPTION_LOG_DB_ERRORS_TO_CONSOLE)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return _out;
        }
        public static double Get_REP_Balance()
        {
            double _out = 0;
            try
            {
                _out = Convert.ToDouble(SqlHelper.ExecuteScalar(
                    Config.DBConn, CommandType.StoredProcedure, "GET_BAL_REP"));
            }
            catch (SqlException s_ex)
            {
                if (Config.OPTION_LOG_DB_ERRORS_TO_CONSOLE)
                {
                    Console.WriteLine(s_ex.ToString());
                }
            }
            catch (Exception ex)
            {
                if (Config.OPTION_LOG_DB_ERRORS_TO_CONSOLE)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return _out;
        }



        /// <summary>
        /// Log messages to the database
        /// </summary>
        /// <param name="message">The message to be written</param>
        public static void LogDB(string message)
        {
            try
            {
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter
                {
                    SqlDbType = SqlDbType.NVarChar,
                    ParameterName = "@message",
                    DbType = DbType.String,
                    Value = message
                };
                SqlHelper.ExecuteNonQuery(Config.DBConn, CommandType.StoredProcedure, "LOG_INSERT", p);
            }
            catch (Exception ex) // if sql server is down then write an error to the system log
            {
                if (Config.OPTION_LOG_DB_ERRORS_TO_CONSOLE) { Console.WriteLine(ex.ToString()); }

                Logging.WriteToEventLog("Application", "Paul.Utils", "Exception Occurred: " + ex.ToString(), EventLogEntryType.Error);
            }
        }
    }
}