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
using System.Windows.Navigation;
using System.Windows.Shapes;

using ServiceLayer;

namespace WPF_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            MovieList objMovieList = new MovieList();
            Console.WriteLine("Username: {0}", loginUsername.Text);
            Console.WriteLine("Password: {0}", loginPassword.Password);

            if (String.IsNullOrEmpty(loginUsername.Text) || String.IsNullOrEmpty(loginPassword.Password))
            {
                MessageBoxResult mesgBoxResult = System.Windows.MessageBox.Show
                    ("Username or password missing. \nType in anything to test.", "Oops!",
                        System.Windows.MessageBoxButton.OK);
            }
            else
            {
                if(CommonService.ValidUser(loginUsername.Text, loginPassword.Password))
                {
                    Console.WriteLine("Welcome {0}", loginUsername.Text);
                    this.Visibility = Visibility.Hidden; // Hides login window
                    objMovieList.Show();
                }
                else
                {
                    MessageBoxResult mesgBoxResult = System.Windows.MessageBox.Show
                   ("Invalid Username or password. \n Please verify.", "Who are you?",
                       System.Windows.MessageBoxButton.OK);
                }
                
            }
        }
    }
}
