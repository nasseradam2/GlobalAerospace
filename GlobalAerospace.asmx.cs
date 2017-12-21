using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebApplication9.Classes;

namespace WebApplication9
{
    /// <summary>
    /// Summary description for GlobalAerospace
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class GlobalAerospace : System.Web.Services.WebService
    {
        
        [WebMethod]
        public void GetCurrencies()
        {

            CurrencyController cc = new CurrencyController();

            Context.Response.Write(cc.getCurrencies());

        }

        [WebMethod]
        public void CalculateExchangeRate(string currentCurrency, string newCurrency)
        {
            ExchangeRate ex = new ExchangeRate(currentCurrency, newCurrency);
            Context.Response.Write(ex.calculate());
        }
    }
}
