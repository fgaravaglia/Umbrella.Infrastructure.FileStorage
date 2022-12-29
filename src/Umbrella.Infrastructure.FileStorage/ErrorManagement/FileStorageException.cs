using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Umbrella.Infrastructure.FileStorage.ErrorManagement
{
    /// <summary>
    /// Exception raised by Storage components
    /// </summary>
    [Serializable]
    public class FileStorageException : Exception
    {
        /// <summary>
        /// Storage resource that raised the error
        /// </summary>
        public string StorageResource { get; private set; }
        /// <summary>
        /// RUE if <see cref="StorageResource"/> is a container
        /// </summary>
        public bool StorageResourceIsAContainer { get; private set; }
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public FileStorageException() : base()
        {
            this.StorageResource = "";
            this.StorageResourceIsAContainer = false;
        }
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="message"></param>
        public FileStorageException(string message) : base(message)
        {
            this.StorageResource = "";
            this.StorageResourceIsAContainer = false;
        }
        /// <summary>
        /// Constructor defining the context
        /// </summary>
        /// <param name="message"></param>
        /// <param name="resourceName"></param>
        /// <param name="isContainer"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public FileStorageException(string message, string resourceName, bool isContainer) : this(message)
        { 
            if(string.IsNullOrEmpty(resourceName))
                throw new ArgumentNullException(nameof(resourceName));
            this.StorageResource = resourceName;
            this.StorageResourceIsAContainer = isContainer;
        }
    }
}
