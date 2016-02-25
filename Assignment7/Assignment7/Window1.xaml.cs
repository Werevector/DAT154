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

namespace Assignment7
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1(DataClasses1DataContext database, string c)
        {

            InitializeComponent();
            var t = from stud in database.students
                    join grade in database.grades on stud.id equals grade.studentid
                    join course in database.courses on grade.coursecode equals course.coursecode
                    where course.coursename.Equals(c)
                    select new
                    {
                        Grade = grade.grade1,
                        Course = course.coursename,
                        Student = stud.studentname,
                    };
            courseList.ItemsSource = t;
        }
    }
}
