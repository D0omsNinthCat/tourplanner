using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace tourplanner.Readers
{
    public abstract class object_Reader_With_Connection<T> : object_Reader_Base<T>
    {
        private string connection_String = ConfigurationManager.AppSettings.Get("connectionstring");

        protected override IDbConnection GetConnection()
        {
            return new NpgsqlConnection(connection_String);
        }
    }
}