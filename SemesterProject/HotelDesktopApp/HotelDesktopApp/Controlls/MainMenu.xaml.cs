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
        public int cstmr_ID { get; set; }
        public string name { get; set; }
        public string birthDate { get; set; }
        public long cardNr { get; set; }
    }

    public class Maintenance
    {
        public int mtr_ID { get; set; }
        public string mtr_Note { get; set; }
        public string mtr_status { get; set; }
        public int mtr_room { get; set; }
    }

    public class Service
    {
        public int srv_ID { get; set; }
        public string note { get; set; }
        public int room_ID { get; set; }
    }


    public partial class MainMenu : UserControl
    {
        public IEnumerable<Room> roomObjects;
        public IEnumerable<Reservation> reservationObjects;
        public IEnumerable<Customer> customerObjects;
        public IEnumerable<Service> serviceObjects;
        public IEnumerable<Maintenance> maintenanceObjects;

        public Reservation currentResv = null;
        public Customer currentCustomer = null;
        public Room selRoom = null;

        public string URL = "http://92.221.124.167/rest/";

        public MainMenu()
        {
            InitializeComponent();
            roomDataGrid.AutoGenerateColumns = false;
            pollRestSrvr();

        }

        private void pollRestSrvr()
        {
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

            response = client.GetAsync("listing/services").Result;

            if (response.IsSuccessStatusCode)
            {
                serviceObjects = response.Content.ReadAsAsync<IEnumerable<Service>>().Result;
            }

            response = client.GetAsync("listing/maintenance").Result;

            if (response.IsSuccessStatusCode)
            {
                maintenanceObjects = response.Content.ReadAsAsync<IEnumerable<Maintenance>>().Result;
            }

        }

        private void populateServices(Room room)
        {
            foreach (var item in serviceObjects)
            {
                if(item.room_ID == room.room_ID)
                {

                    StackPanel service = new StackPanel();
                    service.Orientation = Orientation.Vertical;
                    service.HorizontalAlignment = HorizontalAlignment.Stretch;
                    service.VerticalAlignment = VerticalAlignment.Stretch;

                    Label lbl = new Label();
                    lbl.Content = "ID: " + item.srv_ID + " ";

                    TextBlock txtBlock = new TextBlock();
                    txtBlock.Text = item.note;
                    txtBlock.TextWrapping = TextWrapping.Wrap;

                    service.Children.Add(lbl);
                    service.Children.Add(txtBlock);

                    serviceStack.Children.Add(service);

                }
                
            }
           
        }

        private void populateMaintenance(Room room)
        {
            foreach (var item in maintenanceObjects)
            {
                if (item.mtr_ID == room.room_ID)
                {

                    StackPanel maintenance = new StackPanel();
                    maintenance.Orientation = Orientation.Vertical;
                    maintenance.HorizontalAlignment = HorizontalAlignment.Stretch;
                    maintenance.VerticalAlignment = VerticalAlignment.Stretch;

                    Label lbl = new Label();
                    lbl.Content = "ID: " + item.mtr_ID + " ";

                    TextBlock txtBlock = new TextBlock();
                    txtBlock.Text = item.mtr_Note;
                    txtBlock.TextWrapping = TextWrapping.Wrap;

                    maintenance.Children.Add(lbl);
                    maintenance.Children.Add(txtBlock);

                    maintenanceStack.Children.Add(maintenance);

                }

            }
        }

        private void clearServices()
        {
            serviceStack.Children.Clear();
        }

        private void clearMaintenance()
        {
            maintenanceStack.Children.Clear();
        }

        private void populateSelectionData()
        {
            selRoom = (Room)roomDataGrid.SelectedItem;

            if (selRoom != null)
            {

                currentCustomer = null;
                currentResv = null;
                clearServices();
                clearMaintenance();

                foreach (var item in reservationObjects)
                {
                    if (item.rsvr_rID == selRoom.room_ID) { currentResv = item; }
                }

                if (currentResv != null)
                {

                    foreach (var item in customerObjects)
                    {
                        if (item.cstmr_ID == currentResv.cstmr_ID) { currentCustomer = item; }
                    }

                    reservedLbl.Content = "True";
                    reservedLbl.Foreground = new SolidColorBrush(Colors.Green);

                    isReserved.Header = "True";
                    isReserved.Foreground = new SolidColorBrush(Colors.Green);

                    resvDetailName.Content = "Name";
                    resvDetailFrom.Content = currentResv.startDate.Substring(0, 10);
                    resvDetailTo.Content = currentResv.endDate.Substring(0, 10);
                    resvStatus.Header = currentResv.status;
                    resvStatus.Foreground = new SolidColorBrush(Colors.Black);
                    resvDetailName.Content = currentCustomer.name;

                    populateServices(selRoom);
                    populateMaintenance(selRoom);

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
                    populateMaintenance(selRoom);
                }
            }
        }

        private void roomDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            populateSelectionData();
        }

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            pollRestSrvr();
        }

        private void customerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentCustomer != null)
            {
                CustomerInfoWindow ciw = new CustomerInfoWindow(currentCustomer);
                ciw.Show();
            }
        }

        private void EditReservationBtn_Click(object sender, RoutedEventArgs e)
        {
            if(currentResv != null)
            {
                ReservationEditorWindow rew = new ReservationEditorWindow(currentResv);
                rew.ShowDialog();
                pollRestSrvr();
                populateSelectionData();
            }
            else
            {
                if (selRoom != null)
                {
                    ReservationAdd rea = new ReservationAdd(selRoom);
                    rea.ShowDialog();
                    pollRestSrvr();
                    populateSelectionData();
                }
            }
        }
    }
}
