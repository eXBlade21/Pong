using System;
using System.Windows;

namespace DlgMenuDemo
{
    /// <summary>
    /// Interaktionslogik für StartDlg.xaml
    /// </summary>
    public partial class StartDlg : Window
    {
        public Double paddle_width { get; set; }
        public Double paddle_height { get; set; }
        public Double paddle_speed { get; set; }
        public Double Radius { get; set; }


        public StartDlg()
        {
            InitializeComponent();
            tb_radius.Text = "10";
            tb_paddle_width.Text = "20";
            tb_paddle_height.Text = "90";
            tb_paddle_speed.Text = "60";
        }

        public void OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Radius = Convert.ToDouble(tb_radius.Text);
                paddle_width = Convert.ToDouble(tb_paddle_width.Text);
                paddle_height = Convert.ToDouble(tb_paddle_height.Text);
                paddle_speed = Convert.ToDouble(tb_paddle_speed.Text);

                if (Radius < 1 || Radius > 100)
                {
                    throw new Exception("Der Radius muss zwischen 1 und 100 einschließlich liegen.");
                }
                else if (paddle_width < 1 || paddle_width > 150)
                {
                    throw new Exception("Die Paddle breite muss zwischen 1 und 150 einschließlich liegen.");
                }
                else if (paddle_height < 10 || paddle_height > 165)
                {
                    throw new Exception("Die Paddle höhe muss zwischen 10 und 165 einschließlich liegen.");
                }
                else if (paddle_speed < 10 || paddle_speed > 200)
                {
                    throw new Exception("Der Paddle speed muss zwischen 10 und 200 einschließlich liegen.");
                }
                else if (sender == null && e == null)
                {
                    
                }
                else
                {
                    DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler: " + ex.Message, "Eingabefehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void bt_reset_Click(object sender, RoutedEventArgs e)
        {
            tb_radius.Text = "10";
            tb_paddle_width.Text = "20";
            tb_paddle_height.Text = "90";
            tb_paddle_speed.Text = "60";
        }
    }
}
