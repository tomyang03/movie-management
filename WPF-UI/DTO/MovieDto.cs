using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class MovieDto
    {
        /* Attributes */
        private int movieId;
        private string title;
        private int runtimeMinutes;
        private string director;
        private string production;
        private string synopsys;
        private string imagePath;
        private string premiereDate;
        private int year;
        //Foreign
        private int seasonId;
        private int filmGenreId;


        /*   */
        public int MovieId
        {
            get { return this.movieId; }

            set { this.movieId = value; }
        }
        public string Title
        {
            get { return this.title; }

            set { this.title = value; }
        }
        public int RuntimeMinutes
        {
            get { return this.runtimeMinutes; }

            set { this.runtimeMinutes = value; }
        }
        public string Director
        {
            get { return this.director; }

            set { this.director = value; }
        }
        public string Production
        {
            get { return this.production; }

            set { this.production = value; }
        }
        public string Synopsys
        {
            get { return this.synopsys; }

            set { this.synopsys = value; }
        }
        public string ImagePath
        {
            get { return this.imagePath; }

            set { this.imagePath = value; }
        }
        public string PremiereDate
        {
            get { return this.premiereDate; }

            set { this.premiereDate = value; }
        }
        public int Year
        {
            get { return this.year; }

            set { this.year = value; }
        }
        public int SeasonId
        {
            get { return this.seasonId; }

            set { this.seasonId = value; }
        }
        public int FilmGenreId
        {
            get { return this.filmGenreId; }

            set { this.filmGenreId = value; }
        }

        public MovieDto()
        {

        }
        public MovieDto(int idMovie, string title, int year)
        {
            this.movieId = idMovie;
            this.title = title;
            this.year = year;
        }


        public override string ToString()
        {
            //return base.ToString();
            return $"[movieId: "+this.movieId + ", title: " + this.title + ", year: "+this.year+"]";
        }
    }
}
