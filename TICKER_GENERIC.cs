using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Paul.Utils
{
    public class TickerObject
    {
        public TickerObject()
        { }

        #region definition

        //  CREATE PROCEDURE[dbo].[TICKER_INSERT]
        //  (
        //@assetpair nvarchar(50),
        //@ask decimal,
        //@askwlvolume decimal,
        //@asklotvolume decimal,
        //@bid decimal,
        //@bidwlvolume decimal,
        //@bidlotvolume decimal,
        //@volumetoday decimal,
        //@volume24hrs decimal,
        //@volweightedavgpricetoday decimal,
        //@volweightedavgprice24hr decimal,
        //@numberoftradestoday decimal,
        //@numberoftrades24hrs decimal,
        //@lowtoday decimal,
        //@low24hr decimal,
        //@hightoday decimal,
        //@high24hr decimal,
        //@openprice decimal

        #endregion definition

        public string assetpair { get; set; } = "";
        public double ask { get; set; } = 0;
        public double askwlvolume { get; set; } = 0;
        public double asklotvolume { get; set; } = 0;
        public double bid { get; set; } = 0;
        public double bidwlvolume { get; set; } = 0;
        public double bidlotvolume { get; set; } = 0;
        public double volumetoday { get; set; } = 0;
        public double volume24hr { get; set; } = 0;
        public double volweightedavgpricetoday { get; set; } = 0;
        public double volweightedavgprice24hr { get; set; } = 0;
        public double numberoftradestoday { get; set; } = 0;
        public double Numberoftrades24hrs { get; set; } = 0;
        public double lowtoday { get; set; } = 0;
        public double low24hr { get; set; } = 0;
        public double hightoday { get; set; } = 0;
        public double high24hr { get; set; } = 0;
        public double openprice { get; set; } = 0;
    }

    internal class TICKER_GENERIC
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Result
        {
            public XXBTZUSD XXBTZUSD { get; set; }
            public XLTCUSD XLTCUSD { get; set; }
            public XETHZUSD XETHZUSD { get; set; }
            public XDGUSD XDGUSD { get; set; }
            public XMRUSD XMRUSD { get; set; }
            public DASHUSD DASHUSD { get; set; }
            public XZECUSD XZECUSD { get; set; }
            public XREPZUSD XREPZUSD { get; set; }
        }

        public class Root
        {
            public List<object> error { get; set; }
            public Result result { get; set; }
        }

        public class XXBTZUSD
        {
            public List<string> a { get; set; }
            public List<string> b { get; set; }
            public List<string> c { get; set; }
            public List<string> v { get; set; }
            public List<string> p { get; set; }
            public List<int> t { get; set; }
            public List<string> l { get; set; }
            public List<string> h { get; set; }
            public string o { get; set; }
        }

        public class XLTCUSD
        {
            public List<string> a { get; set; }
            public List<string> b { get; set; }
            public List<string> c { get; set; }
            public List<string> v { get; set; }
            public List<string> p { get; set; }
            public List<int> t { get; set; }
            public List<string> l { get; set; }
            public List<string> h { get; set; }
            public string o { get; set; }
        }

        public class XETHZUSD
        {
            public List<string> a { get; set; }
            public List<string> b { get; set; }
            public List<string> c { get; set; }
            public List<string> v { get; set; }
            public List<string> p { get; set; }
            public List<int> t { get; set; }
            public List<string> l { get; set; }
            public List<string> h { get; set; }
            public string o { get; set; }
        }

        public class XDGUSD
        {
            public List<string> a { get; set; }
            public List<string> b { get; set; }
            public List<string> c { get; set; }
            public List<string> v { get; set; }
            public List<string> p { get; set; }
            public List<int> t { get; set; }
            public List<string> l { get; set; }
            public List<string> h { get; set; }
            public string o { get; set; }
        }

        public class XMRUSD
        {
            public List<string> a { get; set; }
            public List<string> b { get; set; }
            public List<string> c { get; set; }
            public List<string> v { get; set; }
            public List<string> p { get; set; }
            public List<int> t { get; set; }
            public List<string> l { get; set; }
            public List<string> h { get; set; }
            public string o { get; set; }
        }

        public class DASHUSD
        {
            public List<string> a { get; set; }
            public List<string> b { get; set; }
            public List<string> c { get; set; }
            public List<string> v { get; set; }
            public List<string> p { get; set; }
            public List<int> t { get; set; }
            public List<string> l { get; set; }
            public List<string> h { get; set; }
            public string o { get; set; }
        }

        public class XZECUSD
        {
            public List<string> a { get; set; }
            public List<string> b { get; set; }
            public List<string> c { get; set; }
            public List<string> v { get; set; }
            public List<string> p { get; set; }
            public List<int> t { get; set; }
            public List<string> l { get; set; }
            public List<string> h { get; set; }
            public string o { get; set; }
        }

        public class XREPZUSD
        {
            public List<string> a { get; set; }
            public List<string> b { get; set; }
            public List<string> c { get; set; }
            public List<string> v { get; set; }
            public List<string> p { get; set; }
            public List<int> t { get; set; }
            public List<string> l { get; set; }
            public List<string> h { get; set; }
            public string o { get; set; }
        }

        /// <summary>
        /// save ohlc data to the mssql table using OHLC_INSERT stored proedure
        /// </summary>
        /// <param name="root">data structure</param>
        /// <returns>(double) Number of records added to the table</returns>
        public static double SaveToTickerTable(Root root)
        {
            double idx = 0;

            ///save bitcoin
            if (root.result.XXBTZUSD != null)
            {
                Console.WriteLine("XXBTZUSD Ticker: Ask: $" + root.result.XXBTZUSD.a);

                TickerObject obj = new TickerObject();
                
                #region ref material
                //public string assetpair { get; set; } = "";
                //public double ask { get; set; } = 0;
                //public double askwlvolume { get; set; } = 0;
                //public double asklotvolume { get; set; } = 0;
                //public double bid { get; set; } = 0;
                //public double bidwlvolume { get; set; } = 0;
                //public double bidlotvolume { get; set; } = 0;
                //public double volumetoday { get; set; } = 0;
                //public double volume24hr { get; set; } = 0;
                //public double volweightedavgpricetoday { get; set; } = 0;
                //public double volweightedavgprice24hr { get; set; } = 0;
                //public double numberoftradestoday { get; set; } = 0;
                //public double Numberoftrades24hrs { get; set; } = 0;
                //public double lowtoday { get; set; } = 0;
                //public double low24hr { get; set; } = 0;
                //public double hightoday { get; set; } = 0;
                //public double high24hr { get; set; } = 0;
                //public double openprice { get; set; } = 0;
                #endregion
                
                obj.assetpair = "XXBTZUSD";
                obj.ask = Convert.ToDouble(root.result.XXBTZUSD.a[0]);
                obj.askwlvolume = Convert.ToDouble(root.result.XXBTZUSD.a[1]);
                obj.asklotvolume = Convert.ToDouble(root.result.XXBTZUSD.a[2]);
                obj.bid = Convert.ToDouble(root.result.XXBTZUSD.b[0]);
                obj.bidwlvolume = Convert.ToDouble(root.result.XXBTZUSD.b[1]);
                obj.bidlotvolume = Convert.ToDouble(root.result.XXBTZUSD.b[2]);
                obj.volumetoday = Convert.ToDouble(root.result.XXBTZUSD.v[0]);
                obj.volume24hr = Convert.ToDouble(root.result.XXBTZUSD.v[1]);

                obj.volweightedavgpricetoday = Convert.ToDouble(root.result.XXBTZUSD.v[0]);
                obj.volumetoday = Convert.ToDouble(root.result.XXBTZUSD.v[0]);

                obj.timestamp = Utilities.UnixTimeStampToDateTime(Convert.ToDouble(root.result.XXBTZUSD[i][0].ToString()));
                obj.OpenPrice = Convert.ToDouble(root.result.XXBTZUSD[i][1].ToString());
                obj.HighPrice = Convert.ToDouble(root.result.XXBTZUSD[i][2].ToString());
                obj.LowPrice = Convert.ToDouble(root.result.XXBTZUSD[i][3].ToString());
                obj.ClosePrice = Convert.ToDouble(root.result.XXBTZUSD[i][4].ToString());
                obj.VolWeightedAvgPrice = Convert.ToDouble(root.result.XXBTZUSD[i][5].ToString());
                obj.Volume = Convert.ToDouble(root.result.XXBTZUSD[i][6].ToString());
                obj.Count = Convert.ToDouble(root.result.XXBTZUSD[i][7].ToString());

                SaveTickerRecord(obj);
                idx++;
            }
        }

        public static Root GetClassFromJson(string myJsonResponse)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(myJsonResponse);
        }

        public static void ClearTickerTable()
        {
            SqlHelper.ExecuteNonQuery(Config.DBConn, System.Data.CommandType.StoredProcedure, "TICKER_DELETE_ALL");
            SqlHelper.ExecuteNonQuery(Config.DBConn, System.Data.CommandType.StoredProcedure, "TICKER_SET_STATUS_LOADING");
        }

        private static void SaveTickerRecord(TickerObject o)
        {
            //  CREATE PROCEDURE[dbo].[TICKER_INSERT]
            //  (
            //@assetpair nvarchar(50),
            //@ask decimal,
            //@askwlvolume decimal,
            //@asklotvolume decimal,
            //@bid decimal,
            //@bidwlvolume decimal,
            //@bidlotvolume decimal,
            //@volumetoday decimal,
            //@volume24hrs decimal,
            //@volweightedavgpricetoday decimal,
            //@volweightedavgprice24hr decimal,
            //@numberoftradestoday decimal,
            //@numberoftrades24hrs decimal,
            //@lowtoday decimal,
            //@low24hr decimal,
            //@hightoday decimal,
            //@high24hr decimal,
            //@openprice decimal

            SqlParameter[] sp = new SqlParameter[18];

            sp[0] = new SqlParameter("@assetpair", System.Data.SqlDbType.NVarChar, 50);
            sp[0].Value = o.assetpair;

            sp[1] = new SqlParameter("@ask", System.Data.SqlDbType.Decimal);
            sp[1].Value = o.ask;

            sp[2] = new SqlParameter("@askwlvolume", System.Data.SqlDbType.Decimal);
            sp[2].Value = o.askwlvolume;

            sp[3] = new SqlParameter("@asklotvolume", System.Data.SqlDbType.Decimal);
            sp[3].Value = o.asklotvolume;

            sp[4] = new SqlParameter("@bid", System.Data.SqlDbType.Decimal);
            sp[4].Value = o.bid;

            sp[5] = new SqlParameter("@bidwlvolume", System.Data.SqlDbType.Decimal);
            sp[5].Value = o.bidwlvolume;

            sp[6] = new SqlParameter("@bidlotvolume", System.Data.SqlDbType.Decimal);
            sp[6].Value = o.bidlotvolume;

            sp[7] = new SqlParameter("@volumetoday", System.Data.SqlDbType.Decimal);
            sp[7].Value = o.volumetoday;

            sp[8] = new SqlParameter("@volume24hrs", System.Data.SqlDbType.Decimal);
            sp[8].Value = o.volume24hr;

            sp[9] = new SqlParameter("@volweightedavgpricetoday", System.Data.SqlDbType.Decimal);
            sp[9].Value = o.volweightedavgpricetoday;

            sp[10] = new SqlParameter("@volweightedavgprice24hr", System.Data.SqlDbType.Decimal);
            sp[10].Value = o.volweightedavgprice24hr;

            sp[11] = new SqlParameter("@numberoftradestoday", System.Data.SqlDbType.Decimal);
            sp[11].Value = o.numberoftradestoday;

            sp[12] = new SqlParameter("@numberoftrades24hrs", System.Data.SqlDbType.Decimal);
            sp[12].Value = o.Numberoftrades24hrs;

            sp[13] = new SqlParameter("@lowtoday", System.Data.SqlDbType.Decimal);
            sp[13].Value = o.lowtoday;

            sp[14] = new SqlParameter("@low24hr", System.Data.SqlDbType.Decimal);
            sp[14].Value = o.low24hr;

            sp[15] = new SqlParameter("@hightoday", System.Data.SqlDbType.Decimal);
            sp[15].Value = o.hightoday;

            sp[16] = new SqlParameter("@high24hr", System.Data.SqlDbType.Decimal);
            sp[16].Value = o.high24hr;

            sp[17] = new SqlParameter("@open", System.Data.SqlDbType.Int);
            sp[17].Value = o.openprice;

            try
            {
                SqlHelper.ExecuteNonQuery(Config.DBConn, System.Data.CommandType.StoredProcedure, "TICKER_INSERT", sp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logging.LogDB(ex.ToString());
            }
        }
    }
}