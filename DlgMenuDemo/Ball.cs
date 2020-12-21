using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DlgMenuDemo
{
    class Ball
    {
        public Ellipse Elli { get; set; }
        public Double X { get; set; }
        public Double Y { get; set; }
        public Double Vx { get; set; }
        public Double Vy { get; set; }
        public Double Radius { get; set; }
        public Double multiplyer { get; set; }

        public MainWindow mw { get; set; }

        public Ball(Double X = 100, Double Y = 100, Double Vx = 20, Double Vy = 20, Double Radius = 10)
        {
            this.X = X;
            this.Y = Y;
            this.Vx = Vx;
            this.Vy = Vy;
            this.Radius = Radius;

            Elli = new Ellipse();
            Elli.Width = 2 * Radius;
            Elli.Height = 2 * Radius;
            Elli.Fill = Brushes.Blue;

            Canvas.SetLeft(Elli, X - Radius);
            Canvas.SetTop(Elli, Y - Radius);

            multiplyer = 1;
        }

        public void Draw(Canvas c)
        {
            if (!c.Children.Contains(Elli))
            {
                c.Children.Add(Elli);
            }
        }

        public void UnDraw(Canvas c)
        {
            if (c.Children.Contains(Elli))
            {
                c.Children.Remove(Elli);
            }
        }

        public void Resize(double sx, double sy)
        {
            X *= sx;
            Y *= sy;

            Vx *= sx;
            Vy *= sy;

            Radius *= (sx + sy) / 2;

            Elli.Width *= (sx + sy) / 2;
            Elli.Height *= (sx + sy) / 2;

            Canvas.SetLeft(Elli, sx * Canvas.GetLeft(Elli));
            Canvas.SetTop(Elli, sy * Canvas.GetTop(Elli));
        }

        public void Move(Double dt)
        {
            X = X + Vx * dt / 1000 * multiplyer;
            Y = Y + Vy * dt / 1000 * multiplyer;
        }

        public int Collision(Rectangle r)
        {
            int winner = 0;
            // Obere oder untere Bande
            if (Y - Radius <= Canvas.GetTop(r))
            {
                Vy = -Vy;                                       // Reflexion and der Bande
                Y = Y + 2 * (Canvas.GetTop(r) - (Y - Radius));  // Korrektur des Detektionsfehlers
            }
            else if (Y + Radius >= Canvas.GetTop(r) + r.Height)
            {
                Vy = -Vy;
                Y = Y - 2 * (Y + Radius - Canvas.GetTop(r) - r.Height);
            }

            // Linke oder rechte Bande
            if (X - Radius <= Canvas.GetLeft(r))
            {
                Vx = -Vx;
                X = X + 2 * (Canvas.GetLeft(r) - (X - Radius));
                mw.p2_punkte++;
                winner = 1;
            }
            else if (X + Radius >= Canvas.GetLeft(r) + r.Width)
            {
                Vx = -Vx;
                X = X - 2 * (X + Radius - Canvas.GetLeft(r) - r.Width);
                mw.p1_punkte++;
                winner = 2;
            }

            Canvas.SetLeft(Elli, X - Radius);
            Canvas.SetTop(Elli, Y - Radius);
            return winner;
        }
        public void Collision(Paddle p)
        {
            Rectangle r = p.rect;

            //Collision Right
            if (X - Radius <= Canvas.GetLeft(r) + r.Width && X - Radius > Canvas.GetLeft(r) && Y + Radius > Canvas.GetTop(r) && Y - Radius < Canvas.GetTop(r) + r.Height)
            {
                Vx = -Vx;
                X = X + Radius;
            }
            //Collision Left
            if (X + Radius >= Canvas.GetLeft(r) && X + Radius <= Canvas.GetLeft(r) + r.Width && Y + Radius > Canvas.GetTop(r) && Y - Radius < Canvas.GetTop(r) + r.Height)
            {
                Vx = -Vx;
                X = X - Radius;
            }
            // Collision Oben
            if (Y + Radius >= Canvas.GetTop(r) && Y + Radius < Canvas.GetTop(r) + r.Height && X + Radius >= Canvas.GetLeft(r) && X - Radius <= Canvas.GetLeft(r) + r.Width)
            {
                Vy = -Vy;
                Y = Y - Radius;
            }
            // Collision Unten
            if (Y - Radius <= Canvas.GetTop(r) + r.Height && Y - Radius > Canvas.GetTop(r) && X + Radius >= Canvas.GetLeft(r) && X - Radius <= Canvas.GetLeft(r) + r.Width)
            {
                Vy = -Vy;
                Y = Y + Radius;
            }

            Canvas.SetLeft(Elli, X - Radius);
            Canvas.SetTop(Elli, Y - Radius);
        }
    }
}
