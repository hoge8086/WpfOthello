using System;
using System.ComponentModel;
using System.Windows.Input;

using Othello.Business.ApplicationService;

namespace Othello.OthelloApp.Presentation.ViewModel
{
    public class PutStoneCommand : ICommand
    {
        private OthelloBoardCtrlViewModel board;
        public event EventHandler CanExecuteChanged;

        public PutStoneCommand(OthelloBoardCtrlViewModel board)
        {
            this.board = board;
            this.board.PropertyChanged += OnCanExecuteChanged;
        }

        public void OnCanExecuteChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(null, EventArgs.Empty);
        }


        public bool CanExecute(object parameter)
        {
            var cell = parameter as CellViewModel;
            if (cell == null)
                return false;

            return board.Board[cell.Y][cell.X].CellType == CellType.EmptyAndCanPut;
        }
        public void Execute(object parameter)
        {
            var cell = parameter as CellViewModel;
            if (cell == null)
                return;

            board.PutStone(cell.X, cell.Y);
        }
    }
}
