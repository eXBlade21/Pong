using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DlgMenuDemo
{
    class Paddle
    {
        public Rectangle rect { get; set; }
        public Double X { get; set; }
        public Double Y { get; set; }
        public Double Vx { get; set; }
        public Double Vy { get; set; }
        public Double width { get; set; }

        public Paddle(Double X, Double Y, Double Vx, Double Vy, Double width, Double height)
        {
            this.X = X;
            this.Y = Y;
            this.Vx = Vx;
            this.Vy = Vy;
            this.width = width;

            rect = new Rectangle();
            rect.Width = width;
            rect.Height = height;
            rect.Fill = Brushes.Red;

            Canvas.SetLeft(rect, X);
            Canvas.SetTop(rect, Y);
        }

        public void Move(Double dt, bool direction)
        {
            if(direction == true)
                Canvas.SetTop(rect, Canvas.GetTop(rect) - dt * Vy / 200);
            else if(direction==false)
                Canvas.SetTop(rect, Canvas.GetTop(rect) + dt * Vy / 200);
        }

        public void Resize(double sx, double sy)
        {
            rect.Width = rect.Width * sx;
            rect.Height = rect.Height * sy;
            Canvas.SetLeft(rect, Canvas.GetLeft(rect) * sx);
            Canvas.SetTop(rect, Canvas.GetTop(rect) * sy);
        }

        public void Draw(Canvas c)
        {
            if (!c.Children.Contains(rect))
            {
                c.Children.Add(rect);
            }
        }
        public void UnDraw(Canvas c)
        {
            if (c.Children.Contains(rect))
            {
                c.Children.Remove(rect);
            }
        }
    }
}
