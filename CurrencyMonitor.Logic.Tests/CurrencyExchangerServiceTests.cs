
using System;
using System.Linq;
using System.Threading.Tasks;
using CurrencyMonitor.Logic.Services;
using CurrencyMonitor.Logic.Services.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CurrencyMonitor.Logic.Tests
{
    [TestClass]
    public class CurrencyExchangerServiceTests
    {
        [TestMethod]
        public async Task CurrencyDataProvider_Currency_List_Contains_Essential_Currencies() {

            var provider = new CurrencyDataProvider();
            var exchangerService = new CurrencyExchangerService(provider);

            var currencyList = await exchangerService.GetCurrencyListAsync();
            Assert.IsTrue(currencyList.Any(c => c.CharCode.Equals("RUB", StringComparison.OrdinalIgnoreCase) || 
                                                c.CharCode.Equals("USD", StringComparison.OrdinalIgnoreCase) ||
                                                c.CharCode.Equals("EUR", StringComparison.OrdinalIgnoreCase)));
        }
    }
}
