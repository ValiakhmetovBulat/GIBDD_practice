using GIBDD.Classes;
using GIBDD.Entities;
using GIBDD.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using VIN_LIB;

namespace GIBDD
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Button _showDrivers;
        public MainWindow()
        {
            InitializeComponent();
            Manager.MainFrame = MainFrame;
            Manager.MainFrame.Navigate(new MainPage());
            _showDrivers = btnShowDrivers;
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                btnBack.Visibility = Visibility.Visible;
            }
            else
            {
                btnBack.Visibility = Visibility.Hidden;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void changeButtonColors(Button current)
        {
            btnShowDrivers.Background = new SolidColorBrush(Color.FromRgb(209, 238, 252));
            btnShowLicences.Background = new SolidColorBrush(Color.FromRgb(209, 238, 252));
            btnShowCars.Background = new SolidColorBrush(Color.FromRgb(209, 238, 252));
            //btnShowFines.Background = new SolidColorBrush(Color.FromRgb(209, 238, 252));
            //btnShowAccidents.Background = new SolidColorBrush(Color.FromRgb(209, 238, 252));

            current.Background = new SolidColorBrush(Color.FromRgb(224, 169, 175));
        }

        private void btnShowDrivers_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new MainPage());
            changeButtonColors(btnShowDrivers);
        }

        private void btnShowLicences_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new LicencePage());
            changeButtonColors(btnShowLicences);
        }

        private void btnShowCars_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new CarPage());
            changeButtonColors(btnShowCars);
        }

        private void btnShowFines_Click(object sender, RoutedEventArgs e)
        {
            //changeButtonColors(btnShowFines);
        }

        private void btnShowAccidents_Click(object sender, RoutedEventArgs e)
        {
            //changeButtonColors(btnShowAccidents);
        }

    }
}
