using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CurrencyMonitor.App.Annotations;
using CurrencyMonitor.Logic.Interfaces;

namespace CurrencyMonitor.App.ViewModels
{
    public class CurrencyInputViewModel : ICurrencyInputViewModel, INotifyPropertyChanged
    {
        [NotNull] 
        private ObservableCollection<ICurrency> _currencyList;
        private decimal _fromCurrencyValue;
        private decimal _toCurrencyValue;

        public CurrencyInputViewModel(IEnumerable<ICurrency> currencyList) {
            _currencyList = new ObservableCollection<ICurrency>(currencyList);
        }

        public ObservableCollection<ICurrency> CurrencyList {
            get => _currencyList;
            set {
                _currencyList = value;
                OnPropertyChanged(nameof(CurrencyList));
            }
        }

        public decimal FromCurrencyValue {
            get => _fromCurrencyValue;
            set {
                _fromCurrencyValue = value;
                OnPropertyChanged(nameof(FromCurrencyValue));
            }
        }

        public decimal ToCurrencyValue {
            get => _toCurrencyValue;
            set {
                _toCurrencyValue = value;
                OnPropertyChanged(nameof(ToCurrencyValue));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
