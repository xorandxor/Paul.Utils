using System;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Paul.Utils
{
    /// <summary>
    /// This class handles the nuts and bolts of hitting the Kraken API. Methods include
    /// QueryPrivateEndpoint (trade, order, balance, wallet), QueryPublicEndpoint (ohlc, ticker,
    /// system status, recent trades, order book) as well as worker methods to create the auth
    /// signature (256/512 hash)
    /// </summary>
    public class API
    {
        #region Public Methods

        /// <summary>
        /// call this every time you hit the API 
        ///  or your gonna have problems
        ///  https://docs.kraken.com/rest/#section/Rate-Limits
        ///  generally 2 sec is good
        ///  todo: move this to the actual api call and use the app config to configure the timeout value
        /// </summary>
        /// <param name="millisec"></param>
        public static void Cooldown(int millisec)
        {
            System.Threading.Thread.Sleep(millisec);
        }

        public static string GetOHLCJsonData(string pairname, int intervl)
        {
            string publicEndpoint = "OHLC";
            string publicInputParameters = "pair=" + pairname + "&interval=15";
            string publicResponse = API.QueryPublicEndpoint(publicEndpoint, publicInputParameters);

            return publicResponse;
        }

        public static string GetTickerJsonData(string pairname)
        {
            string publicEndpoint = "Ticker";
            string publicInputParameters = "pair=" + pairname;
            string publicResponse = API.QueryPublicEndpoint(publicEndpoint, publicInputParameters);
            return publicResponse;
        }

        //https://api.kraken.com/0/public/Time

        public static string GetServerTimeJsonData(string pairname)
        {
            string publicEndpoint = "Time";
            string publicInputParameters = "";
            string publicResponse = API.QueryPublicEndpoint(publicEndpoint, publicInputParameters);
            return publicResponse;
        }

        //https://api.kraken.com/0/public/SystemStatus

        public static string GetSystemStatusJsonData(string pairname)
        {
            string publicEndpoint = "SystemStatus";
            string publicInputParameters = "";
            string publicResponse = API.QueryPublicEndpoint(publicEndpoint, publicInputParameters);
            return publicResponse;
        }

        //https://api.kraken.com/0/public/Trades?pair=XBTUSD

        public static string GetTradesJsonData(string pairname)
        {
            string publicEndpoint = "Trades";
            string publicInputParameters = "";
            string publicResponse = API.QueryPublicEndpoint(publicEndpoint, publicInputParameters);
            return publicResponse;
        }

        /// <summary> redid this one to do it synchronously since everything runs step by step anyway </summary>
        /// <param name="endpointName">AddOrder</param>
        /// <param name="inputParameters">pair=x&price=y</param>
        /// <param name="apiPublicKey">api key</param>
        /// <param name="apiPrivateKey">api key</param>
        /// <returns></returns>
        public static string QueryPrivateEndpoint(string endpointName,
                                                       string inputParameters,
                                                       string apiPublicKey,
                                                       string apiPrivateKey)
        {
            string baseDomain = "https://api.kraken.com";
            string privatePath = "/0/private/";

            string apiEndpointFullURL = baseDomain + privatePath + endpointName;
            string nonce = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            if (string.IsNullOrWhiteSpace(inputParameters) == false)
            {
                inputParameters = "&" + inputParameters;
            }
            string apiPostBodyData = "nonce=" + nonce + inputParameters;
            string signature = CreateAuthenticationSignature(apiPrivateKey,
                                                             privatePath,
                                                             endpointName,
                                                             nonce,
                                                             inputParameters);
            string jsonData = "";

            try
            {
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    client.Headers.Clear();
                    client.Headers.Add("API-Key", apiPublicKey);
                    client.Headers.Add("API-Sign", signature);
                    client.Headers.Add("User-Agent", "KrakenDotNet Client");
                    StringContent data = new StringContent(apiPostBodyData, Encoding.UTF8, "application/x-www-form-urlencoded");
                    string stringToUpload = data.ToString();
                    string response = client.UploadString(apiEndpointFullURL, apiPostBodyData);
                    jsonData = response;
                }
            }
            catch (Exception ex)
            {
                if (Config.OPTION_LOG_API_ERRORS_TO_CONSOLE)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return jsonData;
        }

        /// <summary> 
        /// update: works like a dream
        /// </summary> <param
        /// name="endpointName">AddOrder</param> <param
        /// name="inputParameters">pair=x&price=y</param> <param name="apiPublicKey">Api Public
        /// Key</param> <param name="apiPrivateKey">Api Private Key</param>
        /// <returns>JSON String</returns>
        public static string QueryPublicEndpoint(string endpointName, string inputParameters)
        {
            string jsonData = "";
            string baseDomain = "https://api.kraken.com";
            string publicPath = "/0/public/";
            string apiEndpointFullURL = baseDomain + publicPath + endpointName + "?" + inputParameters;
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                try
                {
                    jsonData = client.DownloadString(apiEndpointFullURL);
                }
                catch (Exception e)
                {
                    if (Config.OPTION_LOG_API_ERRORS_TO_CONSOLE)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                return jsonData;
            }
        }
        #endregion Public Methods

        #region Private Methods

        private static byte[] ComputeSha256Hash(string nonce, string inputParams)
        {
            byte[] sha256Hash;
            string sha256HashData = nonce.ToString() + "nonce=" + nonce.ToString() + inputParams;
            using (var sha = SHA256.Create())
            {
                sha256Hash = sha.ComputeHash(Encoding.UTF8.GetBytes(sha256HashData));
            }
            return sha256Hash;
        }

        private static byte[] ComputeSha512Hash(string apiPrivateKey,
                                                                byte[] sha256Hash,
                                                                string apiPath,
                                                                string endpointName,
                                                                string nonce,
                                                                string inputParams)
        {
            string apiEndpointPath = apiPath + endpointName;
            byte[] apiEndpointPathBytes = Encoding.UTF8.GetBytes(apiEndpointPath);
            byte[] sha512HashData = apiEndpointPathBytes.Concat(sha256Hash).ToArray();
            HMACSHA512 encryptor = new HMACSHA512(Convert.FromBase64String(apiPrivateKey));
            byte[] sha512Hash = encryptor.ComputeHash(sha512HashData);
            return sha512Hash;
        }

        private static string CreateAuthenticationSignature(string apiPrivateKey,
                                                           string apiPath,
                                                           string endpointName,
                                                           string nonce,
                                                           string inputParams)
        {
            byte[] sha256Hash = ComputeSha256Hash(nonce, inputParams);
            byte[] sha512Hash = ComputeSha512Hash(apiPrivateKey, sha256Hash, apiPath, endpointName, nonce, inputParams);
            string signatureString = Convert.ToBase64String(sha512Hash);
            return signatureString;
        }

        /// <summary>
        /// original Async method by kraken deprecated since i know nothing about asynchronous stuff
        /// </summary>
        /// <param name="endpointName"> </param>
        /// <param name="inputParameters"> </param>
        /// <param name="apiPublicKey"> </param>
        /// <param name="apiPrivateKey"> </param>
        /// <returns> </returns>
        private static async Task<string> QueryPrivateEndpointAsync(string endpointName,
                                                               string inputParameters,
                                                               string apiPublicKey,
                                                               string apiPrivateKey)
        {
            string baseDomain = "https://api.kraken.com";
            string privatePath = "/0/private/";

            string apiEndpointFullURL = baseDomain + privatePath + endpointName;
            string nonce = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

            if (string.IsNullOrWhiteSpace(inputParameters) == false)
            {
                inputParameters = "&" + inputParameters;
            }
            string apiPostBodyData = "nonce=" + nonce + inputParameters;
            string signature = CreateAuthenticationSignature(apiPrivateKey,
                                                             privatePath,
                                                             endpointName,
                                                             nonce,
                                                             inputParameters);
            string jsonData;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("API-Key", apiPublicKey);
                client.DefaultRequestHeaders.Add("API-Sign", signature);
                client.DefaultRequestHeaders.Add("User-Agent", "KrakenDotNet Client");
                StringContent data = new StringContent(apiPostBodyData, Encoding.UTF8, "application/x-www-form-urlencoded");
                HttpResponseMessage response = await client.PostAsync(apiEndpointFullURL, data);
                jsonData = response.Content.ReadAsStringAsync().Result;
            }

            return jsonData;
        }

        /// <summary>
        /// original async method by kraken deprecated since i know nothing about asynchronous stuff
        /// </summary>
        /// <param name="endpointName"> </param>
        /// <param name="inputParameters"> </param>
        /// <returns> </returns>
        private static async Task<string> QueryPublicEndpointAsync(string endpointName, string inputParameters)
        {
            string jsonData;
            string baseDomain = "https://api.kraken.com";
            string publicPath = "/0/public/";
            string apiEndpointFullURL = baseDomain + publicPath + endpointName + "?" + inputParameters;
            using (HttpClient client = new HttpClient())
            {
                jsonData = await client.GetStringAsync(apiEndpointFullURL);
            }
            return jsonData;
        }

        #endregion Private Methods
    }
}