using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Paul.Utils
{
    public class OHLCObject
    {
        public OHLCObject()
        { }

        public string AssetPair { get; set; }
        public DateTime timestamp { get; set; }
        public double OpenPrice { get; set; }
        public double HighPrice { get; set; }
        public double LowPrice { get; set; }
        public double ClosePrice { get; set; }
        public double VolWeightedAvgPrice { get; set; }
        public double Volume { get; set; }
        public double Count { get; set; }
    }

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
            public List<List<object>> XLTCZUSD { get; set; }
            public List<List<object>> XDGUSD { get; set; }
            public List<List<object>> XMRUSD { get; set; }
            public List<List<object>> DASHUSD { get; set; }
            public List<List<object>> XZECUSD { get; set; }
            public List<List<object>> XREPZUSD { get; set; }

            public int last { get; set; }
        }

        public static void ClearOHLCTable(string pairname)
        {
            string sql = "delete from OHLC where AssetPair = '" + pairname + "'";

            SqlHelper.ExecuteNonQuery(Config.DBConn, System.Data.CommandType.Text, sql);
        }

        public static void ClearOHLCTable()
        {
            string sql = "delete from OHLC";

            SqlHelper.ExecuteNonQuery(Config.DBConn, System.Data.CommandType.Text, sql);
        }

        /// <summary>
        /// save ohlc data to the mssql table using OHLC_INSERT stored proedure
        /// </summary>
        /// <param name="root">data structure</param>
        /// <returns>(double) Number of records added to the table</returns>
        public static double SaveToOHLCTable(Root root)
        {
            double idx = 0;

            ///save bitcoin
            if (root.result.XXBTZUSD != null)
            {
                if (root.result.XXBTZUSD.Count > 0)
                {
                    Console.Write("XXBTZUSD Records: " + root.result.XXBTZUSD.Count.ToString());

                    for (int i = 0; i < root.result.XXBTZUSD.Count; i++)
                    {
                        OHLCObject obj = new OHLCObject();

                        obj.AssetPair = "XXBTZUSD";

                        obj.timestamp = Utilities.UnixTimeStampToDateTime(Convert.ToDouble(root.result.XXBTZUSD[i][0].ToString()));
                        obj.OpenPrice = Convert.ToDouble(root.result.XXBTZUSD[i][1].ToString());
                        obj.HighPrice = Convert.ToDouble(root.result.XXBTZUSD[i][2].ToString());
                        obj.LowPrice = Convert.ToDouble(root.result.XXBTZUSD[i][3].ToString());
                        obj.ClosePrice = Convert.ToDouble(root.result.XXBTZUSD[i][4].ToString());
                        obj.VolWeightedAvgPrice = Convert.ToDouble(root.result.XXBTZUSD[i][5].ToString());
                        obj.Volume = Convert.ToDouble(root.result.XXBTZUSD[i][6].ToString());
                        obj.Count = Convert.ToDouble(root.result.XXBTZUSD[i][7].ToString());

                        SaveRecord(obj);
                        idx++;
                    }
                }
            }

            ///save litecoin
            ///
            if (root.result.XLTCZUSD != null)
            {
                if (root.result.XLTCZUSD.Count > 0)
                {
                    Console.Write("XLTCZUSD Records: " + root.result.XLTCZUSD.Count.ToString());

                    for (int i = 0; i < root.result.XLTCZUSD.Count; i++)
                    {
                        ClearOHLCTable("XLTZUSD");

                        OHLCObject obj = new OHLCObject();
                        obj.AssetPair = "XLTZUSD";
                        obj.timestamp = Utilities.UnixTimeStampToDateTime(Convert.ToDouble(root.result.XLTCZUSD[i][0].ToString()));
                        obj.OpenPrice = Convert.ToDouble(root.result.XLTCZUSD[i][1].ToString());
                        obj.HighPrice = Convert.ToDouble(root.result.XLTCZUSD[i][2].ToString());
                        obj.LowPrice = Convert.ToDouble(root.result.XLTCZUSD[i][3].ToString());
                        obj.ClosePrice = Convert.ToDouble(root.result.XLTCZUSD[i][4].ToString());
                        obj.VolWeightedAvgPrice = Convert.ToDouble(root.result.XLTCZUSD[i][5].ToString());
                        obj.Volume = Convert.ToDouble(root.result.XLTCZUSD[i][6].ToString());
                        obj.Count = Convert.ToDouble(root.result.XLTCZUSD[i][7].ToString());
                        SaveRecord(obj);
                        idx++;
                        //Console.WriteLine("Saved record with timestamp: " + obj.timestamp.ToString());
                    }
                }
            }

            ///save ethereum
            ///
            if (root.result.XETHZUSD != null)
            {
                if (root.result.XETHZUSD.Count > 0)
                {
                    ClearOHLCTable("XETHZUSD");
                    Console.Write("XETHZUSD Records: " + root.result.XETHZUSD.Count.ToString());

                    for (int i = 0; i < root.result.XETHZUSD.Count; i++)
                    {
                        OHLCObject obj = new OHLCObject();
                        obj.AssetPair = "XETHZUSD";
                        obj.timestamp = Utilities.UnixTimeStampToDateTime(Convert.ToDouble(root.result.XETHZUSD[i][0].ToString()));
                        obj.OpenPrice = Convert.ToDouble(root.result.XETHZUSD[i][1].ToString());
                        obj.HighPrice = Convert.ToDouble(root.result.XETHZUSD[i][2].ToString());
                        obj.LowPrice = Convert.ToDouble(root.result.XETHZUSD[i][3].ToString());
                        obj.ClosePrice = Convert.ToDouble(root.result.XETHZUSD[i][4].ToString());
                        obj.VolWeightedAvgPrice = Convert.ToDouble(root.result.XETHZUSD[i][5].ToString());
                        obj.Volume = Convert.ToDouble(root.result.XETHZUSD[i][6].ToString());
                        obj.Count = Convert.ToDouble(root.result.XETHZUSD[i][7].ToString());
                        SaveRecord(obj);
                        idx++;
                        //Console.WriteLine("Saved record with timestamp: " + obj.timestamp.ToString());
                    }
                }
            }

            ///save doge

            if (root.result.XDGUSD != null)
            {
                if (root.result.XDGUSD.Count > 0)
                {
                    ClearOHLCTable("XDGUSD");
                    Console.Write("XDGUSD Records: " + root.result.XDGUSD.Count.ToString());

                    for (int i = 0; i < root.result.XDGUSD.Count; i++)
                    {
                        OHLCObject obj = new OHLCObject();
                        obj.AssetPair = "XDGUSD";
                        obj.timestamp = Utilities.UnixTimeStampToDateTime(Convert.ToDouble(root.result.XDGUSD[i][0].ToString())); obj.OpenPrice = Convert.ToDouble(root.result.XDGUSD[i][1].ToString());
                        obj.HighPrice = Convert.ToDouble(root.result.XDGUSD[i][2].ToString());
                        obj.LowPrice = Convert.ToDouble(root.result.XDGUSD[i][3].ToString());
                        obj.ClosePrice = Convert.ToDouble(root.result.XDGUSD[i][4].ToString());
                        obj.VolWeightedAvgPrice = Convert.ToDouble(root.result.XDGUSD[i][5].ToString());
                        obj.Volume = Convert.ToDouble(root.result.XDGUSD[i][6].ToString());
                        obj.Count = Convert.ToDouble(root.result.XDGUSD[i][7].ToString());
                        SaveRecord(obj);
                        idx++;
                        //Console.WriteLine("Saved record with timestamp: " + obj.timestamp.ToString());
                    }
                }
            }

            //monero/xmr
            if (root.result.XMRUSD != null)
            {
                if (root.result.XMRUSD.Count > 0)
                {
                    ClearOHLCTable("XMRUSD");
                    Console.Write("XMRUSD Records: " + root.result.XMRUSD.Count.ToString());

                    for (int i = 0; i < root.result.XMRUSD.Count; i++)
                    {
                        OHLCObject obj = new OHLCObject();
                        obj.AssetPair = "XMRUSD";
                        obj.timestamp = Utilities.UnixTimeStampToDateTime(Convert.ToDouble(root.result.XMRUSD[i][0].ToString())); obj.OpenPrice = Convert.ToDouble(root.result.XMRUSD[i][1].ToString());
                        obj.HighPrice = Convert.ToDouble(root.result.XMRUSD[i][2].ToString());
                        obj.LowPrice = Convert.ToDouble(root.result.XMRUSD[i][3].ToString());
                        obj.ClosePrice = Convert.ToDouble(root.result.XMRUSD[i][4].ToString());
                        obj.VolWeightedAvgPrice = Convert.ToDouble(root.result.XMRUSD[i][5].ToString());
                        obj.Volume = Convert.ToDouble(root.result.XMRUSD[i][6].ToString());
                        obj.Count = Convert.ToDouble(root.result.XMRUSD[i][7].ToString());
                        SaveRecord(obj);
                        idx++;
                        //Console.WriteLine("Saved record with timestamp: " + obj.timestamp.ToString());
                    }
                }
            }

            //dash
            if (root.result.DASHUSD != null)
            {
                if (root.result.DASHUSD.Count > 0)
                {
                    ClearOHLCTable("DASHUSD");
                    Console.Write("DASHUSD Records: " + root.result.DASHUSD.Count.ToString());

                    for (int i = 0; i < root.result.DASHUSD.Count; i++)
                    {
                        OHLCObject obj = new OHLCObject();
                        obj.AssetPair = "DASHUSD";
                        obj.timestamp = Utilities.UnixTimeStampToDateTime(Convert.ToDouble(root.result.DASHUSD[i][0].ToString())); obj.OpenPrice = Convert.ToDouble(root.result.DASHUSD[i][1].ToString());
                        obj.HighPrice = Convert.ToDouble(root.result.DASHUSD[i][2].ToString());
                        obj.LowPrice = Convert.ToDouble(root.result.DASHUSD[i][3].ToString());
                        obj.ClosePrice = Convert.ToDouble(root.result.DASHUSD[i][4].ToString());
                        obj.VolWeightedAvgPrice = Convert.ToDouble(root.result.DASHUSD[i][5].ToString());
                        obj.Volume = Convert.ToDouble(root.result.DASHUSD[i][6].ToString());
                        obj.Count = Convert.ToDouble(root.result.DASHUSD[i][7].ToString());
                        SaveRecord(obj);
                        idx++;
                        //Console.WriteLine("Saved record with timestamp: " + obj.timestamp.ToString());
                    }
                }
            }

            //zcash
            if (root.result.XZECUSD != null)
            {
                if (root.result.XZECUSD.Count > 0)
                {
                    ClearOHLCTable("XZECUSD");
                    Console.Write("XZECUSD Records: " + root.result.XZECUSD.Count.ToString());

                    for (int i = 0; i < root.result.XZECUSD.Count; i++)
                    {
                        OHLCObject obj = new OHLCObject();
                        obj.AssetPair = "XZECUSD";
                        obj.timestamp = Utilities.UnixTimeStampToDateTime(Convert.ToDouble(root.result.XZECUSD[i][0].ToString())); obj.OpenPrice = Convert.ToDouble(root.result.XZECUSD[i][1].ToString());
                        obj.HighPrice = Convert.ToDouble(root.result.XZECUSD[i][2].ToString());
                        obj.LowPrice = Convert.ToDouble(root.result.XZECUSD[i][3].ToString());
                        obj.ClosePrice = Convert.ToDouble(root.result.XZECUSD[i][4].ToString());
                        obj.VolWeightedAvgPrice = Convert.ToDouble(root.result.XZECUSD[i][5].ToString());
                        obj.Volume = Convert.ToDouble(root.result.XZECUSD[i][6].ToString());
                        obj.Count = Convert.ToDouble(root.result.XZECUSD[i][7].ToString());
                        SaveRecord(obj);
                        idx++;
                        //Console.WriteLine("Saved record with timestamp: " + obj.timestamp.ToString());
                    }
                }
            }

            //augur
            if (root.result.XREPZUSD != null)
            {
                if (root.result.XREPZUSD.Count > 0)
                {
                    ClearOHLCTable("XREPZUSD");
                    Console.Write("XREPZUSD Records: " + root.result.XREPZUSD.Count.ToString());

                    for (int i = 0; i < root.result.XREPZUSD.Count; i++)
                    {
                        OHLCObject obj = new OHLCObject();
                        obj.AssetPair = "XREPZUSD";
                        obj.timestamp = Utilities.UnixTimeStampToDateTime(Convert.ToDouble(root.result.XREPZUSD[i][0].ToString()));
                        obj.OpenPrice = Convert.ToDouble(root.result.XREPZUSD[i][1].ToString());
                        obj.HighPrice = Convert.ToDouble(root.result.XREPZUSD[i][2].ToString());
                        obj.LowPrice = Convert.ToDouble(root.result.XREPZUSD[i][3].ToString());
                        obj.ClosePrice = Convert.ToDouble(root.result.XREPZUSD[i][4].ToString());
                        obj.VolWeightedAvgPrice = Convert.ToDouble(root.result.XREPZUSD[i][5].ToString());
                        obj.Volume = Convert.ToDouble(root.result.XREPZUSD[i][6].ToString());
                        obj.Count = Convert.ToDouble(root.result.XREPZUSD[i][7].ToString());
                        SaveRecord(obj);
                        idx++;
                        //Console.WriteLine("Saved record with timestamp: " + obj.timestamp.ToString());
                    }
                }
            }
            return idx;
        }

        private static void SaveRecord(OHLCObject o)
        {
            //[OHLC_INSERT]
            /*@timestampjson datetime,
		  @assetpair nvarchar(50),
		  @openprice decimal(18,0),
		  @highprice decimal (18,0),
		  @lowprice decimal (18,0),
          @closeprice decimal(18,0),
		  @volweightedavgprice decimal (18,0),
		  @vol decimal(18,0),
		  @count int
            */

            SqlParameter[] sp = new SqlParameter[9];
            sp[0] = new SqlParameter("@timestampjson", System.Data.SqlDbType.DateTime);
            sp[0].Value = o.timestamp;

            sp[1] = new SqlParameter("@assetpair", System.Data.SqlDbType.NVarChar, 50);
            sp[1].Value = o.AssetPair;

            sp[2] = new SqlParameter("@openprice", System.Data.SqlDbType.Decimal);
            sp[2].Value = o.OpenPrice;

            sp[3] = new SqlParameter("@highprice", System.Data.SqlDbType.Decimal);
            sp[3].Value = o.HighPrice;

            sp[4] = new SqlParameter("@lowprice", System.Data.SqlDbType.Decimal);
            sp[4].Value = o.LowPrice;

            sp[5] = new SqlParameter("@closeprice", System.Data.SqlDbType.Decimal);
            sp[5].Value = o.ClosePrice;

            sp[6] = new SqlParameter("@volweightedavgprice", System.Data.SqlDbType.Decimal);
            sp[6].Value = o.VolWeightedAvgPrice;

            sp[7] = new SqlParameter("@vol", System.Data.SqlDbType.Decimal);
            sp[7].Value = o.Volume;

            sp[8] = new SqlParameter("@count", System.Data.SqlDbType.Int);
            sp[8].Value = o.Count;

            try
            {
                SqlHelper.ExecuteNonQuery(Config.DBConn, System.Data.CommandType.StoredProcedure, "OHLC_INSERT", sp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logging.LogDB(ex.ToString());
            }
        }

        public class Root
        {
            public List<object> error { get; set; }
            public Result result { get; set; }
        }
    }
}