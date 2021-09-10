using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.SqlServer;
using System.Threading.Tasks;

namespace tourplanner_DAL
{
    abstract class object_Reader_Base<T>
    {
        public CommandType CommandType { get; set; }
        public string CommandText { get; set; }
        protected abstract IDbConnection GetConnection();
        protected abstract mapper_Base<T> GetMapper();
        protected abstract Collection<IDataParameter> GetParameters(IDbCommand command);

        public Collection<T> Execute()
        {
            Collection<T> collection = new Collection<T>();
            using (IDbConnection connection = GetConnection())
            {
                IDbCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandType = this.CommandType;
                command.CommandText = this.CommandText;
                foreach (IDataParameter param in this.GetParameters(command))
                    command.Parameters.Add(param);
                try
                {
                    connection.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        try
                        {
                            mapper_Base<T> mapper = GetMapper();
                            collection = mapper.MapAll(reader);
                            return collection;
                        }
                        catch
                        {
                            throw;
                        }
                        finally
                        {
                            reader.Close();
                        }
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
