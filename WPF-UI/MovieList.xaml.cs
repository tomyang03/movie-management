using WPF_UI.DTO;
using MySql.Data.MySqlClient;
using WPF_UI.Service;
using System;
using System.Collections.Generic;
using System.Data;
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
using WPF_UI.DataAccess;
using System.Collections.ObjectModel;

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for MovieList.xaml
    /// </summary>
    public partial class MovieList : Window
    {               
        //private int SelectedMovieID;
        private MovieDto SelectedMovie;
        ObservableCollection <MovieDto> movies;

        public MovieList()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            movies = CommonService.findAll();           
            movieListGrid.DataContext = movies;

        }    

        private void newMovieBtn_Click(object sender, RoutedEventArgs e)
        {
            NewMovie objNewMovie = new NewMovie(movies);
            objNewMovie.ShowDialog();
            // Rebind movies to the list of movies from NewMovie.XAML after the new movie was added to the list
            // movieListGrid.DataContext =  objNewMovie.movieList;                   
        }

        private void editMovieBtn_Click(object sender, RoutedEventArgs e)
        {
            EditMovie objEditMovie = new EditMovie(SelectedMovie, movies);
            objEditMovie.ShowDialog();
            // Rebind movies to the list of movies from EditMovie.XAML 
            // after the selectedMovie was updated or a movie was deleted
            // movieListGrid.DataContext = objEditMovie.movieList;
        }

        private void movieListRow_DoubleClick(object sender, MouseEventArgs e)
        {            
            MovieDetail objMovieDetail = new MovieDetail(SelectedMovie.MovieId);
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
            var dg = sender as DataGrid;   
            SelectedMovie  = (MovieDto) this.movieListGrid.CurrentItem;                  
            editMovieButton.IsEnabled = true;
            
        }
    }
}
