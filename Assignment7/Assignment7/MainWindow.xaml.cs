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
            StudentViewModel model = new StudentViewModel();
            this._studentGrid.ItemsSource = model.getData();
        }

    }

    public class StudentViewModel
    {

        List<Student> _data = new List<Student>();
        List<Student> Data = new List<Student>();

        public List<Student> getData()
        {
            Data = _data;
            return Data;
        }

        public StudentViewModel()
        {
            _data.Add(new Student());
            _data.Add(new Student("Karl" , "C", "INF121"));
            _data.Add(new Student("Anne", "B", "INF121"));
        }

    }

    public class Student
    {
        public String Name { get; set; }
        public String Grade { get; set; }
        public String Class { get; set; }

        public Student()
        {
            Name = "Tore";
            Grade = "A";
            Class = "DAT101";
        }
        public Student(String n, String g, String c)
        {
            Name = n;
            Grade = g;
            Class = c;
        }
    }
}
