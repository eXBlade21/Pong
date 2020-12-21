using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;

namespace DlgMenuDemo
{
    class Spielfeld
    {
        public Rectangle spielfeld = new Rectangle();
        public Rectangle midline = new Rectangle();

        public Spielfeld(double x , double y, double laenge , double hoehe)//double x = 36, double y = 49, double laenge = 425, double hoehe = 300)
        {
            spielfeld.Width = laenge;
            spielfeld.Height = hoehe;

            Canvas.SetLeft(spielfeld, x);
            Canvas.SetTop(spielfeld, y);

            spielfeld.Fill = Brushes.Gainsboro;
            spielfeld.Stroke = Brushes.Black;

            midline.Width = 1;
            midline.Height = hoehe;
            midline.Stroke = Brushes.Black;
            midline.StrokeThickness = 2;
            midline.StrokeDashArray = new DoubleCollection() { 1.5 };

            Canvas.SetLeft(midline, x + laenge/2);
            Canvas.SetTop(midline, y);
        }

        public void Draw(Canvas c)
        {
            if (!c.Children.Contains(spielfeld))
            {
                c.Children.Add(spielfeld);
                c.Children.Add(midline);
            }
        }
        public void UnDraw(Canvas c)
        {
            if (c.Children.Contains(spielfeld))
            {
                c.Children.Remove(spielfeld);
                c.Children.Remove(midline);
            }
        }
    }
}
