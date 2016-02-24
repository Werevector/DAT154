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

    public partial class MainMenu : UserControl
    {
        public IEnumerable<Room> roomObjects;

        public MainMenu()
        {
            InitializeComponent();

            string URL = "http://92.221.124.167/rest/listing/rooms";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            HttpResponseMessage response = client.GetAsync("").Result;

            if (response.IsSuccessStatusCode)
            {
                roomDataGrid.AutoGenerateColumns = false;
                roomObjects = response.Content.ReadAsAsync<IEnumerable<Room>>().Result;
                roomDataGrid.ItemsSource = roomObjects;

            }

        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            Room selRoom = (Room)roomDataGrid.SelectedItem;
            if (selRoom != null)
            {
                Switcher.Switch(new RoomMenu(selRoom));
            }
        }

    }
}
