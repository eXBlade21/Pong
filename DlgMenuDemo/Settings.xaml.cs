using System;
using System.Windows;
using System.Drawing;


namespace DlgMenuDemo
{
    /// <summary>
    /// Interaktionslogik für Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public bool ballreset = false;
        public bool autoplay = false;
        public bool clock = false;
        public StartDlg _dlg;

        public Settings()
        {
            InitializeComponent();
            _dlg = new StartDlg();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void bt_moreSettings_Click(object sender, RoutedEventArgs e)
        {
            _dlg.Close();
            _dlg = new StartDlg();

            if ((bool)_dlg.ShowDialog())
            {
                InitializeComponent();
            }
            else
            {
                Close();
            }
        }

        private void bt_ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            _dlg.OK_Click(null, null);
            _dlg.Close();
        }

        private void bt_cancel_Click(object sender, RoutedEventArgs e)
        {
            _dlg.Close();
            Close();
        }

        private void cb_clock_Click(object sender, RoutedEventArgs e)
        {
            clock = !clock;
        }

        private void cb_secondPlayer_Click(object sender, RoutedEventArgs e)
        {
            autoplay = !autoplay;
        }

        private void cb_reset_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ACHTUNG: Feature noch nicht fertig! Es werden bei aktivierung Bugs mit dem Ball Auftreten!", "ACHTUNG!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            ballreset = true;
        }

        private void cb_reset_Unchecked(object sender, RoutedEventArgs e)
        {
            ballreset = false;
        }
    }
}
