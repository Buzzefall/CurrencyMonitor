using System;
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
        private double _fromCurrencyValue;

        private double _exchangeRate = 1.0;

        private ICurrency _toCurrencySelected;
        private double _toCurrencyValue;

        public int LastCurrencySelectedIndex { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateExchangeRate() {
            if (_fromCurrencySelected is null || _toCurrencySelected is null) return;

            var fromCurrencyInRubles = _fromCurrencySelected.Value / _fromCurrencySelected.Nominal;
            var toCurrencyInRubles =  _toCurrencySelected.Value / _toCurrencySelected.Nominal;
            _exchangeRate = fromCurrencyInRubles / toCurrencyInRubles;
        }


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

                UpdateExchangeRate();

                _fromCurrencyValue = Math.Round(_toCurrencyValue / _exchangeRate, 2, MidpointRounding.AwayFromZero);

                OnPropertyChanged(nameof(FromCurrencySelected));
                OnPropertyChanged(nameof(FromCurrencyValue));
            }
        }

        public double FromCurrencyValue {
            get => _fromCurrencyValue;
            set {
                if (Math.Abs(_fromCurrencyValue - value) < 1e-3) return;

                _fromCurrencyValue = value;
                _toCurrencyValue = Math.Round(value * _exchangeRate, 2, MidpointRounding.AwayFromZero);

                OnPropertyChanged(nameof(FromCurrencyValue));
                OnPropertyChanged(nameof(ToCurrencyValue));
            }
        }

        [CanBeNull]
        public ICurrency ToCurrencySelected {
            get => _toCurrencySelected;
            set  {
                if (_toCurrencySelected == value) return;

                _toCurrencySelected = value;

                UpdateExchangeRate();

                _toCurrencyValue = Math.Round(_fromCurrencyValue * _exchangeRate, 2, MidpointRounding.AwayFromZero);

                OnPropertyChanged(nameof(ToCurrencySelected));
                OnPropertyChanged(nameof(ToCurrencyValue));
            }
        }

        public double ToCurrencyValue {
            get => _toCurrencyValue;
            set {
                if (Math.Abs(_toCurrencyValue - value) < 1e-3) return;

                _toCurrencyValue = value;
                _fromCurrencyValue = Math.Round(value / _exchangeRate, 2, MidpointRounding.AwayFromZero);

                OnPropertyChanged(nameof(ToCurrencyValue));
                OnPropertyChanged(nameof(FromCurrencyValue));
            }
        }
    }
}
