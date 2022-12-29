using Microsoft.Extensions.DependencyInjection;
using System;

namespace Umbrella.Infrastructure.FileStorage.DependencyInjection
{
    /// <summary>
    /// Extensions to inject components into DImanagement
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Injects the <see cref="IFileStorageFactory"/> using Filesistem implementation.
        /// </summary>
        /// <remarks>please add your own extensions to be more specific, and use this methods only to cusomtize it</remarks>
        /// <param name="services"></param>
        /// <param name="factoryBuilder"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void UseFileStorage(this IServiceCollection services, Func<IServiceProvider, FileStorageFactory, IFileStorageFactory> factoryBuilder)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            if (factoryBuilder == null)
                throw new ArgumentNullException(nameof(factoryBuilder));

            services.AddSingleton<IFileStorageFactory>(x =>
            {
                var factory = new FileStorageFactory();
                return factoryBuilder.Invoke(x, factory);
            });
            services.AddTransient<IFileStorage>(x =>
            {
                var factory = x.GetRequiredService<IFileStorageFactory>();
                return factory.CreateFileStorage();
            });
        }
    }
}
