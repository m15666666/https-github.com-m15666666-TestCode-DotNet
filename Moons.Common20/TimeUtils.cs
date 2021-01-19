using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Moons.Common20
{
    /// <summary>
    /// 时间格式
    /// </summary>
    public enum TimeFormat
    {
        /// <summary>
        /// 只有日期，例如：2011-01-01。
        /// </summary>
        Date,

        /// <summary>
        /// 只有时间，例如：12:01:01。
        /// </summary>
        Time,

        /// <summary>
        /// 日期时间，例如：2011-01-01 12:01:01。
        /// </summary>
        DateTime,
    }

    /// <summary>
    /// 时间的实用工具
    /// </summary>
    public sealed class TimeUtils
    {
        #region double / DateTime 转换

        private static readonly DateTime _timeBegin = new DateTime( 1899, 12, 30 );

        /// <summary>
        /// DateTime转换为double
        /// </summary>
        /// <param name="time">DateTime</param>
        /// <returns>DateTime转换的double</returns>
        public static double TimeToDouble( DateTime time )
        {
            return ( time - _timeBegin ).TotalDays;
        }

        /// <summary>
        /// double转换为DateTime
        /// </summary>
        /// <param name="time">double</param>
        /// <returns>double转换的DateTime</returns>
        public static DateTime DoubleToTime( double time )
        {
            return _timeBegin + TimeSpan.FromDays( time );
        }

        #endregion

        #region int32 minute / DateTime 转换

        /// <summary>
        /// 将时间转换为int32类型分钟数的起点
        /// </summary>
        public static DateTime Int32MinuteBegin { get; set; } = new DateTime(2000, 1, 1);

        /// <summary>
        /// DateTime转换为int32类型分钟数
        /// </summary>
        /// <param name="time">DateTime</param>
        /// <returns>DateTime转换的int32类型分钟数</returns>
        public static int TimeToInt32Minutes( DateTime time ) => (int)( time - Int32MinuteBegin ).TotalMinutes;

        /// <summary>
        /// int32类型分钟数转换为DateTime
        /// </summary>
        /// <param name="int32Minutes">double</param>
        /// <returns>int32类型分钟数转换的DateTime</returns>
        public static DateTime Int32MinuteToTime( int int32Minutes ) => Int32MinuteBegin + TimeSpan.FromMinutes( int32Minutes );

        #endregion

        #region string / DateTime 转换

        #region 格式

        /// <summary>
        /// 默认的日期时间格式
        /// </summary>
        public const string DefaultDateTimeFormat_DateTime = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 默认的日期格式
        /// </summary>
        public const string DefaultDateTimeFormat_Date = "yyyy-MM-dd";

        /// <summary>
        /// 默认的时间格式
        /// </summary>
        public const string DefaultDateTimeFormat_Time = "HH:mm:ss";

        /// <summary>
        /// 以14个字符表示到秒的日期时间格式
        /// </summary>
        public const string DateTimeFormat_DateTime14 = "yyyyMMddHHmmss";

        #endregion

        #region 日期转为字符串

        /// <summary>
        /// 日期转为字符串的转换函数
        /// </summary>
        public static Func20<DateTime, string> DateToStringConvertHandler { private get; set; }

        /// <summary>
        /// 根据日期转为字符串
        /// </summary>
        /// <param name="dt">日期</param>
        /// <returns>转换后的日期字符串</returns>
        public static string DateToString( DateTime dt )
        {
            Func20<DateTime, string> handler = DateToStringConvertHandler;
            if( handler != null )
            {
                return EventUtils.FireEvent( handler, dt );
            }
            return dt.ToString( DefaultDateTimeFormat_Date );
        }

        #endregion

        #region 时间转为字符串

        /// <summary>
        /// 时间转为字符串的转换函数
        /// </summary>
        public static Func20<DateTime, string> TimeToStringConvertHandler { private get; set; }

        /// <summary>
        /// 根据时间转为字符串
        /// </summary>
        /// <param name="dt">时间</param>
        /// <returns>转换后的时间字符串</returns>
        public static string TimeToString( DateTime dt )
        {
            Func20<DateTime, string> handler = TimeToStringConvertHandler;
            if( handler != null )
            {
                return EventUtils.FireEvent( handler, dt );
            }
            return dt.ToString( DefaultDateTimeFormat_Time );
        }

        #endregion

        #region 日期时间转为字符串

        /// <summary>
        /// 日期时间转为字符串的转换函数
        /// </summary>
        public static Func20<DateTime, string> DateTimeToStringConvertHandler { private get; set; }

        /// <summary>
        /// 根据日期时间转为字符串
        /// </summary>
        /// <param name="dt">日期时间</param>
        /// <returns>转换后的日期时间字符串</returns>
        public static string DateTimeToString( DateTime dt )
        {
            Func20<DateTime, string> handler = DateTimeToStringConvertHandler;
            if( handler != null )
            {
                return EventUtils.FireEvent( handler, dt );
            }
            return dt.ToString( DefaultDateTimeFormat_DateTime );
        }

        #endregion

        #region 字符串转为DateTime

        /// <summary>
        /// 根据格式解析时间字符串
        /// </summary>
        /// <param name="s">时间字符串</param>
        /// <param name="format">格式字符串</param>
        /// <returns>时间</returns>
        public static DateTime ParseStringToDateTime( string s, string format )
        {
            return DateTime.ParseExact( s, format, null );
        }

        /// <summary>
        /// 根据字符串转为日期
        /// </summary>
        /// <param name="s">日期字符串，格式为："yyyy-MM-dd"</param>
        /// <returns>转换后的日期，时间部分为"00:00:00"</returns>
        public static DateTime StringToDate( string s )
        {
            return ParseStringToDateTime( s, DefaultDateTimeFormat_Date );
        }

        /// <summary>
        /// 根据字符串转为日期
        /// </summary>
        /// <param name="s">日期字符串，格式为："yyyy-MM-dd"</param>
        /// <returns>转换后的日期，时间部分为当前时间</returns>
        public static DateTime StringToDate_NowTime( string s )
        {
            int year = Convert.ToInt32( s.Substring( 0, 4 ) );
            int month = Convert.ToInt32( s.Substring( 5, 2 ) );
            int day = Convert.ToInt32( s.Substring( 8, 2 ) );
            return new DateTime( year, month, day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second );
        }

        /// <summary>
        /// 根据字符串转为时间
        /// </summary>
        /// <param name="s">时间字符串，格式为："HH:mm:ss"</param>
        /// <returns>转换后的时间，日期部分为当前日期</returns>
        public static DateTime StringToTime_NowDate( string s )
        {
            int hour = Convert.ToInt32( s.Substring( 0, 2 ) );
            int minute = Convert.ToInt32( s.Substring( 3, 2 ) );
            int second = Convert.ToInt32( s.Substring( 6, 2 ) );
            return new DateTime( DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, second );
        }

        /// <summary>
        /// 根据字符串转为日期时间
        /// </summary>
        /// <param name="s">日期时间字符串</param>
        /// <returns>转换后的日期时间</returns>
        public static DateTime StringToDateTime( string s )
        {
            return ParseStringToDateTime( s, DefaultDateTimeFormat_DateTime );
        }

        #endregion

        #endregion

        #region 以8个字节表示到秒的日期时间格式

        /// <summary>
        /// DayOfWeek对数值的映射，星期一到星期日分别表示为：1~7。
        /// </summary>
        private static readonly Dictionary<DayOfWeek, byte> _dayOfWeek2Number =
            new Dictionary<DayOfWeek, byte>
                {
                    { DayOfWeek.Monday, 1 },
                    { DayOfWeek.Tuesday, 2 },
                    { DayOfWeek.Wednesday, 3 },
                    { DayOfWeek.Thursday, 4 },
                    { DayOfWeek.Friday, 5 },
                    { DayOfWeek.Saturday, 6 },
                    { DayOfWeek.Sunday, 7 },
                };

        /// <summary>
        /// 将DayOfWeek类型转化为数值，星期一到星期日分别表示为：1~7。
        /// </summary>
        /// <param name="dayOfWeek">DayOfWeek</param>
        /// <returns>数值</returns>
        public static byte DayOfWeek2Byte( DayOfWeek dayOfWeek )
        {
            return _dayOfWeek2Number[dayOfWeek];
        }

        /// <summary>
        /// 转化为以8个字节表示到秒的日期时间格式的字节数组
        /// </summary>
        /// <param name="dateTime">DateTime</param>
        /// <returns>字节数组</returns>
        public static byte[] ToDateTime8Bytes( DateTime dateTime )
        {
            const int TimeByteCount = 8;
            var ret = new byte[TimeByteCount];
            using( var stream = new MemoryStream( ret, true ) )
            {
                using( var writer = new BinaryWriter( stream ) )
                {
                    ToDateTime8Bytes( writer, dateTime );
                }
            }
            return ret;
        }

        /// <summary>
        /// 转化为以8个字节表示到秒的日期时间格式的字节数组
        /// </summary>
        /// <param name="writer">BinaryWriter</param>
        /// <param name="dateTime">DateTime</param>
        public static void ToDateTime8Bytes( BinaryWriter writer, DateTime dateTime )
        {
            writer.Write( (short)dateTime.Year );
            writer.Write( (byte)dateTime.Month );
            writer.Write( (byte)dateTime.Day );
            writer.Write( DayOfWeek2Byte( dateTime.DayOfWeek ) );
            writer.Write( (byte)dateTime.Hour );
            writer.Write( (byte)dateTime.Minute );
            writer.Write( (byte)dateTime.Second );
        }

        /// <summary>
        /// 将字节数组转化为以8个字节表示到秒的日期时间格式的DateTime
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>DateTime</returns>
        public static DateTime FromDateTime8Bytes( byte[] bytes )
        {
            using( var stream = new MemoryStream( bytes ) )
            {
                using( var reader = new BinaryReader( stream ) )
                {
                    return FromDateTime8Bytes( reader );
                }
            }
        }

        /// <summary>
        /// 将字节数组转化为以8个字节表示到秒的日期时间格式的DateTime
        /// </summary>
        /// <param name="reader">BinaryReader</param>
        /// <returns>DateTime</returns>
        public static DateTime FromDateTime8Bytes( BinaryReader reader )
        {
            int year = reader.ReadInt16();
            int month = reader.ReadByte();
            int day = reader.ReadByte();
// ReSharper disable UnusedVariable
            int dayOfWeek = reader.ReadByte();
// ReSharper restore UnusedVariable
            int hour = reader.ReadByte();
            int minute = reader.ReadByte();
            int second = reader.ReadByte();

            return new DateTime( year, month, day, hour, minute, second );
        }

        #endregion

        #region 以14个字符表示到秒的日期时间格式

        /// <summary>
        /// 转化为以14个字符表示到秒的日期时间格式的字节数组
        /// </summary>
        /// <param name="dateTime">DateTime</param>
        /// <returns>字节数组</returns>
        public static byte[] ToDateTime14Bytes( DateTime dateTime )
        {
            return Encoding.ASCII.GetBytes( dateTime.ToString( DateTimeFormat_DateTime14 ) );
        }

        /// <summary>
        /// 将字节数组转化为以14个字符表示到秒的日期时间格式的DateTime
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>DateTime</returns>
        public static DateTime FromDateTime14Bytes( byte[] bytes )
        {
            if( bytes == null || bytes.Length != DateTimeFormat_DateTime14.Length )
            {
                throw new ArgumentException( "字节数组长度不正确({0})！", StringUtils.ToHex( bytes ) );
            }

            return ParseStringToDateTime( StringUtils.ASCIIBytes2String( bytes ), DateTimeFormat_DateTime14 );
        }

        /// <summary>
        /// 将字节数组转化为以14个字符表示到秒的日期时间格式的DateTime
        /// </summary>
        /// <param name="reader">BinaryReader</param>
        /// <returns>DateTime</returns>
        public static DateTime FromDateTime14Bytes( BinaryReader reader )
        {
            return FromDateTime14Bytes( reader.ReadBytes( DateTimeFormat_DateTime14.Length ) );
        }

        #endregion

        #region 实例成员

        private TimeUtils()
        {
        }

        #region 变量和属性

        /// <summary>
        /// 一周的天数，7天
        /// </summary>
        public const int DayCountOfAWeek = 7;

        /// <summary>
        /// 内部时间
        /// </summary>
        private DateTime _time;

        #endregion

        /// <summary>
        /// 当天的时间
        /// </summary>
        public DateTime Today
        {
            get { return _time.Date; }
        }

        /// <summary>
        /// 那个礼拜的第一天
        /// </summary>
        public DateTime FirstDayOfThisWeek
        {
            get
            {
                int dayOfWeek = -(int)_time.DayOfWeek;
                if( dayOfWeek == 0 )
                {
                    return Today.AddDays( -6 );
                }

                return Today.AddDays( -(int)_time.DayOfWeek + 1 );
            }
        }

        /// <summary>
        /// 这个礼拜的最后一天
        /// </summary>
        public DateTime LastDayOfThisWeek
        {
            get { return FirstDayOfThisWeek.AddDays( 6 ); }
        }

        /// <summary>
        /// 这个月的第一天
        /// </summary>
        public DateTime FirstDayOfThisMonth
        {
            get { return new DateTime( _time.Year, _time.Month, 1 ); }
        }

        /// <summary>
        /// 这个月的最后一天
        /// </summary>
        public DateTime LastDayOfThisMonth
        {
            get { return FirstDayOfThisMonth.AddMonths( 1 ).AddDays( -1 ); }
        }

        /// <summary>
        /// 上个月的第一天
        /// </summary>
        public DateTime FirstDayOfLastMonth
        {
            get { return FirstDayOfThisMonth.AddMonths( -1 ); }
        }

        /// <summary>
        /// 上个月的最后一天
        /// </summary>
        public DateTime LastDayOfLastMonth
        {
            get { return FirstDayOfLastMonth.AddMonths( 1 ).AddDays( -1 ); }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dt"></param>
        public static TimeUtils Create( DateTime dt )
        {
            return new TimeUtils { _time = dt };
        }

        #endregion

        #region 与星期相关

        /// <summary>
        /// 获取星期几
        /// </summary>
        /// <param name="time">DateTime</param>
        /// <returns>星期几</returns>
        public static string GetDayOfWeekTitle( DateTime time )
        {
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            switch( calendar.GetDayOfWeek( time ) )
            {
                case DayOfWeek.Sunday:
                    return "星期日";

                case DayOfWeek.Monday:
                    return "星期一";

                case DayOfWeek.Tuesday:
                    return "星期二";

                case DayOfWeek.Wednesday:
                    return "星期三";

                case DayOfWeek.Thursday:
                    return "星期四";

                case DayOfWeek.Friday:
                    return "星期五";
            }
            return "星期六";
        }

        /// <summary>
        /// 获取一年中的第几个星期
        /// </summary>
        /// <param name="time">DateTime</param>
        /// <param name="startOfWeek">一个星期的起点</param>
        /// <returns>一年中的第几个星期</returns>
        public static int GetWeekNumberOfYear( DateTime time, DayOfWeek startOfWeek )
        {
            int dayOfYear = time.DayOfYear;

            // 1月1号
            var firstDay = new DateTime( time.Year, 1, 1 );

            // 一个星期的起点与1月1号之前相差的天数
            int differenceOfStartOfWeekVsFirstDay = GetDayOfWeekDifference( firstDay.DayOfWeek, startOfWeek );

            // 由于1月1日与一个星期的起点不重合导致需要增加的星期个数
            int addWeekCountByStartOfWeek = 0;

            // 第一个“一个星期的起点”
            int firstDayOfStartOfWeek = firstDay.DayOfYear;
            if( 0 < differenceOfStartOfWeekVsFirstDay )
            {
                firstDayOfStartOfWeek += differenceOfStartOfWeekVsFirstDay;
                addWeekCountByStartOfWeek = 1;
            }

            if( dayOfYear < firstDayOfStartOfWeek )
            {
                return 1;
            }

            // 当前到第一个“一个星期的起点”的总天数，包括起始和截至点。
            int dayCount = dayOfYear - firstDayOfStartOfWeek + 1;
            return addWeekCountByStartOfWeek + ( dayCount / 7 ) + ( 0 < dayCount % 7 ? 1 : 0 );
        }

        /// <summary>
        /// 基于当前的星期几加上偏移天数(正、负、零都可以)，计算结果是星期几
        /// </summary>
        /// <param name="origin">当前的星期几</param>
        /// <param name="count">偏移天数</param>
        /// <returns>结果是星期几</returns>
        public static DayOfWeek AddDayOfWeek( DayOfWeek origin, int count )
        {
            int ret = (int)origin + ( count % DayCountOfAWeek );
            if( (int)DayOfWeek.Saturday < ret )
            {
                ret -= DayCountOfAWeek;
            }
            else if( ret < (int)DayOfWeek.Sunday )
            {
                ret += DayCountOfAWeek;
            }
            return (DayOfWeek)ret;
        }

        /// <summary>
        /// 计算两个DayOfWeek的差值，即：(int)(end - start)，结果一定大于零
        /// </summary>
        /// <param name="start">起始</param>
        /// <param name="end">终止</param>
        /// <returns>两个DayOfWeek的差值</returns>
        public static int GetDayOfWeekDifference( DayOfWeek start, DayOfWeek end )
        {
            int ret = end - start;
            return ret < 0 ? ret + DayCountOfAWeek : ret;
        }

        #endregion
    }
}