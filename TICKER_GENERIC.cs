using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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

    public class TICKER_GENERIC
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Result
        {
            public XXBTZUSD XXBTZUSD { get; set; }
            public XLTCZUSD XLTCZUSD { get; set; }
            public XETHZUSD XETHZUSD { get; set; }
            public XDGUSD XDGUSD { get; set; }
            public XXMRZUSD XXMRZUSD { get; set; }
            public DASHUSD DASHUSD { get; set; }
            public XZECUSD XZECZUSD { get; set; }//XZECZUSD
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

        public class XLTCZUSD
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

        public class XXMRZUSD
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

        #region kraken api ticker data structure

        /*
         *
         * additional property

        object (AssetTickerInfo)

        Asset Ticker Info
        a
        sk [<price>, <whole lot volume>, <lot volume>]
        b
        Array of strings

        Bid [<price>, <whole lot volume>, <lot volume>]
        c
        Array of strings

        Last trade closed [<price>, <lot volume>]
        v
        Array of strings

        Volume [<today>, <last 24 hours>]
        p
        Array of strings

        Volume weighted average price [<today>, <last 24 hours>]
        t
        Array of integers

        Number of trades [<today>, <last 24 hours>]
        l
        Array of strings

        Low [<today>, <last 24 hours>]
        h
        Array of strings

        High [<today>, <last 24 hours>]
        o
        string

        Today's opening price
        error
        Array of strings (error)
        */

        #endregion kraken api ticker data structure

        /// <summary>
        /// save ohlc data to the mssql table using OHLC_INSERT stored proedure
        /// </summary>
        /// <param name="root">data structure</param>
        /// <returns>(double) Number of records added to the table</returns>
        public static double SaveToTickerTable(Root root)
        {
            double idx = 0;

            #region reference fields

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

            #endregion reference fields

            // data structure to hold the ticker information
            TickerObject obj = new TickerObject();

            ///save bitcoin
            if (root.result.XXBTZUSD != null)
            {
                Console.WriteLine("XXBTZUSD Ticker: Ask: $" + root.result.XXBTZUSD.a[0]);

                obj.assetpair = "XXBTZUSD";
                obj.ask = (double)Convert.ToDecimal(root.result.XXBTZUSD.a[0]);
                obj.askwlvolume = Convert.ToDouble(root.result.XXBTZUSD.a[1]);
                obj.asklotvolume = Convert.ToDouble(root.result.XXBTZUSD.a[2]);
                obj.bid = Convert.ToDouble(root.result.XXBTZUSD.b[0]);
                obj.bidwlvolume = Convert.ToDouble(root.result.XXBTZUSD.b[1]);
                obj.bidlotvolume = Convert.ToDouble(root.result.XXBTZUSD.b[2]);
                obj.volumetoday = Convert.ToDouble(root.result.XXBTZUSD.v[0]);
                obj.volume24hr = Convert.ToDouble(root.result.XXBTZUSD.v[1]);
                obj.volweightedavgpricetoday = Convert.ToDouble(root.result.XXBTZUSD.p[0]);
                obj.volweightedavgprice24hr = Convert.ToDouble(root.result.XXBTZUSD.p[1]);
                obj.numberoftradestoday = Convert.ToDouble(root.result.XXBTZUSD.t[0]);
                obj.Numberoftrades24hrs = Convert.ToDouble(root.result.XXBTZUSD.t[1]);
                obj.lowtoday = Convert.ToDouble(root.result.XXBTZUSD.l[0]);
                obj.low24hr = Convert.ToDouble(root.result.XXBTZUSD.l[1]);
                obj.hightoday = Convert.ToDouble(root.result.XXBTZUSD.h[0]);
                obj.high24hr = Convert.ToDouble(root.result.XXBTZUSD.h[1]);
                obj.openprice = Convert.ToDouble(root.result.XXBTZUSD.o);

                SaveTickerRecord(obj);

                idx++;
            }

            ///save litecoin
            if (root.result.XLTCZUSD != null)
            {
                Console.WriteLine("XLTCUSD Ticker: Ask: $" + root.result.XLTCZUSD.a[0]);

                obj.assetpair = "XLTCUSD";
                obj.ask = Convert.ToDouble(root.result.XLTCZUSD.a[0]);
                obj.askwlvolume = Convert.ToDouble(root.result.XLTCZUSD.a[1]);
                obj.asklotvolume = Convert.ToDouble(root.result.XLTCZUSD.a[2]);
                obj.bid = Convert.ToDouble(root.result.XLTCZUSD.b[0]);
                obj.bidwlvolume = Convert.ToDouble(root.result.XLTCZUSD.b[1]);
                obj.bidlotvolume = Convert.ToDouble(root.result.XLTCZUSD.b[2]);
                obj.volumetoday = Convert.ToDouble(root.result.XLTCZUSD.v[0]);
                obj.volume24hr = Convert.ToDouble(root.result.XLTCZUSD.v[1]);
                obj.volweightedavgpricetoday = Convert.ToDouble(root.result.XLTCZUSD.p[0]);
                obj.volweightedavgprice24hr = Convert.ToDouble(root.result.XLTCZUSD.p[1]);
                obj.numberoftradestoday = Convert.ToDouble(root.result.XLTCZUSD.t[0]);
                obj.Numberoftrades24hrs = Convert.ToDouble(root.result.XLTCZUSD.t[1]);
                obj.lowtoday = Convert.ToDouble(root.result.XLTCZUSD.l[0]);
                obj.low24hr = Convert.ToDouble(root.result.XLTCZUSD.l[1]);
                obj.hightoday = Convert.ToDouble(root.result.XLTCZUSD.h[0]);
                obj.high24hr = Convert.ToDouble(root.result.XLTCZUSD.h[1]);
                obj.openprice = Convert.ToDouble(root.result.XLTCZUSD.o);

                SaveTickerRecord(obj);

                idx++;
            }

            ///save ethereum
            if (root.result.XETHZUSD != null)
            {
                Console.WriteLine("XETHZUSD Ticker: Ask: $" + root.result.XETHZUSD.a[0]);

                obj.assetpair = "XETHZUSD";
                obj.ask = Convert.ToDouble(root.result.XETHZUSD.a[0]);
                obj.askwlvolume = Convert.ToDouble(root.result.XETHZUSD.a[1]);
                obj.asklotvolume = Convert.ToDouble(root.result.XETHZUSD.a[2]);
                obj.bid = Convert.ToDouble(root.result.XETHZUSD.b[0]);
                obj.bidwlvolume = Convert.ToDouble(root.result.XETHZUSD.b[1]);
                obj.bidlotvolume = Convert.ToDouble(root.result.XETHZUSD.b[2]);
                obj.volumetoday = Convert.ToDouble(root.result.XETHZUSD.v[0]);
                obj.volume24hr = Convert.ToDouble(root.result.XETHZUSD.v[1]);
                obj.volweightedavgpricetoday = Convert.ToDouble(root.result.XETHZUSD.p[0]);
                obj.volweightedavgprice24hr = Convert.ToDouble(root.result.XETHZUSD.p[1]);
                obj.numberoftradestoday = Convert.ToDouble(root.result.XETHZUSD.t[0]);
                obj.Numberoftrades24hrs = Convert.ToDouble(root.result.XETHZUSD.t[1]);
                obj.lowtoday = Convert.ToDouble(root.result.XETHZUSD.l[0]);
                obj.low24hr = Convert.ToDouble(root.result.XETHZUSD.l[1]);
                obj.hightoday = Convert.ToDouble(root.result.XETHZUSD.h[0]);
                obj.high24hr = Convert.ToDouble(root.result.XETHZUSD.h[1]);
                obj.openprice = Convert.ToDouble(root.result.XETHZUSD.o);

                SaveTickerRecord(obj);

                idx++;
            }

            ///save doge
            if (root.result.XDGUSD != null)
            {
                Console.WriteLine("XDGUSD Ticker: Ask: $" + root.result.XDGUSD.a[0]);

                obj.assetpair = "XDGUSD";
                obj.ask = Convert.ToDouble(root.result.XDGUSD.a[0]);
                obj.askwlvolume = Convert.ToDouble(root.result.XDGUSD.a[1]);
                obj.asklotvolume = Convert.ToDouble(root.result.XDGUSD.a[2]);
                obj.bid = Convert.ToDouble(root.result.XDGUSD.b[0]);
                obj.bidwlvolume = Convert.ToDouble(root.result.XDGUSD.b[1]);
                obj.bidlotvolume = Convert.ToDouble(root.result.XDGUSD.b[2]);
                obj.volumetoday = Convert.ToDouble(root.result.XDGUSD.v[0]);
                obj.volume24hr = Convert.ToDouble(root.result.XDGUSD.v[1]);
                obj.volweightedavgpricetoday = Convert.ToDouble(root.result.XDGUSD.p[0]);
                obj.volweightedavgprice24hr = Convert.ToDouble(root.result.XDGUSD.p[1]);
                obj.numberoftradestoday = Convert.ToDouble(root.result.XDGUSD.t[0]);
                obj.Numberoftrades24hrs = Convert.ToDouble(root.result.XDGUSD.t[1]);
                obj.lowtoday = Convert.ToDouble(root.result.XDGUSD.l[0]);
                obj.low24hr = Convert.ToDouble(root.result.XDGUSD.l[1]);
                obj.hightoday = Convert.ToDouble(root.result.XDGUSD.h[0]);
                obj.high24hr = Convert.ToDouble(root.result.XDGUSD.h[1]);
                obj.openprice = Convert.ToDouble(root.result.XDGUSD.o);

                SaveTickerRecord(obj);

                idx++;
            }

            ///save monero
            if (root.result.XXMRZUSD != null)
            {
                Console.WriteLine("XMRUSD Ticker: Ask: $" + root.result.XXMRZUSD.a[0]);

                obj.assetpair = "XMRUSD";
                obj.ask = Convert.ToDouble(root.result.XXMRZUSD.a[0]);
                obj.askwlvolume = Convert.ToDouble(root.result.XXMRZUSD.a[1]);
                obj.asklotvolume = Convert.ToDouble(root.result.XXMRZUSD.a[2]);
                obj.bid = Convert.ToDouble(root.result.XXMRZUSD.b[0]);
                obj.bidwlvolume = Convert.ToDouble(root.result.XXMRZUSD.b[1]);
                obj.bidlotvolume = Convert.ToDouble(root.result.XXMRZUSD.b[2]);
                obj.volumetoday = Convert.ToDouble(root.result.XXMRZUSD.v[0]);
                obj.volume24hr = Convert.ToDouble(root.result.XXMRZUSD.v[1]);
                obj.volweightedavgpricetoday = Convert.ToDouble(root.result.XXMRZUSD.p[0]);
                obj.volweightedavgprice24hr = Convert.ToDouble(root.result.XXMRZUSD.p[1]);
                obj.numberoftradestoday = Convert.ToDouble(root.result.XXMRZUSD.t[0]);
                obj.Numberoftrades24hrs = Convert.ToDouble(root.result.XXMRZUSD.t[1]);
                obj.lowtoday = Convert.ToDouble(root.result.XXMRZUSD.l[0]);
                obj.low24hr = Convert.ToDouble(root.result.XXMRZUSD.l[1]);
                obj.hightoday = Convert.ToDouble(root.result.XXMRZUSD.h[0]);
                obj.high24hr = Convert.ToDouble(root.result.XXMRZUSD.h[1]);
                obj.openprice = Convert.ToDouble(root.result.XXMRZUSD.o);

                SaveTickerRecord(obj);

                idx++;
            }

            ///save dash
            if (root.result.DASHUSD != null)
            {
                Console.WriteLine("DASHUSD Ticker: Ask: $" + root.result.DASHUSD.a[0]);

                obj.assetpair = "DASHUSD";
                obj.ask = Convert.ToDouble(root.result.DASHUSD.a[0]);
                obj.askwlvolume = Convert.ToDouble(root.result.DASHUSD.a[1]);
                obj.asklotvolume = Convert.ToDouble(root.result.DASHUSD.a[2]);
                obj.bid = Convert.ToDouble(root.result.DASHUSD.b[0]);
                obj.bidwlvolume = Convert.ToDouble(root.result.DASHUSD.b[1]);
                obj.bidlotvolume = Convert.ToDouble(root.result.DASHUSD.b[2]);
                obj.volumetoday = Convert.ToDouble(root.result.DASHUSD.v[0]);
                obj.volume24hr = Convert.ToDouble(root.result.DASHUSD.v[1]);
                obj.volweightedavgpricetoday = Convert.ToDouble(root.result.DASHUSD.p[0]);
                obj.volweightedavgprice24hr = Convert.ToDouble(root.result.DASHUSD.p[1]);
                obj.numberoftradestoday = Convert.ToDouble(root.result.DASHUSD.t[0]);
                obj.Numberoftrades24hrs = Convert.ToDouble(root.result.DASHUSD.t[1]);
                obj.lowtoday = Convert.ToDouble(root.result.DASHUSD.l[0]);
                obj.low24hr = Convert.ToDouble(root.result.DASHUSD.l[1]);
                obj.hightoday = Convert.ToDouble(root.result.DASHUSD.h[0]);
                obj.high24hr = Convert.ToDouble(root.result.DASHUSD.h[1]);
                obj.openprice = Convert.ToDouble(root.result.DASHUSD.o);

                SaveTickerRecord(obj);

                idx++;
            }

            ///save Z Cash
            if (root.result.XZECZUSD != null)
            {
                Console.WriteLine("XZECUSD Ticker: Ask: $" + root.result.XZECZUSD.a[0]);

                obj.assetpair = "XZECUSD";
                obj.ask = Convert.ToDouble(root.result.XZECZUSD.a[0]);
                obj.askwlvolume = Convert.ToDouble(root.result.XZECZUSD.a[1]);
                obj.asklotvolume = Convert.ToDouble(root.result.XZECZUSD.a[2]);
                obj.bid = Convert.ToDouble(root.result.XZECZUSD.b[0]);
                obj.bidwlvolume = Convert.ToDouble(root.result.XZECZUSD.b[1]);
                obj.bidlotvolume = Convert.ToDouble(root.result.XZECZUSD.b[2]);
                obj.volumetoday = Convert.ToDouble(root.result.XZECZUSD.v[0]);
                obj.volume24hr = Convert.ToDouble(root.result.XZECZUSD.v[1]);
                obj.volweightedavgpricetoday = Convert.ToDouble(root.result.XZECZUSD.p[0]);
                obj.volweightedavgprice24hr = Convert.ToDouble(root.result.XZECZUSD.p[1]);
                obj.numberoftradestoday = Convert.ToDouble(root.result.XZECZUSD.t[0]);
                obj.Numberoftrades24hrs = Convert.ToDouble(root.result.XZECZUSD.t[1]);
                obj.lowtoday = Convert.ToDouble(root.result.XZECZUSD.l[0]);
                obj.low24hr = Convert.ToDouble(root.result.XZECZUSD.l[1]);
                obj.hightoday = Convert.ToDouble(root.result.XZECZUSD.h[0]);
                obj.high24hr = Convert.ToDouble(root.result.XZECZUSD.h[1]);
                obj.openprice = Convert.ToDouble(root.result.XZECZUSD.o);

                SaveTickerRecord(obj);

                idx++;
            }

            ///save augur
            if (root.result.XREPZUSD != null)
            {
                Console.WriteLine("XREPZUSD Ticker: Ask: $" + root.result.XREPZUSD.a[0]);

                obj.assetpair = "XREPZUSD";
                obj.ask = Convert.ToDouble(root.result.XREPZUSD.a[0]);
                obj.askwlvolume = Convert.ToDouble(root.result.XREPZUSD.a[1]);
                obj.asklotvolume = Convert.ToDouble(root.result.XREPZUSD.a[2]);
                obj.bid = Convert.ToDouble(root.result.XREPZUSD.b[0]);
                obj.bidwlvolume = Convert.ToDouble(root.result.XREPZUSD.b[1]);
                obj.bidlotvolume = Convert.ToDouble(root.result.XREPZUSD.b[2]);
                obj.volumetoday = Convert.ToDouble(root.result.XREPZUSD.v[0]);
                obj.volume24hr = Convert.ToDouble(root.result.XREPZUSD.v[1]);
                obj.volweightedavgpricetoday = Convert.ToDouble(root.result.XREPZUSD.p[0]);
                obj.volweightedavgprice24hr = Convert.ToDouble(root.result.XREPZUSD.p[1]);
                obj.numberoftradestoday = Convert.ToDouble(root.result.XREPZUSD.t[0]);
                obj.Numberoftrades24hrs = Convert.ToDouble(root.result.XREPZUSD.t[1]);
                obj.lowtoday = Convert.ToDouble(root.result.XREPZUSD.l[0]);
                obj.low24hr = Convert.ToDouble(root.result.XREPZUSD.l[1]);
                obj.hightoday = Convert.ToDouble(root.result.XREPZUSD.h[0]);
                obj.high24hr = Convert.ToDouble(root.result.XREPZUSD.h[1]);
                obj.openprice = Convert.ToDouble(root.result.XREPZUSD.o);

                SaveTickerRecord(obj);

                idx++;
            }
            return idx;
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

            sp[17] = new SqlParameter("@openprice", System.Data.SqlDbType.Decimal);
            sp[17].Value = o.openprice;

            try
            {
                SqlHelper.ExecuteNonQuery(Config.DBConn, System.Data.CommandType.StoredProcedure, "TICKER_INSERT", sp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Logging.LogDB(ex.ToString());
                Console.ReadKey();
            }
        }
    }
}