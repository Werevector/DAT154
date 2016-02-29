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

using HotelDesktopApp.Controlls;

namespace HotelDesktopApp
{
    /// <summary>
    /// Interaction logic for CustomerInfoWindow.xaml
    /// </summary>

    public partial class CustomerInfoWindow : Window
    {
        Customer selectedCustomer = null;

        public CustomerInfoWindow(Customer customer)
        {
            selectedCustomer = customer;
            InitializeComponent();
            InitFields();

        }

        private void InitFields()
        {
            idLbl.Content = selectedCustomer.cstmr_ID;
            birthLbl.Content = selectedCustomer.birthDate.Substring(0, 10);
            cardLbl.Content = selectedCustomer.cardNr;
            nameLbl.Content = selectedCustomer.name;
        }

        private void editCustmrBtn_Click(object sender, RoutedEventArgs e)
        {
            EditCustomerWindow ecw = new EditCustomerWindow(selectedCustomer);
            ecw.Show();
            this.Close();
        }
    }
}
