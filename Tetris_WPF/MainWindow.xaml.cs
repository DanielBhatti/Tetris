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

            int fieldWidth = _mainViewModel.GameViewModel.Width;
            int fieldHeight = _mainViewModel.GameViewModel.Height;

            for (int i = 0; i < fieldWidth; i++) Grid_Tetris.ColumnDefinitions.Add(new ColumnDefinition());
            for (int j = 0; j < fieldHeight; j++) Grid_Tetris.RowDefinitions.Add(new RowDefinition());

            for (int i = 0; i < fieldWidth; i++)
            {
                for (int j = 0; j < fieldHeight; j++)
                {
                    Image image = new Image();

                    MultiBinding multiBinding = new MultiBinding();
                    Binding binding1 = new Binding("GameViewModel.Cells");
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

                    Grid.SetColumn(image, i);
                    Grid.SetRow(image, fieldHeight - 1 - j);
                    Grid_Tetris.Children.Add(image);
                }
            }
        }
    }
}
