using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Umbrella.Infrastructure.FileStorage.ErrorManagement;

namespace Umbrella.Infrastructure.FileStorage.Providers.FileSystem
{
    /// <summary>
    /// Extensions to work with IO
    /// </summary>
    static class IOExtensions
    {
        /// <summary>
        /// Maps the <see cref="DirectoryInfo"/> item into <see cref="FileContainer"/> dto
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FileStorageException"></exception>
        public static FileContainer ToFileContainer(this DirectoryInfo directory)
        {
            if (directory == null)
                throw new ArgumentNullException(nameof(directory));
            if (!directory.Exists)
                throw new FileStorageException($"Folder does not exist");
            return new FileContainer(directory.FullName, directory.Name, directory.FullName);
        }
    }
}
