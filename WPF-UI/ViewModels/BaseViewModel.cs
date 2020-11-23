using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_UI.ViewModels
{
    // Supre Class - applies to all ViewModels
    public class BaseViewModel : INotifyPropertyChanged
    {
        // When property (ViewModel) changes, display correct View 
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertName)
        { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertName));
        }
    }
}
