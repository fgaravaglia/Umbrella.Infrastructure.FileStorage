using Microsoft.Extensions.DependencyInjection;
using System;
using Umbrella.Infrastructure.FileStorage.DependencyInjection;

namespace Umbrella.Infrastructure.FileStorage.Providers.FileSystem
{
    /// <summary>
    /// Extensions to inject components into DImanagement
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Injects the <see cref="IFileStorageFactory"/> using Filesistem implementation.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="rootFolder"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static void UseFileStorageFromFileSystem(this IServiceCollection services, string rootFolder)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            if (String.IsNullOrEmpty(rootFolder))
                throw new ArgumentNullException(nameof(rootFolder));

            services.UseFileStorage((ctx, factory) =>
            {
                factory.UseFileSystem(rootFolder);
                return factory;
            });
        }

    }
}
