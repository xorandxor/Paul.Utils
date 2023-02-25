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

    }
}
