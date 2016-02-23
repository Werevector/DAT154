using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment7
{
    class TestObject
    {
        public string field1 { get; set;}
        public int field2 { get; set; }

        public TestObject(string field1, int field2)
        {
            this.field1 = field1;
            this.field2 = field2;
        }
    }

    class DummyDatabase
    {
        public static List<TestObject> database = new List<TestObject>
        {
            new TestObject("Banan",10),
            new TestObject("Eple",6)
        };

        public List<TestObject> getDB()
        {
            return database;
        }
    }

    class Test
    {
        public void Q()
        {
            DummyDatabase test = new DummyDatabase();
            List<TestObject> database = test.getDB();
            IEnumerable<TestObject> testQuery =
            from testObject in database
            where testObject.field1 == "Eple"
            select testObject;
            foreach (var a in testQuery)
            {
                System.Console.WriteLine(a.field2);
            }


        }
    }
}
