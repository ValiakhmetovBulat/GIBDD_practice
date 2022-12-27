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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage(Licences licence = null, Cars car = null)
        {            
            InitializeComponent();            

            if (licence != null)
            {
                var currentDrivers = GIBDDEntities.GetContext().Drivers.ToList();
                currentDrivers = currentDrivers.Where(p => p.Licence.ToString().Equals(licence.Id.ToString())).ToList();

                lvDrivers.ItemsSource = currentDrivers;
            }
            else if (car != null)
            {
                var currentDrivers = GIBDDEntities.GetContext().Drivers.ToList();
                currentDrivers = currentDrivers.Where(p => p.Car.ToString().Equals(car.Id.ToString())).ToList();

                lvDrivers.ItemsSource = currentDrivers;
            }
            else
                lvDrivers.ItemsSource = GIBDDEntities.GetContext().Drivers.ToList();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _find = tbSearch.Text.ToLower();

            if (!(string.IsNullOrEmpty(_find)))
            {
                var _drivers = GIBDDEntities
                    .GetContext()
                    .Drivers
                    .OrderBy(x => x.Name)
                    .ToList();
                _drivers = _drivers
                    .OrderBy(x => x.Name)
                    .Where(x => (x.Surname.ToLower().Contains(_find)) ||
                    (x.Name.ToLower().Contains(_find)) ||
                    (x.MiddleName.ToLower().Contains(_find)) ||
                    (x.PassportNumber.ToLower().Contains(_find)) ||
                    (x.PassportSerial.ToLower().Contains(_find)) ||
                    (x.PostCode.ToLower().Contains(_find)) ||
                    (x.Address.ToLower().Contains(_find)) ||
                    (x.Company.ToLower().Contains(_find)) ||
                    (x.JobName.ToLower().Contains(_find)) ||
                    (x.Phone.ToLower().Contains(_find)) ||
                    (x.Email.ToLower().Contains(_find)))
                    .ToList();
                lvDrivers.ItemsSource = _drivers;
            }
            else
            {
                lvDrivers.ItemsSource = GIBDDEntities.GetContext().Drivers.OrderBy(x => x.Name).ToList();
            }
        }

        private void btnEditDriver_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditDriversPage((sender as Button).DataContext as Drivers));
        }

        private void AddDriver_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditDriversPage(null));
        }

        private void DeleteDriver_Click(object sender, RoutedEventArgs e)
        {
            var selectedDriver = lvDrivers.SelectedItems.Cast<Drivers>().ToList();

            if (selectedDriver.Count == 0)
            {
                MessageBox.Show("Выберите водителя, которого желаете удалить нажатием на карточку");
                return;
            }                

            if ((MessageBox.Show("Выбранный водитель будет удален. Продолжить?", "Удаление водителя", MessageBoxButton.YesNo, MessageBoxImage.Question)) == MessageBoxResult.Yes) 
            {
                GIBDDEntities.GetContext().Drivers.RemoveRange(selectedDriver);
                GIBDDEntities.GetContext().SaveChanges();
                MessageBox.Show("Водитель был успешно удален");
                lvDrivers.ItemsSource = GIBDDEntities.GetContext().Drivers.ToList();
            }
        }

        private void btnFindLicence_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new LicencePage((sender as Button).DataContext as Drivers));
        }

        private void btnFindCar_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new CarPage((sender as Button).DataContext as Drivers));
        }
    }
}
