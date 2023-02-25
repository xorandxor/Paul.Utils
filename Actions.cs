using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paul.Utils
{
    public static class Actions
    {

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

    }
}
