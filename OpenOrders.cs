using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paul.Utils
{
    public class OpenOrders
    {

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Descr
        {
        }

        public class Open
        {
            public Txid1 txid1 { get; set; }
            public Txid2 txid2 { get; set; }
        }

        public class Result
        {
            public Open open { get; set; }
        }

        public class Root
        {
            public Result result { get; set; }
            public List<string> error { get; set; }
        }

        public class Txid1
        {
            public string refid { get; set; }
            public string userref { get; set; }
            public string status { get; set; }
            public int opentm { get; set; }
            public int starttm { get; set; }
            public int expiretm { get; set; }
            public Descr descr { get; set; }
            public string vol { get; set; }
            public string vol_exec { get; set; }
            public string cost { get; set; }
            public string fee { get; set; }
            public string price { get; set; }
            public string stopprice { get; set; }
            public string limitprice { get; set; }
            public string trigger { get; set; }
            public string misc { get; set; }
            public string oflags { get; set; }
            public List<object> trades { get; set; }
        }

        public class Txid2
        {
            public string refid { get; set; }
            public string userref { get; set; }
            public string status { get; set; }
            public int opentm { get; set; }
            public int starttm { get; set; }
            public int expiretm { get; set; }
            public Descr descr { get; set; }
            public string vol { get; set; }
            public string vol_exec { get; set; }
            public string cost { get; set; }
            public string fee { get; set; }
            public string price { get; set; }
            public string stopprice { get; set; }
            public string limitprice { get; set; }
            public string trigger { get; set; }
            public string misc { get; set; }
            public string oflags { get; set; }
            public List<object> trades { get; set; }
        }




    }
}
