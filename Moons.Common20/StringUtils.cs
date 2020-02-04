using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Moons.Common20
{
    /// <summary>
    /// 字符串的实用工具类
    /// </summary>
    public static class StringUtils
    {
        #region HtmlDocumentTextToPlainText 替换无意义内容

        /// <summary>
        ///     替换转义符
        ///     参考内容：http://www.utexas.edu/learn/html/spchar.html
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string HtmlDocumentTextToPlainText( string text )
        {
            if( string.IsNullOrEmpty( text ) )
            {
                return text;
            }

            // 移除HTML标签
            text = text.Replace( "<br />", "\r\n" );
            text = text.Replace( "<br/>", string.Empty );
            text = text.Replace( "<br>", string.Empty );

            //替换标点符号

            text = text.Replace( "&quot;", "\"" );
            text = text.Replace( "&nbsp", "/r/n" );

            //特殊编码字符/符号转换
            text = text.Replace( "&amp;", "&" );
            text = text.Replace( "&gt;", ">" );
            text = text.Replace( "&lt;", "<" );
            text = text.Replace( "&gt;", "" );
            text = text.Replace( "&gt;", "" );

            return text;
        }

        #endregion

        #region 变量和属性

        #region 表示bool值的字符串

        /// <summary>
        /// 表示true的字符串
        /// </summary>
        public static readonly string True = Boolean.TrueString;

        /// <summary>
        /// 表示false的字符串
        /// </summary>
        public static readonly string False = Boolean.FalseString;

        /// <summary>
        /// 表示true的小写字符串
        /// </summary>
        public static readonly string TrueLowerCase = "true";

        /// <summary>
        /// 表示false的小写字符串
        /// </summary>
        public static readonly string FalseLowerCase = "false";

        /// <summary>
        /// 表示true的大写字符串
        /// </summary>
        public static readonly string TrueUpperCase = TrueLowerCase.ToUpper();

        /// <summary>
        /// 表示false的大写字符串
        /// </summary>
        public static readonly string FalseUpperCase = FalseLowerCase.ToUpper();

        #region Javascript中bool的字符串表示

        /// <summary>
        /// 表示Javascript中true的字符串
        /// </summary>
        public static readonly string True_Javascript = TrueLowerCase;

        /// <summary>
        /// 表示Javascript中false的字符串
        /// </summary>
        public static readonly string False_Javascript = FalseLowerCase;

        /// <summary>
        /// Javascript中bool的字符串表示
        /// </summary>
        private static readonly Dictionary<bool, string> _jsBool2String = new Dictionary<bool, string>
                                                                              {
                                                                                  { true, True_Javascript },
                                                                                  { false, False_Javascript }
                                                                              };

        /// <summary>
        /// Javascript中bool的字符串表示
        /// </summary>
        public static IDictionary<bool, string> JsBool2String
        {
            get { return _jsBool2String; }
        }

        #endregion

        #endregion

        /// <summary>
        /// 空对象引用
        /// </summary>
        public static string NullReference = "null";

        /// <summary>
        /// 空字符，用于字符串结尾
        /// </summary>
        public static readonly char NullChar = '\0';

        /// <summary>
        /// 空字符对应的字节
        /// </summary>
        public static readonly byte NullCharByte = 0;

        /// <summary>
        /// 空字符字符串，用于字符串结尾
        /// </summary>
        public static readonly string Null = NullChar.ToString();

        /// <summary>
        /// 小数点字符
        /// </summary>
        public static readonly char PointChar = '.';

        /// <summary>
        /// 小数点字符串
        /// </summary>
        public static readonly string Point = PointChar.ToString();

        /// <summary>
        /// 下划线字符
        /// </summary>
        public static readonly char UnderScoreChar = '_';

        /// <summary>
        /// 下划线字符串
        /// </summary>
        public static readonly string UnderScore = UnderScoreChar.ToString();

        /// <summary>
        /// 半角逗号字符
        /// </summary>
        public static readonly char CommaChar = ',';

        /// <summary>
        /// 半角逗号字符串
        /// </summary>
        public static readonly string Comma = CommaChar.ToString();

        /// <summary>
        /// 半角冒号字符
        /// </summary>
        public static readonly char ColonChar = ':';

        /// <summary>
        /// 半角冒号字符串
        /// </summary>
        public static readonly string Colon = ColonChar.ToString();

        /// <summary>
        /// 半角分号字符
        /// </summary>
        public static readonly char SemicolonChar = ';';

        /// <summary>
        /// 半角分号字符串
        /// </summary>
        public static readonly string Semicolon = SemicolonChar.ToString();

        /// <summary>
        /// 半角斜线字符
        /// </summary>
        public static readonly char SlashChar = '/';

        /// <summary>
        /// 半角斜线字符串
        /// </summary>
        public static readonly string Slash = SlashChar.ToString();

        /// <summary>
        /// 半角反斜线字符
        /// </summary>
        public static readonly char BackSlashChar = '\\';

        /// <summary>
        /// 半角反斜线字符串
        /// </summary>
        public static readonly string BackSlash = BackSlashChar.ToString();

        /// <summary>
        /// 空格字符串
        /// </summary>
        public static readonly char WhiteSpaceChar = ' ';

        /// <summary>
        /// 空格字符串
        /// </summary>
        public static readonly string WhiteSpace = WhiteSpaceChar.ToString();

        /// <summary>
        /// 左小括号
        /// </summary>
        public static readonly string LeftParenthese = "(";

        /// <summary>
        /// 右小括号
        /// </summary>
        public static readonly string RightParenthese = ")";

        /// <summary>
        /// 竖线
        /// </summary>
        public static readonly string VerticalVirgule = "|";

        /// <summary>
        /// 四个竖线
        /// </summary>
        public static readonly string FourFoldVerticalVirgule = "||||";

        #region 与Web相关的字符

        #region 绝对、相对路径相关的字符前缀

        /// <summary>
        /// 相对路径前缀字符
        /// </summary>
        public static readonly char RelativePathPrefixChar = '~';

        /// <summary>
        /// 相对路径前缀字符
        /// </summary>
        public static readonly string RelativePathPrefix = RelativePathPrefixChar.ToString();

        /// <summary>
        /// 相对路径前缀字符加上斜线："~/"
        /// </summary>
        public static readonly string RelativePathPrefixSlash = RelativePathPrefix + Slash;

        /// <summary>
        /// 网址斜线标记："://"
        /// </summary>
        public static readonly string WebAdressSlash = Colon + Slash + Slash;

        #endregion

        #endregion

        #endregion

        #region 基本操作

        /// <summary>
        /// 两个字符串是否相当，忽略大小写
        /// </summary>
        /// <param name="a">第一个字符串</param>
        /// <param name="b">第二个字符串</param>
        /// <returns>true：相等，false：不相等</returns>
        public static bool EqualIgnoreCase(string a, string b)
        {
            return string.Compare(a, b, StringComparison.OrdinalIgnoreCase) == 0;
        }

        /// <summary>
        /// 判断对应的字符串是否为空（去掉前后包含的空格）
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>是否为空</returns>
        public static bool IsNullOrEmpty(object obj)
        {
            return string.IsNullOrEmpty(Trim(obj));
        }

        /// <summary>
        /// 将对象转换为字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>字符串</returns>
        public static string ToString(object obj)
        {
            return obj != null ? obj.ToString() : null;
        }

        /// <summary>
        /// 将对象转换为带换行的字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>字符串</returns>
        public static string ToStringRelace(object obj)
        {
            return obj != null ? obj.ToString().Replace("\r\n", "<br/>") : null;
        }

        /// <summary>
        /// 获得子字符串
        /// </summary>
        /// <param name="text">字符串</param>
        /// <param name="startIndex">开始下标</param>
        /// <param name="length">长度</param>
        /// <returns>子字符串</returns>
        public static string Substring(string text, int startIndex, int length)
        {
            if (text.Length <= startIndex)
            {
                return null;
            }

            if (text.Length < startIndex + length)
            {
                return text.Substring(startIndex);
            }

            return text.Substring(startIndex, length);
        }

        /// <summary>
        /// 将字符转换为字符串
        /// </summary>
        /// <param name="chars">字符数组</param>
        /// <returns>字符串数组</returns>
        public static string[] Char2String(params char[] chars)
        {
            if (chars == null || chars.Length == 0)
            {
                return null;
            }

            var ret = new string[chars.Length];
            int index = 0;
            Array.ForEach(chars, c => ret[index++] = c.ToString());

            return ret;
        }

        /// <summary>
        /// 按字符串位数补0
        /// </summary>
        /// <param name="text">字符串</param>
        /// <param name="length">字符长度</param>
        /// <returns>新字符串</returns>
        public static string FillZero(string text, int length)
        {
            if (length <= text.Length)
            {
                return text;
            }
            var buffer = new StringBuilder();
            for (int index = 0; index < length - text.Length; index++)
            {
                buffer.Append("0");
            }

            buffer.Append(text);

            return buffer.ToString();
        }

        #region Trim、ToUpper、ToLower

        /// <summary>
        ///     转换为大写格式
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns>转换为大写格式后的字符串</returns>
        public static string ToUpper( string text )
        {
            return text != null ? text.ToUpper( CultureInfo.InvariantCulture ) : null;
        }

        /// <summary>
        ///     转换为小写格式
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns>转换为小写格式后的字符串</returns>
        public static string ToLower( string text )
        {
            return text != null ? text.ToLower( CultureInfo.InvariantCulture ) : null;
        }

        /// <summary>
        /// 先去掉前后空格然后转换为大写格式
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>转换后的字符串</returns>
        public static string TrimToUpper(object obj)
        {
            return ToUpper(Trim(obj));
        }

        /// <summary>
        ///  先去掉前后空格然后转换为小写格式
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>转换后的字符串</returns>
        public static string TrimToLower(object obj)
        {
            return ToLower(Trim(obj));
        }

        /// <summary>
        /// 先去掉前后空格然后转换为大写格式
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns>转换后的字符串</returns>
        public static string TrimToUpper(string text)
        {
            return ToUpper(Trim(text));
        }

        /// <summary>
        ///  先去掉前后空格然后转换为小写格式
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns>转换后的字符串</returns>
        public static string TrimToLower(string text)
        {
            return ToLower(Trim(text));
        }

        /// <summary>
        /// 去掉对象转换为字符串后前后的空格
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>去掉空格后的字符串</returns>
        public static string Trim(object obj)
        {
            return obj != null ? Trim(obj.ToString()) : null;
        }

        /// <summary>
        /// 去掉前后的空格
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns>去掉空格后的字符串</returns>
        public static string Trim(string text)
        {
            return text != null ? text.Trim() : null;
        }

        #endregion

        #endregion

        #region 填充空格
#if !SILVERLIGHT
        /// <summary>
        /// 获得字节数的差别：bytes(textA) - bytes(textB)
        /// </summary>
        /// <param name="textA">字符串A</param>
        /// <param name="textB">字符串B</param>
        /// <returns>字节数的差别：bytes(textA) - bytes(textB)</returns>
        public static int GetByteCountDiff(string textA, string textB)
        {
            return Encoding.Default.GetByteCount(textA) - Encoding.Default.GetByteCount(textB);
        }
#endif

        /// <summary>
        /// 获得空格字符串
        /// </summary>
        /// <param name="count">空格个数</param>
        /// <returns>空格字符串</returns>
        public static string GetWhiteSpaces(int count)
        {
            return new string(WhiteSpaceChar, count);
        }

        #endregion

        #region 布尔操作相关

        /// <summary>
        /// 将bool转为字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>字符串</returns>
        public static string BooleanToString(bool value)
        {
            return value ? True : False;
        }

        /// <summary>
        /// 转化为bool值。
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>bool值</returns>
        public static bool ToBoolean(string value)
        {
            return EqualIgnoreCase(Trim(value), True);
        }

        #endregion

        #region 分隔字符串

        /// <summary>
        /// 使用半角逗号，将字符串分割为string[]
        /// </summary>
        /// <param name="list">字符串</param>
        /// <returns>string[]</returns>
        public static string[] SplitByComma(string list)
        {
            return Split(list, CommaChar);
        }

        /// <summary>
        /// 将字符串分割为string[]
        /// </summary>
        /// <param name="list">字符串</param>
        /// <param name="delimiter">分隔符</param>
        /// <returns>string[]</returns>
        public static string[] Split(string list, string delimiter)
        {
            list = Trim(list);
            if (String.IsNullOrEmpty(list))
            {
                return new string[0];
            }

            return RemoveEmptyEntries(list.Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries));
        }

        /// <summary>
        /// 将字符串分割为string[]
        /// </summary>
        /// <param name="list">字符串</param>
        /// <param name="delimiters">分隔符</param>
        /// <returns>string[]</returns>
        public static string[] Split(string list, params char[] delimiters)
        {
            list = Trim(list);
            if (String.IsNullOrEmpty(list))
            {
                return new string[0];
            }

            return RemoveEmptyEntries(list.Split(delimiters, StringSplitOptions.RemoveEmptyEntries));
        }

        /// <summary>
        /// 移除空的字符串
        /// </summary>
        /// <param name="texts">字符串集合</param>
        /// <returns>移除空的字符串后的集合</returns>
        private static string[] RemoveEmptyEntries(string[] texts)
        {
            if (texts == null)
            {
                return new string[0];
            }

            var ret = new List<string>();
            Array.ForEach(texts, text =>
                                      {
                                          text = Trim(text);
                                          if (!string.IsNullOrEmpty(text))
                                          {
                                              ret.Add(text);
                                          }
                                      });

            return ret.ToArray();
        }

        #endregion

        #region 将字符串分割为int数组

        /// <summary>
        /// 使用半角逗号，将字符串分割为int数组
        /// </summary>
        /// <param name="list">字符串</param>
        /// <returns>int数组</returns>
        public static int[] ToIntsByComma(string list)
        {
            return ToInts(SplitByComma(list));
        }

        /// <summary>
        /// 将字符串分割为int数组
        /// </summary>
        /// <param name="list">字符串</param>
        /// <param name="delimiter">分隔符</param>
        /// <returns>int数组</returns>
        public static int[] ToInts(string list, string delimiter)
        {
            return ToInts(Split(list, delimiter));
        }

        /// <summary>
        /// 将字符串分割为int数组
        /// </summary>
        /// <param name="list">字符串</param>
        /// <param name="delimiters">分隔符</param>
        /// <returns>int数组</returns>
        public static int[] ToInts(string list, params char[] delimiters)
        {
            return ToInts(Split(list, delimiters));
        }

        /// <summary>
        /// 将字符串数组转换为int数组
        /// </summary>
        /// <param name="texts">字符串数组</param>
        /// <returns>int数组</returns>
        private static int[] ToInts(string[] texts)
        {
            if (texts == null || texts.Length == 0)
            {
                return new int[0];
            }

            int index = 0;
            var ret = new int[texts.Length];
            Array.ForEach(texts, text => ret[index++] = Int32.Parse(text));

            return ret;
        }

        #endregion

        #region 过滤字符串

        /// <summary>
        /// 过滤字符串
        /// </summary>
        /// <param name="text">字符串</param>
        /// <param name="newValue">替换的字符串</param>
        /// <param name="chars">需要过滤掉的字符</param>
        /// <returns>过滤后的字符串</returns>
        public static string Filter(string text, string newValue, params char[] chars)
        {
            return Filter(text, newValue, Char2String(chars));
        }

        /// <summary>
        /// 过滤字符串
        /// </summary>
        /// <param name="text">字符串</param>
        /// <param name="newValue">替换的字符串</param>
        /// <param name="oldValues">需要过滤掉的字符串</param>
        /// <returns>过滤后的字符串</returns>
        public static string Filter(string text, string newValue, params string[] oldValues)
        {
            if (String.IsNullOrEmpty(text) || oldValues == null || oldValues.Length == 0)
            {
                return text;
            }

            StringBuilder buffer = null;
            foreach (string oldValue in oldValues)
            {
                if (String.IsNullOrEmpty(oldValue))
                {
                    continue;
                }

                if (text.Contains(oldValue))
                {
                    if (buffer == null)
                    {
                        buffer = new StringBuilder(text);
                    }

                    buffer.Replace(oldValue, newValue);
                }
            }

            return buffer != null ? buffer.ToString() : text;
        }

        #endregion

        #region 连接字符串(string.Join)

        /// <summary>
        ///     连接字符串
        /// </summary>
        /// <param name="values">字符串集合</param>
        /// <returns>连接后的字符串</returns>
        public static string Join( IEnumerable values )
        {
            return Join( values, Comma );
        }

        /// <summary>
        ///     连接字符串
        /// </summary>
        /// <param name="values">字符串集合</param>
        /// <param name="separator">分隔符</param>
        /// <returns>连接后的字符串</returns>
        public static string Join( IEnumerable values, string separator )
        {
            var list = new List<string>();
            ForUtils.ForEach( values, item => list.Add( item.ToString() ) );
            return Join( list.ToArray(), separator );
        }

        /// <summary>
        ///     连接字符串
        /// </summary>
        /// <param name="values">字符串集合</param>
        /// <returns>连接后的字符串</returns>
        public static string Join( string[] values )
        {
            return Join( values, Comma );
        }

        /// <summary>
        ///     连接字符串
        /// </summary>
        /// <param name="values">字符串集合</param>
        /// <param name="separator">分隔符</param>
        /// <returns>连接后的字符串</returns>
        public static string Join( string[] values, string separator )
        {
            return string.Join( separator, values );
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="values">值集合</param>
        /// <param name="converter">将值转换为字符串的代理</param>
        /// <returns>连接后的字符串</returns>
        public static string Join<T>(T[] values, Converter<T, string> converter)
        {
            return Join(values, converter, Comma);
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="values">值集合</param>
        /// <param name="converter">将值转换为字符串的代理</param>
        /// <param name="separator">分隔符</param>
        /// <returns>连接后的字符串</returns>
        public static string Join<T>(T[] values, Converter<T, string> converter, string separator)
        {
            if (values == null || values.Length == 0)
            {
                return null;
            }

            var strings = new string[values.Length];
            int index = 0;
            Array.ForEach(values, value => strings[index++] = converter(value));

            return string.Join(separator, strings);
        }

        #endregion

        #region 与文本文件相关

        /// <summary>
        /// 获得文件中的所有不为空的行，文件为UTF-8编码
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>所有不为空的行</returns>
        public static List<string> GetLinesFromFile_UTF8(string path)
        {
            using (StreamReader reader = File.OpenText(path))
            {
                var ret = new List<string>();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    line = line != null ? line.Trim() : null;
                    if (String.IsNullOrEmpty(line))
                    {
                        continue;
                    }

                    ret.Add(line);
                }
                return ret;
            }
        }

        #endregion

        #region 转换为16进制字符串

        ///// <summary>
        ///// 将字节转换为字符串的代理
        ///// </summary>
        //private static readonly Converter<byte, string> _byte2HexStringConverter = value => value.ToString( "X2" );

        /// <summary>
        /// 转换为16进制字符串
        /// </summary>
        /// <param name="values">值集合</param>
        /// <returns>16进制字符串</returns>
        public static string ToHex(params byte[] values)
        {
            return ToHex(values, 0, values.Length);
        }

        /// <summary>
        /// 转换为16进制字符串
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="size">字节数</param>
        /// <returns>16进制字符串</returns>
        public static string ToHex(byte[] buffer, int offset, int size)
        {
            return BitConverter.ToString(buffer, offset, size);
        }

        /// <summary>
        /// 将十六进制字符串转化为字节数组
        /// </summary>
        /// <param name="hexString">十六进制字符串，例如：“A0F1”</param>
        /// <returns>字节数组</returns>
        public static byte[] HexString2Bytes(string hexString)
        {
            return HexString2Bytes(hexString, null);
        }

        /// <summary>
        /// 将十六进制字符串转化为字节数组
        /// </summary>
        /// <param name="hexString">十六进制字符串，例如：“A0F1”</param>
        /// <param name="separator">分隔符，例如：“-”，null，string.Empty</param>
        /// <returns>字节数组</returns>
        public static byte[] HexString2Bytes(string hexString, string separator)
        {
            if (hexString != null && !string.IsNullOrEmpty(separator))
            {
                hexString = hexString.Replace(separator, string.Empty);
            }

            if (string.IsNullOrEmpty(hexString))
            {
                throw new ArgumentNullException("hexString");
            }

            if ((hexString.Length % 2) != 0)
            {
                throw new ArgumentOutOfRangeException("hexString:" + hexString);
            }

            var ret = new byte[hexString.Length / 2];
            for (int index = 0, retIndex = 0; index < hexString.Length; index += 2, retIndex++)
            {
                ret[retIndex] = byte.Parse(hexString.Substring(index, 2), NumberStyles.HexNumber);
            }
            return ret;
        }

        #endregion

        #region 将表示字符的字节数组转换为字符串

        /// <summary>
        /// GB2312编码的Encoding
        /// </summary>
        public static Encoding GB2312
        {
            get { return Encoding.GetEncoding("GB2312"); }
        }

        /// <summary>
        /// 获得GB2312表示字符的字节数组
        /// </summary>
        /// <param name="text">字符串</param>
        /// <param name="byteCount">字节数组的长度</param>
        /// <returns>字节数组</returns>
        public static byte[] GetGBFixLengthBytes(string text, int byteCount)
        {
            return GetFixLengthBytes(GB2312, text, byteCount);
        }

        /// <summary>
        /// 获得GB2312表示字符的最少的字节数组
        /// </summary>
        /// <param name="text">字符串</param>
        /// <param name="maxByteCount">字节数组的最大长度</param>
        /// <returns>字节数组</returns>
        public static byte[] GetGBMinBytes(string text, int maxByteCount)
        {
            return GetMinBytes(GB2312, text, maxByteCount);
        }

        /// <summary>
        /// 获得字节数组
        /// </summary>
        /// <param name="encoding">Encoding</param>
        /// <param name="text">字符串</param>
        /// <param name="byteCount">字节数组的长度</param>
        /// <returns>字节数组</returns>
        public static byte[] GetFixLengthBytes(Encoding encoding, string text, int byteCount)
        {
            byte[] ret = ArrayUtils.CreateArray(byteCount, NullCharByte);

            byte[] array = GetMinBytes(encoding, text, byteCount);
            if (CollectionUtils.IsNullOrEmptyG(array))
            {
                return ret;
            }

            array.CopyTo(ret, 0);

            return ret;
        }

        /// <summary>
        /// 获得最少的字节数组
        /// </summary>
        /// <param name="encoding">Encoding</param>
        /// <param name="text">字符串</param>
        /// <param name="maxByteCount">字节数组的最大长度</param>
        /// <returns>字节数组</returns>
        public static byte[] GetMinBytes(Encoding encoding, string text, int maxByteCount)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            var detect = new char[1];
            int totalCount = 0;
            int charCount = 0;
            char[] chars = text.ToCharArray();
            foreach (char c in chars)
            {
                detect[0] = c;
                totalCount += encoding.GetByteCount(detect);
                if (maxByteCount < totalCount)
                {
                    break;
                }

                charCount++;
            }

            if (charCount == 0)
            {
                return null;
            }

            return encoding.GetBytes(chars, 0, charCount);
        }
#if !SILVERLIGHT
        /// <summary>
        /// 将ASCII表示字符的字节数组转换为字符串
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <returns>字符串</returns>
        public static string ASCIIBytes2String(byte[] buffer)
        {
            return CharBytes2String(Encoding.ASCII, buffer);
        }

        /// <summary>
        /// 将ASCII表示字符的字节数组转换为字符串
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="size">字节数</param>
        /// <returns>字符串</returns>
        public static string ASCIIBytes2String(byte[] buffer, int offset, int size)
        {
            return CharBytes2String(Encoding.ASCII, buffer, offset, size);
        }
#endif

        /// <summary>
        /// 将GB2312表示字符的字节数组转换为字符串
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <returns>字符串</returns>
        public static string GBBytes2String(byte[] buffer)
        {
            return CharBytes2String(GB2312, buffer);
        }

        /// <summary>
        /// 将GB2312表示字符的字节数组转换为字符串
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="size">字节数</param>
        /// <returns>字符串</returns>
        public static string GBBytes2String(byte[] buffer, int offset, int size)
        {
            return CharBytes2String(GB2312, buffer, offset, size);
        }

        /// <summary>
        /// 将表示字符的字节数组转换为字符串
        /// </summary>
        /// <param name="encoding">Encoding</param>
        /// <param name="buffer">缓冲区</param>
        /// <returns>字符串</returns>
        public static string CharBytes2String(Encoding encoding, byte[] buffer)
        {
            return CharBytes2String(encoding, buffer, 0, buffer.Length);
        }

        /// <summary>
        /// 将表示字符的字节数组转换为字符串
        /// </summary>
        /// <param name="encoding">Encoding</param>
        /// <param name="buffer">缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="size">字节数</param>
        /// <returns>字符串</returns>
        public static string CharBytes2String(Encoding encoding, byte[] buffer, int offset, int size)
        {
            byte nullCharByte = NullCharByte;

            // 返回空字符串
            if (buffer[offset] == nullCharByte)
            {
                return null;
            }

            for (int index = offset; index < offset + size; index++)
            {
                if (buffer[index] == nullCharByte)
                {
                    return encoding.GetString(buffer, offset, index - offset);
                }
            }

            // 不存在Null，返回全部的字符串
            return encoding.GetString(buffer, offset, size);
        }

        #endregion

        # region 格式化

        /// <summary>
        /// 字符串首字母大写
        /// </summary>
        /// <param name="text">字符串</param>
        /// <param name="delimiter">分隔符</param>
        /// <returns>处理后的字符串</returns>
        public static string Capitalize(string text, string delimiter )
        {
            var parts = Split(text, delimiter);
            StringBuilder builder = new StringBuilder();
            foreach( var part in parts)
            {
                var firstC = part[0];
                if (char.IsUpper(firstC))
                {
                    builder.Append(part);
                    continue;
                }

                var firstUpper = char.ToUpper(firstC);
                builder.Append(firstUpper);
                int len = part.Length;
                if(len == 1) continue;

                builder.Append(part.Substring(1));
            }
            return builder.ToString();
        }
        #endregion
    }
}