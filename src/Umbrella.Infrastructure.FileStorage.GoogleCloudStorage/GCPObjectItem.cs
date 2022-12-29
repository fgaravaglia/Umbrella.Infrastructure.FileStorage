using System;
using Google.Apis.Storage.v1.Data;

namespace Umbrella.Infrastructure.FileStorage.GoogleCloudStorage
{
    /// <summary>
    /// Speciazation of <see cref="FileItem"/> for GCP context
    /// </summary>
    public class GcpObjectItem : FileItem
    {
        /// <summary>
        /// Default Constr
        /// </summary>
        /// <param name="file"></param>
        /// <param name="containerId"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public GcpObjectItem(Google.Apis.Storage.v1.Data.Object file, string containerId) : base()
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            if (string.IsNullOrEmpty(file.Name))
                throw new ArgumentNullException(nameof(file), "Filename cannot be null");

            this.ContainerId = containerId ?? "";
            this.Name = file.Name;
            this.Extension = "";
            this.CreationTime = file.TimeCreated.HasValue ? file.TimeCreated.Value : DateTime.MinValue;
            this.LastWriteTime = file.Updated.HasValue ? file.Updated.Value : this.CreationTime;
        }
    }
}
