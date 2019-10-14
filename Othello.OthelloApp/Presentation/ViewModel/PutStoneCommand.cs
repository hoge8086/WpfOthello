using System;
using System.Windows.Input;

using Othello.Business.ApplicationService;

namespace Othello.OthelloApp.Presentation.ViewModel
{
    public class PutStoneCommand : ICommand
    {
        private OthelloApplicationService service;
        private Action updateBoard;
        public PutStoneCommand(OthelloApplicationService service, Action updateBoard) { this.service = service; this.updateBoard = updateBoard; }
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) { return true; }
        public void Execute(object parameter)
        {
            var cell = parameter as CellViewModel;
            if (cell == null)
                return;

            service.PutStone(cell.X, cell.Y, service.GetGame().CurrentTurn);
            updateBoard();
        }
    }
}
