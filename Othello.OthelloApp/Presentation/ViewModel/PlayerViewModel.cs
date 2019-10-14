using Othello.Business.Domain.Model;

namespace Othello.OthelloApp.Presentation.ViewModel
{
    public class PlayerViewModel
    {
        public StoneType StoneType { get; set; }
        public int StoneCount { get; set; }
        public bool IsCurrentTurn { get; set; }
    }
}
