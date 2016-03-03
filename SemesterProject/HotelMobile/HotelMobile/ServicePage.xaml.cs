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
    public sealed partial class ServicePage : Page
    {

        public IEnumerable<Service> serviceList = null;
        Service currentSrv = null;

        public ServicePage()
        {
            this.InitializeComponent();
            refresh();
            updateListView();
        }

        private void listView_ItemClick(object sender, ItemClickEventArgs e)
        {
            currentSrv = (Service)e.ClickedItem;
            textBlock.Text = currentSrv.note;
        }

        private async void appBarEdit_Click(object sender, RoutedEventArgs e)
        {
            if (currentSrv != null)
            {
                currentSrv.note = textBlock.Text;

                await currentSrv.editModelData();
                refresh();
                updateListView();
            }
        }

        private void appBarHome_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void refresh()
        {
            serviceList = RestServerData.getServiceList();
        }

        private void updateListView()
        {
            listView.ItemsSource = serviceList;
        }

        private void appBarDelete_Click(object sender, RoutedEventArgs e)
        {
            if (currentSrv != null)
            {
                currentSrv.deleteModel();
                currentSrv = null;
                refresh();
                updateListView();
            }
        }
    }
}
