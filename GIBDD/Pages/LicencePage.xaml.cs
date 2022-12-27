using GIBDD.Classes;
using GIBDD.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для LicencePage.xaml
    /// </summary>
    public partial class LicencePage : Page
    {
        public LicencePage(Drivers driver = null)
        {
            InitializeComponent();

            if (driver != null)
            {
                var currentlicence = GIBDDEntities.GetContext().Licences.ToList();
                currentlicence = currentlicence.Where(p => p.Id.ToString().Equals(driver.Licence.ToString())).ToList();

                dgLicences.ItemsSource = currentlicence;
            }
            else
                dgLicences.ItemsSource = GIBDDEntities.GetContext().Licences.ToList();
            
        }

        private void dgLicences_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditLicencePage((sender as Button).DataContext as Licences));
        }

        private void btnFindDriver_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new MainPage((sender as Button).DataContext as Licences));
        }

        private void btnAddLicence_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditLicencePage(null));
        }
    }
}
