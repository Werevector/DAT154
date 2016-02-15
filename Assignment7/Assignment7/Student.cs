using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment7
{
    class Student
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
