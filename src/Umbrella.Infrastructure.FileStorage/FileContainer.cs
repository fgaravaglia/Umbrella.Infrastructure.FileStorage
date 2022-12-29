using System;
using System.Collections.Generic;
using System.Text;

namespace Umbrella.Infrastructure.FileStorage
{
    /// <summary>
    /// simple DTO to model a file container (a bucket, a folder, etc)
    /// </summary>
    public class FileContainer
    {
        /// <summary>
        /// Name of Container
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Path of Container
        /// </summary>
        public string Path { get; private set; }
        /// <summary>
        /// Id of Container; in case of folder on Filesystem, it is the same value of <see cref="FileContainer.Name"/>
        /// </summary>
        public string Id { get; private set; }
        /// <summary>
        /// Children for subdirectories / subcontainers
        /// </summary>
        public List<FileContainer> Children { get; private set; }

        /// <summary>
        /// empty Constructor for serialization
        /// </summary>
        public FileContainer()
        {
            this.Name = "";
            this.Id = "";
            this.Path = "";
            this.Children = new List<FileContainer>();
        }
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <exception cref="NotImplementedException"></exception>
        public FileContainer(string id, string name, string path)
        {
            if(string.IsNullOrEmpty(id))
                    throw new NotImplementedException(nameof(id));
            if (string.IsNullOrEmpty(name))
                throw new NotImplementedException(nameof(name));
            if (string.IsNullOrEmpty(path))
                throw new NotImplementedException(nameof(path));

            this.Id = id;
            this.Name = name;
            this.Path = path;
            this.Children = new List<FileContainer>();
        }
        /// <summary>
        /// set children
        /// </summary>
        /// <param name="list"></param>
        public void SetChildren(IEnumerable<FileContainer> list)
        {
            this.Children.Clear();
            this.Children.AddRange(list);
        }
    }
}
