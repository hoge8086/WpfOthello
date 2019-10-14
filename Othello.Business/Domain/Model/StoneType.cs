using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Business.Domain.Model
{
    public enum StoneType
    {
        Player1 = 0,
        Player2,
        Player3,
        Player4,
        Player5,
        Player6,
        Player7,
        Player8,
        Player9,
        Player10,
        Player11,
        Player12,
        Player13,
        Player14,
        Player15,
        Player16,
        Player17,
        Player18,
        Player19,
        Player20,
        Player21,
        Player22,
        Player23,
        Player24,
    }
    public static partial class EnumExtend {
        public static string GetChar(this StoneType type)
        {
            return ((char)('a' + type)).ToString();
        }

        public static StoneType Next(this StoneType type, StoneType max)
        {
            var i = type + 1;
            if (i > max)
                return StoneType.Player1;

            return (StoneType)i;
        }
    }

    //public class StoneType
    //{
    //    public int Id { get; private set; }
    //    private string debugStr;

    //    private static readonly int MaxStoneTypeNum = 10;
    //    private static readonly int MinStoneTypeNum = 2;

    //    public static List<StoneType> StoneTypeList { get; private set; }
    //    public static StoneType First { get => StoneTypeList[0]; }
    //    public static StoneType Second { get => StoneTypeList[1]; }

    //    private StoneType(int id, string debugStr)
    //    {
    //        this.Id = id;
    //        this.debugStr = debugStr;
    //    }

    //    static StoneType()
    //    {
    //        SetMaxStoneType(MinStoneTypeNum);
    //    }

    //    public static void SetMaxStoneType(int num)
    //    {
    //        if (num < MinStoneTypeNum || MaxStoneTypeNum < num)
    //            throw new NotSupportedException("Number of stone types is more than max ore less than min");

    //        StoneTypeList = new List<StoneType>();
    //        for (int id = 0; id < num; id++)
    //            StoneTypeList.Add(new StoneType(id, ((char)('a' + id)).ToString()));
    //    }

    //    public override string ToString()
    //    {
    //        return debugStr;
    //    }
    //    public StoneType Next()
    //    {
    //        var i = StoneTypeList.IndexOf(this) + 1;
    //        if (i >= StoneTypeList.Count)
    //            return StoneTypeList[0];

    //        return StoneTypeList[i];
    //    }

    //    public override bool Equals(object obj)
    //    {
    //        var type = obj as StoneType;
    //        return type != null &&
    //               Id == type.Id;
    //    }

    //    public override int GetHashCode()
    //    {
    //        return 828553871 + Id.GetHashCode();
    //    }

    //    public static bool operator==(StoneType a, StoneType b)
    //    {
    //        // 同一のインスタンスを参照している場合は true
    //        if (System.Object.ReferenceEquals(a, b))
    //        {
    //            return true;
    //        }

    //        // どちらか片方でも null なら false
    //        if (((object)a == null) || ((object)b == null))
    //        {
    //            return false;
    //        }

    //        return a.Equals(b);
    //    }

    //    public static bool operator!=(StoneType a, StoneType b)
    //    {
    //        return !(a == b);
    //    }
    //}
}
