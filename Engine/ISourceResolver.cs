using System.Collections.Generic;

namespace Engine
{
    public interface ISourceResolver
    {
        IEnumerable<Release> Resolve(string path);
    }
}
