using Castle.MicroKernel;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testORM.IOC
{
    /// <summary>
    /// IOC容器
    /// </summary>
    public sealed class Container
    {
        #region Fields & Properies
        /// <summary>
        /// WindsorContainer object
        /// </summary>
        private WindsorContainer windsor;
        private IKernel kernel;
        public IKernel Kernel
        {
            get { return kernel; }
        }

        /// <summary>
        /// 容器实例
        /// </summary>
        private static readonly Container instance = new Container();
        public static Container Instance
        {
            get { return Container.instance; }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor Method.
        /// Initialization IOC.
        /// </summary>
        public Container()
        {
            try
            {
                Castle.Core.Resource.ConfigResource source = new Castle.Core.Resource.ConfigResource();
                XmlInterpreter interpreter = new XmlInterpreter(source);
                windsor = new WindsorContainer(interpreter);
                kernel = windsor.Kernel;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns a component instance by the type of service.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return kernel.Resolve<T>();
        }
        /// <summary>
        /// Release resource that be container used.
        /// </summary>
        public void Dispose()
        {
            kernel.Dispose();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Returns a component instance by the service name.
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        private object Resolve(Type service)
        {
            return kernel.Resolve(service);
        }

        /// <summary>
        /// Returns a component instance by the service name.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private object Resolve(String key)
        {
            return kernel.Resolve<object>(key);
        }
        #endregion

    }
}
