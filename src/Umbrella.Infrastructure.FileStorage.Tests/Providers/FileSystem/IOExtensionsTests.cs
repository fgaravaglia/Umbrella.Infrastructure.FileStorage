using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbrella.Infrastructure.FileStorage.Providers.FileSystem;

namespace Umbrella.Infrastructure.FileStorage.Tests.Providers.FileSystem
{
    public class IOExtensionsTests
    {

        [Test]
        public void FileSystem_MapsFullPathIntoContainerId()
        {
            //******* GIVEN
            var folder = new DirectoryInfo(Environment.CurrentDirectory);

            //******* WHEN
            var container = folder.ToFileContainer();

            //******* ASSERT
            Assert.False(container == null);
            Assert.That(container.Id, Is.EqualTo(folder.FullName));
            Assert.That(container.Name, Is.EqualTo(folder.Name));
            Assert.That(container.Path, Is.EqualTo(folder.FullName));
            Assert.Pass();
        }
    }
}
