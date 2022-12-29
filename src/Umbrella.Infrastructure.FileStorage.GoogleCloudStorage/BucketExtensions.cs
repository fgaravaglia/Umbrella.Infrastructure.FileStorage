using Google.Apis.Storage.v1.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Umbrella.Infrastructure.FileStorage.GoogleCloudStorage
{
    /// <summary>
    /// Extensions to manage Bucket
    /// </summary>
    internal static class BucketExtensions
    {
        /// <summary>
        /// COnverts the GCP bucket to File Container
        /// </summary>
        /// <param name="bucket"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static FileContainer ToFileContainer(this Bucket bucket)
        { 
            if(bucket == null)
                throw new ArgumentNullException(nameof(bucket));

            string containerId = bucket.Id;
            string containerName = bucket.Name;
            string containerPath = bucket.SelfLink;
            return new FileContainer(containerId, containerName, containerPath);
        }
    }
}
