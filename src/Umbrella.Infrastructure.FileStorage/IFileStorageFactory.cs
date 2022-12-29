using System;
using System.Collections.Generic;
using System.Text;

namespace Umbrella.Infrastructure.FileStorage
{
    /// <summary>
    /// Abstraction of factory to inject proper concrete implementations of storages
    /// </summary>
    public interface IFileStorageFactory
    {
        /// <summary>
        /// Name of provider to get storage
        /// </summary>
        string StorageProviderName { get; }
        /// <summary>
        /// generates a new instance of storage
        /// </summary>
        /// <returns></returns>
        IFileStorage CreateFileStorage();
    }
}
