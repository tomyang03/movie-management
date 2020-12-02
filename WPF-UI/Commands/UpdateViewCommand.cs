using WPF_UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF_UI.Commands
{
    public class UpdateViewCommand : ICommand
    {
        private MainViewModel viewModel;

        // Pass MainViewModel through constructor
        public UpdateViewCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            switch (parameter)
            {
                case "MovieList":
                    viewModel.SelectedViewModel = new MovieListViewModel();
                    break;
                case "NewMovie":
                    viewModel.SelectedViewModel = new NewMovieViewModel();
                    break;
                case "MovieDetail":
                    viewModel.SelectedViewModel = new MovieDetailViewModel();
                    break;
                default:
                    break;
            }
        }
    }
}
