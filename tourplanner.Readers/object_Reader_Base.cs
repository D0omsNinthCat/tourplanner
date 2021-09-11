using System;
using System.Collections.ObjectModel;
using System.Data;
using tourplanner.Mappers;

namespace tourplanner.Readers
{
    public abstract class object_Reader_Base<T>
    {
        protected abstract string CommandText { get; }
        protected abstract CommandType CommandType { get; }
        protected abstract IDbConnection GetConnection();
        protected abstract mapper_Base<T> GetMapper();
        protected abstract Collection<IDataParameter> GetParameters(IDbCommand command);

        public Collection<T> Execute()
        {
            Collection<T> collection = new Collection<T>();
            using (IDbConnection connection = GetConnection())
            {
                IDbCommand command = connection.CreateCommand();
                command.CommandText = CommandText;
                command.CommandType = CommandType;
                command.Connection = connection;

                foreach (IDataParameter parameter in GetParameters(command))
                {
                    command.Parameters.Add(parameter);
                }

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
