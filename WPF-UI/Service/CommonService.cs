using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dto;

namespace ServiceLayer
{
    class CommonService
    {

        public static bool ValidUser(string username, string password)
        {
            Console.WriteLine("<ValidUser> username: {0}", username );
            Console.WriteLine("<ValidUser> password: {0} ", password);
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
                /* TEMPORARY code to simulate DB query */
                //return true;
                if(username == "Dennis" && password == "Nay")
                {
                    return true;
                } else if(username == "Ernesto" && password == "Valle")
                {
                    return true;
                } else if (username == "Kang" && password == "Yang")
                {
                    return true;
                }
                else if (username == "Youngyun" && password == "Namkung")
                {
                    return true;
                }
                else
                {
                    return false;
                }
                // */
            }
            else
            {
                /* Additional validation for null operation */
                return false;
            }
        }

        /*  MOVIE SERVICES */
        public static MovieDto ReadMovie(int idMovie)
        {
            Console.WriteLine("<ReadMovie> id: " + idMovie);
            List<MovieDto> movieList = findAll();
            MovieDto movieDto = movieList.ElementAt(0);
            return movieDto;
        }
        public static bool SaveMovie(MovieDto movieDto)
        {
            Console.WriteLine("<SaveMovie> title: {0}", movieDto.Title);
            Console.WriteLine("<SaveMovie> year: {0}", movieDto.Year);

            return true;
        }
        public static bool UpdateMovie(MovieDto movieDto)
        {
            Console.WriteLine("<updateMovie> movieId: {0} ", movieDto.MovieId);
            Console.WriteLine("<updateMovie> title: {0}", movieDto.Title);
            Console.WriteLine("<updateMovie> year: {0}", movieDto.Year);

            return true;
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

            movieList.Add(new MovieDto(1, "The Last Black Man in San Francisco", 2019));
            movieList.Add(new MovieDto(1, "The Handmaiden", 2016));
            movieList.Add(new MovieDto(1, "Roma", 2018));
            movieList.Add(new MovieDto(1, "The Farewell", 2019));
            movieList.Add(new MovieDto(1, "Moonlight", 2016));

            return movieList;
        }
    }
}
