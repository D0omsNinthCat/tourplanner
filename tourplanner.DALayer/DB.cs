using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourplanner.Models;
using Npgsql;
using System.Configuration;
using System.Data;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Windows.Media.Imaging;
using System.IO;
using System.Net;
using System.Drawing;

namespace tourplanner.DALayer
{
    public class DB : DB_Interface
    {
        private static DB database_instance;
        private NpgsqlConnection connection;
        private string connectionstring = ConfigurationManager.AppSettings.Get("connectionstring");
        private string APIkey = ConfigurationManager.AppSettings.Get("apikey");
        private string filePath = ConfigurationManager.AppSettings.Get("filepath");

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
                        t.tour_From = (DBNull.Value == reader["tour_From"]) ? string.Empty : (string)reader["tour_From"];
                        t.tour_To = (DBNull.Value == reader["tour_To"]) ? string.Empty : (string)reader["tour_To"];
                        t.tour_Distance = (DBNull.Value == reader["tour_Distance"]) ? 0 : (double)reader["tour_Distance"];
                        tours.Add(t);
                    }
                    connection.Close();
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
        public void DeleteTour(Tour t)
        {
            try
            {
                //string sql_command = "DELETE FROM tours WHERE tour_ID = (@p)";
                connection.Open();
                using (var cmd = new NpgsqlCommand("DELETE FROM tours WHERE \"tour_ID\" = (@i)", connection))
                {
                    cmd.Parameters.AddWithValue("i", t.tour_ID);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async void EditTour(Tour t)
        {
            t = await GetAPI(t);
            try
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("UPDATE tours SET \"tour_Name\"=(@n), \"tour_Description\"=(@d), \"tour_From\"=(@f), \"tour_To\"=(@t), \"tour_Distance\"=(@k), \"tour_Map\"=(@m) WHERE \"tour_ID\"=(@i)", connection))
                {
                    cmd.Parameters.AddWithValue("n", t.tour_Name);
                    cmd.Parameters.AddWithValue("d", t.tour_Description);
                    cmd.Parameters.AddWithValue("f", t.tour_From);
                    cmd.Parameters.AddWithValue("t", t.tour_To);
                    cmd.Parameters.AddWithValue("i", t.tour_ID);
                    cmd.Parameters.AddWithValue("k", t.tour_Distance);
                    cmd.Parameters.AddWithValue("m", t.tour_Map);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async void AddTour(Tour t)
        {
            t = await GetAPI(t);
            try
            {
                connection.Open();
                using(var cmd = new NpgsqlCommand("INSERT INTO tours (\"tour_Name\", \"tour_Description\", \"tour_From\", \"tour_To\", \"tour_Distance\", \"tour_Map\") VALUES ((@n),(@d),(@f),(@t),(@k),(@m));", connection))
                {
                    cmd.Parameters.AddWithValue("n", t.tour_Name);
                    cmd.Parameters.AddWithValue("d", t.tour_Description);
                    cmd.Parameters.AddWithValue("f", t.tour_From);
                    cmd.Parameters.AddWithValue("t", t.tour_To);
                    cmd.Parameters.AddWithValue("k", t.tour_Distance);
                    cmd.Parameters.AddWithValue("m", t.tour_Map);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();

            }
            catch(Exception)
            {
                throw;
            }
        }
        public async Task<Tour> GetAPI(Tour t)
        {
            HttpClient client = new HttpClient();
            try
            {
                var result = await client.GetAsync(new Uri($"http://www.mapquestapi.com/directions/v2/route?key={APIkey}&from={t.tour_From}&to={t.tour_To}"));
                string result_s = await result.Content.ReadAsStringAsync();
                JObject result_j = JObject.Parse(result_s);
                t.tour_Distance = double.Parse(result_j["route"]["distance"].ToString());
                t.tour_Map = $"{filePath}{t.tour_ID}.jpg";
                using (WebClient client_w = new WebClient())
                {
                    client_w.DownloadFile(new Uri($"https://www.mapquestapi.com/staticmap/v5/map?locations={t.tour_From}||{t.tour_To}&size=170,170&key={APIkey}"), $@"{t.tour_Map}");
                }
            }
            catch(Exception)
            {
                throw;
            }

            return await Task.FromResult(t);
        }
        
    }
}
