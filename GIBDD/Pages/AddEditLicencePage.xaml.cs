using GIBDD.Classes;
using GIBDD.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для AddEditLicencePage.xaml
    /// </summary>
    public partial class AddEditLicencePage : Page
    {

        private Licences _currentLicence = new Licences();
        public AddEditLicencePage(Licences selectedLicence)
        {
            InitializeComponent();

            if (selectedLicence != null)
                _currentLicence = selectedLicence;

            if (_currentLicence.Id == 0)
            {
                _currentLicence.LicenceDate = DateTime.Today;
                _currentLicence.ExpireDate = DateTime.Today;
            }

            DataContext = _currentLicence;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            var stackPanels = grid.Children.OfType<StackPanel>();
            List<TextBox> textBoxes = new List<TextBox>();

            foreach (StackPanel sp in stackPanels)
            {
                textBoxes.AddRange(sp.Children.OfType<TextBox>());
            }

            foreach (var item in textBoxes)
            {
                if (string.IsNullOrWhiteSpace(item.Text) && item.Name != "tbDescription")
                {
                    errors.AppendLine($"Поле: {item.Name} не может быть пустым");
                }
            }

            if (_currentLicence.LicenceDate > DateTime.Now)
                errors.AppendLine("Дата регистрации не может находится в будущем");

            if (_currentLicence.LicenceDate > _currentLicence.ExpireDate)
                errors.AppendLine("Дата регистрации не может быть позже даты истечения");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentLicence.Id == 0)
            {
                GIBDDEntities.GetContext().Licences.Add(_currentLicence);
            }


            try
            {
                MessageBox.Show(_currentLicence.LicenceDate.ToString());
                MessageBox.Show(_currentLicence.ExpireDate.ToString());
                GIBDDEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.Navigate(new LicencePage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
