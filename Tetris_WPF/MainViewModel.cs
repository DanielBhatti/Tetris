using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris_WPF
{
    public class MainViewModel : ViewModelBase
    {
        public string BindingTestString { get; set; }

        public MainViewModel()
        {
            BindingTestString = "Test Value";
        }
    }
}
