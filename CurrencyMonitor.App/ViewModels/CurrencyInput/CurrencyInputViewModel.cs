using System.ComponentModel;
using System.Runtime.CompilerServices;
using CurrencyMonitor.App.Annotations;

namespace CurrencyMonitor.App.ViewModels
{
    public class CurrencyInputViewModel : ICurrencyInputViewModel, INotifyPropertyChanged
    {
        private double _fromCurrencyValue;
        private double _toCurrencyValue;

        public double FromCurrencyValue {
            get => _fromCurrencyValue;
            set {
                if (_fromCurrencyValue == value) return;

                _fromCurrencyValue = value;
                OnPropertyChanged(nameof(FromCurrencyValue));
            }
        }

        public double ToCurrencyValue {
            get => _toCurrencyValue;
            set {
                if (_toCurrencyValue == value) return;

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
