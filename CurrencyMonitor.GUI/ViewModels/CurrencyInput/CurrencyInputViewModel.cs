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
        private double _fromCurrencySelectedValue;

        private double _exchangeRate;

        private ICurrency _toCurrencySelected;
        private double _toCurrencySelectedValue;

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

                _fromCurrencySelectedValue = Math.Round(_toCurrencySelectedValue / _exchangeRate, 2, MidpointRounding.AwayFromZero);

                OnPropertyChanged(nameof(FromCurrencySelected));
                OnPropertyChanged(nameof(FromCurrencySelectedValue));
            }
        }

        public double FromCurrencySelectedValue {
            get => _fromCurrencySelectedValue;
            set {
                if (Math.Abs(_fromCurrencySelectedValue - value) < 1e-3) return;

                _fromCurrencySelectedValue = value;
                _toCurrencySelectedValue = Math.Round(value * _exchangeRate, 2, MidpointRounding.AwayFromZero);

                OnPropertyChanged(nameof(FromCurrencySelectedValue));
                OnPropertyChanged(nameof(ToCurrencySelectedValue));
            }
        }

        [CanBeNull]
        public ICurrency ToCurrencySelected {
            get => _toCurrencySelected;
            set  {
                if (_toCurrencySelected == value) return;

                _toCurrencySelected = value;

                UpdateExchangeRate();

                _toCurrencySelectedValue = Math.Round(_fromCurrencySelectedValue * _exchangeRate, 2, MidpointRounding.AwayFromZero);

                OnPropertyChanged(nameof(ToCurrencySelected));
                OnPropertyChanged(nameof(ToCurrencySelectedValue));
            }
        }

        public double ToCurrencySelectedValue {
            get => _toCurrencySelectedValue;
            set {
                if (Math.Abs(_toCurrencySelectedValue - value) < 1e-3) return;

                _toCurrencySelectedValue = value;
                _fromCurrencySelectedValue = Math.Round(value / _exchangeRate, 2, MidpointRounding.AwayFromZero);

                OnPropertyChanged(nameof(ToCurrencySelectedValue));
                OnPropertyChanged(nameof(FromCurrencySelectedValue));
            }
        }
    }
}
