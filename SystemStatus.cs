using System;
using System.Collections.Generic;

namespace Paul.Utils
{
    public class SystemStatus
    {
        public Root GetClassFromJson(string myJsonResponse)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(myJsonResponse);
        }

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

        public double GetAPIResponseTime()
        {
            DateTime st = DateTime.Now;
            string publicEndpoint = "SystemStatus";
            string publicInputParameters = "";
            string publicResponse = API.QueryPublicEndpoint(publicEndpoint, publicInputParameters);
            //Logging.Log(publicResponse);
            TimeSpan ts = DateTime.Now.Subtract(st);

            return ts.TotalMilliseconds;
        }

        public string GetSystemStatus()
        {
            string publicEndpoint = "SystemStatus";
            string publicInputParameters = "";
            string publicResponse = API.QueryPublicEndpoint(publicEndpoint, publicInputParameters);
            Logging.LogDB(publicResponse);
            return publicResponse;
        }

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Result
        {
            public string status { get; set; }
            public DateTime timestamp { get; set; }
        }

        public class Root
        {
            public List<object> error { get; set; }
            public Result result { get; set; }
        }
    }
}