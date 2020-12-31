using GalaSoft.MvvmLight;
using Tetris;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Threading;

namespace Tetris_WPF
{
    public class MainViewModel : ViewModelBase
    {
        public GameViewModel GameViewModel { get; }
        private Timer _timer;
        private Dispatcher _dispatcher;

        public MainViewModel()
        {
            GameViewModel = new GameViewModel();
            _timer = new Timer(Timer_Tick, null, 1000, 1000);
            _dispatcher = Dispatcher.CurrentDispatcher;
        }

        private void Timer_Tick(object sender)
        {
            GameViewModel.MoveDown();
            _dispatcher.BeginInvoke((Action)(() => { RaisePropertyChanged(nameof(GameViewModel)); }));
        }
    }
}