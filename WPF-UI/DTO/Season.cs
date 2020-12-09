using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_UI.DTO
{
    public class Season : INotifyPropertyChanged
    {
        private string movieSeason; 

        public string MovieSeason
        {
            get { return this.movieSeason; }
            set
            {
                if (this.movieSeason != value)
                {
                    this.movieSeason = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MovieSeason"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return MovieSeason;
        }
    }
}
