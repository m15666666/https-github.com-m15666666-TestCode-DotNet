using System;
using System.Collections.Generic;
using System.Text;
using Moons.Common20.PromptMessage;
using Moons.Common20.StringResources;

namespace Moons.Common20
{
    /// <summary>
    /// 不同类型之间转换的实用工具类
    /// </summary>
    public static class ConvertUtils
    {
        #region 类型转换

        #region ToBoolean

        /// <summary>
        /// string转bool
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>bool值</returns>
        public static bool ToBoolean( string value )
        {
            return ToBoolean( value, false );
        }

        /// <summary>
        /// string转bool
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>bool值</returns>
        public static bool ToBoolean( string value, bool defaultValue )
        {
            bool ret;
            return bool.TryParse( StringUtils.Trim( value ), out ret ) ? ret : defaultValue;
        }

        /// <summary>
        /// string转bool
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>bool值</returns>
        public static bool? ToNullBoolean( string value )
        {
            bool ret;
            return bool.TryParse( StringUtils.Trim( value ), out ret ) ? ret : default( bool? );
        }

        #endregion

        #region ToInt32

        /// <summary>
        /// string转Int32
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>Int32值</returns>
        public static int ToInt32( string value, int defaultValue )
        {
            int ret;
            return Int32.TryParse( StringUtils.Trim( value ), out ret ) ? ret : defaultValue;
        }

        /// <summary>
        /// string转Int32，默认值为0
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>Int32值</returns>
        public static int ToInt32( string value )
        {
            return ToInt32( value, 0 );
        }

        /// <summary>
        /// object转Int32
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>Int32值</returns>
        public static int ToInt32( object value, int defaultValue )
        {
            return ToInt32( StringUtils.Trim( value ), defaultValue );
        }

        /// <summary>
        /// object转Int32，默认值为0
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>Int32值</returns>
        public static int ToInt32( object value )
        {
            return ToInt32( value, 0 );
        }

        /// <summary>
        /// string转Int32
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>Int32值</returns>
        public static int? ToNullInt32( string value )
        {
            int ret;
            return Int32.TryParse( StringUtils.Trim( value ), out ret ) ? ret : default( int? );
        }

        #endregion

        #region ToInt64

        /// <summary>
        /// string转Int64
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>Int64值</returns>
        public static long ToInt64( string value, long defaultValue )
        {
            long ret;
            return Int64.TryParse( StringUtils.Trim( value ), out ret ) ? ret : defaultValue;
        }

        /// <summary>
        /// string转Int64，默认值为0
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>Int64值</returns>
        public static long ToInt64( string value )
        {
            return ToInt64( value, 0 );
        }

        /// <summary>
        /// object转Int64
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>Int64值</returns>
        public static long ToInt64( object value, long defaultValue )
        {
            return ToInt64( StringUtils.Trim( value ), defaultValue );
        }

        /// <summary>
        /// object转Int64，默认值为0
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>Int64值</returns>
        public static long ToInt64( object value )
        {
            return ToInt64( value, 0 );
        }

        /// <summary>
        /// string转Int64
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>Int64值</returns>
        public static long? ToNullInt64( string value )
        {
            long ret;
            return Int64.TryParse( StringUtils.Trim( value ), out ret ) ? ret : default( long? );
        }

        #endregion

        #region ToSingle

        /// <summary>
        /// object转float
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>string</returns>
        public static float ToFloat( object value )
        {
            return ToSingle( StringUtils.ToString( value ) );
        }

        /// <summary>
        /// string转Single
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>Single值</returns>
        public static Single ToSingle( string value )
        {
            return ToSingle( value, 0f );
        }

        /// <summary>
        /// string转Single
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>Single值</returns>
        public static Single ToSingle( string value, Single defaultValue )
        {
            Single ret;
            return Single.TryParse( StringUtils.Trim( value ), out ret ) ? ret : defaultValue;
        }

        /// <summary>
        /// string转Single
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>Single值</returns>
        public static Single? ToNullSingle( string value )
        {
            Single ret;
            return Single.TryParse( StringUtils.Trim( value ), out ret ) ? ret : default( Single? );
        }

        #endregion

        #region ToDouble

        /// <summary>
        /// string转Double
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>Double值</returns>
        public static Double ToDouble( string value )
        {
            return ToDouble( value, 0 );
        }

        /// <summary>
        /// string转Double
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>Double值</returns>
        public static Double ToDouble( string value, double defaultValue )
        {
            Double ret;
            return Double.TryParse( StringUtils.Trim( value ), out ret ) ? ret : defaultValue;
        }

        /// <summary>
        /// string转Double
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>Double值</returns>
        public static Double? ToNullDouble( string value )
        {
            Double ret;
            return Double.TryParse( StringUtils.Trim( value ), out ret ) ? ret : default( Double? );
        }

        #endregion

        #region ToDouble

        /// <summary>
        /// string转DateTime
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>DateTime值</returns>
        public static DateTime ToDateTime( string value )
        {
            return ToDateTime( value, DateTime.MinValue );
        }

        /// <summary>
        /// string转DateTime
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>DateTime值</returns>
        public static DateTime ToDateTime( string value, DateTime defaultValue )
        {
            DateTime ret;
            return DateTime.TryParse( StringUtils.Trim( value ), out ret ) ? ret : defaultValue;
        }

        /// <summary>
        /// string转DateTime
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>Double值</returns>
        public static DateTime? ToNullDateTime( string value )
        {
            DateTime ret;
            return DateTime.TryParse( StringUtils.Trim( value ), out ret ) ? ret : default( DateTime? );
        }

        #endregion

        #region ToString

        /// <summary>
        /// object转string
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>string</returns>
        public static string ToString( object value, string defaultValue )
        {
            return StringUtils.Trim( value ) ?? defaultValue;
        }

        /// <summary>
        /// object转string，默认值为string.Empty
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>string</returns>
        public static string ToString( object value )
        {
            return ToString( value, String.Empty );
        }

        #endregion

        /// <summary>
        /// 将字符串型的值转换为dataType类型的值
        /// </summary>
        /// <param name="data">字符串型的值</param>
        /// <param name="dataType">值类型</param>
        /// <returns>dataType类型的值，null表示转换失败</returns>
        public static object ToDataType( string data, DataType4Convert dataType )
        {
            data = StringUtils.Trim( data );
            if( String.IsNullOrEmpty( data ) )
            {
                return null;
            }

            try
            {
                switch( dataType )
                {
                    case DataType4Convert.Int32:
                        return Int32.Parse( data );

                    case DataType4Convert.DateTime:
                        return DateTime.Parse( data );

                    case DataType4Convert.Int64:
                        return Int64.Parse( data );

                    case DataType4Convert.Float:
                        return Single.Parse( data );

                    case DataType4Convert.Double:
                        return Double.Parse( data );

                    case DataType4Convert.String:
                    case DataType4Convert.CharAndNum:
                    case DataType4Convert.CharAndNumAndChinese:
                    case DataType4Convert.Email:
                        return data;

                    default:
                        return null;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 将字符串型的值转换为dataType类型的值
        /// </summary>
        /// <param name="datas">字符串型的值</param>
        /// <param name="dataType">值类型</param>
        /// <returns>dataType类型的值，null表示转换失败</returns>
        public static object[] ToDataType(string[] datas, DataType4Convert dataType)
        {
            List<object> result = new List<object>();
            foreach( var data in datas )
            {
                var obj = ToDataType( data, dataType );
                if( obj == null )
                {
                    continue;
                }

                result.Add(obj);
            }

            return result.ToArray();
        }

        #endregion

        #region 转换为可空类型/从可空类型转换

        /// <summary>
        /// 转换为可空值类型，hasValue为false表示value无效。
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="hasValue">是否包含值</param>
        /// <param name="value">内部的值</param>
        /// <returns>可空值类型</returns>
        public static T? ToNullable<T>( bool hasValue, T value ) where T : struct
        {
            return hasValue ? value : default( T? );
        }

        /// <summary>
        /// 从可空类型转换，hasValue为false表示value无效。
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="nullableValue">可空类型的值</param>
        /// <param name="hasValue">输出，是否包含值</param>
        /// <param name="value">输出，内部的值</param>
        public static void FromNullable<T>( T? nullableValue, out bool hasValue, out T value ) where T : struct
        {
            hasValue = nullableValue.HasValue;
            if( hasValue )
            {
                value = nullableValue.Value;
                return;
            }
            value = default( T );
        }

        #endregion

        #region 判断是不是某个类型

        /// <summary>
        /// 判断是否Float
        /// </summary>
        /// <param name="data">字符串型的值</param>
        /// <returns>true：是该类型，false：不是该类型</returns>
        public static bool IsFloat( string data )
        {
            Single result;
            return Single.TryParse( data, out result );
        }

        /// <summary>
        /// 判断是否是Decimal类型
        /// </summary>
        /// <param name="data">字符串型的值</param>
        /// <returns>true：是该类型，false：不是该类型</returns>
        public static bool IsDecimal( string data )
        {
            decimal result;
            return decimal.TryParse( data, out result );
        }

        #endregion

        #region 校验相关函数

        #region GetStringValue

        /// <summary>
        /// 长度过长
        /// </summary>
        private static readonly LocaleString LengthGreatError =
            new LocaleString( InputValidationMsgKey.LengthGreatError, "{0} 长度不能超过{1}!" );

        /// <summary>
        /// 长度过短
        /// </summary>
        private static readonly LocaleString LengthLessError =
            new LocaleString( InputValidationMsgKey.LengthLessError, "{0} 长度不能低于{1}!" );

        /// <summary>
        /// 不能为空
        /// </summary>
        private static readonly LocaleString EmptyError =
            new LocaleString( InputValidationMsgKey.EmptyError, "{0} 不能为空!" );

        /// <summary>
        /// 获取值并校验
        /// </summary>
        /// <param name="data">字符串数据</param>
        /// <param name="name">对应字段名称</param>
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="handler">有校验错误时执行的函数</param>
        /// <returns>值</returns>
        public static string GetStringValue( string data, string name, int minLength, int maxLength,
                                             DataType4Convert dataType, Action<string> handler )
        {
            data = StringUtils.Trim( data ) ?? string.Empty;

            #region "检测最大允许长度"

            if( 0 < maxLength )
            {
                byte[] tempbytes = Encoding.Default.GetBytes( data.ToCharArray() );
                if( tempbytes.Length > maxLength )
                {
                    handler( string.Format( LengthGreatError, name, maxLength ) );
                    return null;
                }
            }

            #endregion

            #region "检测最小允许长度"

            if( 0 < minLength )
            {
                if( data.Length < minLength )
                {
                    handler( minLength == 1
                                 ? string.Format( EmptyError, name )
                                 : string.Format( LengthLessError, name, minLength ) );

                    return null;
                }
            }

            #endregion

            #region "检测数据类型"

            if( string.IsNullOrEmpty( data ) )
            {
                return data;
            }

            switch( dataType )
            {
                case DataType4Convert.CharAndNum:
                    if( !RegExpUtils.IsMatch( data, "^[A-Za-z0-9]+$" ) )
                    {
                        handler( string.Format( "{0} 必须为英文或数字!", name ) );
                        return null;
                    }
                    return data;

                case DataType4Convert.CharAndNumAndChinese:
                    if( !RegExpUtils.IsMatch( data, "^[A-Za-z0-9\u00A1-\u2999\u3001-\uFFFD]+$" ) )
                    {
                        handler( string.Format( "{0} 必须为英文或数字或中文!", name ) );
                        return null;
                    }
                    return data;

                case DataType4Convert.Email:
                    if( !RegExpUtils.IsMatch( data, "\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*" ) )
                    {
                        handler( string.Format( "{0} 必须为邮件地址!", name ) );
                        return null;
                    }
                    return data;

                default:
                    return data;
            }

            #endregion
        }

        /// <summary>
        /// 获取值并校验
        /// </summary>
        /// <param name="data">字符串数据</param>
        /// <param name="name">对应字段名称</param>
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="messages">校验信息，不为空表示有校验错误</param>
        /// <returns>值</returns>
        public static string GetStringValue( string data, string name, int minLength, int maxLength,
                                             DataType4Convert dataType, IList<string> messages )
        {
            return GetStringValue( data, name, minLength, maxLength, dataType, messages.Add );
        }

        #endregion

        #region GetDateTimeValue

        /// <summary>
        /// 获取值并校验
        /// </summary>
        /// <param name="data">字符串数据</param>
        /// <param name="name">对应字段名称</param>
        /// <param name="allowEmpty">true：允许为空，false：不允许为空</param>
        /// <param name="handler">有校验错误时执行的函数</param>
        /// <returns>值</returns>
        public static DateTime? GetDateTimeValue( string data, string name, bool allowEmpty, Action<string> handler )
        {
            data = StringUtils.Trim( data ) ?? string.Empty;

            string error = string.Format( "字段值：{0}数据类型必须为日期型!", name );
            if( string.IsNullOrEmpty( data ) )
            {
                if( !allowEmpty )
                {
                    handler( error );
                    return DateTime.MinValue;
                }

                return null;
            }

            DateTime ret;
            if( !DateTime.TryParse( data, out ret ) )
            {
                handler( error );
                return DateTime.MinValue;
            }

            return ret;
        }

        /// <summary>
        /// 获取值并校验
        /// </summary>
        /// <param name="data">字符串数据</param>
        /// <param name="name">对应字段名称</param>
        /// <param name="handler">有校验错误时执行的函数</param>
        /// <returns>值</returns>
        public static DateTime GetDateTimeValue( string data, string name, Action<string> handler )
        {
// ReSharper disable PossibleInvalidOperationException
            return GetDateTimeValue( data, name, false, handler ).Value;
// ReSharper restore PossibleInvalidOperationException
        }

        /// <summary>
        /// 获取值并校验
        /// </summary>
        /// <param name="data">字符串数据</param>
        /// <param name="name">对应字段名称</param>
        /// <param name="allowEmpty">true：允许为空，false：不允许为空</param>
        /// <param name="messages">校验信息，不为空表示有校验错误</param>
        /// <returns>值</returns>
        public static DateTime? GetDateTimeValue( string data, string name, bool allowEmpty, IList<string> messages )
        {
            return GetDateTimeValue( data, name, allowEmpty, messages.Add );
        }

        /// <summary>
        /// 获取值并校验
        /// </summary>
        /// <param name="data">字符串数据</param>
        /// <param name="name">对应字段名称</param>
        /// <param name="messages">校验信息，不为空表示有校验错误</param>
        /// <returns>值</returns>
        public static DateTime GetDateTimeValue( string data, string name, IList<string> messages )
        {
            return GetDateTimeValue( data, name, messages.Add );
        }

        #endregion

        #region GetInt32Value

        /// <summary>
        /// 必须为整型
        /// </summary>
        private static readonly LocaleString MustIntegerError =
            new LocaleString( InputValidationMsgKey.MustIntegerError, "字段值：{0}数据类型必须为整型!" );

        /// <summary>
        /// 必须大于
        /// </summary>
        private static readonly LocaleString MustGreatError =
            new LocaleString( InputValidationMsgKey.MustGreatError, "字段值：{0}必须大于{1}!" );

        /// <summary>
        /// 必须小于
        /// </summary>
        private static readonly LocaleString MustLessError =
            new LocaleString( InputValidationMsgKey.MustLessError, "字段值：{0}必须小于{1}!" );

        /// <summary>
        /// 获取值并校验
        /// </summary>
        /// <param name="data">字符串数据</param>
        /// <param name="name">对应字段名称</param>
        /// <param name="allowEmpty">true：允许为空，false：不允许为空</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="handler">有校验错误时执行的函数</param>
        /// <returns>值</returns>
        public static Int32? GetInt32Value( string data, string name, bool allowEmpty, Int32 min, Int32 max,
                                            Action<string> handler )
        {
            data = StringUtils.Trim( data ) ?? string.Empty;

            string error = string.Format( MustIntegerError, name );
            if( string.IsNullOrEmpty( data ) )
            {
                if( !allowEmpty )
                {
                    handler( error );
                    return Int32.MinValue;
                }

                return null;
            }

            Int32 ret;
            if( !Int32.TryParse( data, out ret ) )
            {
                handler( error );
                return Int32.MinValue;
            }

            if( ret < min )
            {
                handler( string.Format( MustGreatError, name, min ) );
            }

            if( max < ret )
            {
                handler( string.Format( MustLessError, name, max ) );
            }

            return ret;
        }

        /// <summary>
        /// 获取值并校验
        /// </summary>
        /// <param name="data">字符串数据</param>
        /// <param name="name">对应字段名称</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="handler">有校验错误时执行的函数</param>
        /// <returns>值</returns>
        public static Int32 GetInt32Value( string data, string name, Int32 min, Int32 max, Action<string> handler )
        {
// ReSharper disable PossibleInvalidOperationException
            return GetInt32Value( data, name, false, min, max, handler ).Value;
// ReSharper restore PossibleInvalidOperationException
        }

        /// <summary>
        /// 获取值并校验
        /// </summary>
        /// <param name="data">字符串数据</param>
        /// <param name="name">对应字段名称</param>
        /// <param name="allowEmpty">true：允许为空，false：不允许为空</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="messages">校验信息，不为空表示有校验错误</param>
        /// <returns>值</returns>
        public static Int32? GetInt32Value( string data, string name, bool allowEmpty, Int32 min, Int32 max,
                                            IList<string> messages )
        {
            return GetInt32Value( data, name, allowEmpty, min, max, messages.Add );
        }

        /// <summary>
        /// 获取值并校验
        /// </summary>
        /// <param name="data">字符串数据</param>
        /// <param name="name">对应字段名称</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="messages">校验信息，不为空表示有校验错误</param>
        /// <returns>值</returns>
        public static Int32 GetInt32Value( string data, string name, Int32 min, Int32 max, IList<string> messages )
        {
            return GetInt32Value( data, name, min, max, messages.Add );
        }

        #endregion

        #region GetInt64Value

        /// <summary>
        /// 获取值并校验
        /// </summary>
        /// <param name="data">字符串数据</param>
        /// <param name="name">对应字段名称</param>
        /// <param name="allowEmpty">true：允许为空，false：不允许为空</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="handler">有校验错误时执行的函数</param>
        /// <returns>值</returns>
        public static Int64? GetInt64Value( string data, string name, bool allowEmpty, Int64 min, Int64 max,
                                            Action<string> handler )
        {
            data = StringUtils.Trim( data ) ?? string.Empty;

            string error = string.Format( MustIntegerError, name );
            if( string.IsNullOrEmpty( data ) )
            {
                if( !allowEmpty )
                {
                    handler( error );
                    return Int64.MinValue;
                }

                return null;
            }

            Int64 ret;
            if( !Int64.TryParse( data, out ret ) )
            {
                handler( error );
                return Int64.MinValue;
            }

            if( ret < min )
            {
                handler( string.Format( MustGreatError, name, min ) );
            }

            if( max < ret )
            {
                handler( string.Format( MustLessError, name, max ) );
            }

            return ret;
        }

        /// <summary>
        /// 获取值并校验
        /// </summary>
        /// <param name="data">字符串数据</param>
        /// <param name="name">对应字段名称</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="handler">有校验错误时执行的函数</param>
        /// <returns>值</returns>
        public static Int64 GetInt64Value( string data, string name, Int64 min, Int64 max, Action<string> handler )
        {
// ReSharper disable PossibleInvalidOperationException
            return GetInt64Value( data, name, false, min, max, handler ).Value;
// ReSharper restore PossibleInvalidOperationException
        }

        /// <summary>
        /// 获取值并校验
        /// </summary>
        /// <param name="data">字符串数据</param>
        /// <param name="name">对应字段名称</param>
        /// <param name="allowEmpty">true：允许为空，false：不允许为空</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="messages">校验信息，不为空表示有校验错误</param>
        /// <returns>值</returns>
        public static Int64? GetInt64Value( string data, string name, bool allowEmpty, Int64 min, Int64 max,
                                            IList<string> messages )
        {
            return GetInt64Value( data, name, allowEmpty, min, max, messages.Add );
        }

        /// <summary>
        /// 获取值并校验
        /// </summary>
        /// <param name="data">字符串数据</param>
        /// <param name="name">对应字段名称</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="messages">校验信息，不为空表示有校验错误</param>
        /// <returns>值</returns>
        public static Int64 GetInt64Value( string data, string name, Int64 min, Int64 max, IList<string> messages )
        {
            return GetInt64Value( data, name, min, max, messages.Add );
        }

        #endregion

        #region GetSingleValue

        /// <summary>
        /// 必须为浮点型
        /// </summary>
        private static readonly LocaleString MustFloatError =
            new LocaleString( InputValidationMsgKey.MustFloatError, "字段值：{0}数据类型必须为浮点型!" );

        /// <summary>
        /// 获取值并校验
        /// </summary>
        /// <param name="data">字符串数据</param>
        /// <param name="name">对应字段名称</param>
        /// <param name="allowEmpty">true：允许为空，false：不允许为空</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="handler">有校验错误时执行的函数</param>
        /// <returns>值</returns>
        public static Single? GetSingleValue( string data, string name, bool allowEmpty, Single min, Single max,
                                              Action<string> handler )
        {
            data = StringUtils.Trim( data ) ?? string.Empty;

            string error = string.Format( MustFloatError, name );
            if( string.IsNullOrEmpty( data ) )
            {
                if( !allowEmpty )
                {
                    handler( error );
                    return Single.MinValue;
                }

                return null;
            }

            Single ret;
            if( !Single.TryParse( data, out ret ) )
            {
                handler( error );
                return Single.MinValue;
            }

            if( ret < min )
            {
                handler( string.Format( MustGreatError, name, min ) );
            }

            if( max < ret )
            {
                handler( string.Format( MustLessError, name, max ) );
            }

            return ret;
        }

        /// <summary>
        /// 获取值并校验
        /// </summary>
        /// <param name="data">字符串数据</param>
        /// <param name="name">对应字段名称</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="handler">有校验错误时执行的函数</param>
        /// <returns>值</returns>
        public static Single GetSingleValue( string data, string name, Single min, Single max, Action<string> handler )
        {
// ReSharper disable PossibleInvalidOperationException
            return GetSingleValue( data, name, false, min, max, handler ).Value;
// ReSharper restore PossibleInvalidOperationException
        }

        /// <summary>
        /// 获取值并校验
        /// </summary>
        /// <param name="data">字符串数据</param>
        /// <param name="name">对应字段名称</param>
        /// <param name="allowEmpty">true：允许为空，false：不允许为空</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="messages">校验信息，不为空表示有校验错误</param>
        /// <returns>值</returns>
        public static Single? GetSingleValue( string data, string name, bool allowEmpty, Single min, Single max,
                                              IList<string> messages )
        {
            return GetSingleValue( data, name, allowEmpty, min, max, messages.Add );
        }

        /// <summary>
        /// 获取值并校验
        /// </summary>
        /// <param name="data">字符串数据</param>
        /// <param name="name">对应字段名称</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="messages">校验信息，不为空表示有校验错误</param>
        /// <returns>值</returns>
        public static Single GetSingleValue( string data, string name, Single min, Single max, IList<string> messages )
        {
            return GetSingleValue( data, name, min, max, messages.Add );
        }

        #endregion

        #region GetDoubleValue

        /// <summary>
        /// 获取值并校验
        /// </summary>
        /// <param name="data">字符串数据</param>
        /// <param name="name">对应字段名称</param>
        /// <param name="allowEmpty">true：允许为空，false：不允许为空</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="handler">有校验错误时执行的函数</param>
        /// <returns>值</returns>
        public static Double? GetDoubleValue( string data, string name, bool allowEmpty, Double min, Double max,
                                              Action<string> handler )
        {
            data = StringUtils.Trim( data ) ?? string.Empty;

            string error = string.Format( MustFloatError, name );
            if( string.IsNullOrEmpty( data ) )
            {
                if( !allowEmpty )
                {
                    handler( error );
                    return Double.MinValue;
                }

                return null;
            }

            Double ret;
            if( !Double.TryParse( data, out ret ) )
            {
                handler( error );
                return Double.MinValue;
            }

            if( ret < min )
            {
                handler( string.Format( MustGreatError, name, min ) );
            }

            if( max < ret )
            {
                handler( string.Format( MustLessError, name, max ) );
            }

            return ret;
        }

        /// <summary>
        /// 获取值并校验
        /// </summary>
        /// <param name="data">字符串数据</param>
        /// <param name="name">对应字段名称</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="handler">有校验错误时执行的函数</param>
        /// <returns>值</returns>
        public static Double GetDoubleValue( string data, string name, Double min, Double max, Action<string> handler )
        {
// ReSharper disable PossibleInvalidOperationException
            return GetDoubleValue( data, name, false, min, max, handler ).Value;
// ReSharper restore PossibleInvalidOperationException
        }

        /// <summary>
        /// 获取值并校验
        /// </summary>
        /// <param name="data">字符串数据</param>
        /// <param name="name">对应字段名称</param>
        /// <param name="allowEmpty">true：允许为空，false：不允许为空</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="messages">校验信息，不为空表示有校验错误</param>
        /// <returns>值</returns>
        public static Double? GetDoubleValue( string data, string name, bool allowEmpty, Double min, Double max,
                                              IList<string> messages )
        {
            return GetDoubleValue( data, name, allowEmpty, min, max, messages.Add );
        }

        /// <summary>
        /// 获取值并校验
        /// </summary>
        /// <param name="data">字符串数据</param>
        /// <param name="name">对应字段名称</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="messages">校验信息，不为空表示有校验错误</param>
        /// <returns>值</returns>
        public static Double GetDoubleValue( string data, string name, Double min, Double max, IList<string> messages )
        {
            return GetDoubleValue( data, name, min, max, messages.Add );
        }

        #endregion

        #endregion
    }
}