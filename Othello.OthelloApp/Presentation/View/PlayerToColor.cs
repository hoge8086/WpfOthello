using Othello.Business.Domain.Model;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Othello.OthelloApp.Presentation.View
{
    public class PlayerToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = value as StoneType;
            if (type.Id == 0)
                return Brushes.Black.Color;
            if (type.Id == 1)
                return Brushes.White.Color;
            if (type.Id == 2)
                return Brushes.Red.Color;
            if (type.Id == 3)
                return Brushes.Yellow.Color;
            if (type.Id == 4)
                return Brushes.Green.Color;
            if (type.Id == 5)
                return Brushes.Blue.Color;
            if (type.Id == 6)
                return Brushes.Peru.Color;
            if (type.Id == 7)
                return Brushes.Gold.Color;

            return Brushes.Plum.Color;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
