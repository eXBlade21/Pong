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
    class Zeiger
    {
        Line l;
        Double length;
        public Double angle;

        public Zeiger(Ellipse elli, int length)
        {
            l = new Line();
            l.Stroke = Brushes.Red;
            l.StrokeThickness = 3;

            l.X1 = Canvas.GetLeft(elli) + elli.Width / 2;
            l.Y1 = Canvas.GetTop(elli) + elli.Height / 2;

            this.length = elli.Width / 2 - length;
            l.X2 = l.X1;
            l.Y2 = l.Y1 - this.length;
        }

        public void Draw(Canvas c)
        {
            if (!c.Children.Contains(l))
            {
                c.Children.Add(l);
            }
        }
        public void UnDraw(Canvas c)
        {
            if (c.Children.Contains(l))
            {
                c.Children.Remove(l);
            }
        }

        public void Resize(double sx, double sy, Ellipse elli)
        {
            length *= (sx + sy) / 2;
            l.X1 = Canvas.GetLeft(elli) + elli.Width / 2;
            l.Y1 = Canvas.GetTop(elli) + elli.Height / 2;
            l.X2 = l.X1 + length * Math.Sin(angle);
            l.Y2 = l.Y1 - length * Math.Cos(angle);
        }

        public void UpdateZeiger()
        {
            l.X2 = l.X1 + length * Math.Sin(angle);
            l.Y2 = l.Y1 - length * Math.Cos(angle);
        }
    }
}
