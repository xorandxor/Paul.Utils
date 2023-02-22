using System;

namespace Paul.Utils
{
    public static class Utilities
    {

        
            public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
            {
                // Unix timestamp is seconds past epoch
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
                return dtDateTime;
            }

        public static string getUnixTimestamp()
        {
            return Convert.ToString((int)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
        }

        public static string getUnixTimestampMinusOneHour()
        {
            DateTime x = DateTime.Now.Subtract(new TimeSpan(0, 1, 0, 0));
            return Convert.ToString((int)x.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
        }

        public static string getUnixTimestampMinusTwoHours()
        {
            DateTime x = DateTime.Now.Subtract(new TimeSpan(0, 2, 0, 0));
            return Convert.ToString((int)x.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
        }

        public static string GetPercentageDifference(double number1, double number2)
        {
            double difference = number1 - number2;
            difference = Math.Round((difference / number1) * 100, 4);
            return (difference).ToString() + "%";
        }



    }
}