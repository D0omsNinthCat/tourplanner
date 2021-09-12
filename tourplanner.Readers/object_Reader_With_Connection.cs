using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tourplanner.Readers
{
    public abstract class object_Reader_With_Connection<T> : object_Reader_Base<T>
    {
        private static string connection_String = @"Host=localhost;Username=postgres;Password=1234;Database=tourplanner";

        protected override IDbConnection GetConnection()
        {
            return new NpgsqlConnection(connection_String);
        }
    }
}