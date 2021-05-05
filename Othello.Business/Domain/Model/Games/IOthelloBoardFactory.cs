using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Business.Domain.Model.Games
{
    public interface IOthelloBoardFactory
    {
        BoardInfo Create(OthelloBoardType boardType);
        List<BoardInfo> CreateAll();
    }

    public class BoardInfo
    {
        public OthelloBoardType BoardType { get; set; }
        public int PlayerNumber { get; set; }
        public Game Game { get; set; }
    }

    public enum OthelloBoardType
    {
        Standard,
        Board1,
        Board2,
        Board3,
        Board4,
        Board5,
    }

}
