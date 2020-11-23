using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_UI.Commands;

namespace WPF_UI.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private BaseViewModel selectedViewModel = new MovieListViewModel();
        
        public BaseViewModel SelectedViewModel
        {
            get { return selectedViewModel; }
            set 
            {
                selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }
        public ICommand UpdateViewCommand { get; set; }

        public LoginViewModel()
        {
            //UpdateViewCommand = new UpdateViewCommand(this);
        }
    }
}
