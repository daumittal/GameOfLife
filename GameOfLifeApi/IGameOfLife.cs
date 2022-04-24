using System.Collections.Generic;

namespace badlife
{
    public interface IGameOfLife
    {
        IEnumerable<Output> Evolve();
    }
}
