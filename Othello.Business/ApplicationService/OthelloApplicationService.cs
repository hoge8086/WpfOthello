using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Othello.Business.Domain.Model;
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

        public void PutStone(int x, int y, StoneType stone)
        {
            game.PutStone(new Position(x, y), stone);
            game.Board.ShowDebug();
        }

        public Game GetGame()
        {
            return game;
        }
    }
}
