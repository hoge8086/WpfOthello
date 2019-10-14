using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Business.Domain.Model
{
    public class Position
    {
        public class Direction
        {
            public int dX, dY;
            private Direction(int dx, int dy) { this.dX = dx; this.dY = dy; }

            public static readonly List<Direction> AllDirections = new List<Direction>()
                                {
                                    new Direction(-1, 1),
                                    new Direction(-1, 0),
                                    new Direction(-1, -1),
                                    new Direction(0, 1),
                                    new Direction(0, -1),
                                    new Direction(1, 1),
                                    new Direction(1, 0),
                                    new Direction(1, -1)
                                };
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        public Position(int x, int y)
        {
            //負の値を許す（範囲外判定はBoardクラスの責務のため)
            //if (x < 0 || y < 0)
            //    throw new ArgumentOutOfRangeException("invalid position value.");
            this.X = x;
            this.Y = y;
        }

        public override bool Equals(object obj)
        {
            var position = obj as Position;
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

        public static bool operator ==(Position position1, Position position2)
        {
            return EqualityComparer<Position>.Default.Equals(position1, position2);
        }

        public static bool operator !=(Position position1, Position position2)
        {
            return !(position1 == position2);
        }
    }
}
