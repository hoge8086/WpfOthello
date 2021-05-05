using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Othello.Business.Domain.Model.Games;
using Othello.Business.Domain.Utils;


namespace Othello.Business.ApplicationService
{
    public class OthelloApplicationService
    {
        private Game game;
        private OthelloBoardType currentBoardType;
        private IOthelloBoardFactory othelloBoardFactory;

        public OthelloApplicationService(IOthelloBoardFactory othelloBoardFactory)
        {
            this.othelloBoardFactory = othelloBoardFactory;
            Restart();
        }
        public void Restart(OthelloBoardType? othelloBoardType = null)
        {
            OthelloBoardType selected = othelloBoardType ?? currentBoardType;
            game = othelloBoardFactory.Create(selected).Game;
            currentBoardType = selected;
        }

        public void PutStone(Board.IPosition pos, StoneType stone)
        {
            game.PutStone(pos, stone);
            game.Board.ShowDebug();
        }

        public Game GetGame()
        {
            return game;
        }
    }
}
