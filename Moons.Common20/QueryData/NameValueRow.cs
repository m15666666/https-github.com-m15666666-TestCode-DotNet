using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using Moons.Common20.Reflection;

namespace Moons.Common20.QueryData
{
    /// <summary>
    /// 关联的 String 键和 Object 值的集合（可通过键或索引来访问它）。
    /// </summary>
    public class NameValueRow : NameObjectCollectionBase
    {
        #region 基本操作

        /// <summary>
        /// 键对初始键的映射
        /// </summary>
        private readonly Dictionary<string, string> _key2OriginKey = new Dictionary<string, string>();

        /// <summary>
        /// 获取/设置数据
        /// </summary>
        /// <param name="index">下标</param>
        /// <returns>值</returns>
        public object this[ int index ]
        {
            get { return BaseGet( index ); }
            set { BaseSet( index, value ); }
        }

        /// <summary>
        /// 获取/设置数据
        /// </summary>
        /// <param name="name">名</param>
        /// <returns>值</returns>
        public object this[ string name ]
        {
            get { return BaseGet( CharCaseUtils.ToCase( name ) ); }
            set
            {
                string key = CharCaseUtils.ToCase( name );
                _key2OriginKey[key] = name;
                BaseSet( key, value );
            }
        }

        /// <summary>
        /// 获得所有列名
        /// </summary>
        public string[] ColumnNames
        {
            get { return Array.ConvertAll( BaseGetAllKeys(), name => CharCaseUtils.ToCase( name ) ); }
        }

        /// <summary>
        /// 获得初始的所有列名（未转换大小写）
        /// </summary>
        public string[] OriginColumnNames
        {
            get { return Array.ConvertAll( BaseGetAllKeys(), name => _key2OriginKey[name] ); }
        }

        /// <summary>
        /// 获得所有值
        /// </summary>
        public object[] Values
        {
            get { return BaseGetAllValues(); }
        }

        /// <summary>
        /// 增加一项
        /// </summary>
        /// <param name="name">名</param>
        /// <param name="value">值</param>
        public void Add( string name, object value )
        {
            this[name] = value;
        }

        #endregion

        #region 通过反射转化为对象

        /// <summary>
        /// 类型对可写的公共属性的映射
        /// </summary>
        private static readonly HashDictionary<Type, PropertyInfoCollection> _type2PublicCanWriteProperties =
            new HashDictionary<Type, PropertyInfoCollection>();

        /// <summary>
        /// 通过反射转化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns>对象集合</returns>
        public T ToObjectByReflection<T>() where T : class, new()
        {
            Type type = typeof(T);
            PropertyInfoCollection propertyies = _type2PublicCanWriteProperties[type];
            if( propertyies == null )
            {
                PropertyInfoCollection canWriteProperties = ReflectionUtils.GetPublicCanWriteProperties( type );
                propertyies = new PropertyInfoCollection();
                foreach( string columnName in ColumnNames )
                {
                    string propertyName = columnName;
                    PropertyInfo property =
                        canWriteProperties.Find( item => StringUtils.EqualIgnoreCase( item.Name, propertyName ) );
                    if( property != null )
                    {
                        propertyies.Add( property );
                    }
                }

                _type2PublicCanWriteProperties[type] = propertyies;
            }

            var ret = new T();
            foreach( PropertyInfo propertyInfo in propertyies )
            {
                ReflectionUtils.SetPropertyValue( propertyInfo, ret, this[propertyInfo.Name] );
            }

            return ret;
        }

        #endregion

        #region 通过反射将对象转化为NameValueRow

        /// <summary>
        /// 类型对可读的公共属性的映射
        /// </summary>
        private static readonly HashDictionary<Type, PropertyInfoCollection> _type2PublicCanReadProperties =
            new HashDictionary<Type, PropertyInfoCollection>();

        /// <summary>
        /// 通过反射将对象转化为NameValueRow
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>NameValueRow</returns>
        public static NameValueRow FromObjectByReflection<T>( T obj ) where T : class
        {
            Type type = typeof(T);
            PropertyInfoCollection propertyies = _type2PublicCanReadProperties[type];
            if( propertyies == null )
            {
                _type2PublicCanReadProperties[type] =
                    propertyies = ReflectionUtils.GetPublicCanReadProperties( type );
            }

            var ret = new NameValueRow();
            foreach( PropertyInfo propertyInfo in propertyies )
            {
                ret[propertyInfo.Name] = ReflectionUtils.GetPropertyValue( propertyInfo, obj );
            }

            return ret;
        }

        #endregion
    }
}