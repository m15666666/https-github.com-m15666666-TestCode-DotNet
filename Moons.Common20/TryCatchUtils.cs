using System;

namespace Moons.Common20
{
    /// <summary>
    /// try catch finally结构的实用工具类
    /// </summary>
    public static class TryCatchUtils
    {
        #region Catch

        /// <summary>
        /// 执行代理函数
        /// </summary>
        /// <param name="handler">代理函数</param>
        /// <param name="data">参数</param>
        /// <returns>true：抛出了异常，false：未抛出异常</returns>
        public static bool Catch<T>( Action<T> handler, T data )
        {
            if( handler != null )
            {
                try
                {
                    handler( data );
                }
                catch
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 执行代理函数
        /// </summary>
        /// <param name="handler">代理函数</param>
        /// <returns>true：抛出了异常，false：未抛出异常</returns>
        public static bool Catch( Action20 handler )
        {
            if( handler != null )
            {
                try
                {
                    handler();
                }
                catch
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        /// <summary>
        /// 执行所有的代理函数，都不抛出异常。
        /// </summary>
        /// <param name="handlers">代理函数</param>
        public static void CatchAll( params Action20[] handlers )
        {
            ForUtils.ForEach( handlers, handler => Catch( handler ) );
        }

        /// <summary>
        /// 尝试执行最少的代理函数，某个函数不抛出异常就返回
        /// </summary>
        /// <param name="handlers">代理函数</param>
        public static void TryMin( params Action20[] handlers )
        {
            ForUtils.UntilFalse4Each( handlers, Catch );
        }
    }
}