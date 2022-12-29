using System;
using System.Collections.Generic;
using System.Text;
using Umbrella.Infrastructure.FileStorage.Providers.FileSystem;

namespace Umbrella.Infrastructure.FileStorage
{
    /// <summary>
    /// Factory to instance the storage, in order to extend the lsit of providers in external assemblies
    /// </summary>
    public class FileStorageFactory : IFileStorageFactory
    {
        public string StorageProviderName { get; private set; }
        readonly Dictionary<string, Func<IFileStorage>> _StorageFactoryCallback;

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public FileStorageFactory()
        {
            this.StorageProviderName = "";
            this._StorageFactoryCallback = new Dictionary<string, Func<IFileStorage>>();
        }

        /// <summary>
        /// Sets the FileStystem to implement file storage
        /// </summary>
        public void UseFileSystem(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            this.UseProvider("FileSystem", () =>
            {
                var storage = new FileSystemStorage(path);
                storage.ScanStorageTree();
                return storage;
            });
        }
        /// <summary>
        /// Inject storage from outside
        /// </summary>
        /// <param name="name"></param>
        /// <param name="instanceFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void UseProvider(string name, Func<IFileStorage> instanceFactory)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            if (instanceFactory == null)
                throw new ArgumentNullException(nameof(instanceFactory));

            this.StorageProviderName = name;
            if (this._StorageFactoryCallback.ContainsKey(this.StorageProviderName))
                this._StorageFactoryCallback[this.StorageProviderName] = instanceFactory;
            else
                this._StorageFactoryCallback.Add(name, instanceFactory);
        }
        /// <summary>
        /// <inheritdoc cref="IFileStorageFactory"/>
        /// </summary>
        public IFileStorage CreateFileStorage()
        {
            if (this._StorageFactoryCallback.ContainsKey(this.StorageProviderName))
                return this._StorageFactoryCallback[this.StorageProviderName].Invoke();

            throw new NotImplementedException($"Unkown provider " + this.StorageProviderName);
        }
    }
}
