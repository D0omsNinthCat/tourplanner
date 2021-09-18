using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourplanner.Models;
using Npgsql;
using System.Data;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Windows.Media.Imaging;
using System.IO;
using System.Net;
using System.Drawing;
using System.Collections.ObjectModel;
using System.Configuration;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;

namespace tourplanner.DALayer
{
    public class DB : DB_Interface
    {
        private static DB database_instance;
        private NpgsqlConnection connection;
        private string connectionstring = ConfigurationManager.AppSettings.Get("connectionstring");
        private string APIkey = ConfigurationManager.AppSettings.Get("apikey");
        private string filePath = ConfigurationManager.AppSettings.Get("filepath");
        public ObservableCollection<Log> logs { get; set; }
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
                log.Info("Trying to connect to Database");
                database_instance = new DB();
                database_instance.connectionstring = sth; //Rework later maybe
                
                return database_instance;
            }
            catch(Exception x)
            {
                //throw;
                log.Info("Something went wrong while establishing the DB connection");
                Console.WriteLine(x);
                return null;
                //SAY SOMETHING
            }
            
        }

        public List<Tour> GetTourList()
        {
            try
            {
                string sql_command = "SELECT * FROM tours";
                List<Tour> tours = new List<Tour>();
                log.Info("creating npgsql command");
                NpgsqlCommand command = new NpgsqlCommand(sql_command, connection);
                log.Info("opening connection");
                connection.Open();
                //IDataReader reader = command.ExecuteReader();

                
                using (IDataReader reader = command.ExecuteReader())
                {
                    log.Info("reading tours");
                    while (reader.Read())
                    {
                        Tour t = new Tour();
                        t.tour_ID = (DBNull.Value == reader["tour_ID"]) ? 0 : (int)reader["tour_ID"];
                        t.tour_Name = (DBNull.Value == reader["tour_Name"]) ? string.Empty : (string)reader["tour_Name"];
                        t.tour_Description = (DBNull.Value == reader["tour_Description"]) ? string.Empty : (string)reader["tour_Description"];
                        t.tour_From = (DBNull.Value == reader["tour_From"]) ? string.Empty : (string)reader["tour_From"];
                        t.tour_To = (DBNull.Value == reader["tour_To"]) ? string.Empty : (string)reader["tour_To"];
                        t.tour_Distance = (DBNull.Value == reader["tour_Distance"]) ? 0 : (double)reader["tour_Distance"];
                        t.tour_Map = (DBNull.Value == reader["tour_Map"]) ? string.Empty : (string)reader["tour_Map"];
                        tours.Add(t);
                    }
                    connection.Close();
                    tours = GetLogs(tours);
                    return tours;
                }
            }
            catch(Exception x)
            {
                log.Info("Something went wrong while reading the Tours from the DB");
                Console.WriteLine(x);
                return null;
                //SAY SOMETHING
            }
        }
        public void DeleteTour(Tour t)
        {
            try
            {
                log.Info("opening connection");
                connection.Open();
                using (var cmd = new NpgsqlCommand("DELETE FROM tours WHERE \"tour_ID\" = (@i)", connection))
                {
                    cmd.Parameters.AddWithValue("i", t.tour_ID);
                    log.Info("Deleting Tour");
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception x)
            {
                log.Info("Something went wrong while deleting the Tour");
                Console.WriteLine(x);
            }
        }
        public async void EditTour(Tour t)
        {
            t = await GetAPI(t);
            try
            {
                log.Info("opening connection");
                connection.Open();
                using (var cmd = new NpgsqlCommand("UPDATE tours SET \"tour_Name\"=(@n), \"tour_Description\"=(@d), \"tour_From\"=(@f), \"tour_To\"=(@t), \"tour_Distance\"=(@k), \"tour_Map\"=(@m) WHERE \"tour_ID\"=(@i)", connection))
                {
                    log.Info("Editing Tour");
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
            catch (Exception x)
            {
                log.Info("Something went wrong while editing the Tour");
                Console.WriteLine(x);
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
            catch(Exception x)
            {
                log.Info("Something went wrong while adding the Tour");
                Console.WriteLine(x);
            }
        }
        public async Task<Tour> GetAPI(Tour t)
        {
            log.Info("creating new Client");
            HttpClient client = new HttpClient();
            try
            {
                log.Info("Route info Request");
                var result = await client.GetAsync(new Uri($"http://www.mapquestapi.com/directions/v2/route?key={APIkey}&from={t.tour_From}&to={t.tour_To}"));
                log.Info("Route info Request received");
                string result_s = await result.Content.ReadAsStringAsync();
                JObject result_j = JObject.Parse(result_s);
                t.tour_Distance = double.Parse(result_j["route"]["distance"].ToString());

                t.tour_Map = $"{filePath}{t.tour_ID}.jpg";
                using (WebClient client_w = new WebClient())
                {
                    log.Info("Map Data Request");
                    client_w.DownloadFile(new Uri($"https://www.mapquestapi.com/staticmap/v5/map?start={t.tour_From}&end={t.tour_To}&size=170,170&key={APIkey}"), $@"{t.tour_Map}");
                    log.Info("Map Data Request received");
                }
            }
            catch(Exception x)
            {
                log.Info("Something went wrong while getting API Data");
                Console.WriteLine(x);
            }

            return await Task.FromResult(t);
        }
        public List<Tour> GetLogs(List<Tour> tours)
        {
            try
            {
                log.Info("Creating Npgsql command");
                string sql_command = "SELECT * FROM logs";
                NpgsqlCommand command = new NpgsqlCommand(sql_command, connection);
                log.Info("Opening Connection");
                connection.Open();
                ObservableCollection<Log> logs = new ObservableCollection<Log>();

                using (IDataReader reader = command.ExecuteReader())
                {
                    log.Info("Reading Logs");
                    while (reader.Read())
                    {
                        Log l = new Log();
                        l.tour_ID = (DBNull.Value == reader["tour_ID"]) ? 0 : (int)reader["tour_ID"];
                        l.log_ID = (DBNull.Value == reader["log_ID"]) ? 0 : (int)reader["log_ID"];
                        l.log_Date = (DBNull.Value == reader["log_Date"]) ? default : (DateTime)reader["log_Date"];
                        l.log_Duration = (DBNull.Value == reader["log_Duration"]) ? 0 : (int)reader["log_Duration"];
                        l.log_Distance = (DBNull.Value == reader["log_Distance"]) ? 0 : (double)reader["log_Distance"];
                        l.log_Rating = (DBNull.Value == reader["log_Rating"]) ? 0 : (int)reader["log_Rating"];
                        l.log_Report = (DBNull.Value == reader["log_Report"]) ? string.Empty : reader["log_Report"].ToString();
                        l.log_Author = (DBNull.Value == reader["log_Author"]) ? string.Empty : reader["log_Author"].ToString();
                        l.log_Speed = (DBNull.Value == reader["log_Speed"]) ? 0 : (double)reader["log_Speed"];
                        l.log_Energy = (DBNull.Value == reader["log_Energy"]) ? 0 : (double)reader["log_Energy"];
                        l.log_Name = (DBNull.Value == reader["log_Name"]) ? string.Empty : reader["log_Name"].ToString();
                        l.log_Transport = (DBNull.Value == reader["log_Transport"]) ? string.Empty : reader["log_Transport"].ToString(); ;

                        logs.Add(l);
                    }
                    connection.Close();
                    log.Info("Adding Logs to Tours");
                    foreach (Tour t in tours)
                    {
                        t.Logs = new ObservableCollection<Log>();
                        foreach (Log l in logs)
                        {
                            if(l.tour_ID == t.tour_ID)
                            {
                                t.Logs.Add(l);
                            }
                        }
                    }
                    return tours;
                }
            }
            catch (Exception x)
            {
                log.Info("Something went wrong while getting Log List from DB");
                Console.WriteLine(x);
                return null;
            }
        }
        public Log CalculateLog(Log l)
        {
            log.Info("Calculating Energy consumption");
            try
            {
                l.log_Speed = l.log_Distance / (l.log_Duration / 60);
                if (l.log_Transport == "Bicycle" | l.log_Transport == "Walk")
                {
                    l.log_Energy = 75000 * ((l.log_Distance / 1000) / (l.log_Duration / 60));
                }
                //ADD FUEL THINGY HERE
                else if(l.log_Transport == "Car")
                {
                    l.log_Energy = l.log_Distance * 0.094; 
                    //The average fuel economy for new 2017 model year cars, light trucks and SUVs in the United States was 24.9 mpgUS (9.4 L/100 km).
                    }
                else if (l.log_Transport == "Motorbike")
                {
                    l.log_Energy = l.log_Distance * 0.044;
                    //The motorcycles use between 2,8 and 5,7 litres fuel per 100 kilometres with an average of 4,4 l/100km.
                }
                return l;
            }
            catch (Exception x)
            {
                log.Info("Something went wrong while calculating energy, resuming with 0");
                Console.WriteLine(x);
                l.log_Energy = 0;
                return l;
            }
            
        }

        public void AddLog(Log l)
        {
            l = CalculateLog(l);
            try
            {
                log.Info("Opening Connection");
                connection.Open();
                using (var cmd = new NpgsqlCommand("INSERT INTO logs (" +
                    "\"log_Date\", " +
                    "\"log_Duration\", " +
                    "\"log_Distance\", " +
                    "\"log_Rating\", " +
                    "\"log_Report\", " +
                    "\"tour_ID\", " +
                    "\"log_Author\", " +
                    "\"log_Speed\", " +
                    "\"log_Transport\", " +
                    "\"log_Name\", " +
                    "\"log_Energy\") VALUES ((@dat),(@dur),(@dis),(@rat),(@rep),(@id),(@aut),(@spe),(@tra),(@nam),(@ene));", connection))
                {
                    cmd.Parameters.AddWithValue("dat", l.log_Date);
                    cmd.Parameters.AddWithValue("dur", l.log_Duration);
                    cmd.Parameters.AddWithValue("dis", l.log_Distance);
                    cmd.Parameters.AddWithValue("rat", l.log_Rating);
                    cmd.Parameters.AddWithValue("rep", l.log_Report);
                    cmd.Parameters.AddWithValue("id", l.tour_ID);
                    cmd.Parameters.AddWithValue("aut", l.log_Author);
                    cmd.Parameters.AddWithValue("spe", l.log_Speed);
                    cmd.Parameters.AddWithValue("tra", l.log_Transport);
                    cmd.Parameters.AddWithValue("nam", l.log_Name);
                    cmd.Parameters.AddWithValue("ene", l.log_Energy);
                    log.Info("Executing Add Log query");
                    cmd.ExecuteNonQuery();
                }
                connection.Close();

            }
            catch (Exception x)
            {
                log.Info("Something went wrong while adding Log to DB");
                Console.WriteLine(x);
            }

        }
        public void EditLog(Log l)
        {
            l = CalculateLog(l);
            try
            {
                log.Info("Opening Connection");
                connection.Open();
                using (var cmd = new NpgsqlCommand("UPDATE logs SET \"log_Date\" = (@dat), " +
                    "\"log_Duration\" = (@dur), " +
                    "\"log_Distance\" = (@dis), " +
                    "\"log_Rating\" = (@rat), " +
                    "\"log_Report\" = (@rep), " +
                    "\"tour_ID\" = (@id), " +
                    "\"log_Author\" = (@aut), " +
                    "\"log_Speed\" = (@spe), " +
                    "\"log_Transport\" = (@tra), " +
                    "\"log_Name\" = (@nam), " +
                    "\"log_Energy\" = (@ene) WHERE \"log_ID\"=(@i)"
                    , connection)) 
                {
                    cmd.Parameters.AddWithValue("dat", l.log_Date);
                    cmd.Parameters.AddWithValue("dur", l.log_Duration);
                    cmd.Parameters.AddWithValue("dis", l.log_Distance);
                    cmd.Parameters.AddWithValue("rat", l.log_Rating);
                    cmd.Parameters.AddWithValue("rep", l.log_Report);
                    cmd.Parameters.AddWithValue("id", l.tour_ID);
                    cmd.Parameters.AddWithValue("aut", l.log_Author);
                    cmd.Parameters.AddWithValue("spe", l.log_Speed);
                    cmd.Parameters.AddWithValue("tra", l.log_Transport);
                    cmd.Parameters.AddWithValue("nam", l.log_Name);
                    cmd.Parameters.AddWithValue("ene", l.log_Energy);
                    cmd.Parameters.AddWithValue("i", l.log_ID);
                    log.Info("Executing Edit Log Query");
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception x)
            {
                log.Info("Something went wrong while editing Log");
                Console.WriteLine(x);
            }
        }
        public void DeleteLog(Log l)
        {
            try
            {
                log.Info("Opening Connection");
                connection.Open();
                using (var cmd = new NpgsqlCommand("DELETE FROM logs WHERE \"log_ID\" = (@i)", connection))
                {
                    cmd.Parameters.AddWithValue("i", l.log_ID);
                    log.Info("Executing Delete Log Query");
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception x)
            {
                log.Info("Something went wrong while deleting Log");
                Console.WriteLine(x);
            }
        }

    }
}
