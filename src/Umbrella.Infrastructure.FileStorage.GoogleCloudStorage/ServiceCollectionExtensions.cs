using Microsoft.Extensions.DependencyInjection;
using System;
using Umbrella.Infrastructure.FileStorage.DependencyInjection;

namespace Umbrella.Infrastructure.FileStorage.GoogleCloudStorage
{
    /// <summary>
    /// Extensions to inject components into DImanagement
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Injects the <see cref="IFileStorageFactory"/> using Cloud Storage implementation.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="bucketName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void UseFileStorageFromGoogle(this IServiceCollection services, string bucketName)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            if (String.IsNullOrEmpty(bucketName))
                throw new ArgumentNullException(nameof(bucketName));

            services.UseFileStorage( (ctx, factory) =>
            {
                factory.UseProvider("GCP", () =>
                {
                    return new GcpCloudStorage(bucketName);
                });
                return factory;
            });
        }
    }
}
