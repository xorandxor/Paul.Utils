using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;

namespace Paul.Utils
{
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
        public static Root GetClassFromJson(string myJsonResponse)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(myJsonResponse);
        }

        public static void GetAccountBalance()
        {
            DateTime st = DateTime.Now;
            string privateEndpoint = "Balance";
            string privateInputParameters = "";
            string apiPrivateKey = Config.ApiPrivateKey;
            string apiPublicKey = Config.ApiPublicKey;

            string privateResponse = API.QueryPrivateEndpoint(privateEndpoint, privateInputParameters, apiPublicKey, apiPrivateKey);
            string prirespfixed = privateResponse.Replace("USD.HOLD", "USDHOLD");
            Root acctbal = GetClassFromJson(prirespfixed);

            Logging.LogDB("Account Balance");

            Console.WriteLine("****************************************** ");
            Console.WriteLine("** ACCOUNT BALANCE                      ** ");
            Console.WriteLine("****************************************** ");
            Console.WriteLine("*      usd: " + acctbal.result.ZUSD);
            Console.WriteLine("*  bitcoin: " + acctbal.result.XXBT);
            Console.WriteLine("* litecoin: " + acctbal.result.XLTC);
            Console.WriteLine("* ethereum: " + acctbal.result.XETH);
            Console.WriteLine("*   monero: " + acctbal.result.XXMR);
            Console.WriteLine("*     dash: " + acctbal.result.DASH);
            Console.WriteLine("*   z-cash: " + acctbal.result.XZEC);
            Console.WriteLine("*    augur: " + acctbal.result.XREP);
            Console.WriteLine("****************************************** ");
            Console.WriteLine(" Saving to database.. ");

            BalanceObject bal = new BalanceObject();
            bal.USD = Convert.ToDouble(acctbal.result.ZUSD.ToString());
            bal.BTC = Convert.ToDouble(acctbal.result.XXBT.ToString());
            bal.LTC = Convert.ToDouble(acctbal.result.XLTC.ToString());
            bal.ETH = Convert.ToDouble(acctbal.result.XETH.ToString());
            bal.DGE = Convert.ToDouble(acctbal.result.XXDG.ToString());
            bal.XMR = Convert.ToDouble(acctbal.result.XXMR.ToString());
            bal.DASH = Convert.ToDouble(acctbal.result.DASH.ToString());
            bal.ZEC = Convert.ToDouble(acctbal.result.XZEC.ToString());
            bal.REP = Convert.ToDouble(acctbal.result.XREP.ToString());

            AccountBalance_Insert(bal);

            wait(2000);
        }

        public static void ClearBalanceTable()
        {
            SqlHelper.ExecuteNonQuery(Config.DBConn, System.Data.CommandType.StoredProcedure, "AccountBalance_DELETE_ALL");
        }
        public static void AccountBalance_Insert(BalanceObject bal)
        {

            // first we clear out the balance table
            ClearBalanceTable();

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
            #endregion

            SqlParameter[] p = new SqlParameter[9];
            p[0] = new SqlParameter("@usd", SqlDbType.Decimal) { Value = Convert.ToDecimal(bal.USD) };
            p[1] = new SqlParameter("@btc", SqlDbType.Decimal) { Value = Convert.ToDecimal(bal.BTC) };
            p[2] = new SqlParameter("@ltc", SqlDbType.Decimal) { Value = Convert.ToDecimal(bal.LTC) };
            p[3] = new SqlParameter("@eth", SqlDbType.Decimal) { Value = Convert.ToDecimal(bal.ETH) };
            p[4] = new SqlParameter("@xdg",  SqlDbType.Decimal) { Value = Convert.ToDecimal(bal.DGE) };
            p[5] = new SqlParameter("@xmr", SqlDbType.Decimal) { Value = Convert.ToDecimal(bal.XMR) };
            p[6] = new SqlParameter("@dash", SqlDbType.Decimal) { Value = Convert.ToDecimal(bal.DASH) };
            p[7] = new SqlParameter("@zec", SqlDbType.Decimal) { Value = Convert.ToDecimal(bal.ZEC) };
            p[8] = new SqlParameter("@rep", SqlDbType.Decimal) { Value = Convert.ToDecimal(bal.REP) };
            SqlHelper.ExecuteNonQuery(Config.DBConn, CommandType.StoredProcedure, "AccountBalance_Insert", p);
        }

       

        public static void wait(int msec)
        {
            System.Threading.Thread.Sleep(msec);
        }

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
            /// Link Balance "XZEC":"0.1500000000","XXMR":"0.1000000000"
            /// </summary>
            public string LINK { get; set; }

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