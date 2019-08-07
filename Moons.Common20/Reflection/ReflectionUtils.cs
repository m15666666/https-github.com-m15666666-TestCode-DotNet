using System;
using System.Collections.Generic;
using System.Reflection;

namespace Moons.Common20.Reflection
{
    /// <summary>
    /// 关于反射的实用工具
    /// </summary>
    public static class ReflectionUtils
    {
        #region 创建类实例

        /// <summary>
        /// 通过程序集名和类名创建类实例
        /// </summary>
        /// <typeparam name="T">类实例的类型</typeparam>
        /// <param name="dLLName">程序集名</param>
        /// <param name="className">类名</param>
        /// <returns>类实例</returns>
        public static T CreateInstanceByClassName<T>( string dLLName, string className ) where T : class
        {
            return Assembly.Load( dLLName ).CreateInstance( className ) as T;
        }

        #endregion

        #region 绑定公共属性值

        /// <summary>
        /// 类型对类型的公共属性的映射
        /// </summary>
        private static readonly HashDictionary<Type, PropertyInfoCollection> _type2PublicProperties =
            new HashDictionary<Type, PropertyInfoCollection>();

        /// <summary>
        /// 类型对类型值转换函数的映射
        /// </summary>
        private static readonly HashDictionary<Type, Func20<object, object>> _type2ValueConvertHandler =
            new HashDictionary<Type, Func20<object, object>>
                {
                    { typeof(bool), o => Convert.ToBoolean( o ) },
                    { typeof(bool?), o => Convert.ToBoolean( o ) },
                    { typeof(byte), o => Convert.ToByte( o ) },
                    { typeof(byte?), o => Convert.ToByte( o ) },
                    { typeof(short), o => Convert.ToInt16( o ) },
                    { typeof(short?), o => Convert.ToInt16( o ) },
                    { typeof(int), o => Convert.ToInt32( o ) },
                    { typeof(int?), o => Convert.ToInt32( o ) },
                    { typeof(long), o => Convert.ToInt64( o ) },
                    { typeof(long?), o => Convert.ToInt64( o ) },
                    { typeof(float), o => Convert.ToSingle( o ) },
                    { typeof(float?), o => Convert.ToSingle( o ) },
                    { typeof(double), o => Convert.ToDouble( o ) },
                    { typeof(double?), o => Convert.ToDouble( o ) },
                    { typeof(DateTime), o => Convert.ToDateTime( o ) },
                    { typeof(DateTime?), o => Convert.ToDateTime( o ) },
                };

        /// <summary>
        /// 获得类型的公共属性
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>类型的公共属性</returns>
        public static PropertyInfoCollection GetPublicProperties( Type type )
        {
            return _type2PublicProperties[type] ?? ( _type2PublicProperties[type] =
                                                     new PropertyInfoCollection(
                                                         type.GetProperties( BindingFlags.Instance | BindingFlags.Public ) ) );
        }

        /// <summary>
        /// 获得类型的可读的公共属性
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>类型的公共属性</returns>
        public static PropertyInfoCollection GetPublicCanReadProperties( Type type )
        {
            return GetPublicProperties( type ).GetCanRead();
        }

        /// <summary>
        /// 获得类型的可写的公共属性
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>类型的公共属性</returns>
        public static PropertyInfoCollection GetPublicCanWriteProperties( Type type )
        {
            return GetPublicProperties( type ).GetCanWrite();
        }

        /// <summary>
        /// 获得类型的可读可写的公共属性
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>类型的公共属性</returns>
        public static PropertyInfoCollection GetPublicCanReadWriteProperties( Type type )
        {
            return GetPublicProperties( type ).GetCanReadWrite();
        }

        /// <summary>
        /// 通过反射设置属性值
        /// </summary>
        /// <param name="propertyInfo">PropertyInfo</param>
        /// <param name="obj">对象</param>
        /// <param name="value">属性值</param>
        public static void SetPropertyValue( PropertyInfo propertyInfo, object obj, object value )
        {
            if( obj == null || DBConvertUtils.IsNull( value ) || propertyInfo == null || !propertyInfo.CanWrite )
            {
                return;
            }

            Func20<object, object> handler = _type2ValueConvertHandler[propertyInfo.PropertyType];
            if( handler != null )
            {
                value = handler( value );
            }
            propertyInfo.SetValue( obj, value, null );
        }

        /// <summary>
        /// 通过反射获得属性值
        /// </summary>
        /// <param name="propertyInfo">PropertyInfo</param>
        /// <param name="data">对象</param>
        /// <returns>属性值</returns>
        public static object GetPropertyValue( PropertyInfo propertyInfo, object data )
        {
            if( data == null || propertyInfo == null || !propertyInfo.CanRead )
            {
                return null;
            }

            return propertyInfo.GetValue( data, null );
        }

        #endregion

        #region 对象转化为字符串

        /// <summary>
        /// 通过反射获得对象的描述信息
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>对象的描述信息</returns>
        public static string Object2String( object obj )
        {
            if( obj == null )
            {
                return string.Format( "[{0}]", StringUtils.NullReference );
            }

            var list = new List<string>();
            foreach( PropertyInfo propertyInfo in GetPublicCanReadProperties( obj.GetType() ) )
            {
                object value;
                try
                {
                    value = GetPropertyValue( propertyInfo, obj );
                    list.Add(string.Format("{0} = {1}", propertyInfo.Name, value ?? StringUtils.NullReference));
                }
                catch
                {
                    //value = "_访_问_异_常_";
                }
            }

            return string.Format( "[{0}]", StringUtils.Join( list.ToArray() ) );
        }

        #endregion

        #region 判断

        /// <summary>
        /// 是否为系统内置类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>true：是系统内置类型，false：不是系统内置类型</returns>
        public static bool IsSystemType( Type type )
        {
            return type.Namespace.Equals( "System" );
        }

        #endregion
    }
}