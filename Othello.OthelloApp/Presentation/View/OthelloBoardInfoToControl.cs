using System;
using System.Globalization;
using System.Windows.Data;
using Othello.Business.Domain.Model;
using System.Windows.Media;
using Othello.OthelloApp.Presentation.ViewModel;

namespace Othello.OthelloApp.Presentation.View
{
    public class OthelloBoardInfoToControl : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BoardInfo info = value as BoardInfo;
            if (info == null)
                return null;

            var brush = new VisualBrush();
            var boardCtrl = new OthelloBoardCtrl();
            boardCtrl.DataContext = new OthelloBoardCtrlViewModel(info.Game);
            return boardCtrl;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
