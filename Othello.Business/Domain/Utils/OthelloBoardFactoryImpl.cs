using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Othello.Business.Domain.Model;

namespace Othello.Business.Domain.Utils
{
    public class OthelloBoardFactoryImpl : IOthelloBoardFactory
    {
        public BoardInfo Create(OthelloBoardType boardType)
        {
            var info = tbl.FirstOrDefault(x => x.boardType == boardType);
            if (info == null)
                throw new NotImplementedException("That othello board is not implemented.");
            var factory = new OthelloBoardFactoryFromString();
            Dictionary<char, StoneType> players;
            var game = factory.Create(info.board, info.first, out players);

            return new BoardInfo() { BoardType = boardType, Game = game, PlayerNumber = players.Count };
        }

        public List<BoardInfo> CreateAll()
        {
            throw new NotImplementedException();
        }

        private class InnerBoardInfo { public OthelloBoardType boardType; public string board; public char first;}

        private InnerBoardInfo[] tbl = new InnerBoardInfo[]
        {
            new InnerBoardInfo()
            {
                boardType=OthelloBoardType.Standard,
                first='a',
                board=
                "________\n" +
                "________\n" +
                "________\n" +
                "___ab___\n" +
                "___ba___\n" +
                "________\n" +
                "________\n" +
                "________"
            },
            new InnerBoardInfo()
            {
                boardType=OthelloBoardType.Board1,
                first='a',
                board=
                "____\n" +
                "_ab_\n" +
                "_ba_\n" +
                "____"
            },
            new InnerBoardInfo()
            {
                boardType=OthelloBoardType.Board2,
                first='a',
                board=
                "____\n" +
                "_ab_\n" +
                "_ba_\n" +
                "____\n" +
                "____\n" +
                "_ab_\n" +
                "_ab_\n" +
                "____\n"
            },
            new InnerBoardInfo()
            {
                boardType=OthelloBoardType.Board3,
                first='a',
                board=
                "  _________  \n" +
                "  _________  \n" +
                "_____cab_____\n" +
                "_____bac_____\n" +
                "_____cab_____\n" +
                "  _________  \n" +
                "  _________  \n"
            },
            new InnerBoardInfo()
            {
                boardType=OthelloBoardType.Board4,
                first='a',
                board=
                "   ______________\n" +
                "   ______________\n" +
                "   ______________\n" +
                "____bcdeabca_____\n" +
                "____a      b_____\n" +
                "____e      c_____\n" +
                "____d      d_____\n" +
                "____c      e_____\n" +
                "____b      a__\n" +
                "____a      b__\n" +
                "____edcbaedc__\n" +
                "______________\n"
            }
        };

    }
}
