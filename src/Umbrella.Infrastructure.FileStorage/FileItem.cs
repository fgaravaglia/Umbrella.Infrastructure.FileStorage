using System;
using System.Collections.Generic;
using System.Text;

namespace Umbrella.Infrastructure.FileStorage
{

    /// <summary>
    /// Plain Object to model a file
    /// </summary>
    public abstract class FileItem
    {
        /// <summary>
        /// Id of container that is still storing the item
        /// </summary>
        public string ContainerId { get; protected set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; protected set; }
        /// <summary>
        /// Extension
        /// </summary>
        public string Extension { get; protected set; }
        /// <summary>
        /// File content
        /// </summary>
        public byte[]? Content { get; protected set; }
        /// <summary>
        /// Mime type
        /// </summary>
        public string MimeType { get; protected set; }

        public DateTime CreationTime { get; protected set; }

        public DateTime LastWriteTime { get; protected set; }

        /// <summary>
        /// EMpty COnstructor
        /// </summary>
        protected FileItem()
        {
            this.Name = "";
            this.Extension = "";
            this.MimeType = "";
            this.ContainerId = "";
        }
    }
}
