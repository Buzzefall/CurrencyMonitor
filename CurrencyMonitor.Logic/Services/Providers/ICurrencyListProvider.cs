using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyMonitor.Domain.Entities;

namespace CurrencyMonitor.Logic.Services.Providers {
    public interface ICurrencyListProvider {
        Task<List<ICurrency>> GetCurrencyListAsync();
    }
}