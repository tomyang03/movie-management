using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WPF_UI.DTO;
using WPF_UI;
using WPF_UI.DataAccess;

namespace WPF_UI.Service
{
    class CommonService
    {
        public const string dbHost = "localhost";
        public const string dbUser = "root";
        public const string dbPassw = "Breneli23";
        public const string dbName = "moviedb";
        

        /*  MOVIE SERVICES */
        public static MovieDto ReadMovie(int idMovie)
        {
            Console.WriteLine("<ReadMovie> id: " + idMovie);
            List<MovieDto> movieList = findAll();
            MovieDto theMovie = movieList.Find( movie => movie.MovieId == idMovie);
            return theMovie;
        }
        public static List<MovieDto>  SaveMovie(MovieDto movieDto)
        {
            Console.WriteLine("<SaveMovie> title: {0}", movieDto.Title);
            Console.WriteLine("<SaveMovie> year: {0}", movieDto.Year);
            DataAccessLayer databaseConnection = new DataAccessLayer(dbHost, dbUser, dbPassw, dbName);
            DataAccessLayer.AddMovie(databaseConnection.Connstring, databaseConnection.Conn, movieDto);
            List<MovieDto> movieList = findAll();
            return movieList;
        }
        public static List<MovieDto> UpdateMovie(MovieDto movieDto)
        {
            Console.WriteLine("<updateMovie> movieId: {0} ", movieDto.MovieId);
            Console.WriteLine("<updateMovie> title: {0}", movieDto.Title);
            Console.WriteLine("<updateMovie> year: {0}", movieDto.Year);

            DataAccessLayer databaseConnection = new DataAccessLayer(dbHost, dbUser, dbPassw, dbName);
            DataAccessLayer.UpdateMovie(databaseConnection.Connstring, databaseConnection.Conn, movieDto);
            List<MovieDto> movieList = findAll();
            return movieList;
        }

        public static bool DeleteMovie(int idMovie)
        {
            Console.WriteLine("<DeleteMovie> movieId: {0}", idMovie);

            return true;
        }

        public static List<MovieDto> findAll()
        {
            Console.WriteLine("<findAll>");
            List<MovieDto> movieList = new List<MovieDto>();
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
                string premiereDate = movieRow["PremiereDate"].ToString();
                string season = movieRow["SeasonLabel"].ToString();
                string filmGenre = movieRow["FilmGenreLabel"].ToString();
                MovieDto theMovie = new MovieDto(movieId, title, runtimeMinutes, director, production, synopsys, imagePath, premiereDate, season, filmGenre);
                movieList.Add(theMovie);
            }
            return movieList;
        }

        public static List<filmGenre> findAllGenres()
        {
            Console.WriteLine("<findAll>");
            List<filmGenre> genreList = new List<filmGenre>();
            DataAccessLayer databaseConnection = new DataAccessLayer(dbHost, dbUser, dbPassw, dbName);
            DataTable commandDatabase = DataAccessLayer.FetchAllGenres(databaseConnection.Connstring, databaseConnection.Conn);

            foreach (DataRow movieRow in commandDatabase.Rows)
            {
                int filmgenreId = Convert.ToInt32(movieRow["FilmGenre_ID"]);
                string label = movieRow["Label"].ToString();
                
                filmGenre genre = new filmGenre(filmgenreId, label);
                genreList.Add(genre);
            }
            return genreList;
        }
    }
}
