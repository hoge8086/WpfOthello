using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Othello.Business.Domain.Model.Games;

namespace Othello.Business.Domain.Model.Boards
{
    public class HexagonalDirection : Board.IDirection
    {
        public static readonly HexagonalDirection Right     = new HexagonalDirection();
        public static readonly HexagonalDirection Left      = new HexagonalDirection();
        public static readonly HexagonalDirection RightUp   = new HexagonalDirection();
        public static readonly HexagonalDirection LeftUp    = new HexagonalDirection();
        public static readonly HexagonalDirection RightDown = new HexagonalDirection();
        public static readonly HexagonalDirection LeftDown  = new HexagonalDirection();
        private HexagonalDirection() {}

        public static readonly List<HexagonalDirection> AllDirections = new List<HexagonalDirection>()
                            {
                                Right,
                                Left,
                                RightUp,
                                LeftUp,
                                RightDown,
                                LeftDown,
                            };
    }



    // [六角ボードの座標の体系]
    // ※2次元座標をベースに、奇数行だけ半分ずらしたもの(0行始まり)
    // -------------------------------
    // [0,0] [0,1] [0,2] [0,3] [0,4]
    //    [1,0] [1,1] [1,2] [1,3] [1,4]
    // [2,0] [2,1] [2,2] [2,3] [2,4]
    //    [3,0] [3,1] [3,2] [3,3] [3,4]
    // [4,0] [4,1] [4,2] [4,3] [4,4]
    //    [5,0] [5,1] [5,2] [5,3] [5,4]
    // -------------------------------
    public class HexagonalPosition : Board.IPosition
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public HexagonalPosition(int x, int y)
        {
            //負の値を許す（範囲外判定はBoardクラスの責務のため)
            //if (x < 0 || y < 0)
            //    throw new ArgumentOutOfRangeException("invalid position value.");
            this.X = x;
            this.Y = y;
        }
        public override bool Equals(object obj)
        {
            var position = obj as HexagonalPosition;
            return position != null &&
                   X == position.X &&
                   Y == position.Y;
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(HexagonalPosition position1, HexagonalPosition position2)
        {
            return EqualityComparer<HexagonalPosition>.Default.Equals(position1, position2);
        }

        public static bool operator !=(HexagonalPosition position1, HexagonalPosition position2)
        {
            return !(position1 == position2);
        }

        public Board.IPosition GetNeighber(Board.IDirection direction)
        {
            var dir = direction as HexagonalDirection;
            if(dir == null)
                throw new ArgumentException("Direction type and position type do not match.");

            if (dir == HexagonalDirection.Right)
            {
                return new HexagonalPosition(X + 1, Y);
            }
            else if(dir == HexagonalDirection.Left)
            {
                return new HexagonalPosition(X - 1, Y);
            }
            else
            {
                // [奇数行/偶数行によって、上下に隣接するX軸の値が異なる]
                var x = ((Y % 2) == 0) ?  X - 1 : X;

                if (dir == HexagonalDirection.RightUp)
                {
                    return new HexagonalPosition(x + 1, Y - 1);
                }
                else if(dir == HexagonalDirection.RightDown)
                {
                    return new HexagonalPosition(x + 1, Y + 1);
                }
                else if(dir == HexagonalDirection.LeftUp)
                {
                    return new HexagonalPosition(x, Y - 1);
                }
                //else if(dir == HexagonalDirection.LeftDown)
                else
                {
                    return new HexagonalPosition(x, Y + 1);
                }
            }

        }
    }

    public class HexagonalBoard : Board
    {
        public int Width { get; private set;}
        public int Height{ get; private set; }

        public HexagonalBoard(List<Cell> cellsList) : base(cellsList)
        {
            Width = cellsList.Max(c => ((HexagonalPosition)c.Position).X) + 1;
            Height = cellsList.Max(c => ((HexagonalPosition)c.Position).Y) + 1;
        }

        public override List<IDirection> Directions
        {
            get {
                return HexagonalDirection.AllDirections.Select(x => (IDirection)x).ToList();
            }
        }
        public override void ShowDebug()
        {

            for(int y=0; y<Height; y++)
            {
                // [奇数行だけずらす]
                if((y % 2) != 0)
                    System.Diagnostics.Debug.Write(" ");

                for(int x=0; x<Width; x++)
                {
                    var cell = Cell(new HexagonalPosition(x, y));

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
