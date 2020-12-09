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

using WPF_UI.Service;
using WPF_UI.DTO;
using System.Data;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Globalization;

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for EditMovie.xaml
    /// </summary>
    public partial class EditMovie : Window
    {
        public ObservableCollection<Season> movieSeason = new ObservableCollection<Season>();
        public MovieDto SelectedMovie;
        public string imagePath;
        public ObservableCollection<MovieDto> movieList;
        public int SelectedGenre_Id;

        public EditMovie(MovieDto SelectedMovie ,ObservableCollection<MovieDto> movies)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            movieSeason.Add(new Season { MovieSeason = "Fall" });
            movieSeason.Add(new Season { MovieSeason = "Winter" });
            movieSeason.Add(new Season { MovieSeason = "Spring" });
            movieSeason.Add(new Season { MovieSeason = "Summer" });

            //populate the film genres
            ObservableCollection<filmGenre> genreList = CommonService.findAllGenres();

            // Bind DataContext to SelectedMovie, to autopopulate the comboBox field with the FilmGenre 
            // of the Movie the user has selected
            editFilmGenre.DataContext = SelectedMovie;
            // Set Itemsource to the list of genre, to populate the comboBox with the FilmGenres from the DB
            editFilmGenre.ItemsSource = genreList;
            // Autopopulate the season with the Season from the SelectedMovie
            editMovieSeason.DataContext = SelectedMovie;
            editMovieSeason.ItemsSource = movieSeason;
            // Autopopulate the Date with the SelectedMovie's PremiereDate
            editMoviePremiereDate.DataContext = SelectedMovie;

            SelectedGenre_Id = 1;

            // movieList: a reference to movies from the MovieList.xaml            
            movieList = movies;
            // Set default imagePath if user does not choose to edit the image
            imagePath = SelectedMovie.ImagePath;

            // Autopopulate remaining fields
            this.SelectedMovie = SelectedMovie;
            editMovieTitle.Text = SelectedMovie.Title;
            editMovieDirector.Text = SelectedMovie.Director;
            editMovieProduction.Text = SelectedMovie.Production;
            editMovieRuntime.Text = SelectedMovie.RuntimeMinutes.ToString();
            editMovieSynopsis.Text = SelectedMovie.Synopsys;            
        }
        private void saveMovieBtn_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(editMovieTitle.Text) || String.IsNullOrEmpty(editMovieDirector.Text) ||
                String.IsNullOrEmpty(editMovieProduction.Text) || String.IsNullOrEmpty(editMovieRuntime.Text) ||
                String.IsNullOrEmpty(editMoviePremiereDate.Text) || String.IsNullOrEmpty(editMovieSeason.Text) ||
                String.IsNullOrEmpty(editMovieSynopsis.Text))
            {
                MessageBoxResult mesgBoxResult = System.Windows.MessageBox.Show
                    ("Some fields are missing. \nType in anything to test.", "Incomplete Form",
                        System.Windows.MessageBoxButton.OK);
            }
            else
            { 
                MessageBoxResult msgBoxResult = System.Windows.MessageBox.Show
                    ("Save movie details?", "Save Confirmation",
                        System.Windows.MessageBoxButton.YesNo);

                if (msgBoxResult == MessageBoxResult.Yes)
                {

                    int runtimeMinutes = 0;
                    try
                    {
                        runtimeMinutes = Convert.ToInt32(editMovieRuntime.Text);
                    }
                    catch (FormatException f)
                    {
                        MessageBoxResult mesgBoxResult = System.Windows.MessageBox.Show
                    (f.Message + "\nRuntime is not a valid integer. \nPlease enter an integer.", "Incomplete Form",
                        System.Windows.MessageBoxButton.OK);
                        return;
                    }

                    catch (OverflowException ov)
                    {
                        MessageBoxResult mesgBoxResult = System.Windows.MessageBox.Show
                    (ov.Message + "\nRuntimeMinutes is too big or to small. \nPlease enter valid integer within the range of possbile values.", "Incomplete Form",
                        System.Windows.MessageBoxButton.OK);
                        return;
                    }

                    // Retrieve an observable from the Observable collection
                    MovieDto found = movieList.FirstOrDefault(movie => movie.MovieId == SelectedMovie.MovieId);
                    // setter on observable will trigger INotifyPropertyChanged behind the scenes
                    // INotify notifies the Obs collection about the changes and Observable Collection            
                    found.Title = editMovieTitle.Text;
                    found.Director = editMovieDirector.Text;
                    found.Production = editMovieProduction.Text;  //System.Globalization.CultureInfo.InvariantCulture
                    found.PremiereDate = editMoviePremiereDate.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss tt", DateTimeFormatInfo.InvariantInfo);
                    found.Synopsys = editMovieSynopsis.Text;
                    found.Season = ((Season)editMovieSeason.SelectedItem).ToString();
                    found.SeasonId = editMovieSeason.SelectedIndex + 1;
                    found.ImagePath = imagePath;
                    found.FilmGenre = ((filmGenre)editFilmGenre.SelectedItem).ToString();
                    found.FilmGenreId = editFilmGenre.SelectedIndex + 1;

                    int idMovie = SelectedMovie.MovieId;
                    string title = editMovieTitle.Text;
                    string director = editMovieDirector.Text;
                    string production = editMovieProduction.Text;                  
                    string premiereDate = editMoviePremiereDate.SelectedDate.Value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    int seasonId = editMovieSeason.SelectedIndex + 1;                                    
                    string synopsys = editMovieSynopsis.Text;
                    
                    MovieDto newMovie = new MovieDto(idMovie, title, runtimeMinutes, director, production, synopsys, imagePath, premiereDate, seasonId, SelectedGenre_Id);
                    movieList = CommonService.UpdateMovie(newMovie);                    
                    this.Close(); // Update movie in db  
                }
                              
            }
        }

        private void deleteMovieBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgBoxResult = System.Windows.MessageBox.Show
                ("Are you sure you want to delete this movie?", "Delete Confirmation",
                    System.Windows.MessageBoxButton.YesNo);

            if (msgBoxResult == MessageBoxResult.Yes)
            {
                // get selected movie id
                MovieDto found = movieList.FirstOrDefault(movie => movie.MovieId == SelectedMovie.MovieId);
                int selectedMovieId = SelectedMovie.MovieId;

                found.Title = editMovieTitle.Text;
                found.Director = editMovieDirector.Text;
                found.Production = editMovieProduction.Text;  //System.Globalization.CultureInfo.InvariantCulture
                found.PremiereDate = editMoviePremiereDate.SelectedDate.Value
                    .ToString("yyyy-MM-dd HH:mm:ss tt", DateTimeFormatInfo.InvariantInfo);
                found.Synopsys = editMovieSynopsis.Text;
                found.Season = ((Season)editMovieSeason.SelectedItem).ToString();
                found.SeasonId = editMovieSeason.SelectedIndex + 1;
                found.ImagePath = imagePath;
                found.FilmGenre = ((filmGenre)editFilmGenre.SelectedItem).ToString();
                found.FilmGenreId = editFilmGenre.SelectedIndex + 1;

                movieList.Remove(SelectedMovie);
                movieList = CommonService.DeleteMovie(selectedMovieId);
                this.Close(); // Deleted movie in db
            }
            else
            {
                Console.WriteLine(" Cancel Deletion ");
                this.Close();
            }
        }

        private void cancelEditBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ChangeImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImagePath = openFileDialog.FileName;
                // strip out doubled slashes \\ to single slash/ and change orientation of slash
                UriBuilder uri = new UriBuilder(selectedImagePath);
                // convert uri back to string 
                imagePath = Uri.UnescapeDataString(uri.Path);               
            }
        }

        private void editFilmGenre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dg = sender as ComboBox;
            var selectedGenre = (filmGenre)this.editFilmGenre.SelectedItem;
            SelectedGenre_Id = selectedGenre.Filmgenre_Id;
        }
    }
}
