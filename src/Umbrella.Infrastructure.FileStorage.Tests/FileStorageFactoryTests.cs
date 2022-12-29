using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbrella.Infrastructure.FileStorage.Tests
{
    public class FileStorageFactoryTests
    {
        [Test]
        public void UseFileSystem_ThrowsException_IfFolderIsNull()
        {
            //GIVEN
            var factory = new FileStorageFactory();
            string folder = "";

            //******* WHEN
            TestDelegate testCode = () => factory.UseFileSystem(folder);

            //ASSERT
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(testCode);
            Assert.That(ex.ParamName, Is.EqualTo("path"));
            Assert.Pass();
        }

        [Test]
        public void UseFileSystem_SetsProviderName()
        {
            //GIVEN
            var factory = new FileStorageFactory();

            //WHEN
            factory.UseFileSystem(Environment.CurrentDirectory);

            //ASSERT
            Assert.That(factory.StorageProviderName, Is.EqualTo("FileSystem"));
            Assert.Pass();
        }
    }
}
