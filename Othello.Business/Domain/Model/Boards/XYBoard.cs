using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Othello.Business.Domain.Model.Games;

namespace Othello.Business.Domain.Model.Boards
{
    public class XYDirection : Board.IDirection
    {
        public int dX, dY;
        private XYDirection(int dx, int dy) { this.dX = dx; this.dY = dy; }

        public static readonly List<XYDirection> AllDirections = new List<XYDirection>()
                            {
                                new XYDirection(-1, 1),
                                new XYDirection(-1, 0),
                                new XYDirection(-1, -1),
                                new XYDirection(0, 1),
                                new XYDirection(0, -1),
                                new XYDirection(1, 1),
                                new XYDirection(1, 0),
                                new XYDirection(1, -1)
                            };
    }

    public class XYPosition : Board.IPosition
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public XYPosition(int x, int y)
        {
            //負の値を許す（範囲外判定はBoardクラスの責務のため)
            //if (x < 0 || y < 0)
            //    throw new ArgumentOutOfRangeException("invalid position value.");
            this.X = x;
            this.Y = y;
        }
        public override bool Equals(object obj)
        {
            var position = obj as XYPosition;
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

        public static bool operator ==(XYPosition position1, XYPosition position2)
        {
            return EqualityComparer<XYPosition>.Default.Equals(position1, position2);
        }

        public static bool operator !=(XYPosition position1, XYPosition position2)
        {
            return !(position1 == position2);
        }

        public Board.IPosition GetNeighber(Board.IDirection direction)
        {
            var dir = direction as XYDirection;
            if(dir == null)
                throw new ArgumentException("Direction type and position type do not match.");

            return new XYPosition(X + dir.dX, Y + dir.dY);
        }
    }

    public class XYBoard : Board
    {
        public int Width { get; private set;}
        public int Height{ get; private set; }

        public XYBoard(List<Cell> cellsList) : base(cellsList)
        {
            Width = cellsList.Max(c => ((XYPosition)c.Position).X) + 1;
            Height = cellsList.Max(c => ((XYPosition)c.Position).Y) + 1;
        }

        public override List<IDirection> Directions
        {
            get {
                return XYDirection.AllDirections.Select(x => (IDirection)x).ToList();
            }
        }
        public override void ShowDebug()
        {
            for(int y=0; y<Height; y++)
            {
                for(int x=0; x<Width; x++)
                {
                    var cell = Cell(new XYPosition(x, y));

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
