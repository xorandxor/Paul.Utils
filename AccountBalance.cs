using System;
using System.Globalization;

namespace Paul.Utils
{
   
 
    public partial class AccountBalance
    {

        /// <summary>
        /// Deserialize json data into a class object that can be access and passed around
        /// </summary>
        /// <param name="myJsonResponse">Ugly json data</param>
        /// <returns>pretty c# class</returns>
        public static Root GetClassFromJson(string myJsonResponse)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(myJsonResponse);
        }

        /// <summary>
        /// This method displays the current number of coins in all the wallets as well as the value of each 
        /// coin and also the total value of the portfolio... additionally, this method saves the account 
        /// balance to the database
        /// </summary>
        public static void GetAccountBalance()
        {
            string privateEndpoint = "Balance";
            string privateInputParameters = "";
            string apiPrivateKey = Config.ApiPrivateKey;
            string apiPublicKey = Config.ApiPublicKey;

            string privateResponse = API.QueryPrivateEndpoint(privateEndpoint, privateInputParameters, apiPublicKey, apiPrivateKey);
            Root acctbal = GetClassFromJson(privateResponse);

            BalanceObject bal = new BalanceObject();
            if (acctbal.result.ZUSD != null)
            {
                bal.USD = Convert.ToDouble(acctbal.result.ZUSD.ToString());
            }
            if (acctbal.result.XXBT != null)
            {
                bal.BTC = Convert.ToDouble(acctbal.result.XXBT.ToString());
            }
            if (acctbal.result.XLTC != null)
            {
                bal.LTC = Convert.ToDouble(acctbal.result.XLTC.ToString());
            }
            if (acctbal.result.XETH != null)
            {
                bal.ETH = Convert.ToDouble(acctbal.result.XETH.ToString());
            }
            if (acctbal.result.XXDG != null)
            {
                bal.DGE = Convert.ToDouble(acctbal.result.XXDG.ToString());
            }
            if (acctbal.result.XXMR != null)
            {
                bal.XMR = Convert.ToDouble(acctbal.result.XXMR.ToString());
            }
            if (acctbal.result.DASH != null)
            {
                bal.DASH = Convert.ToDouble(acctbal.result.DASH.ToString());
            }
            if (acctbal.result.XZEC != null)
            {
                bal.ZEC = Convert.ToDouble(acctbal.result.XZEC.ToString());
            }
            if (acctbal.result.XREP != null)
            {
                bal.REP = Convert.ToDouble(acctbal.result.XREP.ToString());
            }

            Logging.LogDB("Account Balance");
            double portfolio_value = 0;

            portfolio_value += KrakenDB.GetAssetValueUSD("XXBTZUSD") * bal.BTC;
            portfolio_value += KrakenDB.GetAssetValueUSD("XLTCUSD") * bal.LTC;
            portfolio_value += KrakenDB.GetAssetValueUSD("XETHZUSD") * bal.ETH;
            portfolio_value += KrakenDB.GetAssetValueUSD("XDGUSD") * bal.DGE;
            portfolio_value += KrakenDB.GetAssetValueUSD("XMRUSD") * bal.XMR;
            portfolio_value += KrakenDB.GetAssetValueUSD("DASHUSD") * bal.DASH;
            portfolio_value += KrakenDB.GetAssetValueUSD("XZECUSD") * bal.ZEC;
            portfolio_value += KrakenDB.GetAssetValueUSD("XREPZUSD") * bal.REP;

            //whoops dont forget dollars
            portfolio_value += bal.USD;

            Console.WriteLine("****************************************** ");
            Console.WriteLine("* ACCOUNT BALANCE                      ** ");
            Console.WriteLine("****************************************** ");
            Console.WriteLine("*      usd: " + bal.USD);
            Console.WriteLine("*  bitcoin: " + bal.BTC + " [$" + (KrakenDB.GetAssetValueUSD("XXBTZUSD") * bal.BTC) + "]");
            Console.WriteLine("* litecoin: " + bal.LTC + " [$" + KrakenDB.GetAssetValueUSD("XLTCUSD") * bal.LTC + "]");
            Console.WriteLine("* ethereum: " + bal.ETH + " [$" + KrakenDB.GetAssetValueUSD("XETHZUSD") * bal.ETH + "]");
            Console.WriteLine("* dogecoin: " + bal.DGE + " [$" + KrakenDB.GetAssetValueUSD("XDGUSD") * bal.DGE + "]");
            Console.WriteLine("*   monero: " + bal.XMR + " [$" + KrakenDB.GetAssetValueUSD("XMRUSD") * bal.XMR + "]");
            Console.WriteLine("*     dash: " + bal.DASH + " [$" + KrakenDB.GetAssetValueUSD("DASHUSD") * bal.DASH + "]");
            Console.WriteLine("*   z-cash: " + bal.ZEC + " [$" + KrakenDB.GetAssetValueUSD("XZECUSD") * bal.ZEC + "]");
            Console.WriteLine("*    augur: " + bal.REP + " [$" + KrakenDB.GetAssetValueUSD("XREPZUSD") * bal.REP + "]");
            Console.WriteLine("****************************************** ");
            Console.WriteLine("* Total Portfolio Value: " + portfolio_value.ToString("C", CultureInfo.CurrentCulture));
            Console.WriteLine("****************************************** ");

            // delete all entries from the accountbalance table
            KrakenDB.AccountBalanceClear();

            // save this set of balances to the table
            KrakenDB.AccountBalanceInsert(bal);
        }

        


    }
}