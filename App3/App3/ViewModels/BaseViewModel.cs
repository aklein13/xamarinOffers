using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using App3.Models;
using App3.Services;

namespace App3.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Offer> DataStore => DependencyService.Get<IDataStore<Offer>>() ?? new MockDataStore();
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
        string city = string.Empty;
        public string City
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
        List<string> cityList;
        public List<string> CityList
        {
            get { return cityList; }
            set { SetProperty(ref cityList, value); }
        }
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;
            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
