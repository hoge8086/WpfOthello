using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Business.Domain.Model.Games
{
    public abstract class Board
    {
        public interface IPosition
        {
            IPosition GetNeighber(IDirection dir);
        }
        public class IDirection
        {
        }

        //MEMO:Cellクラス内でもPositionクラスを保持しているので、Positionの2重管理になっているため本来はList<Cell>がよいが、検索性能を上げるためDictionaryを使用する.
        protected Dictionary<IPosition, Cell> cells;

        public Board(List<Cell> cellsList)
        {
            cells = new Dictionary<IPosition, Cell>();
            foreach(var cell in cellsList)
                cells.Add(cell.Position, cell);
            ShowDebug();
        }

        public Cell Cell(IPosition position)
        {
            if (!IsValidRange(position))
                return null;
                //throw new IndexOutOfRangeException();

            return cells[position];
        }

        public bool IsValidRange(IPosition position)
        {
            if (!cells.ContainsKey(position))
                return false;

            return true;
        }

        public abstract List<IDirection> Directions { get; }

        public Cell GetNeighberCell(IPosition position, IDirection direction)
        {
            if (!IsValidRange(position))
                throw new ArgumentOutOfRangeException("Invalid position");

            var neighberPosition = position.GetNeighber(direction);//new Position(position.X + direction.dX, position.Y + direction.dY);
            if (!IsValidRange(neighberPosition))
                return null;

            return Cell(neighberPosition);
        }

        private int CountStone(StoneType stoneType)
        {
            return GetAllCells().Count(cell => stoneType.Equals(cell.Stone));
        }

        public Dictionary<StoneType, int> CountStones(StoneTypes stoneTypes)
        {
            Dictionary<StoneType, int> counter = new Dictionary<StoneType, int>();
            foreach(var stoneType in stoneTypes.Types)
            {
                counter.Add(stoneType, CountStone(stoneType));
            }
            return counter;
        }

        public List<StoneType> CalcMostStoneTypes(StoneTypes stoneTypes)
        {
            Dictionary<StoneType, int> counter = CountStones(stoneTypes);
            int maxCount = counter.OrderBy(x => x.Value).First().Value;
            return counter.Where(x => x.Value >= maxCount).Select(x => x.Key).ToList();
        }

        public List<Cell> GetAllCells()
        {
            return cells.Values.ToList();
        }

        public abstract void ShowDebug();
    }

}
