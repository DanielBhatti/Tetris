using GalaSoft.MvvmLight;
using Tetris;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris_WPF
{
    public class MainViewModel : ViewModelBase
    {
        public Field TetrisField { get; set; }

        public MainViewModel()
        {
            TetrisField = new Field();
        }
    }
}
