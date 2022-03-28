using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using CurrencyMonitor.App.Annotations;
using CurrencyMonitor.Domain;

namespace CurrencyMonitor.App.ViewModels
{
    public class CurrencySelectorViewModel : ICurrencySelectorViewModel, INotifyPropertyChanged {
        private ObservableCollection<ICurrency> _currencyList;
        private ICurrency _fromCurrency; 
        private ICurrency _toCurrency; 
        

        public CurrencySelectorViewModel(IEnumerable<ICurrency> list) {
            _currencyList = new ObservableCollection<ICurrency>(list);
        }

        [NotNull]
        public ObservableCollection<ICurrency> CurrencyList {
            get => _currencyList;
            set {
                if (_currencyList == value) return;

                _currencyList = value;
                OnPropertyChanged(nameof(CurrencyList));
            }
        }

        [CanBeNull]
        public ICurrency FromCurrency {
            get => _fromCurrency;
            set {
                if (_fromCurrency == value) return;

                _fromCurrency = value;
                OnPropertyChanged(nameof(FromCurrency));
            }
        }

        [CanBeNull]
        public ICurrency ToCurrency {
            get => _toCurrency;
            set  {
                if (_toCurrency == value) return;

                _toCurrency = value;
                OnPropertyChanged(nameof(ToCurrency));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
