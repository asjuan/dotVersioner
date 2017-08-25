using System.Collections.Generic;

namespace Engine
{
    public interface IFileEditor
    {
        void Edit(string path, IEnumerable<Release> releases);
    }
}
