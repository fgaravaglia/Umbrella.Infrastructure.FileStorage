using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Umbrella.Infrastructure.FileStorage.ErrorManagement;
using Umbrella.Infrastructure.FileStorage.Providers.FileSystem;

namespace Umbrella.Infrastructure.FileStorage.Tests.Providers.FileSystem
{
    public class FileSystemStorageTests
    {
        #region Private methods

        static DirectoryInfo SetUpFolderForTest(string testname, List<string> subfolderLevel1 = null)
        {
            var folder = new DirectoryInfo(Environment.CurrentDirectory);
            var testFolderName = testname + "_" + Guid.NewGuid().ToString();
            folder.CreateSubdirectory(testFolderName);
            var testFolder = new DirectoryInfo(Path.Combine(folder.FullName, testFolderName));

            if (subfolderLevel1 != null && subfolderLevel1.Any())
            {
                //creating first level of children
                subfolderLevel1.ForEach(x => testFolder.CreateSubdirectory(x));
            }

            return testFolder;
        }

#endregion

        [SetUp]
        public void Setup()
        {
            var folder = new DirectoryInfo(Environment.CurrentDirectory);
            
        }

        [Test]
        public void Constructor_ThrowsEx_IfFolderIsNull()
        {
            //******* GIVEN
            string folder = "";

            //******* WHEN
            TestDelegate testCode = () => new FileSystemStorage(folder);

            //******* ASSERT
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(testCode);
            Assert.That(ex.ParamName, Is.EqualTo("fullPath"));
            Assert.Pass();
        }

        [Test]
        public void GetRootContainer_ThrowsEx_IfFolderDoesNotExist()
        {
            //******* GIVEN
            string folder = @"c:\pippo";
            var storage = new FileSystemStorage(folder);

            //******* WHEN
            TestDelegate testCode = () => storage.GetRootContainer();

            //******* ASSERT
            FileStorageException ex = Assert.Throws<FileStorageException>(testCode);
            Assert.That(ex.Message, Is.EqualTo("Folder does not exist"));
            Assert.Pass();
        }

        [Test]
        public void GetRootContainer_ReturnsRootFolder()
        {
            //******* GIVEN
            var folder = new DirectoryInfo(Environment.CurrentDirectory);
            var storage = new FileSystemStorage(folder.FullName);

            //******* WHEN
            var container = storage.GetRootContainer();

            //******* ASSERT
            Assert.False(container == null);
            Assert.That(container.Id, Is.EqualTo(folder.FullName));
            Assert.That(container.Name, Is.EqualTo(folder.Name));
            Assert.That(container.Path, Is.EqualTo(folder.FullName));
            Assert.Pass();
        }

        [Test]
        public void GetFiles_ThrowsEx_IfContainerIdIsNull()
        {
            //******* GIVEN
            var folder = SetUpFolderForTest(nameof(GetFiles_ThrowsEx_IfContainerIdIsNull));
            var storage = new FileSystemStorage(folder.FullName);


            //******* WHEN
            TestDelegate testCode = () => storage.GetFiles("");

            //******* ASSERT
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(testCode);
            Assert.Pass();
        }

        [Test]
        public void GetFiles_ThrowsEx_IfFolderDoesNotExist()
        {
            //******* GIVEN
            var folder = SetUpFolderForTest(nameof(GetFiles_ThrowsEx_IfContainerIdIsNull));
            var storage = new FileSystemStorage(folder.FullName);

            //******* WHEN
            TestDelegate testCode = () => storage.GetFiles("pippo");

            //******* ASSERT
            FileStorageException ex = Assert.Throws<FileStorageException>(testCode);
            Assert.That(ex.Message, Is.EqualTo("Target Folder does not exist"));
            Assert.Pass();
        }

        [Test]
        public void GetFiles_Returns_ExpectedItems()
        {
            //******* GIVEN
            var folder = SetUpFolderForTest(nameof(GetFiles_Returns_ExpectedItems));
            File.WriteAllText(Path.Combine(folder.FullName, "test1.txt"), "test 1");
            File.WriteAllText(Path.Combine(folder.FullName, "test2.txt"), "test 2");
            File.WriteAllText(Path.Combine(folder.FullName, "test3.txt"), "test 3");
            var storage = new FileSystemStorage(folder.FullName);
            storage.ScanStorageTree();
            var container = storage.GetRootContainer();

            //******* WHEN
            var items = storage.GetFiles(container.Id);

            //******* ASSERT
            Assert.True(items.Any());
            Assert.That(3, Is.EqualTo(items.Count()));
            Assert.Pass();
        }

        [Test]
        public void GetFiles_Returns_ExpectedItemsFromLastLeaf()
        {
            //******* GIVEN
            var folder = SetUpFolderForTest(nameof(GetFiles_Returns_ExpectedItems), new List<string>() { "Test1", "Test2"});
            File.WriteAllText(Path.Combine(folder.FullName, @"Test2\test1.txt"), "test 1");
            File.WriteAllText(Path.Combine(folder.FullName, @"Test2\test2.txt"), "test 2");
            File.WriteAllText(Path.Combine(folder.FullName, @"Test2\test3.txt"), "test 3");
            var storage = new FileSystemStorage(folder.FullName);
            storage.ScanStorageTree();

            //******* WHEN
            var items = storage.GetFiles(Path.Combine(folder.FullName, @"Test2"));

            //******* ASSERT
            Assert.True(items.Any());
            Assert.That(3, Is.EqualTo(items.Count()));
            Assert.Pass();
        }
    }
}