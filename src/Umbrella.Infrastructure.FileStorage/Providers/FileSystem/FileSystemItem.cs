using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Umbrella.Infrastructure.FileStorage.Providers.FileSystem
{
    public class FileSystemItem : FileItem
    {
        public FileSystemItem(FileInfo file, string containerId) : base()
        {
            if(file == null)    
                throw new ArgumentNullException("file");    

            if (string.IsNullOrEmpty(file.Name))
                throw new ArgumentNullException(nameof(file), "Filename cannot be null");

            if (string.IsNullOrEmpty(file.Extension))
                throw new ArgumentNullException(nameof(file), "Extension cannot be null");

            this.ContainerId = containerId ?? "";
            this.Name = file.Name;
            this.Extension = file.Extension;
            this.CreationTime = file.CreationTime;
            this.LastWriteTime = file.LastWriteTime;
        }
    }
}
