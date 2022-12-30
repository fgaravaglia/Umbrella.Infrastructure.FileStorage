using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Umbrella.Infrastructure.FileStorage.ErrorManagement;

namespace Umbrella.Infrastructure.FileStorage.Providers.FileSystem
{
    /// <summary>
    /// Abstraction of File storage implemente by File System usage
    /// </summary>
    public interface IFileSystemStorage : IFileStorage
    { 
        /// <summary>
        /// Root folder of the container
        /// </summary>
        string RootFolder { get; }
    }

    /// <summary>
    /// Implementation of storage based on FileSystem
    /// </summary>
    internal class FileSystemStorage : IFileSystemStorage
    {
        #region Fields

        readonly Dictionary<string, FileContainer> _Folders;

        #endregion

        /// <summary>
        /// flag to veerify that storage has been indexed
        /// </summary>
        public bool IsScanPerformed { get; private set; }
        /// <summary>
        /// Root folder of the container
        /// </summary>
        public string RootFolder { get; private set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="fullPath"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public FileSystemStorage(string fullPath)
        {
            if (string.IsNullOrEmpty(fullPath))
                throw new ArgumentNullException(nameof(fullPath));
            this.RootFolder = fullPath;
            this._Folders = new Dictionary<string, FileContainer>();
        }

        /// <summary>
        /// Scan the folder to build an index on available folders
        /// </summary>
        /// <exception cref="FileStorageException"></exception>
        public void ScanStorageTree()
        {
            var directory = new DirectoryInfo(this.RootFolder);
            if (!directory.Exists)
                throw new FileStorageException($"Root Folder does not exist");
            //get folders of root
            AddDirectoryAndChildrenToIndex(directory);
            this.IsScanPerformed = true;
        }
        /// <summary>
        /// <inheritdoc cref="IFileStorage.GetFiles(string)"/>
        /// </summary>
        /// <param name="containerId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FileStorageException"></exception>
        public IEnumerable<FileItem> GetFiles(string containerId)
        {
            if (string.IsNullOrEmpty(containerId))
                throw new ArgumentNullException(nameof(containerId));

            if(!this.IsScanPerformed)
                this.ScanStorageTree();

            // translate containerId into path
            var container = GetContainer(containerId);
            if(container == null)
                throw new FileStorageException($"Target Folder does not exist", containerId, true);

            // the concrete folder path
            string fullPath = Path.GetFullPath(container.Path);

            // get files
            var directory = new DirectoryInfo(fullPath);
            var files = directory.GetFiles().Select(x => new FileSystemItem(x, containerId));
            return files;
        }
        /// <summary>
        /// <inheritdoc cref="IFileStorage.GetRootContainer"/>
        /// </summary>
        /// <returns></returns>
        public FileContainer GetRootContainer()
        {
            var directory = new DirectoryInfo(this.RootFolder);
            return directory.ToFileContainer();
        }
        /// <summary>
        /// gets the path of area if has sense; null otherwise
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public string GetAreaDataPath(string area)
        {
            if (String.IsNullOrEmpty(area))
                throw new ArgumentNullException(nameof(area));

            var finalPath = Path.Combine(this.RootFolder, area + "\\Data");
            return FormatPath(finalPath);
        }
        /// <summary>
        /// FOrmats correctly the path, managing local or Containerized environment
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string FormatPath(string path)
        {
            // assuming only Localhsot is not containerized environment
            return IsLocalhost() ? path : path.Replace("\\", "/");
        }
        /// <summary>
        /// COmbines the path to build the real one
        /// </summary>
        /// <param name="path"></param>
        /// <param name="subpath"></param>
        /// <returns></returns>
        public string CombinePath(string path, string subpath)
        {
            if (String.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            if (String.IsNullOrEmpty(subpath))
                throw new ArgumentNullException(nameof(subpath));

            return FormatPath(Path.GetFullPath(Path.Combine(path, subpath)));
        }

        #region Private Methods

        bool IsLocalhost()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return environment == "Localhost";
        }
        
        void AddDirectoryAndChildrenToIndex(DirectoryInfo directory)
        {
            // build container for this directory
            var container = directory.ToFileContainer();
            var children = directory.GetDirectories();
            container.SetChildren(children.Select(x => x.ToFileContainer()));
            this._Folders.Add(container.Id, container);
            foreach (var child in children)
            {
                AddDirectoryAndChildrenToIndex(child);
            }

        }

        FileContainer? GetContainer(string containerId)
        {
            if (string.IsNullOrEmpty(containerId))
                throw new ArgumentNullException(nameof(containerId));
            if (this._Folders.ContainsKey(containerId))
                return this._Folders[containerId];
            return null;
        }

        #endregion
    }
}
