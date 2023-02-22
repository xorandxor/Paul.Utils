using System;
using System.Collections.Generic;

namespace Paul.Utils
{
    public class AccountBalance
    {
        public Root GetClassFromJson(string myJsonResponse)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(myJsonResponse);
        }

        public void GetAccountBalance()
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
            Logging.LogDB("USD:" + acctbal.result.ZUSD);
            Logging.LogDB("DOGE:" + acctbal.result.XXDG);
            Logging.LogDB("BTC:" + acctbal.result.XXBT);
            //       Logging.LogDB(privateResponse);

            Console.WriteLine("     usd: " + acctbal.result.ZUSD);
            Console.WriteLine(" bitcoin: " + acctbal.result.XXBT);
            Console.WriteLine("     eth: " + acctbal.result.XETH);
            Console.WriteLine("litecoin: " + acctbal.result.XLTC);
            Console.WriteLine("    dash: " + acctbal.result.DASH);
            Console.WriteLine("     xmr: " + acctbal.result.XXMR);
            Console.WriteLine("    link: " + acctbal.result.LINK);
            Console.WriteLine("     zec: " + acctbal.result.XZEC);
            Console.WriteLine("     sol: " + acctbal.result.XSOL);
            wait(2000);
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
            /// SOL Balance
            /// </summary>
            public string XSOL { get; set; }
        }

        public class Root
        {
            public List<object> error { get; set; }
            public Result result { get; set; }
        }
    }
}