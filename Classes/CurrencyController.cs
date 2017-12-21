using WebApplication9.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System;
using System.Web;

namespace WebApplication9.Classes
{
    public class CurrencyController : ICurrency
    {
        public string getCurrencies()
        {
            try
            {
                
                List<string> currencies = new List<string>();

                // Create the web request
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.fixer.io/latest");

                //  Getting the response from the API
                var response = (HttpWebResponse)request.GetResponse();

                // Reading the response through the StreamReader
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                //  Adding the base currency to the list of currencies as it is not included in the rates returned.
                currencies.Add(JObject.Parse(responseString)["base"].ToString());

                //  Getting the portion of the JSON string which contains the rates
                var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(JObject.Parse(responseString)["rates"].ToString());

                //  For each rate, add the currency symbol to the list
                foreach (KeyValuePair<string, string> currency in values)
                {
                    currencies.Add(currency.Key);

                }
                //  Return the list of currencies in JSON format
                return JsonConvert.SerializeObject(currencies);
            }
            catch (HttpException e)
            {
                // Throw an HttpException
                throw new HttpException(e.Message);
            }

        }
    }
}