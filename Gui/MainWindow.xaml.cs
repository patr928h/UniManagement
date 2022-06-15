using DataAccess;
using Entities;
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
namespace Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Repository repo;
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                repo = new();
            }
            catch (Exception e)
            {

                MessageBox.Show($"Fejl under tilgang til data: {e.Message}", "Opstartsfejl", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
            List<ContactInformations> contactInformation = repo.GetContactInformation();
            List<Address> Address = repo.GetAllAddresses();
            DataGrid.ItemsSource = repo.GetAllPersons();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string mailInput = "person@mail.com";
             string phoneNumberInput = "34567987";
            ContactInformations contactInformation = new()
            {
                Mail = mailInput,
                PhoneNumber = phoneNumberInput
            };

            repo.AddNewContactInformation(contactInformation);
        }
    }
}
