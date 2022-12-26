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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GIBDD.Pages
{
    /// <summary>
    /// Логика взаимодействия для CarPage.xaml
    /// </summary>
    public partial class CarPage : Page
    {
        public CarPage()
        {
            InitializeComponent();
            dgCars.ItemsSource = GIBDDEntities.GetContext().Cars.ToList();
            
        }

        private void dgCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnFindDriver_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new MainPage(null, (sender as Button).DataContext as Cars));
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditCarPage((sender as Button).DataContext as Cars));
        }
    }
}
