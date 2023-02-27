using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paul.Utils
{
 /// <summary>
 /// This class is a stopgap measure until I have the Neural Network price prediction working in python
 /// it will save to the database the same way XXBTZUSD $22,400 etc
 /// then if certain criteria are met, the bot remains in the crypto, sells to dollars or converts to another crypto
 /// depending on what crypto is going to rise the most in the next 15 minutes.
 /// if it covers the cost of the trade plus some then that just makes economic sense
 /// plus you can jump in fast on big swings
 /// </summary>
    public class PricePrediction
    {

        private enum PricePressure
        {
            Same = 0,
            Increasing = 1,
            Decreasing = 2
        }



        private static void Predict_Bitcoin()
        {
            PredictNextPrice("XXBTZUSD", 15);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="cryptoname"></param>
        /// <param name="interval"></param>
        public static void PredictNextPrice(string cryptoname, int interval)
        {
            double multiplier = 1;

            string sql = "SELECT TOP (10) * FROM [trade].[dbo].[OHLC] where assetpair = '" + cryptoname + "' order by id desc";
            DataSet ds = SqlHelper.ExecuteDataset(Config.DBConn, CommandType.Text, sql);
            DataTable dt = ds.Tables[0];

            /*
             * now we should have a table of ohlc data with the newest records first
             * looking like 
             * [id][timestampdb][timestampjson][open][high][low][close][volweightedavgprice][volume][count]
             */

            PricePressure pricepressure1 = PricePressure.Same;
            PricePressure pricepressure2 = PricePressure.Same;

            OHLC_GENERIC varOHLC = new OHLC_GENERIC();
            string ohlcjson = API.GetOHLCJsonData(cryptoname, interval);
            OHLC_GENERIC.Root objOHLC = OHLC_GENERIC.GetClassFromJson(ohlcjson);
            int index = objOHLC.result.XXBTZUSD.Count;

            //gen0 price close (the most recent ohlc data point) 
            // to get more recent we use Ticker class
            double btc_gen0_close = System.Convert.ToDouble(dt.Rows[0]["closeprice"].ToString());

            //gen0 price open
            double btc_gen0_open = System.Convert.ToDouble(dt.Rows[0]["openprice"].ToString());

            //gen1 price one interval ago (1m, 5m, 15m, etc)
            double btc_gen1_open = System.Convert.ToDouble(dt.Rows[1]["openprice"].ToString());

            //gen2 price two intervals ago
            double btc_gen2_open = System.Convert.ToDouble(dt.Rows[2]["openprice"].ToString());

            //price three intervals ago
            double btc_gen3_open = System.Convert.ToDouble(dt.Rows[3]["openprice"].ToString());

            //price four intervals ago
            double btc_gen4_open = System.Convert.ToDouble(dt.Rows[4]["openprice"].ToString());

            double gen0_gen1_rate_of_change = System.Convert.ToDouble((btc_gen0_open - btc_gen1_open) / btc_gen0_open);
            double gen1_gen2_rate_of_change = System.Convert.ToDouble((btc_gen1_open - btc_gen2_open) / btc_gen1_open);
            double gen2_gen3_rate_of_change = System.Convert.ToDouble((btc_gen2_open - btc_gen3_open) / btc_gen2_open);
            double gen3_gen4_rate_of_change = System.Convert.ToDouble((btc_gen3_open - btc_gen4_open) / btc_gen3_open);

            Console.WriteLine("Gen 0->1 rate of change: " + gen0_gen1_rate_of_change * 100 + "%");

            if (gen0_gen1_rate_of_change > gen1_gen2_rate_of_change)
            {
                pricepressure1 = PricePressure.Increasing;
                multiplier += 0.15;

                Console.WriteLine("Price pressure 1 is increasing");
            }
            else
            {
                pricepressure1 = PricePressure.Decreasing;
                multiplier -= 0.15;
                Console.WriteLine("Price pressure 1 is decreasing");
            }

            Console.WriteLine("Gen 1->2 rate of change: " + gen1_gen2_rate_of_change * 100 + "%");

            if (gen1_gen2_rate_of_change > gen2_gen3_rate_of_change)
            {
                pricepressure2 = PricePressure.Increasing;
                multiplier += 0.1;
                Console.WriteLine("Price pressure 2 is increasing");
            }
            else
            {
                pricepressure2 = PricePressure.Decreasing;
                multiplier -= 0.1;
                Console.WriteLine("Price pressure 2 is decreasing");
            }

            Console.WriteLine("Gen 2->3 rate of change: " + gen2_gen3_rate_of_change * 100 + "%");

            Console.WriteLine("Gen 3->4 rate of change: " + gen3_gen4_rate_of_change * 100 + "%");

            if (gen0_gen1_rate_of_change > gen1_gen2_rate_of_change)
            {
                Console.WriteLine("gen0 Price strength is increasing");
            }
            else
            {
                Console.WriteLine("gen0 Price strength is decreasing");
            }

            if (gen1_gen2_rate_of_change > gen2_gen3_rate_of_change)
            {
                Console.WriteLine("gen1 Price strength is increasing");
            }
            else
            {
                Console.WriteLine("gen1 Price strength is decreasing");
            }

            if (gen2_gen3_rate_of_change > gen3_gen4_rate_of_change)
            {
                Console.WriteLine("gen2 Price strength is increasing");
            }
            else
            {
                Console.WriteLine("gen2 Price strength is decreasing");
            }

            double price = btc_gen0_open;// + (btc_gen0_open - btc_gen1_open);

            price = price + (btc_gen0_open - btc_gen1_open);

            if (pricepressure1 == PricePressure.Increasing)
            {
                price = price + (btc_gen0_open * gen0_gen1_rate_of_change);
            }
            else
            {
                price = price - (btc_gen0_open * gen0_gen1_rate_of_change);
            }

            Console.WriteLine("Price prediction: " + price.ToString("C", CultureInfo.InvariantCulture));

            try
            {
                SqlParameter[] p = new SqlParameter[4];// (SqlParameter"message", SqlDbType.NVarChar, 255);
                p[0] = new SqlParameter();
                p[0].ParameterName = "@name";
                p[0].DbType = DbType.String;
                p[0].Value = "XXBTZUSD";
                p[1] = new SqlParameter
                {
                    ParameterName = "@currentval",
                    DbType = DbType.Double,
                    Value = btc_gen0_open
                };
                p[2] = new SqlParameter
                {
                    ParameterName = "@predictedval",
                    DbType = DbType.Double,
                    Value = price
                };
                p[3] = new SqlParameter
                {
                    //todo: fix this shit
                    ParameterName = "@percentchange",
                    DbType = DbType.Double,
                    Value = btc_gen0_open
                };

                SqlHelper.ExecuteNonQuery(Config.DBConn, CommandType.StoredProcedure, "Predictions_INSERT", p);
            }
            catch (Exception ex) // if sql server is down then write an error to the system log
            {
                Console.WriteLine(ex.ToString());
            }

            //price sixteen intervals ago
            double btc4hr_open = System.Convert.ToDouble(objOHLC.result.XXBTZUSD[index - 16][1]);
            double btc12hr_open = System.Convert.ToDouble(objOHLC.result.XXBTZUSD[index - 48][1]);
            double btc24hr_open = System.Convert.ToDouble(objOHLC.result.XXBTZUSD[index - 96][1]);

            //ToString("C", CultureInfo.CurrentCulture)
            System.Console.WriteLine("         btc open now: " + btc_gen0_open.ToString("C", CultureInfo.CurrentCulture));
            System.Console.WriteLine("  btc open 15 min ago: " + btc_gen1_open.ToString("C", CultureInfo.CurrentCulture));
            System.Console.WriteLine("  btc open 30 min ago: " + btc_gen2_open.ToString("C", CultureInfo.CurrentCulture));
            System.Console.WriteLine("  btc open 45 min ago: " + btc_gen3_open.ToString("C", CultureInfo.CurrentCulture));
            System.Console.WriteLine("  btc open 60 min ago: " + btc_gen4_open.ToString("C", CultureInfo.CurrentCulture));
            System.Console.WriteLine("  btc open  4 hrs ago: " + btc4hr_open.ToString("C", CultureInfo.CurrentCulture));
            System.Console.WriteLine("  btc open 12 hrs ago: " + btc12hr_open.ToString("C", CultureInfo.CurrentCulture));
            System.Console.WriteLine("  btc open 24 hrs ago: " + btc24hr_open.ToString("C", CultureInfo.CurrentCulture));
        }

    }
}
