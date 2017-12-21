using WebApplication9.Interfaces;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System;
using System.Web;

namespace WebApplication9.Classes
{
    public class ExchangeRate : IExchangeRate
    {
        public string currentCurrency { get; set; }
        public string newCurrency { get; set; }

        public ExchangeRate(string currentCurrency, string newCurrency)
        {
            this.currentCurrency = currentCurrency;
            this.newCurrency = newCurrency;
        }

        public string calculate()
        {
            try
            {
                // Create the web request and appending to the URL the currenct currency and new currency symbols
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.fixer.io/latest?base=" + currentCurrency + "&symbols=" + newCurrency);

                //  Getting the response from the API
                var response = (HttpWebResponse)request.GetResponse();

                // Reading the response through the StreamReader
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                // Parsing the whole JSON String obtained from the API and getting the 'rates' value
                string data = JObject.Parse(responseString)["rates"].ToString();

                //  Parsing the JSON String to get the value of the exchange rate which is appended with the new currency.  Returning the value.
                return JObject.Parse(data)[newCurrency].ToString();
            }
            catch (HttpException e)
            {
                // Throw an HttpException
                throw new HttpException(e.Message);
            }
     
        }
    }
}