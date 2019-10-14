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
            var type = value as StoneType?;
            if (type == StoneType.Player1)
                return Brushes.Black.Color;
            if (type == StoneType.Player2)
                return Brushes.White.Color;
            if (type == StoneType.Player3)
                return Brushes.Blue.Color;
            if (type == StoneType.Player4)
                return Brushes.Yellow.Color;
            if (type == StoneType.Player5)
                return Brushes.Green.Color;
            if (type == StoneType.Player6)
                return Brushes.Orange.Color;
            if (type == StoneType.Player7)
                return Brushes.Peru.Color;
            if (type == StoneType.Player8)
                return Brushes.Gold.Color;

            return Brushes.Red.Color;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
