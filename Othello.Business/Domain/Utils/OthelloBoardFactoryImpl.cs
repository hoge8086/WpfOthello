using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Othello.Business.Domain.Model.Games;

namespace Othello.Business.Domain.Utils
{
    public enum BoardFormType
    {
        XY,
        Hexagonal,
    }

    public class OthelloBoardFactoryImpl : IOthelloBoardFactory
    {
        public BoardInfo Create(OthelloBoardType boardType)
        {
            var info = tbl.FirstOrDefault(x => x.boardType == boardType);
            if (info == null)
                throw new NotImplementedException("That othello board is not implemented.");

            OthelloBoardFactoryFromString factory = null;
            if(info.formType == BoardFormType.Hexagonal)
                factory = new HexagonalOthelloBoardFactoryFromString();
            else if(info.formType == BoardFormType.XY)
                factory = new OthelloBoardFactoryFromString();

            Dictionary<char, StoneType> players;
            var game = factory.Create(info.board, info.first, out players);

            return new BoardInfo() { BoardType = boardType, Game = game, PlayerNumber = players.Count };
        }

        public List<BoardInfo> CreateAll()
        {
            throw new NotImplementedException();
        }

        private class InnerBoardInfo {
            public OthelloBoardType boardType;
            public string board;
            public char first;
            public BoardFormType formType;
        }

        private InnerBoardInfo[] tbl = new InnerBoardInfo[]
        {
            new InnerBoardInfo()
            {
                boardType=OthelloBoardType.Standard,
                formType = BoardFormType.XY,
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
                formType = BoardFormType.XY,
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
                formType = BoardFormType.XY,
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
                formType = BoardFormType.XY,
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
                formType = BoardFormType.Hexagonal,
                first='a',
                board=
                "____\n" +
                "_ab_\n" +
                "_ba_\n" +
                "____\n"
                //"   ____  \n" +
                //"  ______\n" +
                //" ___a___\n" +
                //"___bbb__\n" +
                //" ___a___\n" +
                //"  ______\n" +
                //"   ____  \n"
            },
            new InnerBoardInfo()
            {
                boardType=OthelloBoardType.Board5,
                formType = BoardFormType.Hexagonal,
                first='a',
                board=
                " ___\n" +
                "_ab_\n" +
                "_bac_\n" +
                "_c__\n" +
                " ___\n"
            }
        };

    }
}
