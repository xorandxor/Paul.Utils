using System;

namespace Paul.Utils
{
    /// <summary>
    /// This class does all the dangerous stuff. Buying selling, maybe margin trading/staking at some point.
    /// </summary>
    public static class Actions
    {
        #region actions

        #region ALL CRYPTOS ACTIONS
        public static void CryptoBloodbath()
        {
            Console.WriteLine("CryptoBloodbath() called, selling all crypto to dollars..");

            SellAllBTCMarket();
            API.APICooldown(2000);

            SellAllLTCMarket();
            API.APICooldown(2000);

            SellAllETHMarket();
            API.APICooldown(2000);

            SellAllDGEMarket();
            API.APICooldown(2000);

            SellAllDASHMarket();
            API.APICooldown(2000);

            SellAllXMRmarket();
            API.APICooldown(2000);

            SellAllREPMarket();
            API.APICooldown(2000);

            SellAllZECMarket();
            API.APICooldown(2000);
        }

        #endregion

        #region BTC Actions

        public static void SellAllBTCMarket()
        {
            double btcbal = KrakenDB.Get_BTC_Balance();

            if (btcbal > .001)
            {
                Order o = new Order();
                o.Leverage = KrakenLeverageLevel.None;
                o.OrderType = KrakenOrderType.Market;
                o.Pair = "XBTUSD";
                o.Type = BuyOrSellType.Sell;
                o.Volume = Convert.ToString(btcbal);
                string x = o.SubmitOrder();
                string y = "";
            }
            else
            {
                Console.WriteLine("SellAllBTCMarket() called but btc balance is " + btcbal + "... Aborting.");
            }
        }

        public static void BuyMaxBTC()
        {
            double usdbal = KrakenDB.Get_USD_Balance();

            // fudge factor when submitting market orders
            usdbal *= .95;
            Order o = new Order();
            o.Leverage = KrakenLeverageLevel.None;
            o.OrderType = KrakenOrderType.Market;
            o.Pair = "XBTUSD";
            o.Type = BuyOrSellType.Buy;
            o.Price = Convert.ToString(KrakenDB.Get_BTC_Price());
            o.Volume = Convert.ToString(usdbal * Convert.ToDouble(o.Price));
            string x = o.SubmitOrder();
        }

        #endregion BTC Actions

        #region LTC Actions

        public static void SellAllLTCMarket()
        {
            double balance = KrakenDB.Get_LTC_Balance();

            if (balance > .001)
            {
                Order o = new Order();
                o.Leverage = KrakenLeverageLevel.None;
                o.OrderType = KrakenOrderType.Market;
                o.Pair = "LTCUSD";
                o.Type = BuyOrSellType.Sell;
                o.Volume = Convert.ToString(balance);
                string x = o.SubmitOrder();
                string y = "";
            }
            else
            {
                Console.WriteLine("SellAllLTCMarket() called but LTC balance is " + balance + "... Aborting.");
            }
        }

        public static void BuyMaxLTC()
        {
            double usdbal = KrakenDB.Get_USD_Balance();

            // fudge factor when submitting market orders
            usdbal *= .95;
            Order o = new Order();
            o.Leverage = KrakenLeverageLevel.None;
            o.OrderType = KrakenOrderType.Market;
            o.Pair = "LTCUSD";
            o.Type = BuyOrSellType.Buy;
            o.Price = Convert.ToString(KrakenDB.Get_LTC_Price());
            o.Volume = Convert.ToString(usdbal * Convert.ToDouble(o.Price));
            string x = o.SubmitOrder();
        }

        #endregion LTC Actions

        #region ETH ACTIONS

        public static void SellAllETHMarket()
        {
            double balance = KrakenDB.Get_ETH_Balance();

            if (balance > .001)
            {
                Order o = new Order();
                o.Leverage = KrakenLeverageLevel.None;
                o.OrderType = KrakenOrderType.Market;
                o.Pair = "ETHUSD";
                o.Type = BuyOrSellType.Sell;
                o.Volume = Convert.ToString(balance);
                string x = o.SubmitOrder();
                string y = "";
            }
            else
            {
                Console.WriteLine("SellAllETHMarket() called but eth balance is " + balance + "... Aborting.");
            }
        }
        public static void BuyMaxETH()
        {
            double usdbal = KrakenDB.Get_USD_Balance();

            // fudge factor when submitting market orders
            usdbal *= .95;
            Order o = new Order();
            o.Leverage = KrakenLeverageLevel.None;
            o.OrderType = KrakenOrderType.Market;
            o.Pair = "ETHUSD";
            o.Type = BuyOrSellType.Buy;
            o.Price = Convert.ToString(KrakenDB.Get_ETH_Price());
            o.Volume = Convert.ToString(usdbal * Convert.ToDouble(o.Price));
            string x = o.SubmitOrder();
        }

        #endregion

        #region DOGECOIN ACTIONS

        public static void SellAllDGEMarket()
        {
            double btcbal = KrakenDB.Get_DGE_Balance();

            if (btcbal > .001)
            {
                Order o = new Order();
                o.Leverage = KrakenLeverageLevel.None;
                o.OrderType = KrakenOrderType.Market;
                o.Pair = "XDGUSD";
                o.Type = BuyOrSellType.Sell;
                o.Volume = Convert.ToString(btcbal);
                string x = o.SubmitOrder();
                string y = "";
            }
            else
            {
                Console.WriteLine("SellAllDGEMarket() called but doge balance is " + btcbal + "... Aborting.");
            }
        }
        public static void BuyMaxDGE()
        {
            double usdbal = KrakenDB.Get_USD_Balance();

            // fudge factor when submitting market orders
            usdbal *= .95;
            Order o = new Order();
            o.Leverage = KrakenLeverageLevel.None;
            o.OrderType = KrakenOrderType.Market;
            o.Pair = "XDGUSD";
            o.Type = BuyOrSellType.Buy;
            o.Price = Convert.ToString(KrakenDB.Get_DGE_Price());
            o.Volume = Convert.ToString(usdbal * Convert.ToDouble(o.Price));
            string x = o.SubmitOrder();
        }

        #endregion

        #region DASH ACTIONS

        public static void SellAllDASHMarket()
        {
            double balance = KrakenDB.Get_DASH_Balance();

            if (balance > .001)
            {
                Order o = new Order();
                o.Leverage = KrakenLeverageLevel.None;
                o.OrderType = KrakenOrderType.Market;
                o.Pair = "DASHUSD";
                o.Type = BuyOrSellType.Sell;
                o.Volume = Convert.ToString(balance);
                string x = o.SubmitOrder();
                string y = "";
            }
            else
            {
                Console.WriteLine("SellAllDASHMarket() called but dash balance is " + balance + "... Aborting.");
            }
        }
        public static void BuyMaxDASH()
        {
            double usdbal = KrakenDB.Get_USD_Balance();

            // fudge factor when submitting market orders
            usdbal *= .95;
            Order o = new Order();
            o.Leverage = KrakenLeverageLevel.None;
            o.OrderType = KrakenOrderType.Market;
            o.Pair = "DASHUSD";
            o.Type = BuyOrSellType.Buy;
            o.Price = Convert.ToString(KrakenDB.Get_DASH_Price());
            o.Volume = Convert.ToString(usdbal * Convert.ToDouble(o.Price));
            string x = o.SubmitOrder();
        }

        #endregion

        #region XMR ACXTIONS

        public static void SellAllXMRmarket()
        {
            double balance = KrakenDB.Get_XMR_Balance();

            if (balance > .001)
            {
                Order o = new Order();
                o.Leverage = KrakenLeverageLevel.None;
                o.OrderType = KrakenOrderType.Market;
                o.Pair = "XMRUSD";
                o.Type = BuyOrSellType.Sell;
                o.Volume = Convert.ToString(balance);
                string x = o.SubmitOrder();
                string y = "";
            }
            else
            {
                Console.WriteLine("SellAllXMRMarket() called but xmr balance is " + balance + "... Aborting.");
            }
        }
        public static void BuyMaxXMR()
        {
            double usdbal = KrakenDB.Get_USD_Balance();

            // fudge factor when submitting market orders
            usdbal *= .95;
            Order o = new Order();
            o.Leverage = KrakenLeverageLevel.None;
            o.OrderType = KrakenOrderType.Market;
            o.Pair = "XMRUSD";
            o.Type = BuyOrSellType.Buy;
            o.Price = Convert.ToString(KrakenDB.Get_XMR_Price());
            o.Volume = Convert.ToString(usdbal * Convert.ToDouble(o.Price));
            string x = o.SubmitOrder();
        }

        #endregion

        #region ZEC ACTIONS

        public static void SellAllZECMarket()
        {
            double balance = KrakenDB.Get_ZEC_Balance();

            if (balance > .00001)
            {
                Order o = new Order();
                o.Leverage = KrakenLeverageLevel.None;
                o.OrderType = KrakenOrderType.Market;
                o.Pair = "ZECUSD";
                o.Type = BuyOrSellType.Sell;
                o.Volume = Convert.ToString(balance);
                string x = o.SubmitOrder();
                string y = "";
            }
            else
            {
                Console.WriteLine("SellAllZECMarket() called but zec balance is " + balance + "... Aborting.");
            }
        }
        public static void BuyMaxZEC()
        {
            double usdbal = KrakenDB.Get_USD_Balance();

            // fudge factor when submitting market orders
            usdbal *= .95;
            Order o = new Order();
            o.Leverage = KrakenLeverageLevel.None;
            o.OrderType = KrakenOrderType.Market;
            o.Pair = "ZECUSD";
            o.Type = BuyOrSellType.Buy;
            o.Price = Convert.ToString(KrakenDB.Get_ZEC_Price());
            o.Volume = Convert.ToString(usdbal * Convert.ToDouble(o.Price));
            string x = o.SubmitOrder();
        }

        #endregion

        #region REP ACTIONS
        public static void SellAllREPMarket()
        {
            double balance = KrakenDB.Get_REP_Balance();

            if (balance > .00001)
            {
                Order o = new Order();
                o.Leverage = KrakenLeverageLevel.None;
                o.OrderType = KrakenOrderType.Market;
                o.Pair = "REPUSD";
                o.Type = BuyOrSellType.Sell;
                o.Volume = Convert.ToString(balance);
                string x = o.SubmitOrder();
                string y = "";
            }
            else
            {
                Console.WriteLine("SellAllREPMarket() called but rep balance is " + balance + "... Aborting.");
            }
        }
        public static void BuyMaxREP()
        {
            double usdbal = KrakenDB.Get_USD_Balance();

            // fudge factor when submitting market orders
            usdbal *= .95;
            Order o = new Order();
            o.Leverage = KrakenLeverageLevel.None;
            o.OrderType = KrakenOrderType.Market;
            o.Pair = "REPUSD";
            o.Type = BuyOrSellType.Buy;
            o.Price = Convert.ToString(KrakenDB.Get_REP_Price());
            o.Volume = Convert.ToString(usdbal * Convert.ToDouble(o.Price));
            string x = o.SubmitOrder();
        }

        #endregion

#endregion

    }
}