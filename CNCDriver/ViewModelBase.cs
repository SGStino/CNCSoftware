using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CNCDriver
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T store, T value, [CallerMemberName]string caller = null)
        {
            if (!EqualityComparer<T>.Default.Equals(store, value))
            {
                store = value;
                NotifyPropertyChanged(caller);
                return true;
            }
            return false;
        }

        protected void NotifyPropertyChanged(string caller)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }
}