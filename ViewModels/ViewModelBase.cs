using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows;
using DotaInfoSystem.Models;

namespace DotaInfoSystem.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged 
    {
        public ViewModelBase(IConnection con)
        {
            connection = con;
        }
        protected virtual void OnPropertyChanged(string name) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected IConnection connection;
       
    }
}
