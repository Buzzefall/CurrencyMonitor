using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CurrencyMonitor.Domain.Entities;
using JetBrains.Annotations;

namespace CurrencyMonitor.GUI.ViewModels
{
    public class CurrencyInputViewModel : ICurrencyInputViewModel, INotifyPropertyChanged
    {
        private ObservableCollection<ICurrency> _currencyList;

        private ICurrency _fromCurrencySelected;
        private double _fromCurrencySelectedValue;

        private ICurrency _toCurrencySelected;
        private double _toCurrencySelectedValue;

        public int LastCurrencySelectedIndex { get; set; }

        public CurrencyInputViewModel(IEnumerable<ICurrency> currencyList) {
            _currencyList = new ObservableCollection<ICurrency>(currencyList);
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
        public ICurrency FromCurrencySelected {
            get => _fromCurrencySelected;
            set {
                if (_fromCurrencySelected == value) return;

                _fromCurrencySelected = value;
                OnPropertyChanged(nameof(FromCurrencySelected));
            }
        }

        public double FromCurrencySelectedValue {
            get => _fromCurrencySelectedValue;
            set {
                if (_fromCurrencySelectedValue == value) return;

                _fromCurrencySelectedValue = value;
                OnPropertyChanged(nameof(FromCurrencySelectedValue));
            }
        }

        [CanBeNull]
        public ICurrency ToCurrencySelected {
            get => _toCurrencySelected;
            set  {
                if (_toCurrencySelected == value) return;

                _toCurrencySelected = value;
                OnPropertyChanged(nameof(ToCurrencySelected));
            }
        }

        public double ToCurrencySelectedValue {
            get => _toCurrencySelectedValue;
            set {
                if (_toCurrencySelectedValue == value) return;

                _toCurrencySelectedValue = value;
                OnPropertyChanged(nameof(ToCurrencySelectedValue));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
