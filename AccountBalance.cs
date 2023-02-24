using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Paul.Utils
{
   
    /// <summary>
    /// This class encapsulates the various data assocuated with the accounts you have on kraken
    /// </summary>
    public class BalanceObject
    {
        public BalanceObject()
        { }

        public double USD { get; set; }
        public double BTC { get; set; }
        public double LTC { get; set; }
        public double ETH { get; set; }
        public double DGE { get; set; }
        public double XMR { get; set; }
        public double DASH { get; set; }
        public double ZEC { get; set; }
        public double REP { get; set; }
    }

    public class AccountBalance
    {

        /// <summary>
        /// Deserialize json data into a class object that can be access and passed around
        /// </summary>
        /// <param name="myJsonResponse">Ugly json data</param>
        /// <returns>pretty c# class</returns>
        public static Root GetClassFromJson(string myJsonResponse)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(myJsonResponse);
        }

        /// <summary>
        /// This method displays the current number of coins in all the wallets as well as the value of each 
        /// coin and also the total value of the portfolio... additionally, this method saves the account 
        /// balance to the database
        /// </summary>
        public static void GetAccountBalance()
        {
            string privateEndpoint = "Balance";
            string privateInputParameters = "";
            string apiPrivateKey = Config.ApiPrivateKey;
            string apiPublicKey = Config.ApiPublicKey;

            string privateResponse = API.QueryPrivateEndpoint(privateEndpoint, privateInputParameters, apiPublicKey, apiPrivateKey);
            Root acctbal = GetClassFromJson(privateResponse);

            BalanceObject bal = new BalanceObject();
            if (acctbal.result.ZUSD != null)
            {
                bal.USD = Convert.ToDouble(acctbal.result.ZUSD.ToString());
            }
            if (acctbal.result.XXBT != null)
            {
                bal.BTC = Convert.ToDouble(acctbal.result.XXBT.ToString());
            }
            if (acctbal.result.XLTC != null)
            {
                bal.LTC = Convert.ToDouble(acctbal.result.XLTC.ToString());
            }
            if (acctbal.result.XETH != null)
            {
                bal.ETH = Convert.ToDouble(acctbal.result.XETH.ToString());
            }
            if (acctbal.result.XXDG != null)
            {
                bal.DGE = Convert.ToDouble(acctbal.result.XXDG.ToString());
            }
            if (acctbal.result.XXMR != null)
            {
                bal.XMR = Convert.ToDouble(acctbal.result.XXMR.ToString());
            }
            if (acctbal.result.DASH != null)
            {
                bal.DASH = Convert.ToDouble(acctbal.result.DASH.ToString());
            }
            if (acctbal.result.XZEC != null)
            {
                bal.ZEC = Convert.ToDouble(acctbal.result.XZEC.ToString());
            }
            if (acctbal.result.XREP != null)
            {
                bal.REP = Convert.ToDouble(acctbal.result.XREP.ToString());
            }

            Logging.LogDB("Account Balance");
            double portfolio_value = 0;

            portfolio_value += GetAssetValueUSD("XXBTZUSD") * bal.BTC;
            portfolio_value += GetAssetValueUSD("XLTCUSD") * bal.LTC;
            portfolio_value += GetAssetValueUSD("XETHZUSD") * bal.ETH;
            portfolio_value += GetAssetValueUSD("XDGUSD") * bal.DGE;
            portfolio_value += GetAssetValueUSD("XMRUSD") * bal.XMR;
            portfolio_value += GetAssetValueUSD("DASHUSD") * bal.DASH;
            portfolio_value += GetAssetValueUSD("XZECUSD") * bal.ZEC;
            portfolio_value += GetAssetValueUSD("XREPZUSD") * bal.REP;

            //whoops dont forget dollars
            portfolio_value += bal.USD;

            Console.WriteLine("****************************************** ");
            Console.WriteLine("* ACCOUNT BALANCE                      ** ");
            Console.WriteLine("****************************************** ");
            Console.WriteLine("*      usd: " + bal.USD);
            Console.WriteLine("*  bitcoin: " + bal.BTC + " [$" + (GetAssetValueUSD("XXBTZUSD") * bal.BTC) + "]");
            Console.WriteLine("* litecoin: " + bal.LTC + " [$" + GetAssetValueUSD("XLTCUSD") * bal.LTC + "]");
            Console.WriteLine("* ethereum: " + bal.ETH + " [$" + GetAssetValueUSD("XETHZUSD") * bal.ETH + "]");
            Console.WriteLine("* dogecoin: " + bal.DGE + " [$" + GetAssetValueUSD("XDGUSD") * bal.DGE + "]");
            Console.WriteLine("*   monero: " + bal.XMR + " [$" + GetAssetValueUSD("XMRUSD") * bal.XMR + "]");
            Console.WriteLine("*     dash: " + bal.DASH + " [$" + GetAssetValueUSD("DASHUSD") * bal.DASH + "]");
            Console.WriteLine("*   z-cash: " + bal.ZEC + " [$" + GetAssetValueUSD("XZECUSD") * bal.ZEC + "]");
            Console.WriteLine("*    augur: " + bal.REP + " [$" + GetAssetValueUSD("XREPZUSD") * bal.REP + "]");
            Console.WriteLine("****************************************** ");
            Console.WriteLine("* Total Portfolio Value: " + portfolio_value.ToString("C", CultureInfo.CurrentCulture));
            Console.WriteLine("****************************************** ");

            // delete all entries from the accountbalance table
            AccountBalanceClear();

            // save this set of balances to the table
            AccountBalanceInsert(bal);
        }

        /// <summary>
        /// gets the value is USD of an assetpair from the database
        /// </summary>
        /// <param name="assetpair">XXBTCZUSD for example</param>
        /// <returns>Price of assetpair in dollars</returns>
        private static double GetAssetValueUSD(string assetpair)
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@assetpair", SqlDbType.NVarChar, 50) { Value = assetpair };
            double price = Convert.ToDouble(SqlHelper.ExecuteScalar(Config.DBConn, CommandType.StoredProcedure, "TICKER_GET_CurrentPrice", p));
            return price;
        }

        /// <summary>
        /// delete all records from accountbalance table
        /// </summary>
        public static void AccountBalanceClear()
        {
            SqlHelper.ExecuteNonQuery(Config.DBConn, System.Data.CommandType.StoredProcedure, "AccountBalance_DELETE_ALL");
        }

        /// <summary>
        /// add a recors to the accountbalance table
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

        /// <summary>
        /// the accountbalance object 
        /// an unholy union of json, c# and mexican rodeo carnies
        /// when the app receives the JSON data fro Kraken 
        /// newtonsoft.json deserializes the data into this handy class
        /// </summary>
        public class Result
        {
            /// <summary>
            /// USD Cash Money
            /// </summary>
            public string ZUSD { get; set; }

            /// <summary>
            /// Dogecoin Balance
            /// </summary>
            public string XXDG { get; set; }

            /// <summary>
            /// Bitcoin Balance
            /// </summary>
            public string XXBT { get; set; }

            /// <summary>
            /// Ethereum Balance
            /// </summary>
            public string XETH { get; set; }

            /// <summary>
            /// Litecoin Balance
            /// </summary>
            public string XLTC { get; set; }

            /// <summary>
            /// Monero Balance
            /// </summary>
            public string XXMR { get; set; }

            /// <summary>
            /// DASH Balance
            /// </summary>
            public string DASH { get; set; }

            /// <summary>
            /// ZCASH Balance
            /// </summary>
            public string XZEC { get; set; }

            /// <summary>
            /// REP Balance (Augur)
            /// </summary>
            public string XREP { get; set; }
        }

        public class Root
        {
            public List<object> error { get; set; }
            public Result result { get; set; }
        }
    }
}