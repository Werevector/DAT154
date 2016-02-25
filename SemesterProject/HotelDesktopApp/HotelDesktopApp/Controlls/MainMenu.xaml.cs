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

using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;

namespace HotelDesktopApp.Controlls
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>

    public class Room
    {
        public int room_ID { get; set; }
        public int bedsNr { get; set; }
        public string roomSize { get; set; }
        public string pricing { get; set; }
    }

    public class Reservation
    {
        public int rsvr_ID { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string status { get; set; }
        public int cstmr_ID { get; set; }
        public int rsvr_rID { get; set; }
    }

    public class Customer
    {
        public string cstmr_ID { get; set; }
        public string name { get; set; }
        public string birthDate { get; set; }
        public int cardNr { get; set; }
    }

    public partial class MainMenu : UserControl
    {
        public IEnumerable<Room> roomObjects;
        public IEnumerable<Reservation> reservationObjects;
        public IEnumerable<Customer> customerObjects;

        public MainMenu()
        {
            InitializeComponent();

            string URL = "http://92.221.124.167/rest/";

            roomDataGrid.AutoGenerateColumns = false;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            HttpResponseMessage response = client.GetAsync("listing/rooms").Result;

            if (response.IsSuccessStatusCode)
            {
                roomObjects = response.Content.ReadAsAsync<IEnumerable<Room>>().Result;
                roomDataGrid.ItemsSource = roomObjects;
            }

            response = client.GetAsync("listing/reservations").Result;

            if (response.IsSuccessStatusCode)
            {
                reservationObjects = response.Content.ReadAsAsync<IEnumerable<Reservation>>().Result;
            }

            response = client.GetAsync("listing/customers").Result;

            if (response.IsSuccessStatusCode)
            {
                customerObjects = response.Content.ReadAsAsync<IEnumerable<Customer>>().Result;
            }

        }

        private void roomDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Room selRoom = (Room)roomDataGrid.SelectedItem;

            Reservation resv = null;
            foreach(var item in reservationObjects)
            {
                if(item.rsvr_rID == selRoom.room_ID) { resv = item; }
            }

            if (resv != null) {

                reservedLbl.Content = "True";
                reservedLbl.Foreground = new SolidColorBrush(Colors.Green);

                isReserved.Header = "True";
                isReserved.Foreground = new SolidColorBrush(Colors.Green);

                resvDetailName.Content = "Name";
                resvDetailFrom.Content = resv.startDate.Substring(0,10);
                resvDetailTo.Content = resv.endDate.Substring(0, 10);
                resvStatus.Header = resv.status;
                resvStatus.Foreground = new SolidColorBrush(Colors.Black);

            }
            else
            {
                reservedLbl.Content = "False";
                reservedLbl.Foreground = new SolidColorBrush(Colors.Red);

                isReserved.Header = "False";
                isReserved.Foreground = new SolidColorBrush(Colors.Red);

                resvDetailName.Content = "";
                resvDetailFrom.Content = "";
                resvDetailTo.Content = "";
                resvStatus.Header = "";
            }

        }

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://92.221.124.167/rest/");

            HttpResponseMessage response = client.GetAsync("listing/rooms").Result;

            if (response.IsSuccessStatusCode)
            {
                roomObjects = response.Content.ReadAsAsync<IEnumerable<Room>>().Result;
                roomDataGrid.ItemsSource = roomObjects;
            }

            response = client.GetAsync("listing/reservations").Result;

            if (response.IsSuccessStatusCode)
            {
                reservationObjects = response.Content.ReadAsAsync<IEnumerable<Reservation>>().Result;
            }

            response = client.GetAsync("listing/customers").Result;

            if (response.IsSuccessStatusCode)
            {
                customerObjects = response.Content.ReadAsAsync<IEnumerable<Customer>>().Result;
            }
        }
    }
}
