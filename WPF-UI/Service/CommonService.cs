using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WPF_UI.DTO;
using WPF_UI;
using WPF_UI.DataAccess;
using System.Collections.ObjectModel;
using System.Globalization;

namespace WPF_UI.Service
{
    class CommonService
    {
        public const string dbHost = "localhost";
        public const string dbUser = "root";
        public const string dbPassw = "";
        public const string dbName = "moviedb";
        

        /*  MOVIE SERVICES */
        public static MovieDto ReadMovie(int idMovie)
        {
            Console.WriteLine("<ReadMovie> id: " + idMovie);
            Console.WriteLine("<findAll>");
            ObservableCollection<MovieDto> movieObservableCollection = new ObservableCollection<MovieDto>();
            DataAccessLayer databaseConnection = new DataAccessLayer(dbHost, dbUser, dbPassw, dbName);
            DataTable commandDatabase = DataAccessLayer.FetchMovie(databaseConnection.Connstring, databaseConnection.Conn, idMovie);

            foreach (DataRow movieRow in commandDatabase.Rows)
            {
                int movieId = Convert.ToInt32(movieRow["Movie_ID"]);
                string title = movieRow["Title"].ToString();
                int runtimeMinutes = Convert.ToInt32(movieRow["RuntimeMinutes"]);
                string director = movieRow["Director"].ToString();
                string production = movieRow["Production"].ToString();
                string synopsys = movieRow["Synopsis"].ToString();
                string imagePath = movieRow["ImagePath"].ToString();
                string premiereDate = movieRow["PremiereDate"].ToString();
                string season = movieRow["SeasonLabel"].ToString();
                string filmGenre = movieRow["FilmGenreLabel"].ToString();
                int seasonId = Convert.ToInt32(movieRow["Season_ID"]);
                int filmGenreId = Convert.ToInt32(movieRow["FilmGenre_ID"]);
                MovieDto theMovie = new MovieDto(movieId, title, runtimeMinutes, director, production, synopsys, imagePath, premiereDate, season, filmGenre, seasonId, filmGenreId);
                movieObservableCollection.Add(theMovie);
            }
            return movieObservableCollection.ElementAt(0);
        }
        public static MovieDto  SaveMovie(MovieDto movieDto)
        {
            Console.WriteLine("<SaveMovie> title: {0}", movieDto.Title);
            Console.WriteLine("<SaveMovie> year: {0}", movieDto.Year);
            DataAccessLayer databaseConnection = new DataAccessLayer(dbHost, dbUser, dbPassw, dbName);
            int newmovieId = DataAccessLayer.AddMovie(databaseConnection.Connstring, databaseConnection.Conn, movieDto);
            MovieDto movie = ReadMovie(newmovieId);
            return movie;
        }
        public static ObservableCollection<MovieDto> UpdateMovie(MovieDto movieDto)
        {
            Console.WriteLine("<updateMovie> movieId: {0} ", movieDto.MovieId);
            Console.WriteLine("<updateMovie> title: {0}", movieDto.Title);
            Console.WriteLine("<updateMovie> year: {0}", movieDto.Year);

            DataAccessLayer databaseConnection = new DataAccessLayer(dbHost, dbUser, dbPassw, dbName);
            DataAccessLayer.UpdateMovie(databaseConnection.Connstring, databaseConnection.Conn, movieDto);
            ObservableCollection<MovieDto> movieObservableCollection = findAll();
            return movieObservableCollection;
        }

        public static ObservableCollection<MovieDto> DeleteMovie(int selectedMovieId)
        {
            Console.WriteLine("<DeleteMovie> movieId: {0}", selectedMovieId);

            DataAccessLayer databaseConnection = new DataAccessLayer(dbHost, dbUser, dbPassw, dbName);
            DataAccessLayer.DeleteMovie(databaseConnection.Connstring, databaseConnection.Conn, selectedMovieId);
            ObservableCollection<MovieDto> movieObservableCollection = findAll();
            return movieObservableCollection;
        }

        public static ObservableCollection<MovieDto> findAll()
        {
            Console.WriteLine("<findAll>");
            ObservableCollection<MovieDto> movieObservableCollection = new ObservableCollection<MovieDto>();
            DataAccessLayer databaseConnection = new DataAccessLayer(dbHost, dbUser, dbPassw, dbName);
            DataTable commandDatabase = DataAccessLayer.FetchMovies(databaseConnection.Connstring, databaseConnection.Conn);

            foreach (DataRow movieRow in commandDatabase.Rows)
            {                
                int  movieId = Convert.ToInt32(movieRow["Movie_ID"]);
                string title = movieRow["Title"].ToString();
                int runtimeMinutes = Convert.ToInt32(movieRow["RuntimeMinutes"]);
                string director = movieRow["Director"].ToString();
                string production = movieRow["Production"].ToString();
                string synopsys = movieRow["Synopsis"].ToString();
                string imagePath = movieRow["ImagePath"].ToString();
                string premiereDate = ((DateTime?) movieRow["PremiereDate"]).Value.ToString("yyyy-MM-dd HH:mm:ss tt", DateTimeFormatInfo.InvariantInfo);              
                string season = movieRow["SeasonLabel"].ToString();
                string filmGenre = movieRow["FilmGenreLabel"].ToString();
                int seasonId = Convert.ToInt32(movieRow["Season_ID"]);
                int  filmGenreId = Convert.ToInt32(movieRow["FilmGenre_ID"]);
                MovieDto theMovie = new MovieDto(movieId, title, runtimeMinutes, director, production, synopsys, imagePath, premiereDate, season, filmGenre, seasonId, filmGenreId);
                movieObservableCollection.Add(theMovie);
            }
            return movieObservableCollection;
        }

        public static ObservableCollection<filmGenre> findAllGenres()
        {
            Console.WriteLine("<findAll>");
            ObservableCollection<filmGenre> genreObservableCollection = new ObservableCollection<filmGenre>();
            DataAccessLayer databaseConnection = new DataAccessLayer(dbHost, dbUser, dbPassw, dbName);
            DataTable commandDatabase = DataAccessLayer.FetchAllGenres(databaseConnection.Connstring, databaseConnection.Conn);

            foreach (DataRow movieRow in commandDatabase.Rows)
            {
                int filmgenreId = Convert.ToInt32(movieRow["FilmGenre_ID"]);
                string label = movieRow["Label"].ToString();
                
                filmGenre genre = new filmGenre(filmgenreId, label);
                genreObservableCollection.Add(genre);
            }
            return genreObservableCollection;
        }

        public static ObservableCollection<MovieDto> searchMovies(string searchInput, string searchCombo)
        {
            Console.WriteLine("<searchMovies>");
            Console.WriteLine($"searchInput: {searchInput}, selected search column: {searchCombo}");

            ObservableCollection<MovieDto> movieObservableCollection = new ObservableCollection<MovieDto>();
            DataAccessLayer databaseConnection = new DataAccessLayer(dbHost, dbUser, dbPassw, dbName);
            DataTable commandDatabase = DataAccessLayer.FetchMovies(databaseConnection.Connstring, databaseConnection.Conn);
            
            foreach (DataRow movieRow in commandDatabase.Rows)
            {
                string isSearchMovie = movieRow[searchCombo].ToString().ToLower();
                if (isSearchMovie.Contains(searchInput.ToLower()))
                {
                    int movieId = Convert.ToInt32(movieRow["Movie_ID"]);
                    string title = movieRow["Title"].ToString();
                    int runtimeMinutes = Convert.ToInt32(movieRow["RuntimeMinutes"]);
                    string director = movieRow["Director"].ToString();
                    string production = movieRow["Production"].ToString();
                    string synopsys = movieRow["Synopsis"].ToString();
                    string imagePath = movieRow["ImagePath"].ToString();
                    string premiereDate = ((DateTime?)movieRow["PremiereDate"]).Value.ToString("yyyy-MM-dd HH:mm:ss tt", DateTimeFormatInfo.InvariantInfo);
                    string season = movieRow["SeasonLabel"].ToString();
                    string filmGenre = movieRow["FilmGenreLabel"].ToString();
                    int seasonId = Convert.ToInt32(movieRow["Season_ID"]);
                    int filmGenreId = Convert.ToInt32(movieRow["FilmGenre_ID"]);
                    MovieDto theMovie = new MovieDto(movieId, title, runtimeMinutes, director, production, synopsys, imagePath, premiereDate, season, filmGenre, seasonId, filmGenreId);
                    movieObservableCollection.Add(theMovie);
                }
            }
            return movieObservableCollection;
        }
    }
}
