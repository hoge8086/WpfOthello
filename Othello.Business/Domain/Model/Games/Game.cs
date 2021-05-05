using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Business.Domain.Model.Games
{

    public class Game
    {
        public Board Board { get; private set; }
        public StoneType CurrentTurn { get; private set; }
        public GameResult Result { get; private set; }
        public bool IsEnd { get => Result != null; }
        //public StoneType LastStoneType {get; private set;}
        public StoneTypes StoneTypes {get; private set;}

        public Game(Board board, StoneType turn, int numOfStones)
        {
            Board = board;
            CurrentTurn = turn;
            StoneTypes = new StoneTypes(numOfStones);
            Result = null;
        }

        //public Game(Board board)
        //{
        //    //Board = new Board(8, 8);
        //    Board = board;
        //    CurrentTurn = StoneType.Player1;
        //    LastStoneType = StoneType.Player2;
        //    Result = null;
        //}

        public void PutStone(Board.IPosition putPosition, StoneType putStoneType)
        {
            if (IsEnd)
                throw new InvalidOperationException("This game is finished.");

            if (!CurrentTurn.Equals(putStoneType))
                throw new InvalidOperationException("Not your turn.");

            if (!HasReversiableStone(putPosition, putStoneType))
                throw new InvalidOperationException("There is no reversiable stone.");

            Board.Cell(putPosition).PutStone(putStoneType);
            Reverse(putPosition, putStoneType);
            FowardTurn();
        }

        public List<Cell> GetCanPutCellsOfCurrentTurn()
        {
            return GetCanPutCells(CurrentTurn);
        }

        private void FowardTurn()
        {
            StoneType turn = CurrentTurn;
            do
            {
                turn = StoneTypes.Next(turn);

                if (CanPutSomewhare(turn))
                {
                    CurrentTurn = turn;
                    return;
                }

            } while (!turn.Equals(CurrentTurn));

            Result = CreateGameResult();
        }

        private GameResult CreateGameResult()
        {
            return new GameResult(Board.CalcMostStoneTypes(StoneTypes));
        }

        private bool CanPutSomewhare(StoneType putStoneType)
        {
            return GetCanPutCells(putStoneType).Count > 0;
        }

        private List<Cell> GetCanPutCells(StoneType putStoneType)
        {
            List<Cell> cells = new List<Cell>();
            foreach(var cell in Board.GetAllCells())
            {
                if (cell.IsEmpty() && HasReversiableStone(cell.Position, putStoneType))
                    cells.Add(cell);

            }
            return cells;
        }

        private void Reverse(Board.IPosition putPosition, StoneType putStoneType)
        {
            new SandwichStoneTracer(Board).Trace( putPosition,
                                                  putStoneType,
                                                  (c) => { c.ChangeToMyStoneType(putStoneType); });
        }

        private bool HasReversiableStone(Board.IPosition putPosition, StoneType putStoneType)
        {
            return CountReversiableStones(putPosition, putStoneType) > 0;
        }

        private int CountReversiableStones(Board.IPosition putPosition, StoneType putStoneType)
        {
            int count = 0;
            new SandwichStoneTracer(Board).Trace(putPosition,
                                                  putStoneType,
                                                  (c) => { count++; });
            return count;
        }

        public Dictionary<StoneType, int> CountStones()
        {
            return Board.CountStones(StoneTypes);
        }
    }
}
