using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tetris_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _mainViewModel;

        public MainWindow()
        {
            InitializeComponent();
            _mainViewModel = new MainViewModel();
            DataContext = _mainViewModel;

            Grid_Tetris.Rows = _mainViewModel.TetrisGrid.Height;
            Grid_Tetris.Columns = _mainViewModel.TetrisGrid.Width;

            for (int i = 0; i < _mainViewModel.TetrisGrid.Height; i++)
            {
                for (int j = 0; j < _mainViewModel.TetrisGrid.Width; j++)
                {
                    BitmapImage b = new BitmapImage();
                    b.BeginInit();
                    b.UriSource = new Uri(@"C:\Users\bhatt\Documents\Programming\Tetris\Tetris_WPF\Resources\white_block.png");
                    b.EndInit();

                    Image image = new Image();
                    image.Source = b;
                    image.Stretch = Stretch.Fill;

                    Grid.SetRow(image, i);
                    Grid.SetColumn(image, j);
                    Grid_Tetris.Children.Add(image);
                }
            }
        }
    }
}
