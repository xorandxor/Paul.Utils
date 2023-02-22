using System.Collections.Generic;

namespace Paul.Utils
{
    /// <summary>
    /// class to hold Open High Low Close Data from Kraken API
    /// </summary>
    public class OHLC_XETHUSD
    {
        //Array of strings or integers(TickData) [items[items = 8 items] ]
        // Array of tick data arrays[
        // int < time >,
        // string < open >,
        // string < high >,
        // string < low >,
        // string < close >,
        // string < vwap >,
        // string < volume >,
        // int < count >]
        public Root GetClassFromJson(string myJsonResponse)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(myJsonResponse);
        }

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

        public string GetOhlcJson()
        {
            string pairname = "ETHUSD";
            string publicEndpoint = "OHLC";
            string publicInputParameters = "pair=" + pairname + "&interval=1&since=" + Utilities.getUnixTimestampMinusOneHour();
            string publicResponse = API.QueryPublicEndpoint(publicEndpoint, publicInputParameters);

            Logging.Log("ETH OHLC");
            Logging.Log(publicResponse);
            return publicResponse;
        }

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Result
        {
            public List<List<object>> XETHZUSD { get; set; }
            public int last { get; set; }
        }

        public class Root
        {
            public List<object> error { get; set; }
            public Result result { get; set; }
        }
    }
}