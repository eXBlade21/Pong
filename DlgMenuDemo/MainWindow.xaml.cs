using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;

namespace DlgMenuDemo
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    /// 


    /*
     * - MaxPunkte
     * 
     */


    public partial class MainWindow : Window
    {
        StartDlg _dlg;
        Ball ball;
        Paddle p1, p2;
        DispatcherTimer timer;
        Spielfeld _spieldfeld;
        Rectangle Rect;         // Rectangle im Spielfeld
        Analoguhr uhr;
        Settings _settings;

        Double ticks_old;
        Double spielfeld_laenge;
        Double spielfeld_hoehe = 300;
        Double paddle_width = 20;
        Double paddle_height = 90;
        Double paddle_speed = 60;
        Double radius;

        public int p1_punkte;
        public int p2_punkte;
        public int T_sec;

        double ball_width;
        double ball_height;
        double ball_multiplyer;

        double ms_count;
        int last_point;
        bool reset_ball;
        bool autoplay;
        bool show_clock;

        public MainWindow()
        {
            _settings = new Settings();

            if ((bool)_settings.ShowDialog())
            {
                InitializeComponent();
            }
            else
            {
                Close();
            }
        }

        private void wnd_Loaded(object sender, RoutedEventArgs e)
        {
            ms_count = 0;
            _dlg = _settings._dlg;

            paddle_width = _dlg.paddle_width;
            paddle_height = _dlg.paddle_height;
            paddle_speed = _dlg.paddle_speed;
            radius = _dlg.Radius;

            reset_ball = _settings.ballreset;
            autoplay = _settings.autoplay;
            show_clock = _settings.clock;

            if (show_clock)
            {
                clock.Visibility = Visibility.Visible;
                uhr = new Analoguhr(clock, Cvs);
                T_sec = 0;
            }

            spielfeld_laenge = Cvs.ActualWidth - 200;

            _spieldfeld = new Spielfeld(Cvs.ActualWidth - spielfeld_laenge - (Cvs.ActualWidth - spielfeld_laenge) / 2, 40, spielfeld_laenge, spielfeld_hoehe);
            Rect = _spieldfeld.spielfeld;
            _spieldfeld.Draw(Cvs);

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 15);
            
            ball = new Ball(250, 150, 200, 200, radius);
            ball.mw = this;
            ball.Draw(Cvs);
            p1 = new Paddle(Canvas.GetLeft(Rect) + 50, Canvas.GetTop(Rect) *2 + paddle_height/2, 20, paddle_speed, paddle_width, paddle_height);
            p1.Draw(Cvs);
            p2 = new Paddle(Canvas.GetLeft(Rect) + spielfeld_laenge - p1.width - 50, Canvas.GetTop(Rect) * 2 + paddle_height / 2, 20, paddle_speed, paddle_width, paddle_height);
            p2.Draw(Cvs);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Double ticks = Environment.TickCount;

            ball.Move(ticks - ticks_old);

            last_point = ball.Collision(Rect);

            if (last_point == 1 && reset_ball)
            {
                Random rd = new Random();
                ball_multiplyer = ball.multiplyer;
                ball_width = ball.Elli.Width;
                ball_height = ball.Elli.Height;
                ball.UnDraw(Cvs);
                if (rd.Next(0, 2) == 1)
                {
                    ball = new Ball(250, 150, 200, 200, radius);
                }
                else
                {
                    ball = new Ball(250, 150, 200, -200, radius);
                }
                ball.mw = this;
                ball.Draw(Cvs);
                ball.multiplyer = ball_multiplyer;
                ball.Elli.Width = ball_width;
                ball.Elli.Height = ball_height;
            }
            else if (last_point == 2 && reset_ball)
            {
                Random rd = new Random();
                ball.UnDraw(Cvs);
                if (rd.Next(0, 2) == 1)
                {
                    ball = new Ball(250, 150, -200, -200, radius);
                }
                else
                {
                    ball = new Ball(250, 150, -200, 200, radius);
                }
                ball.mw = this;
                ball.Draw(Cvs);
            }
            ball.Collision(p1);
            ball.Collision(p2);

            lbl_p1_punkte.Content = p1_punkte;
            lbl_p2_punkte.Content = p2_punkte;

            border_w.Background = Brushes.Gainsboro;
            border_s.Background = Brushes.Gainsboro;
            border_up.Background = Brushes.Gainsboro;
            border_down.Background = Brushes.Gainsboro;

            if (Keyboard.IsKeyDown(Key.W))
            {
                if (Canvas.GetTop(p1.rect) > Canvas.GetTop(Rect) + 10)
                {
                    p1.Move(ticks - ticks_old, true);
                    border_w.Background = Brushes.Green;
                }
            }
            if (Keyboard.IsKeyDown(Key.S))
            {
                if (Canvas.GetTop(p1.rect) + p1.rect.Height < Canvas.GetTop(Rect) + Rect.Height - 10)
                {
                    p1.Move(ticks - ticks_old, false);
                    border_s.Background = Brushes.Green;
                }
            }
            if (Keyboard.IsKeyDown(Key.Up) && autoplay == false)
            {
                if (Canvas.GetTop(p2.rect) > Canvas.GetTop(Rect) + 10)
                {
                    p2.Move(ticks - ticks_old, true);
                    border_up.Background = Brushes.Green;
                }
            }
            if (Keyboard.IsKeyDown(Key.Down) && autoplay == false)
            {
                if (Canvas.GetTop(p2.rect) + p1.rect.Height < Canvas.GetTop(Rect) + Rect.Height - 10)
                {
                    p2.Move(ticks - ticks_old, false);
                    border_down.Background = Brushes.Green;
                }
            }
            
            if (autoplay == true && ball.Vx > 0)
            {
                double t = Canvas.GetLeft(p2.rect) - ball.X / ball.Vx;
                double yt = ball.Y + ball.Vy / t;
                if (yt < Canvas.GetTop(p2.rect) + paddle_height / 2 && Canvas.GetTop(p2.rect) > Canvas.GetTop(Rect) + 10)
                {
                    p2.Move(ticks - ticks_old, true);
                }
                else if(yt > Canvas.GetTop(p2.rect) + paddle_height / 2 && Canvas.GetTop(p2.rect) + paddle_height < Canvas.GetTop(Rect) + spielfeld_hoehe - 10)
                {
                    p2.Move(ticks - ticks_old, false);
                }
            }

            double fps = (1 / (ticks - ticks_old)) * 1000;
            fps = Math.Round(fps);
            lbl_fps.Content = "FPS: " + fps;

            if (uhr != null)
            {
                ms_count += (ticks - ticks_old);
                if (ms_count >= 1000)
                {
                    T_sec++;
                    uhr.UpdateUhr(T_sec);

                    ms_count = 0;
                }
            }
            
            ticks_old = ticks;
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            ticks_old = Environment.TickCount;

            timer.Start();
        }

        private void ende_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void parameter_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Window mw = new MainWindow();
            try
            {
                mw.Show();
            }
            catch { }
            Close();
        }

        private void bt_apply_speed_Click(object sender, RoutedEventArgs e)
        {
            ball.multiplyer = JJ_slider.Value;
        }

        private void Cvs_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(e.PreviousSize.Width != 0)
            {
                Double sx = e.NewSize.Width / e.PreviousSize.Width;
                Double sy = e.NewSize.Height / e.PreviousSize.Height;

                ball.Resize(sx, sy);
                p1.Resize(sx, sy);
                p2.Resize(sx, sy);

                clock.Width *= (sx + sy) / 2;
                clock.Height *= (sx + sy) / 2;
                Canvas.SetLeft(clock, Canvas.GetLeft(clock) * sx);
                Canvas.SetTop(clock, Canvas.GetTop(clock) * sy);

                uhr.resize(sx, sy, clock);

                JJ_slider.Width *= sx;
                JJ_slider.Height *= sy;
                Canvas.SetLeft(JJ_slider, Canvas.GetLeft(JJ_slider) * sx);
                Canvas.SetTop(JJ_slider, Canvas.GetTop(JJ_slider) * sy);

                bt_apply_speed.Width *= sx;
                bt_apply_speed.Height *= sy;
                Canvas.SetLeft(bt_apply_speed, Canvas.GetLeft(bt_apply_speed) * sx);
                Canvas.SetTop(bt_apply_speed, Canvas.GetTop(bt_apply_speed) * sy);

                Rect.Width *= sx;
                Rect.Height *= sy;
                Canvas.SetLeft(Rect, sx * Canvas.GetLeft(Rect));
                Canvas.SetTop(Rect, sy * Canvas.GetTop(Rect));

                lbl_fps.FontSize *= sy;
                Canvas.SetLeft(lbl_fps, Canvas.GetLeft(lbl_fps) * sx);

                lbl_p1_punkte.FontSize = lbl_p1_punkte.FontSize * ((sx + sy) / 2);
                Canvas.SetTop(border_p1_punkte, Canvas.GetTop(border_p1_punkte) * sy);
                Canvas.SetLeft(border_p1_punkte, Canvas.GetLeft(border_p1_punkte) * sx);

                lbl_p2_punkte.FontSize = lbl_p2_punkte.FontSize * ((sx + sy) / 2);
                Canvas.SetTop(border_p2_punkte, Canvas.GetTop(border_p2_punkte) * sy);
                Canvas.SetLeft(border_p2_punkte, Canvas.GetLeft(border_p2_punkte) * sx);

                rect_trenner_0.Width *= sx;
                rect_trenner_0.Height *= sy;
                Canvas.SetTop(rect_trenner_0, Canvas.GetTop(rect_trenner_0) * sy);

                rect_trenner_1.Width *= sx;
                rect_trenner_1.Height *= sy;
                Canvas.SetTop(rect_trenner_1, Canvas.GetTop(rect_trenner_1) * sy);
                Canvas.SetLeft(rect_trenner_1, Canvas.GetLeft(rect_trenner_1) * sx);

                rect_trenner_2.Width *= sx;
                rect_trenner_2.Height *= sy;
                Canvas.SetTop(rect_trenner_2, Canvas.GetTop(rect_trenner_2) * sy);
                Canvas.SetLeft(rect_trenner_2, Canvas.GetLeft(rect_trenner_2) * sx);

                border_w.Width *= sx;
                border_w.Height *= sy;
                Canvas.SetTop(border_w, Canvas.GetTop(border_w) * sy);
                Canvas.SetLeft(border_w, Canvas.GetLeft(border_w) * sx);
                tb_w.FontSize *= (sx + sy) / 2;

                border_s.Width *= sx;
                border_s.Height *= sy;
                Canvas.SetTop(border_s, Canvas.GetTop(border_s) * sy);
                Canvas.SetLeft(border_s, Canvas.GetLeft(border_s) * sx);
                tb_s.FontSize *= (sx + sy) / 2;

                border_up.Width *= sx;
                border_up.Height *= sy;
                Canvas.SetTop(border_up, Canvas.GetTop(border_up) * sy);
                Canvas.SetLeft(border_up, Canvas.GetLeft(border_up) * sx);
                tb_up.FontSize *= (sx + sy) / 2;

                border_down.Width *= sx;
                border_down.Height *= sy;
                Canvas.SetTop(border_down, Canvas.GetTop(border_down) * sy);
                Canvas.SetLeft(border_down, Canvas.GetLeft(border_down) * sx);
                tb_down.FontSize *= (sx + sy) / 2;

                _spieldfeld.midline.Width *= sx;
                _spieldfeld.midline.Height *= sy;
                Canvas.SetTop(_spieldfeld.midline, Canvas.GetTop(_spieldfeld.midline) * sy);
                Canvas.SetLeft(_spieldfeld.midline, Canvas.GetLeft(_spieldfeld.midline) * sx);
            }
        }
    }
}
