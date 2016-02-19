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

        public List<Student> searchForStudent (String name)
        {
            //fra String til dao passende type ??
            // dao.searchForStudents(name)
            // fra dao type tilbake til liste std2, dette er det viktigste denne metoden gjør
            return null;
        }

        public List<Student> selectCourse(String course)
        {
            //List<Student> std = dao.selectCourse(course);

            return null;
        }

        public List<String> showGrades(String grade)
        {
            //List grades = dao.getGradesBetterThanThis(grade)
            return null;
        }

        public void ListFailures(String grade)
        {
            // hmm, hvordan gjøre denne bedre??
            //List<Student> std = dao.listStudentsByGrade(grade);
            //View.displayStudentAttribute(std[i])
        }

        // View logikk videre her, hvis view ønsker å gjøre noe som krever logikk ...

    }
}
