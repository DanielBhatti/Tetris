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
using Tetris;

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

            Grid_Tetris.Rows = _mainViewModel.GameViewModel.Height;
            Grid_Tetris.Columns = _mainViewModel.GameViewModel.Width;

            for (int i = 0; i < _mainViewModel.GameViewModel.Width; i++)
            {
                for (int j = 0; j < _mainViewModel.GameViewModel.Height; j++)
                {
                    Cell cell = _mainViewModel.GameViewModel.Cells[i, j];
                    Image image = new Image();

                    MultiBinding multiBinding = new MultiBinding();
                    Binding binding1 = new Binding("GameViewModel");
                    binding1.Source = _mainViewModel;
                    Binding binding2 = new Binding();
                    binding2.Source = i;
                    Binding binding3 = new Binding();
                    binding3.Source = j;
                    multiBinding.Converter = new ColorToImageSourceConverter();
                    multiBinding.Bindings.Add(binding1);
                    multiBinding.Bindings.Add(binding2);
                    multiBinding.Bindings.Add(binding3);

                    image.SetBinding(Image.SourceProperty, multiBinding);
                    image.Stretch = Stretch.Fill;

                    Grid.SetRow(image, i);
                    Grid.SetColumn(image, j);
                    Grid_Tetris.Children.Add(image);
                }
            }
        }
    }
}
