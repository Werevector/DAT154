using HotelDesktopApp.Controlls;
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

using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;

namespace HotelDesktopApp
{
    /// <summary>
    /// Interaction logic for EditCustomerWindow.xaml
    /// </summary>
    public partial class EditCustomerWindow : Window
    {
        public Customer customer = null;
        public string URL = "http://92.221.124.167";

        public EditCustomerWindow(Customer c)
        {
            InitializeComponent();
            customer = c;

            nameBox.Text = customer.name;
            cardNumberBox.Text = customer.cardNr.ToString();
            datePicker.SelectedDate = DateTime.Parse(customer.birthDate);
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            customer.name = nameBox.Text;
            customer.birthDate = datePicker.SelectedDate.Value.ToString("yyyy-MM-dd");
            customer.cardNr = long.Parse(cardNumberBox.Text);

            sendCustomerData();
        }

        private async void sendCustomerData()
        {
            if (customer.name != null && customer.birthDate != null && customer.cardNr != 0)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);
                HttpResponseMessage response = await client.PutAsJsonAsync("/rest/edit/customer", customer).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
                this.Close();
            }
        }
    }
}
