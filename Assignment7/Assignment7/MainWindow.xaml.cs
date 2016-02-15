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
        delegate int modelChangedDelegate(List<Student> data);

        Controller control;

        public MainWindow()
        {
            InitializeComponent();
            control = new Controller();
            control.modelChangeEvent += modelChanged;

            /**
            the controller object should connect to a specific model somehow, probably subscribing to a couple of functions and such.
            Not shure how this should be designed tough, lets not think too much around it.
            **/

            //this._studentGrid.ItemsSource = DAO.getAllStudents();
        }

        private void courseButton_Click(object sender, RoutedEventArgs e)
        {
            control.getAllStudent();
            control.selectCourse("ING101");
        }

        private void failButton_Click(object sender, RoutedEventArgs e)
        {
            control.ListFailures();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void modelChanged(List<Student> data)
        {
            studentView.ItemsSource = data;
        }
    }

}
