using System;
using System.Windows.Input;

using Othello.Business.ApplicationService;
using Othello.Business.Domain.Model;

namespace Othello.OthelloApp.Presentation.ViewModel
{
    public class RestartGameCommand : ICommand
    {
        private OthelloApplicationService service;
        private Action updateBoard;
        public RestartGameCommand(OthelloApplicationService service, Action updateBoard) { this.service = service; this.updateBoard = updateBoard; }
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) { return true; }
        public void Execute(object parameter)
        {
            OthelloBoardType? boardType = parameter as OthelloBoardType?;
            service.Restart(boardType);
            updateBoard();
        }
    }
}
