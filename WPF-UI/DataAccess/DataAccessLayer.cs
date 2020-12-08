using WPF_UI.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_UI.DataAccess
{   
    public class DataAccessLayer
    {
        private string connstring ;
        private MySqlConnection conn;        

        public string Connstring
        {
            get { return this.connstring; }
        }

        public MySqlConnection Conn
        {
            get { return this.conn; }
        }

        public  DataAccessLayer(string host, string user, string password, string databse)
        {
            connstring = $"server={host};userid={user};password={password};database={databse}";
            conn = null;
        }

        public static AppUser existUser(string connstring, MySqlConnection conn, AppUser user)
        {
            user.UserId = -1;
            string query = @"SELECT  User_ID, Role_ID, FullName 
                FROM appuser
                WHERE Username = '"+ user.Username+"' AND PassWord = '"+user.Password+"'; ";
            Console.WriteLine($"<existUser> query: {query}");            
            try
            {
                conn = new MySqlConnection(connstring);

                MySqlCommand commandDatabase = new MySqlCommand(query, conn);
                commandDatabase.CommandTimeout = 60;
                conn.Open(); 
                MySqlDataReader reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        user.UserId = reader.GetInt32(0);
                        user.RoleId = reader.GetInt32(1);
                        user.Fullname = reader.GetString(2);
                    }
                }
                else
                {
                    Console.WriteLine("No Data found.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message); 
                MessageBox.Show("Error: {0}", ex.Message);
            }

            return user;
        }

        public static DataTable FetchMovies(string connstring, MySqlConnection conn)
        {
            DataTable dt = new DataTable();

            try
            {
                conn = new MySqlConnection(connstring);
                conn.Open();
               
                // Note usage of alias to distinguish between Season and Filmgenre Label, as the 2 columns
                // have the same name "Label" 
                string query = @"SELECT  Movie_ID, Title, Director, Production, RuntimeMinutes, PremiereDate,
                ImagePath, Synopsis, season.Label as SeasonLabel, filmgenre.Label as FilmGenreLabel  
                FROM movie INNER JOIN  season ON  movie.Season_ID = season.Season_ID 
                INNER JOIN filmgenre ON  movie.FilmGenre_ID = filmgenre.FilmGenre_ID;";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "movie");
                dt = ds.Tables["movie"];               
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: {0}", e.Message.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }              
            }
            return dt;
        }

        public static void AddMovie(string connstring, MySqlConnection conn, MovieDto theMovie)
        {         
            try
            {
                conn = new MySqlConnection(connstring);
                conn.Open();

                //int movieId = theMovie.MovieId;
                string title = theMovie.Title;
                int runtimeMinutes = theMovie.RuntimeMinutes;
                string director = theMovie.Director;
                string production = theMovie.Production;
                string synopsys = theMovie.Synopsys;
                string imagePath = theMovie.ImagePath;
                int year = 2020;
                string premiereDate = theMovie.PremiereDate;
                int season_ID = theMovie.SeasonId;
                int filmgenre_id = theMovie.FilmGenreId;

                string query = $@"INSERT INTO movie (title, runtimeminutes, director, production, 
                               synopsis, imagepath, premieredate, year, season_ID, filmgenre_id)
                               VALUES(@title,@runtimeMinutes,@director,@production,
                               @synopsys,@imagePath,@premiereDate,@year,@season_ID,@filmgenre_id);";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@runtimeMinutes", runtimeMinutes);
                cmd.Parameters.AddWithValue("@director", director);
                cmd.Parameters.AddWithValue("@production", production);
                cmd.Parameters.AddWithValue("@synopsys", synopsys);
                cmd.Parameters.AddWithValue("@imagePath", imagePath);
                cmd.Parameters.AddWithValue("@premiereDate", premiereDate);
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Parameters.AddWithValue("@season_ID", season_ID);
                cmd.Parameters.AddWithValue("@filmgenre_id", filmgenre_id);              
                cmd.ExecuteNonQuery();           
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: {0}", e.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }      
        }


        public static void UpdateMovie(string connstring, MySqlConnection conn, MovieDto theMovie)
        {
            try
            {
                conn = new MySqlConnection(connstring);
                conn.Open();

                int movieId = theMovie.MovieId;
                string title = theMovie.Title;
                int runtimeMinutes = theMovie.RuntimeMinutes;
                string director = theMovie.Director;
                string production = theMovie.Production;
                string synopsys = theMovie.Synopsys;
                string imagePath = theMovie.ImagePath;
                int year = 2020;
                string premiereDate = theMovie.PremiereDate;
                int season_ID = theMovie.SeasonId;
                int filmgenre_id = theMovie.FilmGenreId;

                string query = $@"UPDATE movie SET Title=@title, RuntimeMinutes=@runtimeMinutes, Director=@director, 
                                Production=@production,Synopsis=@synopsys, ImagePath=@imagePath, PremiereDate=@premiereDate, 
                                Season_ID=@season_ID, FilmGenre_ID=@filmgenre_id
                                WHERE Movie_ID=@movieId;";


                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@movieId", movieId);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@runtimeMinutes", runtimeMinutes);
                cmd.Parameters.AddWithValue("@director", director);
                cmd.Parameters.AddWithValue("@production", production);
                cmd.Parameters.AddWithValue("@synopsys", synopsys);
                cmd.Parameters.AddWithValue("@imagePath", imagePath);
                cmd.Parameters.AddWithValue("@premiereDate", premiereDate);
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Parameters.AddWithValue("@season_ID", season_ID);
                cmd.Parameters.AddWithValue("@filmgenre_id", filmgenre_id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: {0}", e.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public static DataTable FetchAllGenres(string connstring, MySqlConnection conn)
        {
            DataTable dt = new DataTable();

            try
            {
                conn = new MySqlConnection(connstring);
                conn.Open();
              
                string query = @"SELECT  * FROM filmgenre;";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "genre");
                dt = ds.Tables["genre"];
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: {0}", e.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return dt;
        }
    }
}
