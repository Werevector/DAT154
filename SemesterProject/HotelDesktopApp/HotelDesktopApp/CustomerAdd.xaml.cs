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

namespace HotelDesktopApp
{
    /// <summary>
    /// Interaction logic for CustomerAdd.xaml
    /// </summary>
    public partial class CustomerAdd : Window
    {
        public Customer createdCustomer = new Customer();
        public string URL = "http://92.221.124.167";

        public CustomerAdd()
        {
            InitializeComponent();
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {

            createdCustomer.cstmr_ID = 0; //Ignored by REST
            createdCustomer.name = nameBox.Text;
            createdCustomer.birthDate = datePicker.SelectedDate.Value.ToString("yyyy-MM-dd");
            createdCustomer.cardNr = long.Parse(cardNumberBox.Text);

            sendCustomer();
        }

        private async void sendCustomer()
        {
            if (createdCustomer.name != null && createdCustomer.birthDate != null && createdCustomer.cardNr != 0)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);
                HttpResponseMessage response = await client.PostAsJsonAsync("/rest/add/customer", createdCustomer).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
                this.Close();
            }
        }
    }
}
