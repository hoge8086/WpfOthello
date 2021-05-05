using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Business.Domain.Model.Games
{
    public class Cell
    {
        public Board.IPosition Position { get; private set; }

        //石が置いてない場合はnull
        public StoneType Stone { get; private set; }
        public bool IsEmpty() { return Stone == null; }

        public Cell(Board.IPosition position, StoneType putStone = null)
        {
            this.Position = position;
            this.Stone = putStone;
        }

        public void PutStone(StoneType stoneType)
        {
            if (Stone != null)
                throw new InvalidOperationException("Can not put a stone. There is already a stone.");
            this.Stone = stoneType;
        }

        //public void ReverseStone()
        public void ChangeToMyStoneType(StoneType myStoneType)
        {
            if (Stone == null)
                throw new InvalidOperationException("Can not Reverse the stone. There is no stone.");

            this.Stone = myStoneType;
        }
    }
}
