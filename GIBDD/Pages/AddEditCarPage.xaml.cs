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
    /// Логика взаимодействия для AddEditCarPage.xaml
    /// </summary>
    public partial class AddEditCarPage : Page
    {
        private Cars _currentCar = new Cars();
        public AddEditCarPage(Cars selectedCar)
        {
            InitializeComponent();

            if (selectedCar != null)
                _currentCar = selectedCar;

            DataContext = _currentCar;
            cmbColor.ItemsSource = GIBDDEntities.GetContext().CarColors.ToList();
            cmbEngineType.ItemsSource = GIBDDEntities.GetContext().EngineTypes.ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            var stackPanels = grid.Children.OfType<StackPanel>().ToList();
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

            _currentCar.CarColors = cmbColor.SelectedItem as CarColors;
            _currentCar.EngineTypes = cmbEngineType.SelectedItem as EngineTypes;

            if (_currentCar.Id == 0)
            {
                GIBDDEntities.GetContext().Cars.Add(_currentCar);
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
