using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Temp fil for controller, vil fikse på hvor den faktisk skal være osv ... ...
// Oppsettet er fra view-controller-dao, som betyr at view sender til controller det som er best for view
// controller tar dette og formaterer det til det som er best for dao, dao sender så videre til databasen

namespace Assignment7
{
    class Controller
    {

        public delegate void modelChanged(List<Student> data);
        public event modelChanged modelChangeEvent;
        
        //DUMMY
        List<Student> sdata = new List<Student>();

        public Controller()
        {
            //DUMMY
            Student s = new Student();
            Student s2 = new Student("Tore", "C", "DAT150");
            sdata.Add(s);
            sdata.Add(s2);
        }

        public void searchForStudent (String name)
        {
            //fra String til dao passende type ??
            // dao.searchForStudents(name)
            // fra dao type tilbake til liste std2, dette er det viktigste denne metoden gjør
        }

        public void getAllStudent()
        {
            if (modelChangeEvent != null)
            {
                modelChangeEvent(sdata);
            }
        }

        public void selectCourse(String course)
        {
            //List<Student> std = dao.selectCourse(course);

        }

        public void showGrades(String grade)
        {
            //List grades = dao.getGradesBetterThanThis(grade)
        }

        public void ListFailures()
        {
            // hmm, hvordan gjøre denne bedre??
            //List<Student> std = dao.listStudentsByGrade(grade);
            //View.displayStudentAttribute(std[i])
        }

    }
}
