using System.Collections.Generic;

namespace Paul.Utils
{
    /// <summary>
    /// class to hold Open High Low Close Data from Kraken API
    /// </summary>
    public class OHLC_GENERIC
    {
        //Array of strings or integers(TickData) [items[items = 8 items] ]

        // Array of tick data arrays[int < time >,
        // string < open >,
        // string < high >,
        // string < low >,
        // string < close >,
        // string < vwap >,
        // string < volume >,
        // int < count >]
        public static Root GetClassFromJson(string myJsonResponse)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(myJsonResponse);
        }
        
        public class Result
        {
            public List<List<object>> XXBTZUSD { get; set; }
            public List<List<object>> XETHZUSD { get; set; }
            public List<List<object>> XLTCUSD { get; set; }
            public int last { get; set; }
        }

        public class Root
        {
            public List<object> error { get; set; }
            public Result result { get; set; }
        }
    }
}