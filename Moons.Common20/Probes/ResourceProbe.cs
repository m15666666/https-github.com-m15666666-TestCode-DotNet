using System;
using System.Text;
using Moons.Common20.Collections;

namespace Moons.Common20.Probes
{
    /// <summary>
    /// 资源探针，记录资源分配和释放的情况
    /// </summary>
    public class ResourceProbe
    {
        #region 变量和属性

        /// <summary>
        /// 内部锁
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// 资源集合
        /// </summary>
        private readonly Collection<ResourceData> _resourceDatas = new Collection<ResourceData>();

        #endregion

        #region 创建、释放资源

        /// <summary>
        /// 创建资源
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="description">描述</param>
        public void CreateResource( string key, string description )
        {
            if( EnvironmentUtils.IsRelease )
            {
                return;
            }
            if( string.IsNullOrEmpty( key ) )
            {
                return;
            }

            lock( _lock )
            {
                _resourceDatas.Add(
                    new ResourceData
                        {
                            CreateTime = DateTime.Now,
                            Key = key,
                            Description = description
                        } );
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="key">键</param>
        public void DisposeResource( string key )
        {
            if( EnvironmentUtils.IsRelease )
            {
                return;
            }
            if( string.IsNullOrEmpty( key ) )
            {
                return;
            }

            lock( _lock )
            {
                ResourceData item = _resourceDatas.FirstOrDefault( data => data.Key == key );
                if( item != null )
                {
                    _resourceDatas.Remove( item );
                }
            }
        }

        #endregion

        #region 统计信息

        /// <summary>
        /// 当前资源的个数
        /// </summary>
        public int ResourceCount
        {
            get { return _resourceDatas.Count; }
        }

        /// <summary>
        /// to string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if( EnvironmentUtils.IsRelease )
            {
                return string.Empty;
            }

            lock( _lock )
            {
                var buffer = new StringBuilder();
                buffer.AppendFormat( "ResourceCount:{0}", ResourceCount );
                buffer.AppendFormat(
                    ", Resources:({0})",
                    StringUtils.Join(
                        _resourceDatas.ConvertAll(
                            data => data.ToString() )
                        ) );
                return buffer.ToString();
            }
        }

        #endregion
    }
}