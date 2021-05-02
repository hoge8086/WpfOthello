using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Othello.Business.Domain.Model;

namespace Othello.Business.Domain.Utils
{
    public class OthelloBoardFactoryFromString
    {
        public Game Create(string textBoard, char turn, out Dictionary<char, StoneType> players)
        {
            int x = 0;
            int y = 0;
            List<Cell> cells = new List<Cell>();
            //Dictionary<char, int> players = new Dictionary<char, StoneType>();
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
                    cells.Add(new Cell(new Position(x, y), (StoneType)players[lower]));
                    x++;
                }
                else if (lower == '_')
                {
                    cells.Add(new Cell(new Position(x, y), null));
                    x++;
                }
            }
            return new Game(new Board(cells), players[turn], players.Count);
        }
    }
}
