using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_UI.DTO
{
    public class MovieDto : INotifyPropertyChanged
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
        private string season;
        private string filmGenre;
        //Foreign
        private int seasonId;
        private int filmGenreId;
       
        // EventHandler for updating the Observable Collection
        public event PropertyChangedEventHandler PropertyChanged;

        public int MovieId
        {
            get { return this.movieId; }
            set
            {
                if (this.movieId != value)
                {
                    this.movieId = value;
                    // if PropertyChanged event fires
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MovieId"));
                }
            }
        }

        public string Title
        {
            get { return this.title; }

            set
            {
                if (this.title != value)
                {
                    this.title = value;
                    // if PropertyChanged event fires
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Title"));
                }
            }
        }

        public string Director
        {
            get { return this.director; }

            set
            {
                if (this.director != value)
                {
                    this.director = value;
                    // if PropertyChanged event fires
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Director"));
                }
            }
        }


        public string Production
        {
            get { return this.production; }

            set
            {
                if (this.production != value)
                {
                    this.production = value;
                    // if PropertyChanged event fires
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Production"));
                }
            }
        }
       
        public int RuntimeMinutes
        {
            get { return this.runtimeMinutes; }

            set
            {
                if (this.runtimeMinutes != value)
                {
                    this.runtimeMinutes = value;
                    // if PropertyChanged event fires
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RuntimeMinutes"));
                }
            }
        }
      
        public string PremiereDate
        {
            get { return this.premiereDate; }
            set
            {
                if (this.premiereDate != value)
                {
                    this.premiereDate = value;
                    // if PropertyChanged event fires
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PremiereDate"));
                }
            }
        }

        public string Season
        {
            get { return this.season; }
            set
            {
                if (this.season != value)
                {
                    this.season = value;
                    // if PropertyChanged event fires
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Season"));
                }
            }
        }

        public string ImagePath
        {
            get { return this.imagePath; }
            set
            {
                if (this.imagePath != value)
                {
                    this.imagePath = value;
                    // if PropertyChanged event fires
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ImagePath"));
                }
            }
        }
        public string Synopsys
        {
            get { return this.synopsys; }

            set
            {
                if (this.synopsys != value)
                {
                    this.synopsys = value;
                    // if PropertyChanged event fires
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Synopsys"));
                }
            }
        }
     
        public int  Year
        {
            get { return this.year; }

            set
            {
                if (this.year != value)
                {
                    this.year = value;
                    // if PropertyChanged event fires
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Year"));
                }
            }
        }

        //public int SeasonId
        //{
        //    get { return this.seasonId; }

        //    set { this.seasonId = value; }
        //}

        public int SeasonId
        {
            get { return this.seasonId; }

            set
            {
                if (this.seasonId != value)
                {
                    this.seasonId = value;
                    // if PropertyChanged event fires
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SeasonId"));
                }
            }
        }



        //public int FilmGenreId
        //{
        //    get { return this.filmGenreId; }

        //    set { this.filmGenreId = value; }
        //}

    
        //public string FilmGenre
        //{
        //    get { return this.filmGenre; }

        //    set { this.filmGenre = value; }
        //}

        public int FilmGenreId
        {
            get { return this.filmGenreId; }

            set
            {
                if (this.filmGenreId != value)
                {
                    this.filmGenreId = value;
                    // if PropertyChanged event fires
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FilmGenreId"));
                }
            }
        }

        public string FilmGenre
        {
            get { return this.filmGenre; }

            set
            {
                if (this.filmGenre != value)
                {
                    this.filmGenre = value;
                    // if PropertyChanged event fires
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FilmGenre"));
                }
            }
        }

        public MovieDto()
        {

        }

        // Constructor with movieId used for fetching All the movies from the Database (SELECT Statement)
        // Note: season is string here, because we can get the values such as ("Fall", "Winter" etc.) from the INNER JOIN
        public MovieDto(int movieId, string title, int runtimeMinutes, string director, string production, string synopsys, string imagePath, string premiereDate, string season, string filmGenre, int seasonId, int filmGenreId)
        {
            this.movieId = movieId;
            this.title = title;
            this.runtimeMinutes = runtimeMinutes;
            this.director = director;
            this.production = production;
            this.synopsys = synopsys;
            this.imagePath = imagePath;
            this.premiereDate = premiereDate;
            this.season = season;
            this.filmGenre = filmGenre;
            this.seasonId = seasonId;
            this.filmGenreId = filmGenreId;
        }

        // Constructor without movieId used for INSERT since movieId will be autogenerated
        // Note: season is int here, because we can need to INSERT  a FK of type int for Season
        public MovieDto(string title, int runtimeMinutes, string director, string production, string synopsys, string imagePath, string premiereDate, int season_Id, int filmgenre_id)
        {
            
            this.title = title;
            this.runtimeMinutes = runtimeMinutes;
            this.director = director;
            this.production = production;
            this.synopsys = synopsys;
            this.imagePath = imagePath;
            this.premiereDate = premiereDate;
            this.seasonId = season_Id;
            this.filmGenreId = filmgenre_id;
        }

        // Constructor with movieId used for UPDATE 
        // Note: seasonId is int here, because we can need to UPDATE a FK of type int for Season
        // Note: fimgenreId is int here, because we can need to UPDATE a FK of type int for FilmGenre
        public MovieDto(int movieId, string title, int runtimeMinutes, string director, string production, string synopsys, string imagePath, string premiereDate, int season_Id, int filmgenre_id)
        {
            this.movieId = movieId;
            this.title = title;
            this.runtimeMinutes = runtimeMinutes;
            this.director = director;
            this.production = production;
            this.synopsys = synopsys;
            this.imagePath = imagePath;
            this.premiereDate = premiereDate;
            this.seasonId = season_Id;
            this.filmGenreId = filmgenre_id;
        }

        public override string ToString()
        {
            //return base.ToString();
            return $"[movieId: "+this.movieId + ", title: " + this.title + ", year: "+this.year+"]";
        }
    }
}
