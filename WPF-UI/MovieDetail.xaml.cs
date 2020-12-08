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
using WPF_UI.DataAccess;

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for MovieDetail.xaml
    /// </summary>
    public partial class MovieDetail : Window
    {
        public DataAccessLayer db;
        public MovieDetail(int movieID)
        {            
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            MovieDto theMovie = CommonService.ReadMovie(movieID);                          
            this.DataContext = theMovie;

        }

        private void closeMovieDetai_Btn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
