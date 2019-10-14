using System.Collections.Generic;

namespace Othello.Business.Domain.Model
{
    public class GameResult
    {
        public List<StoneType> Winners { get; private set; }
        public GameResult(List<StoneType> mostStoneType)
        {
            this.Winners = mostStoneType;
        }
    }
}
