using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using Umbrella.Infrastructure.FileStorage.DependencyInjection;

namespace Umbrella.Infrastructure.FileStorage.Tests.DependencyInjection
{
    public class ServiceCollectionExtensionsTests
    {
        [Test]
        public void UseFileStorageFromFileSystem_ThrowsEx_IfservicesIsNull()
        {
            //******* GIVEN
            IServiceCollection services = null;

            //******* WHEN
            TestDelegate testCode = () => services.UseFileStorage(null);

            //******* ASSERT
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(testCode);
            Assert.That(ex.ParamName, Is.EqualTo("services"));
            Assert.Pass();
        }

        [Test]
        public void UseFileStorageFromFileSystem_ThrowsEx_IfFactoryBuilderIsNull()
        {
            //******* GIVEN
            IServiceCollection services = new ServiceCollection();
            Func<IServiceProvider, FileStorageFactory, IFileStorageFactory> factoryBuilder = null;

            //******* WHEN
            TestDelegate testCode = () => services.UseFileStorage(factoryBuilder);

            //******* ASSERT
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(testCode);
            Assert.That(ex.ParamName, Is.EqualTo("factoryBuilder"));
            Assert.Pass();
        }
    }
}
