using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyMonitor.Domain.Entities;

namespace CurrencyMonitor.Logic.Services.Providers {
    public interface ICurrencyDataProvider {
        Task<List<ICurrency>> ProvideCurrencyListAsync();
    }
}