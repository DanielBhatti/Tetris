using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Tetris;

namespace Tetris_WPF
{
    public class CellArrayToImageSourceConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Cell cell = ((Cell[,])values[0])[(int)values[1], (int)values[2]];

            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = ColorToUri(cell.Color);
            b.EndInit();

            return b;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private Uri ColorToUri(Tetris.Color color)
        {
            string basePath = @"C:\Users\bhatt\Documents\Programming\Tetris\Tetris_WPF\Resources";
            switch (color)
            {
                case Tetris.Color.Transparent:
                    return new Uri(Path.Combine(basePath, "white_block.png"));
                case Tetris.Color.Blue:
                    return new Uri(Path.Combine(basePath, "blue_block.png"));
                case Tetris.Color.Green:
                    return new Uri(Path.Combine(basePath, "green_block.png"));
                case Tetris.Color.Orange:
                    return new Uri(Path.Combine(basePath, "orange_block.png"));
                case Tetris.Color.Purple:
                    return new Uri(Path.Combine(basePath, "purple_block.png"));
                case Tetris.Color.Red:
                    return new Uri(Path.Combine(basePath, "red_block.png"));
                case Tetris.Color.Teal:
                    return new Uri(Path.Combine(basePath, "teal_block.png"));
                case Tetris.Color.White:
                    return new Uri(Path.Combine(basePath, "white_block.png"));
                case Tetris.Color.Yellow:
                    return new Uri(Path.Combine(basePath, "yellow_block.png"));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
