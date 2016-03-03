using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using HotelMobile.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HotelMobile
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CleaningPage : Page
    {
        IEnumerable<Reservation> reservationList = null;
        Reservation currentRsv = null;

        public CleaningPage()
        {
            this.InitializeComponent();
            refresh();
            updateListView();
        }

        private void cleanedButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentRsv != null)
            {
                currentRsv.deleteModel();
                currentRsv = null;
                refresh();
                updateListView();
            }
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void listView_ItemClick(object sender, ItemClickEventArgs e)
        {
            currentRsv = (Reservation)e.ClickedItem;
        }

        private void refresh()
        {
            reservationList = RestServerData.getCheckedReservationList();
        }

        private void updateListView()
        {
            listView.ItemsSource = reservationList;
        }
    }
}
