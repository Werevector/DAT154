using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Assignment7
{
    class DAO
    {
        DataContext connection;

        public DAO(String dataBaseInfo)
        {
            connection = new DataContext(dataBaseInfo);
        }

        public Table<Student> Fetch()
        {
            Table<Student> students = connection.GetTable<Student>();
            return students;
        }

    }
}
