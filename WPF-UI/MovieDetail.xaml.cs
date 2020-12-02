using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using ServiceLayer;
using Dto;

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for MovieDetail.xaml
    /// </summary>
    public partial class MovieDetail : Window
    {
        public MovieDto movie;
        public MovieDetail()
        {
            movie = CommonService.ReadMovie(1); // replaces Session or Object passing
            Console.WriteLine("movie: {0} ", movie.ToString());

            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void closeMovieDetai_Btn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
