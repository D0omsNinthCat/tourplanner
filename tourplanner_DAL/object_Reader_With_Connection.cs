using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tourplanner.DAL
{
    abstract class ObjectReaderWithConnection<T> : object_Reader_Base<T>
    {
        private static string m_connectionString = @"Host=localhost;Username=postgres;Password=1234;Database=tourplanner";
        protected override IDbConnection GetConnection()
        {
            return new SqlConnection(m_connectionString);
        }
    }
}
