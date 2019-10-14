using Othello.Business.Domain.Utils;
using System;
using System.Globalization;
using System.Windows.Data;
using Othello.Business.Domain.Model;

namespace Othello.OthelloApp.Presentation.View
{
    public class OthelloBoardTypeToInfo : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            OthelloBoardType? boardType = value as OthelloBoardType?;
            if(boardType  == null)
                throw new NotImplementedException();

            var factory = new OthelloBoardFactoryImpl();
            return factory.Create(boardType ?? OthelloBoardType.Standard);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
