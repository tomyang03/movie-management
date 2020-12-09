using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_UI.DTO
{
    public class filmGenre : INotifyPropertyChanged
    {
        private int filmgenre_Id;

        public int Filmgenre_Id
        {
            get { return this.filmgenre_Id; }
            set
            {
                if (this.filmgenre_Id != value)
                {
                    this.filmgenre_Id = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Filmgenre_Id"));
                }
            }
        }


        private string label;

        public string Label
        {
            get { return this.label; }
            set
            {
                if (this.label != value)
                {
                    this.label = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Label"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //public int Filmgenre_Id
        //{
        //    get { return this.filmgenre_Id; }

        //    set { this.filmgenre_Id = value; }
        //}

        //public string Label
        //{
        //    get { return this.label; }

        //    set { this.label = value; }
        //}
        
        public filmGenre(int filmgenre_Id, string label)
        {
            this.filmgenre_Id = filmgenre_Id;
            this.label = label;
        }

        public override string ToString()
        {
            return this.label;
        }

    }
}
