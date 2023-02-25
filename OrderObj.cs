using System.Collections.Generic;

namespace Paul.Utils
{
    public class OrderReply
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Descr
        {
            public string order { get; set; }
            public string close { get; set; }
        }

        public class Result
        {
            public Descr descr { get; set; }
            public List<string> txid { get; set; }
        }

        public class Root
        {
            public List<object> error { get; set; }
            public Result result { get; set; }
        }
    }
}