using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Business.Domain.Model
{

    public class Game
    {
        public Board Board { get; private set; }
        public StoneType CurrentTurn { get; private set; }
        public GameResult Result { get; private set; }
        public bool IsEnd { get => Result != null; }
        public StoneType LastStoneType {get; private set;}

        public Game(Board board, StoneType turn, StoneType lastStoneType)
        {
            Board = board;
            CurrentTurn = turn;
            LastStoneType = lastStoneType;
            Result = null;
        }

        public Game(Board board)
        {
            //Board = new Board(8, 8);
            Board = board;
            CurrentTurn = StoneType.Player1;
            LastStoneType = StoneType.Player2;
            Result = null;
        }

        public void PutStone(Position putPosition, StoneType putStoneType)
        {
            if (IsEnd)
                throw new InvalidOperationException("This game is finished.");

            if (CurrentTurn != putStoneType)
                throw new InvalidOperationException("Not your turn.");

            if (!HasReversiableStone(putPosition, putStoneType))
                throw new InvalidOperationException("There is no reversiable stone.");

            Board.Cell(putPosition).PutStone(putStoneType);
            Reverse(putPosition, putStoneType);
            fowardTurn();
        }

        public List<Cell> GetCanPutCellsOfCurrentTurn()
        {
            return GetCanPutCells(CurrentTurn);
        }

        private void fowardTurn()
        {
            var nextTurn = CurrentTurn.Next(LastStoneType);

            do
            {
                if (CanPutSomewhare(nextTurn))
                {
                    CurrentTurn = nextTurn;
                    return;
                }
                nextTurn = nextTurn.Next(LastStoneType);

            } while (nextTurn != CurrentTurn.Next(LastStoneType));

            Result = CreateGameResult();
        }
        //private void fowardTurn()
        //{
        //    var nextTurn = CurrentTurn.Next();

        //    while(true)
        //    {
        //        if(CanPutSomewhare(nextTurn))
        //        {
        //            CurrentTurn = nextTurn;
        //            return;
        //        }

        //        if (CurrentTurn == nextTurn)
        //        {
        //            Result = CreateGameResult();
        //            return;
        //        }
        //        nextTurn = nextTurn.Next();
        //    } 
        //}

        private GameResult CreateGameResult()
        {
            return new GameResult(Board.CalcMostStoneTypes(LastStoneType));
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

        private void Reverse(Position putPosition, StoneType putStoneType)
        {
            new SandwichStoneTracer(Board).Trace( putPosition,
                                                  putStoneType,
                                                  (c) => { c.ChangeToMyStoneType(putStoneType); });
        }

        private bool HasReversiableStone(Position putPosition, StoneType putStoneType)
        {
            return CountReversiableStones(putPosition, putStoneType) > 0;
        }

        private int CountReversiableStones(Position putPosition, StoneType putStoneType)
        {
            int count = 0;
            new SandwichStoneTracer(Board).Trace(putPosition,
                                                  putStoneType,
                                                  (c) => { count++; });
            return count;
        }

        public Dictionary<StoneType, int> CountStones()
        {
            return Board.CountStones(LastStoneType);
        }
    }
}
