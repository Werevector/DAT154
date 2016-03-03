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
    public sealed partial class MaintainerPage : Page
    {
        public IEnumerable<Maintenance> maintenanceList = null;
        Maintenance currentMt = null;

        public MaintainerPage()
        {
            this.InitializeComponent();
            
            refresh();
            updateListView();

        }

        private void listView_ItemClick(object sender, ItemClickEventArgs e)
        {
            currentMt = (Maintenance)e.ClickedItem;

            textBlock.Text = currentMt.mtr_Note;
            statusBox.SelectedValue = currentMt.mtr_status;
        }

        private async void editBtn_Click(object sender, RoutedEventArgs e)
        {


            if (currentMt != null)
            {
                currentMt.mtr_Note = textBlock.Text;
                currentMt.mtr_status = statusBox.SelectedValue.ToString();

                if (currentMt.mtr_status != "FINISHED")
                {
                    await currentMt.editModelData();
                }
                else
                {
                    currentMt.deleteModel();
                    currentMt = null;
                }
                refresh();
                updateListView();
            }
        }

        private void refresh()
        {
            maintenanceList = RestServerData.getMaintenanceList();
        }

        private void updateListView()
        {
            listView.ItemsSource = maintenanceList;
        }

        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
