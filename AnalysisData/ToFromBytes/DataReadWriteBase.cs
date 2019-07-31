using System.Collections.Generic;
using System.IO;
using Moons.Common20;
using Moons.Common20.Serialization;
using Moons.Common20.ValueWrapper;

namespace AnalysisData.ToFromBytes
{
    /// <summary>
    /// 读写的基类
    /// </summary>
    public class DataReadWriteBase
    {
        #region 变量和属性

        /// <summary>
        /// 用于读取的工具类对象
        /// </summary>
        public ToFromBytesUtils ReadBytesUtils { protected get; set; }

        /// <summary>
        /// 用于写入的工具类对象
        /// </summary>
        public ToFromBytesUtils WriteBytesUtils { protected get; set; }

        /// <summary>
        /// BinaryWriter
        /// </summary>
        public BinaryWriter BinaryWriter
        {
            set { WriteBytesUtils = new ToFromBytesUtils( value ); }
        }

        /// <summary>
        /// BinaryReader
        /// </summary>
        public BinaryReader BinaryReader
        {
            set { ReadBytesUtils = new ToFromBytesUtils( value ); }
        }

        #endregion

        #region 读写函数

        public void ReadValueWrappersContainer( IValueWrappersContainer data )
        {
            ReadBytesUtils.Read( data.ValueWrappers );
        }

        public void ReadValueWrappersContainers( params IValueWrappersContainer[] datas )
        {
            ReadValueWrappersContainers( (IEnumerable<IValueWrappersContainer>)datas );
        }

        private void ReadValueWrappersContainers<T>( IEnumerable<T> collection ) where T : IValueWrappersContainer
        {
            ForUtils.ForEach( collection, item => ReadValueWrappersContainer( item ) );
        }

        /// <summary>
        /// 读IValueWrappersContainer对象
        /// </summary>
        /// <typeparam name="T">实现IValueWrappersContainer的类</typeparam>
        /// <returns>IValueWrappersContainer对象</returns>
        public T ReadValueWrappersContainer<T>() where T : IValueWrappersContainer, new()
        {
            var ret = new T();
            ReadValueWrappersContainer( ret );
            return ret;
        }

        private void ReadValueWrappersContainerCollection<T>( ICollection<T> ret )
            where T : IValueWrappersContainer, new()
        {
            ForUtils.ForCount( ReadBytesUtils.ReadInt32(), () => ret.Add( new T() ) );
            ReadValueWrappersContainers( ret );
        }

        public IList<T> ReadValueWrappersContainerCollection<T>() where T : IValueWrappersContainer, new()
        {
            var ret = new List<T>();
            ReadValueWrappersContainerCollection( ret );
            return ret;
        }

        public TCollection ReadValueWrappersContainerCollection<T, TCollection>()
            where TCollection : IList<T>, new()
            where T : IValueWrappersContainer, new()
        {
            var ret = new TCollection();
            ReadValueWrappersContainerCollection( ret );
            return ret;
        }

        public void WriteValueWrappersContainer( IValueWrappersContainer data )
        {
            WriteBytesUtils.Write( data.ValueWrappers );
        }

        public void WriteValueWrappersContainers( params IValueWrappersContainer[] datas )
        {
            WriteValueWrappersContainers( (IEnumerable<IValueWrappersContainer>)datas );
        }

        public void WriteValueWrappersContainers<T>( ICollection<T> collection ) where T : IValueWrappersContainer
        {
            WriteBytesUtils.WriteInt32( collection.Count );
            WriteValueWrappersContainers( (IEnumerable<T>)collection );
        }

        private void WriteValueWrappersContainers<T>( IEnumerable<T> collection ) where T : IValueWrappersContainer
        {
            ForUtils.ForEach( collection, item => WriteValueWrappersContainer( item ) );
        }

        #endregion
    }
}