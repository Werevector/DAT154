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
    /// Interaction logic for RegisterServMaintWindow.xaml
    /// </summary>
    public partial class RegisterServMaintWindow : Window
    {
        Maintenance mtnc = null;
        Service srv = null;
        bool mtncMode = false; //Set ServiceMode as initial mode
        public string URL = "http://92.221.124.167/rest/";

        int id = 0;

        /// <summary>
        /// set mode to false for service, true for maintenance
        /// </summary>
        /// <param name="roomID"></param>
        /// <param name="mode"></param>
        public RegisterServMaintWindow(int roomID, bool mode)
        {
            InitializeComponent();
            id = roomID;
            mtncMode = mode;
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            if (mtncMode)
            {
                mtnc = new Maintenance();
                mtnc.mtr_room = id;
                mtnc.mtr_Note = noteBox.Text;
                registerMtncData();
            }
            else
            {
                srv = new Service();
                srv.room_ID = id;
                srv.note = noteBox.Text;
                registerSrvData();
            }
        }

        private async void registerMtncData()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            HttpResponseMessage response = await client.PostAsJsonAsync("/rest/add/Maintenance", mtnc).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            this.Close();
        }

        private async void registerSrvData()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            HttpResponseMessage response = await client.PostAsJsonAsync("/rest/add/Service", srv).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            this.Close();
        }
    }
}
