using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public partial class AccountBalance
    {

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
