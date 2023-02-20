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
            Logging.LogDB(privateResponse);
            Console.WriteLine(privateResponse);
            Console.WriteLine("Asset:" + acctbal.result.ZUSD);
        }

        public class Result
        {
            /// <summary>
            /// USD CAAAAASH Balance
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
            /// Link Balance
            /// </summary>
            public string LINK { get; set; }

            /// <summary>
            /// Monero Balance
            /// </summary>
            public string XMR { get; set; }

            /// <summary>
            /// DASH Balance
            /// </summary>
            public string DASH { get; set; }

            /// <summary>
            /// ZCASH Balance
            /// </summary>
            public string ZEC { get; set; }

            /// <summary>
            /// AUGUR Balance
            /// </summary>
            public string REP { get; set; }
        }

        public class Root
        {
            public List<object> error { get; set; }
            public Result result { get; set; }
        }
    }
}