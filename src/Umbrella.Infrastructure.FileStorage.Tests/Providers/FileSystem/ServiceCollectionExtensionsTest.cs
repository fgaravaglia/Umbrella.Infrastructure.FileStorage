using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbrella.Infrastructure.FileStorage.ErrorManagement;
using Umbrella.Infrastructure.FileStorage.GoogleCloudStorage;
using Umbrella.Infrastructure.FileStorage.Providers.FileSystem;

namespace Umbrella.Infrastructure.FileStorage.Tests.Providers.FileSystem
{
    public class ServiceCollectionExtensionsTests
    {
        [Test]
        public void UseFileStorageFromFileSystem_ThrowsEx_IfservicesIsNull()
        {
            //******* GIVEN
            IServiceCollection services = null;

            //******* WHEN
            TestDelegate testCode = () => services.UseFileStorageFromFileSystem(null);

            //******* ASSERT
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(testCode);
            Assert.That(ex.ParamName, Is.EqualTo("services"));
            Assert.Pass();
        }

        [Test]
        public void UseFileStorageFromFileSystem_ThrowsEx_IfFolderIsNull()
        {
            //******* GIVEN
            string folder = "";
            IServiceCollection services = new ServiceCollection();

            //******* WHEN
            TestDelegate testCode = () => services.UseFileStorageFromFileSystem(folder);

            //******* ASSERT
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(testCode);
            Assert.That(ex.ParamName, Is.EqualTo("rootFolder"));
            Assert.Pass();
        }

        [Test]
        public void UseFileStorageFromFileSystem_RegistersFileStorageFactory()
        {
            //******* GIVEN
            string folder = Environment.CurrentDirectory;
            IServiceCollection services = new ServiceCollection();

            //******* WHEN
            services.UseFileStorageFromFileSystem(folder);

            //******* ASSERT
            var provider = services.BuildServiceProvider();
            Assert.False(provider == null, "Provider has not been created!");
            var factory = provider.GetService<IFileStorageFactory>();
            Assert.False(factory == null);
            Assert.Pass();
        }

        [Test]
        public void UseFileStorageFromFileSystem_RegistersFileStorage()
        {
            //******* GIVEN
            string folder = Environment.CurrentDirectory;
            IServiceCollection services = new ServiceCollection();

            //******* WHEN
            services.UseFileStorageFromFileSystem(folder);

            //******* ASSERT
            var provider = services.BuildServiceProvider();
            Assert.False(provider == null, "Provider has not been created!");
            var storage = provider.GetService<IFileStorage>();
            Assert.False(storage == null);
            Assert.IsInstanceOf<FileSystemStorage>(storage);
            Assert.Pass();
        }



    }
}
