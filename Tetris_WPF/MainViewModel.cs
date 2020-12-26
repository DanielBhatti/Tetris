using GalaSoft.MvvmLight;
using Tetris;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris_WPF
{
    public class MainViewModel : ViewModelBase
    {
        public Grid TetrisGrid { get; set; }

        public MainViewModel()
        {
            TetrisGrid = new Grid();
        }
    }
}
