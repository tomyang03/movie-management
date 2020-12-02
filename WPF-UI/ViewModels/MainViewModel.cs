using WPF_UI.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF_UI.ViewModels
{
    // This view model contains the overall application
    // It displays the selected view model and controls navigation
    public class MainViewModel : BaseViewModel
    {
        private BaseViewModel selectedViewModel = new LoginViewModel();

        public BaseViewModel SelectedViewModel
        {
            get { return selectedViewModel; }
            set 
            { 
                selectedViewModel = value;
                // When selected ViewModel changes
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public ICommand UpdateViewCommand { get; set; }

        public MainViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
        }
    }
}
