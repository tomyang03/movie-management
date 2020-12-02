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
    /// Interaction logic for NewMovie.xaml
    /// </summary>
    public partial class NewMovie : Window
    {
        public List<Season> movieSeason = new List<Season>();
        public NewMovie()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            movieSeason.Add(new Season { MovieSeason = "Fall" });
            movieSeason.Add(new Season { MovieSeason = "Winter" });
            movieSeason.Add(new Season { MovieSeason = "Spring" });
            movieSeason.Add(new Season { MovieSeason = "Summer" });

            addMovieSeason.ItemsSource = movieSeason;
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
            else
            {
                MessageBoxResult msgBoxResult = System.Windows.MessageBox.Show
                    ("Add this movie to the database?", "Add Confirmation",
                        System.Windows.MessageBoxButton.YesNo);

                if (msgBoxResult == MessageBoxResult.Yes)
                    this.Close(); // Add new movie to db
            }
        }

        private void cancelAddBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
