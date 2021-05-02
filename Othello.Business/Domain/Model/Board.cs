using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Business.Domain.Model
{
    public class Board
    {
        //MEMO:Cellクラス内でもPositionクラスを保持しているので、Positionの2重管理になっているため本来はList<Cell>がよいが、検索性能を上げるためDictionaryを使用する.
        private Dictionary<Position, Cell> cells;
        public int Width { get; private set;}
        public int Height{ get; private set; }

        public Board(List<Cell> cellsList)
        {
            cells = new Dictionary<Position, Model.Cell>();
            foreach(var cell in cellsList)
                cells.Add(cell.Position, cell);
            Width = cellsList.Max(c => c.Position.X) + 1;
            Height = cellsList.Max(c => c.Position.Y) + 1;
            ShowDebug();
            
        }

        //public Board()
        //{
        //    Width = 8;
        //    Height = 8;

        //    //セル作成
        //    cells = new Dictionary<Position, Cell>();
        //    for(int y=0; y<Height; y++)
        //    {
        //        for(int x=0; x<Width; x++)
        //        {
        //            var pos = new Position(x, y);
        //            cells.Add(pos, new Model.Cell(pos, null));
        //        }
        //    }

        //    //初期配置
        //    try{
        //        Cell(new Position(Width / 2 - 1, Height / 2 - 1)).PutStone(StoneType.Player1);
        //        Cell(new Position(Width / 2, Height / 2)).PutStone(StoneType.Player1);
        //        Cell(new Position(Width / 2 - 1, Height / 2)).PutStone(StoneType.Player2);
        //        Cell(new Position(Width / 2, Height / 2 - 1)).PutStone(StoneType.Player2);
        //    }catch
        //    {
        //        throw new InvalidProgramException("Initialize borad failed.");
        //    }
        //}

        /// <summary>
        /// セルを返す
        /// 範囲内の指定座標にセルがない場合は、nullが返る
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Cell Cell(Position position)
        {
            if (!IsValidRange(position))
                return null;
                //throw new IndexOutOfRangeException();

            return cells[position];
        }

        public bool IsValidRange(Position position)
        {
            if (!cells.ContainsKey(position))
                return false;

            return true;
        }

        public Cell GetNeighberCell(Position position, Position.Direction direction)
        {
            if (!IsValidRange(position))
                throw new ArgumentOutOfRangeException("Invalid position");

            var neighberPosition = new Position(position.X + direction.dX, position.Y + direction.dY);
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

        public void ShowDebug()
        {
            for(int y=0; y<Height; y++)
            {
                for(int x=0; x<Width; x++)
                {
                    var cell = Cell(new Position(x, y));

                    if (cell == null)
                        System.Diagnostics.Debug.Write(" ");
                    else if(cell.Stone != null)
                        System.Diagnostics.Debug.Write(cell.Stone?.GetChar());
                    else
                        System.Diagnostics.Debug.Write("_");

                    System.Diagnostics.Debug.Write(" ");
                }
                System.Diagnostics.Debug.WriteLine("");
            }
        }
    }

}
