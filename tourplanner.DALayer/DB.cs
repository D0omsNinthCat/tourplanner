using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourplanner.Models;
using Npgsql;
using System.Configuration;
using System.Data;

namespace tourplanner.DALayer
{
    public class DB : DB_Interface
    {
        private static DB database_instance;
        private NpgsqlConnection connection;
        private string connectionstring = ConfigurationManager.AppSettings.Get("connectionstring");

        private DB()
        {
            try
            {
                 connection = new NpgsqlConnection(ConfigurationManager.AppSettings.Get("connectionstring"));
            }
            catch(Exception)
            {
                throw;
                //SAY SOMETHING
            }
        }
        public static DB Database_instance(string sth="")
        {
            try
            {
                database_instance = new DB();
                database_instance.connectionstring = sth; //Rework later maybe
                
                return database_instance;
            }
            catch(Exception)
            {
                throw;
                //SAY SOMETHING
            }
            
        }

        public List<Tour> GetTourList()
        {
            try
            {
                string sql_command = "SELECT * FROM tours";
                List<Tour> tours = new List<Tour>();
                NpgsqlCommand command = new NpgsqlCommand(sql_command, connection);
                connection.Open();
                //IDataReader reader = command.ExecuteReader();

                
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Tour t = new Tour();
                        t.tour_ID = (DBNull.Value == reader["tour_ID"]) ? 0 : (int)reader["tour_ID"];
                        t.tour_Name = (DBNull.Value == reader["tour_Name"]) ? string.Empty : (string)reader["tour_Name"];
                        t.tour_Description = (DBNull.Value == reader["tour_Description"]) ? string.Empty : (string)reader["tour_Description"];
                        t.tour_Distance = (DBNull.Value == reader["tour_Distance"]) ? 0 : (double)reader["tour_Distance"];
                        tours.Add(t);
                    }
                    return tours;
                }
            }
            catch(Exception)
            {
                return new List<Tour>();
                throw;
                //SAY SOMETHING
            }
        }
    }
}
