using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using Tetris;

namespace Tetris_WPF
{
    /// <summary>
    /// Interaction logic for TetrisPieceControl.xaml
    /// </summary>
    public partial class TetrisPieceControl : UserControl
    {
        public TetrisPiece Piece {
            get => Piece;
            set
            {
                Piece = value;
                Constructor(Piece);
            }
        }

        public TetrisPieceControl()
        {
            InitializeComponent();
        }

        public void Constructor(TetrisPiece tetrisPiece)
        {
            int minX = int.MaxValue;
            int minY = int.MaxValue;
            int maxX = int.MinValue;
            int maxY = int.MinValue;

            foreach(Position p in tetrisPiece.RelativePositions)
            {
                if (p.X < minX) minX = p.X;
                if (p.Y < minY) minY = p.Y;
                if (p.X > maxX) maxX = p.X;
                if (p.Y > maxY) maxY = p.Y;
            }

            int width = maxX - minX;
            int height = maxY - minY;

            Grid_Piece.ColumnDefinitions.Clear();
            Grid_Piece.RowDefinitions.Clear();

            for (int i = 0; i < width; i++) Grid_Piece.ColumnDefinitions.Add(new ColumnDefinition());
            for (int j = 0; j < height; j++) Grid_Piece.RowDefinitions.Add(new RowDefinition());

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    foreach(Position p in tetrisPiece.RelativePositions)
                    {
                        if(p.X - minX == i && p.Y - minY == j)
                        {
                            System.Windows.Media.Imaging.BitmapImage b = new System.Windows.Media.Imaging.BitmapImage();
                            b.BeginInit();
                            b.UriSource = ColorToUri(tetrisPiece.Color);
                            b.EndInit();

                            Image image = new Image();
                            image.Source = b;
                            image.Stretch = System.Windows.Media.Stretch.Fill;

                            Grid.SetColumn(image, i);
                            Grid.SetRow(image, height - 1 - j);
                            Grid_Piece.Children.Add(image);
                        }
                    }
                }
            }
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
