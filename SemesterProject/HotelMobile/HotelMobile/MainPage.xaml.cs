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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HotelMobile
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void maintainerBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MaintainerPage));
        }

        private void serviceBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ServicePage));
        }

        private void cleanerBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CleaningPage));
        }
    }
}
