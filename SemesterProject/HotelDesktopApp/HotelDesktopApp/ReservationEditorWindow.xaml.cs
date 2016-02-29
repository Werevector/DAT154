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
    /// Interaction logic for ReservationEditorWindow.xaml
    /// </summary>
    public partial class ReservationEditorWindow : Window
    {
        Reservation reservation = new Reservation();

        public ReservationEditorWindow(Reservation reservation)
        {
            InitializeComponent();

            this.reservation = reservation;

            idLabel.Content = reservation.rsvr_ID;
            idLabel.Foreground = new SolidColorBrush(Colors.DarkRed);
            fromDate.SelectedDate = DateTime.Parse(reservation.startDate);
            toDate.SelectedDate = DateTime.Parse(reservation.endDate);
            statusBox.SelectedValue = reservation.status;

        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            reservation.startDate = fromDate.SelectedDate.Value.ToString("yyyy-MM-dd");
            reservation.endDate = toDate.SelectedDate.Value.ToString("yyyy-MM-dd");
            reservation.status = statusBox.SelectedValue.ToString();

            sendReservationDataEdit();
          

        }
        private async void sendReservationDataEdit()
        {
            HttpClient client = new HttpClient();
            string URL = "http://92.221.124.167";
            client.BaseAddress = new Uri(URL);
            HttpResponseMessage response = await client.PutAsJsonAsync("/rest/edit/reservation", reservation).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            this.Close();
        }

    }
}
