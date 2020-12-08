using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPF_UI
{

    // ConverterFunction: used to subtract 1 from SelectedMovie.FilmGenreId or SelectedMovie.SeasonId
    // to match the index of the comboBox which is zero Based
    // If we do not use the  converter, the FilmGenre or Season will be off by 1
    //[ValueConversion(typeof(int), typeof(int))]
    public class SeasonAndGenreConverter : IValueConverter
    {
        // Convert: convert source to target
        // value is coming from the source (SelectedMovie.FilmGenreId or  SelectedMovie.SeasonId)
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Since the index of the comboBox is zero based , we need to subtract 1
            // from the value of the (SelectedMovie.FilmGenreId) 
            int selectedIndex = (int) value - 1;
            return selectedIndex;
        }

        // Used to convert tagert back to source
        // Since Binding Mode in EditMovie.XAML is set to OneWay 
        // for the comboBoxes editFilmGenre and editMovieSeason this Function will not be triggered
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
