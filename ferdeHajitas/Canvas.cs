using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ferdeHajitas
{
    class Canvas
    {
        Grid c;

        public Point Origo { get; private set; }
        public double Unit { get; set; } = 10;

        public Canvas(Grid control)
        {
            c = control;
            Origo = new Point(0, control.ActualHeight);
        }

        public void Clear(bool drawAxis = false, bool drawGrid = false)
        {
            c.Children.Clear();
            if (drawAxis)
            {
                DrawLine(0, -1000, 0, 1000, 3);
                DrawLine(-1000, 0, 1000, 0, 3);
            }
            if (drawGrid)
            {
                for (int i = -20; i < 20; i++)
                {
                    DrawLine(i, -1000, i, 1000, 1);
                    DrawLine(-1000, i, 1000, i, 1);
                }
            }
        }

        public void DrawLine(double x1, double y1, double x2, double y2, double thickness = 2)
        {
            if (double.IsNaN(x1) || double.IsNaN(y1) || double.IsNaN(x2) || double.IsNaN(y2)) return;

            Line line = new Line();
            line.X1 = Math.Round(Origo.X + (double)x1 * Unit);
            line.Y1 = Math.Round(Origo.Y - (double)y1 * Unit);
            line.X2 = Math.Round(Origo.X + (double)x2 * Unit);
            line.Y2 = Math.Round(Origo.Y - (double)y2 * Unit);
            line.Stroke = new SolidColorBrush(Colors.Black);
            line.StrokeThickness = thickness;
            line.HorizontalAlignment = HorizontalAlignment.Left;
            line.VerticalAlignment = VerticalAlignment.Top;
            c.Children.Add(line);
        }
    }
}
