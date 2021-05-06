using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Othello.Business.Domain.Model.Games;
using Othello.Business.Domain.Model.Boards;

namespace Othello.Business.Domain.Utils
{
    public class OthelloBoardFactoryFromString
    {
        protected virtual Board CreateBoard(List<Cell> cells)
        {
            return new XYBoard(cells);

        }
        protected virtual Board.IPosition CreatePosition(int x, int y)
        {
            return new XYPosition(x, y);
        }
        public Game Create(string textBoard, char turn, out Dictionary<char, StoneType> players)
        {
            int x = 0;
            int y = 0;
            List<Cell> cells = new List<Cell>();
            players = new Dictionary<char, StoneType>();
            foreach (var c in textBoard)
            {
                var lower = char.ToLower(c);
                if (lower == '\n')
                {
                    x = 0;
                    y++;
                }
                else if (lower == ' ')
                {
                    //スキップ
                    x++;
                }
                else if ('a' <= lower && lower <= 'z')
                {
                    if (!players.ContainsKey(lower))
                        players.Add(lower, new StoneType(players.Count));
                    cells.Add(new Cell(CreatePosition(x, y), (StoneType)players[lower]));
                    x++;
                }
                else if (lower == '_')
                {
                    cells.Add(new Cell(CreatePosition(x, y), null));
                    x++;
                }
            }
            return new Game(CreateBoard(cells), players[turn], players.Count);

            throw new NotImplementedException("This board type is not implemented.");
        }
    }
    public class HexagonalOthelloBoardFactoryFromString : OthelloBoardFactoryFromString
    {
        protected override Board CreateBoard(List<Cell> cells)
        {
            return new HexagonalBoard(cells);

        }
        protected override Board.IPosition CreatePosition(int x, int y)
        {
            return new HexagonalPosition(x, y);
        }
    }
}
