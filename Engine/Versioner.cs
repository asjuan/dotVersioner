using System.Linq;

namespace Engine
{
    public class Versioner
    {
        private IFileEditor editor;
        private ISourceResolver resolver;

        public Versioner(ISourceResolver resolver, IFileEditor editor)
        {
            this.resolver = resolver;
            this.editor = editor;
        }

        public void ApplyTo(string path)
        {
            var releases = resolver.Resolve(path);
            if (releases == null || !releases.Any()) return;
            editor.Edit(path, releases);
        }
    }
}
