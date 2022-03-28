using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyMonitor.Logic.Interfaces;

namespace CurrencyMonitor.App.ViewModels
{
    public class CurrencySelectorItemViewModel : ICurrencySelectorItemViewModel {

        public ICurrency Item { get; set; }
    }
}
