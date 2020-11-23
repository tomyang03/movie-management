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
    /// Interaction logic for EditMovie.xaml
    /// </summary>
    public partial class EditMovie : Window
    {
        public List<Season> movieSeason = new List<Season>();
        public EditMovie()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            movieSeason.Add(new Season { MovieSeason = "Fall" });
            movieSeason.Add(new Season { MovieSeason = "Winter" });
            movieSeason.Add(new Season { MovieSeason = "Spring" });
            movieSeason.Add(new Season { MovieSeason = "Summer" });

            editMovieSeason.ItemsSource = movieSeason;
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
                    this.Close(); // Update movie in db            
            }
        }

        private void deleteMovieBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgBoxResult = System.Windows.MessageBox.Show
                ("Are you sure you want to delete this movie?","Delete Confirmation", 
                    System.Windows.MessageBoxButton.YesNo);

            if (msgBoxResult == MessageBoxResult.Yes)
                this.Close(); // Delete movie in db
        }

        private void cancelEditBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
