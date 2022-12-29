using Google.Apis.Storage.v1.Data;
using NUnit.Framework;
using System;
using Umbrella.Infrastructure.FileStorage.GoogleCloudStorage;

namespace Umbrella.Infrastructure.FileStorage.Tests.Providers.GoogleCloudStorage
{
    public class BucketExtensionsTests
    {
        [Test]
        public void ToFileContainer_ThrowsEx_IfbucketIsNull()
        {
            //******* GIVEN
            Bucket bucket = null;

            //******* WHEN
            TestDelegate testCode = () => bucket.ToFileContainer();

            //******* ASSERT
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(testCode);
            Assert.That(ex.ParamName, Is.EqualTo("bucket"));
            Assert.Pass();
        }

        [Test]
        public void ToFileContainer_MapsBucketAsExpected()
        {
            //******* GIVEN
            Bucket bucket = new Bucket() 
            { 
                Id = "myId",
                Name ="test",
                SelfLink ="https://my-test-project/test"
            };

            //******* WHEN
            var container = bucket.ToFileContainer();

            //******* ASSERT
            Assert.False(container == null);
            Assert.That(container.Id, Is.EqualTo(bucket.Id));
            Assert.That(container.Name, Is.EqualTo(bucket.Name));
            Assert.That(container.Path, Is.EqualTo(bucket.SelfLink));
            Assert.Pass();
        }
    }
}
