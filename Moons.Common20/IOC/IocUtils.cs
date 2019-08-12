using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.Common20.IOC
{
    /// <summary>
    /// 依赖注入实用工具类
    /// </summary>
    public class IocUtils : IDisposable
    {
        public static IocUtils Instance { get; set; } = new IocUtils();

        private IServiceProvider _serviceProvider;

        /// <summary>
        /// 默认的IServiceProvider
        /// </summary>
        public IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceProvider != null) return _serviceProvider;
                if (ServiceScope != null) return _serviceProvider = ServiceScope.ServiceProvider;
                return null;
            }
            set
            {
                _serviceProvider = value;
            }
        }

        public IServiceScope ServiceScope { get; set; }

        public T GetRequiredService<T>()
        {
            return ServiceProvider != null ? ServiceProvider.GetRequiredService<T>() : default;
        }

        public object GetRequiredService(Type serviceType)
        {
            return ServiceProvider != null ? ServiceProvider.GetRequiredService(serviceType) : default;
        }

        public T GetService<T>()
        {
            return ServiceProvider != null ? ServiceProvider.GetService<T>() : default;
        }

        public object GetService(Type serviceType)
        {
            return ServiceProvider != null ? ServiceProvider.GetService(serviceType) : default;
        }

        public IEnumerable<T> GetServices<T>()
        {
            return ServiceProvider != null ? ServiceProvider.GetServices<T>() : default;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return ServiceProvider != null ? ServiceProvider.GetServices(serviceType) : default;
        }

        public IServiceScope CreateScope()
        {
            return ServiceProvider != null ? ServiceProvider.CreateScope() : default;
        }

        public void Dispose()
        {
            _serviceProvider = null;
            var scope = ServiceScope;
            if (scope != null)
            {
                ServiceScope = null;
                scope.Dispose();
            }
        }
    }
}
