using Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace dotVersioner
{
    [TestClass]
    public class VersionTrackingTests
    {
        [TestMethod]
        public void ShouldIgnoreWhenEmptyFile()
        {
            var releases = new Release[0];
            var mockedResolver = new Mock<ISourceResolver>();
            mockedResolver.Setup(o => o.Resolve(It.IsAny<string>())).Returns(releases);
            var mockedEditor = new Mock<IFileEditor>();
            mockedEditor.Setup(o => o.Edit(It.IsAny<string>(), It.IsAny<IEnumerable<Release>>()));
            var sut = new Versioner(mockedResolver.Object, mockedEditor.Object);
            sut.ApplyTo("anyPath");
            mockedEditor.Verify(o => o.Edit(string.Empty, null), Times.Never);
        }

        [TestMethod]
        public void ShouldIgnoreWhenFileNotFound()
        {
            var resolver = new FileResolver();
            var mockedEditor = new Mock<IFileEditor>();
            mockedEditor.Setup(o => o.Edit(It.IsAny<string>(), It.IsAny<IEnumerable<Release>>()));
            var sut = new Versioner(resolver, mockedEditor.Object);
            sut.ApplyTo("");
            mockedEditor.Verify(o => o.Edit(string.Empty, null), Times.Never);
        }

        [TestMethod]
        public void ShouldApplyWhenFileFound()
        {
            var releases = new Release[1] { new Release() };
            var mockedResolver = new Mock<ISourceResolver>();
            mockedResolver.Setup(o => o.Resolve(It.IsAny<string>())).Returns(releases);
            var mockedEditor = new Mock<IFileEditor>();
            mockedEditor.Setup(o => o.Edit(It.IsAny<string>(), It.IsAny<IEnumerable<Release>>()));
            var sut = new Versioner(mockedResolver.Object, mockedEditor.Object);
            sut.ApplyTo("anyPath");
            mockedEditor.Verify(o => o.Edit("anyPath", releases), Times.Once);
        }

        [TestMethod]
        public void ShouldReadReleaseTrackFile()
        {
            var sut = new FileResolver();
            var collection = sut.Resolve(@"..\\..\\scenario1");
            Assert.IsTrue(collection.Any());
        }

        [TestMethod]
        public void ShouldApplyVersionToScenario1()
        {
            var settings = new List<VersionedFile>
            {
                new VersionedFile {
                    FileName= "AssemblyInfo.cs",
                    Preffix = @"[assembly: AssemblyVersion(""",
                    Suffix = @""")]"
                }
            };
            var sut = new Versioner(new FileResolver(), new FilePropertyEditor(settings));
            sut.ApplyTo(@"..\\..\\scenario1");

        }
    }
}
