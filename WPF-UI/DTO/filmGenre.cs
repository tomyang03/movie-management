using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_UI.DTO
{
    public class filmGenre
    {
        private int filmgenre_Id;

        private string label;

        public int Filmgenre_Id
        {
            get { return this.filmgenre_Id; }

            set { this.filmgenre_Id = value; }
        }

        public string Label
        {
            get { return this.label; }

            set { this.label = value; }
        }
        
        public filmGenre(int filmgenre_Id, string label)
        {
            this.filmgenre_Id = filmgenre_Id;
            this.label = label;
        }


    }
}
