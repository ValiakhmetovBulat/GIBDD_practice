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
        public CarPage(Drivers driver = null)
        {
            InitializeComponent();

            if (driver != null)
            {
                var currentCar = GIBDDEntities.GetContext().Cars.ToList();
                currentCar = currentCar.Where(p => p.Id.ToString().Equals(driver.Car.ToString())).ToList();

                dgCars.ItemsSource = currentCar;
            }
            else 
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

        private void btnDeleteCars_Click(object sender, RoutedEventArgs e)
        {
            var carsForRemoving = dgCars.SelectedItems.Cast<Cars>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить выделенные {carsForRemoving.Count} элементов ?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    GIBDDEntities.GetContext().Cars.RemoveRange(carsForRemoving);
                    GIBDDEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены");

                    dgCars.ItemsSource = GIBDDEntities.GetContext().Cars.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnAddCar_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditCarPage(null));
        }
    }
}
