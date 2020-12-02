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

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for MovieList.xaml
    /// </summary>
    public partial class MovieList : Window
    {
        public MovieList()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void newMovieBtn_Click(object sender, RoutedEventArgs e)
        {
            NewMovie objNewMovie = new NewMovie();
            objNewMovie.ShowDialog();
        }

        private void editMovieBtn_Click(object sender, RoutedEventArgs e)
        {
            EditMovie objEditMovie = new EditMovie();
            objEditMovie.ShowDialog();
        }

        private void movieListRow_DoubleClick(object sender, MouseEventArgs e)
        {
            MovieDetail objMovieDetail = new MovieDetail();
            objMovieDetail.ShowDialog(); // view selected movie details from db
        }

        private void movieListGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            if (editMovieButton.IsMouseOver)
                editMovieButton.IsEnabled = true;
            else
                editMovieButton.IsEnabled = false;
        }

        private void movieListGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            /* Find a way to print selected movie title to console output (to test).
             * Eventually need to find a way to view and edit the details of the 
             * selected movie */

            DataGridRow objSelectedMovie = movieListGrid.SelectedItem as DataGridRow;
            //string movieTitle = objSelectedMovie.ToString();

            editMovieButton.IsEnabled = true;
            //Console.WriteLine($"You clicked on the movie: {movieTitle}");
        }
    }
}
