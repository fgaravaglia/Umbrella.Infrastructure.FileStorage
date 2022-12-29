using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Umbrella.Infrastructure.FileStorage.ErrorManagement;

[assembly: InternalsVisibleTo("Umbrella.Infrastructure.FileStorage.Tests")]

namespace Umbrella.Infrastructure.FileStorage.GoogleCloudStorage
{
    /// <summary>
    /// Implementation of <see cref="IFileStorage"/> based on Google Cloud Storage
    /// </summary>
    public class GcpCloudStorage : IFileStorage
    {
        #region Fields

        readonly Dictionary<string, FileContainer> _Folders;
        readonly StorageClient _Client;

        #endregion

        /// <summary>
        /// flag to veerify that storage has been indexed
        /// </summary>
        public bool IsScanPerformed { get; private set; }
        /// <summary>
        /// name of bucket
        /// </summary>
        public string BucketName { get; private set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="bucketName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public GcpCloudStorage(string bucketName)
        {
            if (String.IsNullOrEmpty(bucketName))
                throw new ArgumentNullException(nameof(bucketName));

            this.BucketName = bucketName;
            this._Client = StorageClient.Create();
            if (this._Client == null)
                throw new ArgumentException($"Creation Bucket Client Failed", nameof(bucketName));
            this._Folders = new Dictionary<string, FileContainer>();
        }
        /// <summary>
        /// <inheritdoc cref="IFileStorage.GetFiles(string)"/>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FileItem> GetFiles(string containerId)
        {
            // get GCP object
            Bucket bucket = this._Client.GetBucket(this.BucketName);
            var objects = this._Client.ListObjects(this.BucketName, options: new ListObjectsOptions()
            { 
                PageSize = 10000
            });

            var files = new List<FileItem>();
            foreach (var obj in objects)
            {
                GcpObjectItem item = new GcpObjectItem(obj, bucket.Id);
                files.Add(item);
            }
            return files;
        }
        /// <summary>
        /// <inheritdoc cref="IFileStorage.GetRootContainer"/>
        /// </summary>
        /// <returns></returns>
        public FileContainer GetRootContainer()
        {
            // get GCP object
            Bucket bucket = this._Client.GetBucket(this.BucketName);
            return bucket.ToFileContainer();
        }
        /// <summary>
        /// Scan the folder to build an index on available folders
        /// </summary>
        /// <exception cref="FileStorageException"></exception>
        public void ScanStorageTree()
        {
            Bucket bucket = this._Client.GetBucket(this.BucketName);
            if (bucket == null)
                throw new FileStorageException("Unable to get bucket details", this.BucketName, true);
            // get folders of root
            AddDirectoryAndChildrenToIndex(bucket.ToFileContainer());
            this.IsScanPerformed = true;
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

            var finalPath = $"{this.BucketName}/{area}/Data";
            return FormatPath(finalPath);
        }
        /// <summary>
        /// FOrmats correctly the path, managing local or Containerized environment
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string FormatPath(string path)
        {
            if (String.IsNullOrEmpty(path))
                return path;

            // assuming only Localhsot is not containerized environment
            return path.Replace("\\", "/");
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

            return FormatPath("{path}/{subpath}");
        }
        #region Private Methods

        void AddDirectoryAndChildrenToIndex(FileContainer bucket)
        {
            // build container for this directory
            var objects = this._Client.ListObjects(bucket.Name);
            var children = new List<FileContainer>();
            foreach (var obj in objects)
            {
                if (obj.Kind != "object")
                {
                    var child = new FileContainer();
                    children.Add(child);
                }

            }
            bucket.SetChildren(children);
            this._Folders.Add(bucket.Id, bucket);
            foreach (var child in children)
            {
                AddDirectoryAndChildrenToIndex(child);
            }

        }

        #endregion
    }
}
