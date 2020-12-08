﻿using System;
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
using System.Data;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for EditMovie.xaml
    /// </summary>
    public partial class EditMovie : Window
    {
        public List<Season> movieSeason = new List<Season>();
        public MovieDto SelectedMovie;
        public string imagePath;
        public List<MovieDto> movieList;
        public int SelectedGenre_Id;

        public EditMovie(MovieDto SelectedMovie ,List<MovieDto> movies)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            movieSeason.Add(new Season { MovieSeason = "Fall" });
            movieSeason.Add(new Season { MovieSeason = "Winter" });
            movieSeason.Add(new Season { MovieSeason = "Spring" });
            movieSeason.Add(new Season { MovieSeason = "Summer" });

            //populate the film genres
            List<filmGenre> genreList = CommonService.findAllGenres();

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
            //idMovie = 3;
            //MessageBoxResult msgBoxResult = System.Windows.MessageBox.Show
            //    ("Are you sure you want to delete this movie?","Delete Confirmation", 
            //        System.Windows.MessageBoxButton.YesNo);

            //if (msgBoxResult == MessageBoxResult.Yes)
            //{
            //    Console.WriteLine(" Proceed Delete id: {0} ", idMovie );
            //    if (CommonService.DeleteMovie(idMovie))
            //    {
            //        Console.WriteLine(" Successfully Deleted ");
            //    }
            //    else
            //    {
            //        Console.WriteLine(" Failure on Deleting ");
            //    }
            //    this.Close(); // Deleted movie in db
            //}
            //else
            //{
            //    Console.WriteLine(" Cancel Deleting ");
            //    this.Close(); 
            //}
                
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