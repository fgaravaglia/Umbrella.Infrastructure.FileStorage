using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using Umbrella.Infrastructure.FileStorage.GoogleCloudStorage;

namespace Umbrella.Infrastructure.FileStorage.Tests.Providers.GoogleCloudStorage
{
    public class ServiceCollectionExtensionsTests
    {
        [Test]
        public void UseFileStorageFromGoogle_ThrowsEx_IfservicesIsNull()
        {
            //******* GIVEN
            IServiceCollection services = null;

            //******* WHEN
            TestDelegate testCode = () => services.UseFileStorageFromGoogle(null);

            //******* ASSERT
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(testCode);
            Assert.That(ex.ParamName, Is.EqualTo("services"));
            Assert.Pass();
        }

        [Test]
        public void UseFileStorageFromGoogle_ThrowsEx_IfBucketNameIsNull()
        {
            //******* GIVEN
            IServiceCollection services = new ServiceCollection();
            string bucketname = "";

            //******* WHEN
            TestDelegate testCode = () => services.UseFileStorageFromGoogle(bucketname);

            //******* ASSERT
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(testCode);
            Assert.That(ex.ParamName, Is.EqualTo("bucketName"));
            Assert.Pass();
        }

        [Test]
        public void UseFileStorageFromGoogle_RegistersFileStorageFactory()
        {
            //******* GIVEN
            IServiceCollection services = new ServiceCollection();
            string bucketname = "test";

            //******* WHEN
            services.UseFileStorageFromGoogle(bucketname);

            //******* ASSERT
            var provider = services.BuildServiceProvider();
            Assert.False(provider == null, "Provider has not been created!");
            var factory = provider.GetService<IFileStorageFactory>();
            Assert.False(factory == null);
            Assert.Pass();
        }

       
        [Test]
        public void UseFileStorageFromGoogle_ThrowsExceptionAtResolveTime_IfCredentialsAreNull()
        {
            //******* GIVEN
            IServiceCollection services = new ServiceCollection();
            string bucketname = "test";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", null);

            //******* WHEN
            services.UseFileStorageFromGoogle(bucketname);

            //******* ASSERT
            var provider = services.BuildServiceProvider();
            Assert.False(provider == null, "Provider has not been created!");
            TestDelegate testCode = () => provider.GetService<IFileStorage>();
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(testCode);
            Assert.True(ex.Message.StartsWith("The Application Default Credentials are not available", StringComparison.InvariantCultureIgnoreCase));
            Assert.Pass();
        }
    }
}
