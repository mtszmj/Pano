using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Pano.Model;
using Pano.Service;

namespace Pano.ViewModel
{
    public class MainViewModel : ViewModelBaseDecorator
    {
        public string Title { get; set; }
        
        public MainViewModel()
        {
            Title = IsInDesignMode ? "Pano (Design Mode)" : "Pano";
        }
    }
}