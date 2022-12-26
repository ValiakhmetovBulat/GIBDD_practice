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
    /// Логика взаимодействия для AddEditDriversPage.xaml
    /// </summary>
    public partial class AddEditDriversPage : Page
    {
        private Drivers _currentDriver = new Drivers();
        public AddEditDriversPage(Drivers selectedDriver)
        {
            InitializeComponent();

            if (selectedDriver != null)
                _currentDriver = selectedDriver;

            DataContext = _currentDriver;
            comboLicence.ItemsSource = GIBDDEntities.GetContext().Licences.ToList();       
            comboCar.ItemsSource = GIBDDEntities.GetContext().Cars.ToList();
            
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            var stackPanels = grid.Children.OfType<StackPanel>();
            List<TextBox> textBoxes = new List<TextBox>();
            List<ComboBox> comboBoxes = new List<ComboBox>();
            
            foreach (StackPanel sp in stackPanels)
            {
                textBoxes.AddRange(sp.Children.OfType<TextBox>());
                comboBoxes.AddRange(sp.Children.OfType<ComboBox>());
            }

            foreach (var item in textBoxes)
            {
                if (string.IsNullOrWhiteSpace(item.Text) && item.Name != "tbDescription")
                {
                    errors.AppendLine($"Поле: {item.Name} не может быть пустым");
                }
            }
            foreach (var item in comboBoxes)
            {
                if (item.SelectedItem == null)
                {
                    errors.AppendLine($"Поле: {item.Name} не может быть пустым");
                }
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            _currentDriver.Licences = comboLicence.SelectedItem as Licences;
            _currentDriver.Cars = comboCar.SelectedItem as Cars;

            if (_currentDriver.Id == 0)
            {
                GIBDDEntities.GetContext().Drivers.Add(_currentDriver);
            }

            try
            {
                GIBDDEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.Navigate(new MainPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
