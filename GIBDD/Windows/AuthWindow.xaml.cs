using GIBDD.Classes;
using GIBDD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GIBDD.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        private static int _attemptsCount = 0;
        private static int _time = 60;
        private static DispatcherTimer _timer;
        public AuthWindow()
        {
            InitializeComponent();
            tbBlock.Visibility = Visibility.Hidden;
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            _attemptsCount++;

            if (_attemptsCount > 3)
            {
                tbBlock.Visibility = Visibility.Visible;
                btnAuth.IsEnabled = false;
                _timer = new DispatcherTimer();
                _timer.Interval = _timer.Interval = new TimeSpan(0, 0, 1);
                _timer.Tick += Timer_Tick;
                _timer.Start();
            }
            bool isAuth = false;

            foreach (var item in GIBDDEntities.GetContext().Inspector)
            {
                if ((item.LoginPassword == tbLogin.Text) && (item.LoginPassword == tbPass.Text))
                {
                    isAuth = true;
                    MainWindow mw = new MainWindow();
                    this.Close();
                    mw.Show();
                    break;
                }
            }
            if (!isAuth)
                MessageBox.Show("Вход не выполнен. Проверьте введенные значения.");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_time != 0)
            {
                tbBlock.Text = $"Временная блокировка: {string.Format("00:0{0}:{1}", _time / 60, _time % 60)}";
                _time--;
            }
            else
            {
                _timer.Stop();
                tbBlock.Visibility = Visibility.Hidden;
                btnAuth.IsEnabled = true;
                _attemptsCount = 0;
                _time = 60;
            }
        }
    }
}
