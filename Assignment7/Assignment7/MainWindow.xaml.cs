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

namespace Assignment7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Controller DAO = new Controller();

            /**
            the controller object should connect to a specific model somehow, probably subscribing to a couple of functions and such.
            Not shure how this should be designed tough, lets not think too much around it.
            **/

            //this._studentGrid.ItemsSource = DAO.getAllStudents();
        }

    }

}
