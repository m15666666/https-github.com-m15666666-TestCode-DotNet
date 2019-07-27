using System;

namespace Moons.Common20
{
    /// <summary>
    /// 数据库中值转换的实用工具类
    /// </summary>
    public static class DBConvertUtils
    {
        #region 基本操作

        /// <summary>
        /// 判读字段值是否为空
        /// </summary>
        /// <param name="value">字段值</param>
        /// <returns>true：为空，false：不为空</returns>
        public static bool IsNull( object value )
        {
            return ( value == null || Convert.IsDBNull( value ) );
        }

        #endregion

        #region 转换

        #region ToNullDateTime

        /// <summary>
        /// ToNullDateTime
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime? ToNullDateTime( object value )
        {
            // 针对SqlServer做特殊处理，SqlServer的null值返回为空字符串
            if( value is string && string.IsNullOrEmpty( value as string ) )
            {
                return null;
            }

            return IsNull( value ) ? default( DateTime? ) : Convert.ToDateTime( value );
        }

        #endregion

        #region ToNullInt16

        /// <summary>
        /// ToNullInt16
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Int16? ToNullInt16( object value )
        {
            return IsNull( value ) ? default( Int16? ) : Convert.ToInt16( value );
        }

        #endregion

        #region ToNullInt32

        /// <summary>
        /// ToNullInt32
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Int32? ToNullInt32( object value )
        {
            return IsNull( value ) ? default( Int32? ) : Convert.ToInt32( value );
        }

        #endregion

        #region ToNullInt64

        /// <summary>
        /// ToNullInt64
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Int64? ToNullInt64( object value )
        {
            return IsNull( value ) ? default( Int64? ) : Convert.ToInt64( value );
        }

        #endregion

        #region ToNullSingle

        /// <summary>
        /// ToNullSingle
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Single? ToNullSingle( object value )
        {
            return IsNull( value ) ? default( Single? ) : Convert.ToSingle( value );
        }

        #endregion

        #region ToNullDouble

        /// <summary>
        /// ToNullDouble
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Double? ToNullDouble( object value )
        {
            return IsNull( value ) ? default( Double? ) : Convert.ToDouble( value );
        }

        #endregion

        #region ToNullByte

        /// <summary>
        /// ToNullDecimal
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Byte? ToNullByte( object value )
        {
            return IsNull( value ) ? default( Byte? ) : Convert.ToByte( value );
        }

        #endregion

        #region ToNullDecimal

        /// <summary>
        /// ToNullDecimal
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Decimal? ToNullDecimal( object value )
        {
            return IsNull( value ) ? default( Decimal? ) : Convert.ToDecimal( value );
        }

        #endregion

        #endregion
    }
}