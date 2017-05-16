using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin.Builder;

namespace WebMVCOwinAutofac
{
    public class Adapter
    {
        /// <summary>
        ///     Microsoft.Owin模式中的“应用程序委托”，是接受请求的入口
        /// </summary>
        private static Func<IDictionary<string, object>, Task> _owinApp;

        /// <summary>
        ///     默认构造函数
        /// </summary>
        public Adapter()
        {
            // 创建默认的“应用程序生成器”
            var builder = new AppBuilder();

            // 实例化用户应用程序的启动类(Startup)
            // 这个类中必须有“Configuration”方法
            var startup = new Startup();

            // 调用启动类的Configuration方法，让用户完成自己的配置
            startup.Configuration( builder );

            // 生成OWIN“入口”函数
            _owinApp = builder.Build();
        }


        /// <summary>
        ///     *** JWS或TinyFox所需要的关键函数 ***
        ///     <para>每个请求到来，JWS/TinyFox都把请求打包成字典，通过这个函数提供给本应用</para>
        /// </summary>
        /// <param name="env">新请求的环境字典</param>
        /// <returns>返回一个正在运行或已经完成的任务</returns>
        public Task OwinMain( IDictionary<string, object> env )
        {
            if( _owinApp == null ) return null;

            // 将请求交给Microsoft.Owin框架对这个请求进行处理
            //（你的处理方法已经在本类的构造函数中加入到它的处理序列中了）
            return _owinApp( env );
        }
    }
}