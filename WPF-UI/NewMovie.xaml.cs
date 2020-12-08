using WPF_UI.DTO;
using Microsoft.Win32;
using WPF_UI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for NewMovie.xaml
    /// </summary>
    public partial class NewMovie : Window
    {
        public List<Season> movieSeason = new List<Season>();
        public List<MovieDto> movieList;
        public string imagePath;
        public bool imageLoaded;

        public int SelectedGenre_Id;

        public NewMovie(List<MovieDto> movies)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            movieSeason.Add(new Season { MovieSeason = "Fall" });
            movieSeason.Add(new Season { MovieSeason = "Winter" });
            movieSeason.Add(new Season { MovieSeason = "Spring" });
            movieSeason.Add(new Season { MovieSeason = "Summer" });

            addMovieSeason.ItemsSource = movieSeason;

            //populate the film genres
            List<filmGenre> genreList = CommonService.findAllGenres();
            addFilmGenre.ItemsSource = genreList;
            SelectedGenre_Id = 1;

            // movieList: a reference to movies from the MovieList.xaml            
            movieList = movies;
        }

        private void addMovieBtn_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(addMovieTitle.Text) || String.IsNullOrEmpty(addMovieDirector.Text) ||
                String.IsNullOrEmpty(addMovieProduction.Text) || String.IsNullOrEmpty(addMovieRuntime.Text) ||
                String.IsNullOrEmpty(addMoviePremiereDate.Text) || String.IsNullOrEmpty(addMovieSeason.Text) ||
                String.IsNullOrEmpty(addMovieSynopsis.Text))
            {
                MessageBoxResult mesgBoxResult = System.Windows.MessageBox.Show
                    ("Some fields are missing. \nType in anything to test.", "Incomplete Form",
                        System.Windows.MessageBoxButton.OK);
            }
            else if (!imageLoaded)
            {
                MessageBoxResult mesgBoxResult = System.Windows.MessageBox.Show
                    ("Image not selected. \nPlease select an Image.", "Incomplete Form",
                        System.Windows.MessageBoxButton.OK);
            }
            else
            {
                MessageBoxResult msgBoxResult = System.Windows.MessageBox.Show
                    ("Add this movie to the database?", "Add Confirmation",
                        System.Windows.MessageBoxButton.YesNo);

                if (msgBoxResult == MessageBoxResult.Yes)
                {
                    
                    int runtimeMinutes = 0;
                    try
                    {
                        runtimeMinutes = Convert.ToInt32(addMovieRuntime.Text);
                    }
                    catch (FormatException f)
                    {
                        MessageBoxResult mesgBoxResult = System.Windows.MessageBox.Show
                    ("Runtime is not a valid integer. \nPlease enter an integer.", "Incomplete Form",
                        System.Windows.MessageBoxButton.OK);
                        return;
                    }
                    catch (OverflowException ov)
                    {
                        MessageBoxResult mesgBoxResult = System.Windows.MessageBox.Show
                    ("RuntimeMinutes is too big or to small. \nPlease enter valid integer within the range of possible values.", "Incomplete Form",
                        System.Windows.MessageBoxButton.OK);
                        return;
                    }

                    string title = addMovieTitle.Text;
                    string director = addMovieDirector.Text;
                    string production = addMovieProduction.Text;
                    string runtime = addMovieRuntime.Text;
                    int seasonId = addMovieSeason.SelectedIndex + 1;                    
                    string premiereDate = addMoviePremiereDate.SelectedDate.Value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    string synopsis = addMovieSynopsis.Text;
                   
                    MovieDto newMovie = new MovieDto(title, runtimeMinutes, director, production, synopsis, imagePath, premiereDate, seasonId, SelectedGenre_Id);
                    movieList = CommonService.SaveMovie(newMovie);                    
                    this.Close(); // Add new movie to db
                }
                    
            }
        }

        private void cancelAddBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // When the user selects a season from the comboBox, assign a new index to seasonId
        // based on the value that the user has picked
        private void addMovieSeasonComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // + 1, because SelectedIndex starts from 0 (0 for Fall)
            int seasonId = addMovieSeason.SelectedIndex + 1 ;          
        }

        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImagePath = openFileDialog.FileName;
                // strip out doubled slashes \\ to single slash/ and change orientation of slash
                UriBuilder uri = new UriBuilder(selectedImagePath);
                // convert uri back to string 
                string sImagePath = Uri.UnescapeDataString(uri.Path);
                // extract the portion from sImagePath which starts with images/
                string pattern = @"images+/(.*)";
                Regex rg = new Regex(pattern);
                string regexMatch = rg.Match(sImagePath).Value;
                // set the imagePath of the field variable as an relative path
                imagePath = @"../../" + regexMatch;
                Console.WriteLine(imagePath);
                imageLoaded = true;               
            }
        }

        private void addFilmGenre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dg = sender as ComboBox;
            var selectedGenre = (filmGenre)this.addFilmGenre.SelectedItem;
            SelectedGenre_Id = selectedGenre.Filmgenre_Id;
        }
    }
}
