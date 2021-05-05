using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Business.Domain.Model.Games
{
    public class StoneTypes
    {
        public List<StoneType> Types { get; private set; }

        public StoneTypes(int num)
        {
            Types = Enumerable.Range(0, num).Select(x => new StoneType(x)).ToList();
        }

        public StoneType Next(StoneType type)
        {
            var i = type.Id + 1;
            if (i >= Types.Count)
                return Types[0];

            return Types[i];
        }
    }

    public class StoneType
    {
        public int Id;
        public string Color;

        public StoneType(int id)
        {
            this.Id = id;
            this.Color = null;
        }

        public string GetChar()
        {
            return ((char)('a' + Id)).ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            StoneType other = obj as StoneType;
            if (other == null)
                return false;
            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
