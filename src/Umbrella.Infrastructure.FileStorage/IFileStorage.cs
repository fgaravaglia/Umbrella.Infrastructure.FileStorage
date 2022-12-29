using System;
using System.Collections.Generic;

namespace Umbrella.Infrastructure.FileStorage
{
    /// <summary>
    /// Abstraction of specific file storage component, to hide the concrete driver used for persistence
    /// example: Google Cloud Storage, Azure Blob Storage, File system, Dorpbox, Etc.
    /// </summary>
    public interface IFileStorage
    {
        /// <summary>
        /// flag to verify that storage has been indexed
        /// </summary>
        bool IsScanPerformed { get; }
        /// <summary>
        /// Scan the folder to build an index on available folders
        /// </summary>
        /// <exception cref="FileStorageException"></exception>
        void ScanStorageTree();
        /// <summary>
        /// Gets the root container
        /// </summary>
        /// <returns></returns>
        FileContainer GetRootContainer();
        /// <summary>
        /// Gets the list of files from given container
        /// </summary>
        /// <param name="containerId">id container</param>
        /// <returns></returns>
        IEnumerable<FileItem> GetFiles(string containerId);
        /// <summary>
        /// gets the path of area if has sense; null otherwise
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        string GetAreaDataPath(string area);
        /// <summary>
        /// FOrmats correctly the path, managing local or Containerized environment
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string FormatPath(string path);
        /// <summary>
        /// COmbines the path to build the real one
        /// </summary>
        /// <param name="path"></param>
        /// <param name="subpath"></param>
        /// <returns></returns>
        string CombinePath(string path, string subpath);
    }
}
