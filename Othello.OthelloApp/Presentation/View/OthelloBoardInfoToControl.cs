using System;
using System.Globalization;
using System.Windows.Data;
using Othello.Business.Domain.Model.Games;
using System.Windows.Media;
using Othello.OthelloApp.Presentation.ViewModel;
using Othello.Business.Domain.Model.Boards;
using System.Windows.Controls;

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
            UserControl boardCtrl = null;// new OthelloBoardCtrl();
            if(info.Game.Board is XYBoard)
            {
                boardCtrl = new OthelloBoardCtrl(); 
                boardCtrl.DataContext = new XYBoardViewModel(info.Game);
            }
            else if(info.Game.Board is HexagonalBoard)
            {
                boardCtrl = new HexagonalOthelloBoardCtrl(); 
                boardCtrl.DataContext = new HexagonalBoardViewModel(info.Game);
            }
            return boardCtrl;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
