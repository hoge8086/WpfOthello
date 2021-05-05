using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Business.Domain.Model.Games
{
    public class SandwichStoneTracer
    {
        private readonly Board board;

        public SandwichStoneTracer(Board board)
        {
            this.board = board;
        }

        public delegate void ActionForSandwichedStoneDelegate(Cell cell);

        public void Trace(Board.IPosition position, StoneType putStoneType, ActionForSandwichedStoneDelegate action)
        {
            if (position == null || action == null)
                throw new ArgumentException();

            foreach(var dir in board.Directions)
            {
                var neighberCell = board.GetNeighberCell(position, dir);
                TraceSub(neighberCell, putStoneType, dir, action);
            }
        }

        private bool TraceSub(Cell tracingCell, StoneType putStoneType, Board.IDirection dir, ActionForSandwichedStoneDelegate action)
        {
            if (tracingCell == null)
                return false;

            if (tracingCell.Stone == null)
                return false;

            if (putStoneType.Equals(tracingCell.Stone))
                return true;

            var neighberCell = board.GetNeighberCell(tracingCell.Position, dir);

            bool isSandwiched = TraceSub(neighberCell, putStoneType, dir, action);

            if (isSandwiched)
                action(tracingCell);

            return isSandwiched;
        }
    }
}
