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
using System.Data.Linq;

namespace Assignment7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {

        DataClasses1DataContext database = new DataClasses1DataContext();

        public MainWindow()
        {
            InitializeComponent();
            /**
            the controller object should connect to a specific model somehow, probably subscribing to a couple of functions and such.
            Not shure how this should be designed tough, lets not think too much around it.
            **/

            //this._studentGrid.ItemsSource = DAO.getAllStudents();
        }

        private void courseButton_Click(object sender, RoutedEventArgs e)
        {
            var t = from courses in database.courses
                    select new
                    {
                        CourseID = courses.coursecode,
                        Course = courses.coursename,
                        Semester = courses.semester,
                        Teacher = courses.teacher,
                    };
            studentView.ItemsSource = t;

        }

        private void failButton_Click(object sender, RoutedEventArgs e)
        {
            var t = from stud in database.students
                    join grad in database.grades on stud.id equals grad.studentid
                    join c in database.courses on grad.coursecode equals c.coursecode
                    where grad.grade1.Equals('F')
                    select new
                    {
                        Name = stud.studentname,
                        Course = c.coursename,
                        Grade = grad.grade1,
                    };
            studentView.ItemsSource = t;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            string search = textBox.Text.ToLower();
            var t = from s in database.students
                    where s.studentname.ToLower().Contains(search)
                    select new
                    {
                        ID = s.id,
                        Name = s.studentname,
                        Age = s.studentage,
                    };

            studentView.ItemsSource = t;
        }

 

        private void studentView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var c = studentView.SelectedItem;

            if (c != null && c.GetType().GetProperty("CourseID") != null) { 
                System.Type type = c.GetType();
                string coursename = (string)type.GetProperty("Course").GetValue(c, null);
                Window1 win = new Window1(database, coursename);
                win.ShowDialog();
            }
        }

        private void gradeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int value = gradeList.SelectedIndex;
            switch (value)
            {
                case 1:
                    var t = from stud in database.students
                            join grade in database.grades on stud.id equals grade.studentid
                            join c in database.courses on grade.coursecode equals c.coursecode
                            where grade.grade1.CompareTo('A') <= 0
                            select new
                            {
                                Grade = grade.grade1,
                                Course = c.coursename,
                                Student = stud.studentname,
                            };
                    studentView.ItemsSource = t;
                    break;
                case 2:
                    t = from stud in database.students
                        join grade in database.grades on stud.id equals grade.studentid
                        join c in database.courses on grade.coursecode equals c.coursecode
                        where grade.grade1.CompareTo('B') <= 0
                        select new
                        {
                            Grade = grade.grade1,
                            Course = c.coursename,
                            Student = stud.studentname,
                        };
                    studentView.ItemsSource = t;
                    break;
                case 3:
                    t = from stud in database.students
                        join grade in database.grades on stud.id equals grade.studentid
                        join c in database.courses on grade.coursecode equals c.coursecode
                        where grade.grade1.CompareTo('C') <= 0
                        select new
                        {
                            Grade = grade.grade1,
                            Course = c.coursename,
                            Student = stud.studentname,
                        };
                    studentView.ItemsSource = t;
                    break;
                case 4:
                    t = from stud in database.students
                        join grade in database.grades on stud.id equals grade.studentid
                        join c in database.courses on grade.coursecode equals c.coursecode
                        where grade.grade1.CompareTo('D') <= 0
                        select new
                        {
                            Grade = grade.grade1,
                            Course = c.coursename,
                            Student = stud.studentname,
                        };
                    studentView.ItemsSource = t;
                    break;
                case 5:
                    t = from stud in database.students
                        join grade in database.grades on stud.id equals grade.studentid
                        join c in database.courses on grade.coursecode equals c.coursecode
                        where grade.grade1.CompareTo('E') <= 0
                        select new
                        {
                            Grade = grade.grade1,
                            Course = c.coursename,
                            Student = stud.studentname,
                        };
                    studentView.ItemsSource = t;
                    break;
                default:
                    t = from stud in database.students
                        join grade in database.grades on stud.id equals grade.studentid
                        join c in database.courses on grade.coursecode equals c.coursecode
                        select new
                        {
                            Grade = grade.grade1,
                            Course = c.coursename,
                            Student = stud.studentname,
                        };
                    studentView.ItemsSource = t;
                    break;
            }

        }
    }
}
