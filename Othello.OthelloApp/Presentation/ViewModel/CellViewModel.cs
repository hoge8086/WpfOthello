using Othello.Business.Domain.Model;
using System.ComponentModel;
using System.Windows.Media;

public enum CellType
{
    NotCell,
    Empty,
    EmptyAndCanPut,
    PutStone,
}
namespace Othello.OthelloApp.Presentation.ViewModel
{
    public class CellViewModel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public CellType CellType { get; set; }
        public StoneType StoneType { get; set; }
    }

}
