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
    /// Interaction logic for ReservationAdd.xaml
    /// </summary>
    public partial class ReservationAdd : Window
    {
        Room roomToReserve = null;
        Customer resv_Customer = null;
        Reservation createdReservation = new Reservation();

        public IEnumerable<Customer> customers;

        public string URL = "http://92.221.124.167/rest/";

        public ReservationAdd(Room room)
        {
            InitializeComponent();
            roomToReserve = room;
            PollCustomers();

        }

        private void PollCustomers()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            HttpResponseMessage response = client.GetAsync("listing/customers").Result;

            if (response.IsSuccessStatusCode)
            {
                customers = response.Content.ReadAsAsync<IEnumerable<Customer>>().Result;
                customerGrid.ItemsSource = customers;
            }
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            resv_Customer = (Customer)customerGrid.SelectedItem;
            if(resv_Customer != null)
            {
                createdReservation.rsvr_ID = 0; //ignored by REST
                createdReservation.cstmr_ID = resv_Customer.cstmr_ID;
                createdReservation.rsvr_rID = roomToReserve.room_ID;
                createdReservation.startDate = fromDate.SelectedDate.Value.ToString("yyyy-MM-dd");
                createdReservation.endDate = toDate.SelectedDate.Value.ToString("yyyy-MM-dd");
                createdReservation.status = ""; //ignored by REST

                sendReservationData();
                

            }

        }

        private async void sendReservationData()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            HttpResponseMessage response = await client.PostAsJsonAsync("/rest/add/reservation", createdReservation).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            this.Close();
        }

        private void addCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomerAdd caw = new CustomerAdd();
            caw.ShowDialog();
            PollCustomers();
        }

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            PollCustomers();
        }
    }

}
