using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Moons.Common20.PC;
using Moons.Common20.ValueWrapper;

namespace Moons.Common20.Serialization
{
    /// <summary>
    ///     将对象与字节数组相互转换的实用工具
    /// </summary>
    public class ToFromBytesUtils
    {
        #region 创建对象

        /// <summary>
        /// cctor
        /// </summary>
        static ToFromBytesUtils()
        {
            DefaultStringByteCount = 128;
        }

        ///// <summary>
        ///// ctor
        ///// </summary>
        ///// <param name="reader"></param>
        //public ToFromBytesUtils( BinaryReader reader )
        //{
        //    Reader = reader;
        //}
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="reader"></param>
        public ToFromBytesUtils( IBinaryRead reader )
        {
            Reader = reader;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="writer"></param>
        public ToFromBytesUtils( IBinaryWrite writer )
        {
            Writer = writer;
        }

        /// <summary>
        /// 记录日志的代理
        /// </summary>
        public Action<string> LogHandler { get; set; } = (_) => { };

        #endregion

        #region 变量和属性

        /// <summary>
        ///     默认的字符串字节数，初始为128。
        /// </summary>
        public static int DefaultStringByteCount { get; set; }

        /// <summary>
        ///     内部BinaryReader
        /// </summary>
        //private BinaryReader Reader { get; set; }
        private IBinaryRead Reader { get; set; }

        /// <summary>
        ///     内部BinaryWriter
        /// </summary>
        //private BinaryWriter Writer { get; set; }
        private IBinaryWrite Writer { get; set; }

        /// <summary>
        /// Reader的基础流中还可以读取的字节数，用于：StructTypeIDs.ByteArray。
        /// </summary>
        private int ByteCountLeft2Read { get; set; }

        #endregion

        #region 读出字符串

        /// <summary>
        ///     StringEncodingID对Encoding的映射
        /// </summary>
        private static readonly Dictionary<StringEncodingID, Encoding> _stringEncodingID2Encoding = new Dictionary
            <StringEncodingID, Encoding>
            {
                {StringEncodingID.ASCII, Encoding.ASCII},
                {StringEncodingID.UTF8, Encoding.UTF8},
                {StringEncodingID.BigEndianUnicode, Encoding.BigEndianUnicode},
                {StringEncodingID.Unicode, Encoding.Unicode},
                {StringEncodingID.GB2312, Encoding.ASCII},//StringUtils.GB2312},//.net core不支持Encoding.GetEncoding("GB2312")
            };

        /// <summary>
        ///     读出变长字符串
        /// </summary>
        /// <param name="reader">BinaryReader</param>
        /// <returns>字符串</returns>
        public static string ReadVarString( IBinaryRead reader )
        {
            int encodingId = reader.ReadInt32();

            // 如果id为0，字节流中没有后面的内容
            if( encodingId == 0 )
            {
                return null;
            }

            int size = reader.ReadInt32();

            // 如果size为0，字节流中没有后面的内容
            if( size == 0 )
            {
                return null;
            }

            byte[] buffer = reader.ReadBytes( size );

            var stringEncodingId = (StringEncodingID)encodingId;
            if( _stringEncodingID2Encoding.ContainsKey( stringEncodingId ) )
            {
                return StringUtils.CharBytes2String( _stringEncodingID2Encoding[stringEncodingId], buffer );
            }

            throw new ArgumentOutOfRangeException( string.Format( "Invalid stringEncodingId({0})", encodingId ) );
        }

        /// <summary>
        ///     读出字符串
        /// </summary>
        /// <param name="reader">BinaryReader</param>
        /// <returns>字符串</returns>
        public static string ReadString( IBinaryRead reader )
        {
            return ReadString( reader, DefaultStringByteCount );
        }

        /// <summary>
        ///     读出字符串
        /// </summary>
        /// <param name="reader">BinaryReader</param>
        /// <param name="count">字节数</param>
        /// <returns>字符串</returns>
        public static string ReadString( IBinaryRead reader, int count )
        {
            return StringUtils.GBBytes2String( reader.ReadBytes( count ) );
        }

        /// <summary>
        ///     读出变长字符串
        /// </summary>
        /// <returns>字符串</returns>
        public string ReadVarString()
        {
            return ReadVarString( Reader );
        }

        /// <summary>
        ///     读出字符串
        /// </summary>
        /// <returns>字符串</returns>
        public string ReadString()
        {
            return ReadString( Reader );
        }

        /// <summary>
        ///     读出字符串
        /// </summary>
        /// <param name="count">字节数</param>
        /// <returns>字符串</returns>
        public string ReadString( int count )
        {
            return ReadString( Reader, count );
        }

        #endregion

        #region 写入字符串

        /// <summary>
        ///     写入字符串
        /// </summary>
        /// <param name="writer">BinaryWriter</param>
        /// <param name="text">字符串</param>
        public static void WriteString( IBinaryWrite writer, string text )
        {
            WriteString( writer, text, DefaultStringByteCount );
        }

        /// <summary>
        ///     写入字符串
        /// </summary>
        /// <param name="writer">BinaryWriter</param>
        /// <param name="text">字符串</param>
        /// <param name="count">字节数</param>
        public static void WriteString( IBinaryWrite writer, string text, int count )
        {
            writer.Write( StringUtils.GetGBFixLengthBytes( text, count ) );
        }

        /// <summary>
        ///     写入字符串
        /// </summary>
        /// <param name="text">字符串</param>
        public void WriteString( string text )
        {
            WriteString( Writer, text );
        }

        /// <summary>
        ///     写入字符串
        /// </summary>
        /// <param name="text">字符串</param>
        /// <param name="count">字节数</param>
        public void WriteString( string text, int count )
        {
            WriteString( Writer, text, count );
        }

        /// <summary>
        ///     写入变长字符串
        /// </summary>
        /// <param name="writer">BinaryWriter</param>
        /// <param name="text">字符串</param>
        /// <param name="stringEncodingId">StringEncodingID</param>
        public static void WriteVarString( IBinaryWrite writer, string text, StringEncodingID stringEncodingId )
        {
            // 字符串编码ID，目前有：0：字节流中没有后续内容，1：ASCII，2：UTF8，4：BigEndianUnicode，8：Unicode，16：GB2312。
            int encodingId = 0;
            if( string.IsNullOrEmpty( text ) )
            {
                writer.Write( encodingId );
                return;
            }

            encodingId = (int)stringEncodingId;
            var encoding = _stringEncodingID2Encoding[stringEncodingId];
            var bytes = encoding.GetBytes( text );

            writer.Write( encodingId );
            writer.Write( bytes.Length );
            writer.Write( bytes );
        }

        /// <summary>
        ///     写入变长字符串
        /// </summary>
        /// <param name="text">字符串</param>
        /// <param name="stringEncodingId">StringEncodingID</param>
        public void WriteVarString( string text, StringEncodingID stringEncodingId )
        {
            WriteVarString( Writer, text, stringEncodingId );
        }

        #endregion

        #region 读数据基本函数

        ///// <summary>
        ///// 读取bool值
        ///// </summary>
        ///// <returns></returns>
        //public bool ReadBoolean()
        //{
        //    return Reader.ReadBoolean();
        //}

        /// <summary>
        ///     读取一个Int32类型作为bool值
        /// </summary>
        /// <returns>一个Int32类型作为bool值</returns>
        public bool ReadInt32Boolean()
        {
            return ReadInt32() == ByteUtils.True;
        }

        /// <summary>
        ///     读取字节数组，只用于读取保留字节。
        /// </summary>
        /// <param name="count">字节个数</param>
        /// <returns>字节数组</returns>
        public byte[] ReadBytes( int count )
        {
            return Reader.ReadBytes( count );
        }

        public byte ReadByte()
        {
            return Reader.ReadByte();
        }

        public short ReadInt16()
        {
            return Reader.ReadInt16();
        }

        /// <summary>
        ///     读取int数据
        /// </summary>
        /// <returns>int数据</returns>
        public int ReadInt32()
        {
            //LogHandler("ReadInt32 begin");
            var ret = Reader.ReadInt32();
            //LogHandler($"ReadInt32: {ret}");
            return ret;
        }

        public long ReadInt64()
        {
            return Reader.ReadInt64();
        }

        public float ReadSingle()
        {
            return Reader.ReadSingle();
        }

        public double ReadDouble()
        {
            return Reader.ReadDouble();
        }

        /// <summary>
        ///     读取以14个字符表示到秒的日期时间
        /// </summary>
        /// <returns>日期时间</returns>
        public DateTime ReadDateTime14()
        {
            DateTime ret = TimeUtils.FromDateTime14Bytes( Reader );

            // 后两个字节是填充字节
            ReadInt16();

            return ret;
        }

        /// <summary>
        ///     读取以8个字节表示到秒的日期时间
        /// </summary>
        /// <returns>日期时间</returns>
        public DateTime ReadDateTime8()
        {
            return TimeUtils.FromDateTime8Bytes( Reader );
        }

        #endregion

        #region 可空类型、数组

        /// <summary>
        ///     读取可空的数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="readHandler">读取的函数代理</param>
        /// <returns>数据?</returns>
        private T? ReadNullable<T>( Func20<T> readHandler ) where T : struct
        {
            bool hasData = ReadInt32Boolean();
            T data = readHandler();
            return hasData ? data : default( T? );
        }

        /// <summary>
        ///     写入可空的数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="data">数据</param>
        /// <param name="writeHandler">写入的函数代理</param>
        private void WriteNullable<T>( T? data, Action<T> writeHandler ) where T : struct
        {
            bool hasData = data.HasValue;
            WriteInt32Boolean( hasData );
            writeHandler( hasData ? data.Value : default( T ) );
        }

        /// <summary>
        ///     读取可空的float数据
        /// </summary>
        /// <returns>float?</returns>
        public float? ReadNullableSingle()
        {
            return ReadNullable( ReadSingle );
        }

        /// <summary>
        ///     写入可空的float数据
        /// </summary>
        /// <param name="data">数据</param>
        public void WriteNullableSingle( float? data )
        {
            WriteNullable( data, WriteSingle );
        }

        /// <summary>
        ///     读取数组
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="readHandler">读取代理</param>
        /// <returns>数组</returns>
        private T[] ReadArray<T>( Func20<T> readHandler )
        {
            int count = ReadInt32();
            if( count == 0 ) return new T[0];

            if (typeof(T) == typeof(byte)) return Reader.ReadBytes(count) as T[];
            if (typeof(T) == typeof(short)) return Reader.ReadInt16Array(count) as T[];
            if (typeof(T) == typeof(int)) return Reader.ReadInt32Array(count) as T[];
            if (typeof(T) == typeof(float)) return Reader.ReadSingleArray(count) as T[];

            var ret = new T[count];
            ReadArray( ret, readHandler );
            return ret;
        }

        /// <summary>
        ///     读取数组
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="readHandler">读取代理</param>
        /// <returns>数组</returns>
        private void ReadArray<T>( T[] array, Func20<T> readHandler )
        {
            for( int index = 0; index < array.Length; index++ )
            {
                array[index] = readHandler();
            }
        }

        /// <summary>
        ///     写入数组
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="writeHandler">写入代理</param>
        private void WriteArray<T>( T[] array, Action<T> writeHandler )
        {
            if( CollectionUtils.IsNullOrEmpty( array ) )
            {
                WriteInt32( 0 );
                return;
            }

            WriteInt32( array.Length );
            WriteArrayContent( array, writeHandler );
        }

        /// <summary>
        ///     写入数组内容
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="writeHandler">写入代理</param>
        private void WriteArrayContent<T>( T[] array, Action<T> writeHandler )
        {
            if( CollectionUtils.IsNullOrEmpty( array ) )
            {
                return;
            }

            foreach( T value in array )
            {
                writeHandler( value );
            }
        }

        /// <summary>
        ///     读取byte[]数据
        /// </summary>
        /// <returns>byte[]</returns>
        public byte[] ReadByteArray()
        {
            return ReadArray( ReadByte );
        }

        /// <summary>
        ///     写入byte[]数据
        /// </summary>
        /// <param name="array">byte[]</param>
        public void WriteByteArray( byte[] array )
        {
            WriteArray( array, WriteByte );
        }

        /// <summary>
        ///     读取short[]数据
        /// </summary>
        /// <returns>short[]</returns>
        public short[] ReadShortArray()
        {
            return ReadArray( ReadInt16 );
        }

        /// <summary>
        ///     写入short[]数据
        /// </summary>
        /// <param name="array">short[]</param>
        public void WriteShortArray( short[] array )
        {
            WriteArray( array, WriteInt16 );
        }

        /// <summary>
        ///     读取int[]数据
        /// </summary>
        /// <returns>int[]</returns>
        public int[] ReadInt32Array()
        {
            return ReadArray( ReadInt32 );
        }

        /// <summary>
        ///     写入int[]数据
        /// </summary>
        /// <param name="array">int[]</param>
        public void WriteInt32Array( int[] array )
        {
            WriteArray( array, WriteInt32 );
        }

        /// <summary>
        ///     读取float[]数据
        /// </summary>
        /// <returns>float[]</returns>
        public float[] ReadSingleArray()
        {
            return ReadArray( ReadSingle );
        }

        /// <summary>
        ///     写入float[]数据
        /// </summary>
        /// <param name="array">float[]</param>
        public void WriteSingleArray( float[] array )
        {
            WriteArray( array, WriteSingle );
        }

        /// <summary>
        ///     读取数组项变长字节数组波形数据
        /// </summary>
        /// <returns>数组项变长字节数组波形</returns>
        public VarByteArrayWave ReadVarByteArrayWave()
        {
            float waveScale = ReadSingle();
            int itemLength = ReadInt32();
            //byte[] waveData = ReadByteArray();
            //var ret = new VarByteArrayWave {WaveScale = waveScale, ItemLength = itemLength, WaveData = waveData};
            //ret.InitInnerIntData();
            int byteCount = ReadInt32();
            var ret = new VarByteArrayWave {WaveScale = waveScale, ItemLength = itemLength};
            ret.InitInnerIntData(Reader, byteCount);
            return ret;
        }

        /// <summary>
        ///     读取短整型数组波形数据
        /// </summary>
        /// <returns>短整型数组波形</returns>
        public ShortArrayWave ReadShortArrayWave()
        {
            float waveScale = ReadSingle();
            short[] waveData = ReadShortArray();
            return new ShortArrayWave {WaveScale = waveScale, WaveData = waveData};
        }

        /// <summary>
        ///     写入短整型数组波形数据
        /// </summary>
        /// <param name="data">短整型数组波形</param>
        public void WriteShortArrayWave( ShortArrayWave data )
        {
            WriteSingle( data.WaveScale );
            WriteShortArray( data.WaveData );
        }

        #endregion

        #region 写数据基本函数

        //public void WriteBoolean( bool value )
        //{
        //    Writer.Write( value );
        //}

        /// <summary>
        ///     写入一个Int32作为bool值
        /// </summary>
        /// <param name="value">bool值</param>
        public void WriteInt32Boolean( bool value )
        {
            WriteInt32( ByteUtils.BooleanToByte( value ) );
        }

        public void WriteByte( byte value )
        {
            Writer.Write( value );
        }

        public void WriteInt16( short value )
        {
            Writer.Write( value );
        }

        /// <summary>
        ///     写入int数据
        /// </summary>
        /// <param name="value">int数据</param>
        public void WriteInt32( int value )
        {
            Writer.Write( value );
        }

        public void WriteInt64( long value )
        {
            Writer.Write( value );
        }

        public void WriteSingle( float value )
        {
            Writer.Write( value );
        }

        public void WriteDouble( double value )
        {
            Writer.Write( value );
        }

        /// <summary>
        ///     写入以14个字符表示到秒的日期时间
        /// </summary>
        /// <param name="value">日期时间</param>
        public void WriteDateTime14( DateTime value )
        {
            Writer.Write( TimeUtils.ToDateTime14Bytes( value ) );

            // 后两个字节是填充字节
            WriteInt16( 0 );
        }

        /// <summary>
        ///     写入以8个字节表示到秒的日期时间
        /// </summary>
        /// <param name="value">日期时间</param>
        public void WriteDateTime8( DateTime value )
        {
            TimeUtils.ToDateTime8Bytes( Writer, value );
        }

        #endregion

        #region IP地址相关

        /// <summary>
        ///     写入以4 + 32个字节表示的IP地址
        /// </summary>
        /// <param name="ip">IP地址</param>
        public void WriteIP( string ip )
        {
            // IP字节数组的长度
            const int IPByteCount = 32;

            byte[] bytes = NetworkUtils.IP2Bytes( ip );
            if( bytes == null || IPByteCount < bytes.Length )
            {
                throw new ArgumentException( string.Format( "IP地址({0})不合法！", ip ) );
            }

            WriteInt32( bytes.Length );

            var ipBytes = new byte[IPByteCount];
            bytes.CopyTo( ipBytes, 0 );

            WriteArrayContent( ipBytes, WriteByte );
        }

        #endregion

        #region 读写IValueWrapper接口

        /// <summary>
        ///     读IValueWrapper接口
        /// </summary>
        /// <param name="valueWrappers">IValueWrapper集合</param>
        public void Read( IValueWrapper[] valueWrappers )
        {
            ForUtils.ForEach( valueWrappers, Read );
        }

        /// <summary>
        ///     读IValueWrapper接口
        /// </summary>
        /// <param name="valueWrapper">IValueWrapper</param>
        public void Read( IValueWrapper valueWrapper )
        {
            switch( valueWrapper.ValueWrapperType )
            {
                case ValueWrapperType.Boolean:
                    ( (IValueWrapper<bool>)valueWrapper ).TypedValue = ReadInt32Boolean();
                    return;

                case ValueWrapperType.Byte:
                    ( (IValueWrapper<byte>)valueWrapper ).TypedValue = ReadByte();
                    return;

                case ValueWrapperType.Int16:
                    ( (IValueWrapper<short>)valueWrapper ).TypedValue = ReadInt16();
                    return;

                case ValueWrapperType.Int32:
                    ( (IValueWrapper<int>)valueWrapper ).TypedValue = ReadInt32();
                    return;

                case ValueWrapperType.Int64:
                    ( (IValueWrapper<long>)valueWrapper ).TypedValue = ReadInt64();
                    return;

                case ValueWrapperType.Single:
                    ( (IValueWrapper<float>)valueWrapper ).TypedValue = ReadSingle();
                    return;

                case ValueWrapperType.Double:
                    ( (IValueWrapper<double>)valueWrapper ).TypedValue = ReadDouble();
                    return;

                case ValueWrapperType.DateTime:
                    ( (IValueWrapper<DateTime>)valueWrapper ).TypedValue = ReadDateTime8();
                    return;

                case ValueWrapperType.String:
                    var stringWrapper = (IValueWrapper<string>)valueWrapper;

                    // 处理变长字符串
                    if( stringWrapper.IsVarString )
                    {
                        stringWrapper.TypedValue = ReadVarString();
                        return;
                    }

                    // 处理定长、定encoding字符串
                    if( 0 < stringWrapper.Size )
                    {
                        stringWrapper.TypedValue = ReadString( stringWrapper.Size );
                        return;
                    }

                    // 处理默认长度、定encoding字符串
                    stringWrapper.TypedValue = ReadString();
                    return;

                case ValueWrapperType.NullableSingle:
                    ( (IValueWrapper<float?>)valueWrapper ).TypedValue =
                        ConvertUtils.ToNullable( ReadInt32Boolean(), ReadSingle() );
                    return;
            } // switch( valueWrapper.ValueWrapperType )
            throw new ArgumentOutOfRangeException( string.Format( "Invalid ValueWrapperType({0})",
                                                                  valueWrapper.ValueWrapperType ) );
        }

        /// <summary>
        ///     写IValueWrapper接口
        /// </summary>
        /// <param name="valueWrappers">IValueWrapper集合</param>
        public void Write( IValueWrapper[] valueWrappers )
        {
            ForUtils.ForEach( valueWrappers, Write );
        }

        /// <summary>
        ///     写IValueWrapper接口
        /// </summary>
        /// <param name="valueWrapper">IValueWrapper</param>
        public void Write( IValueWrapper valueWrapper )
        {
            switch( valueWrapper.ValueWrapperType )
            {
                case ValueWrapperType.Boolean:
                    WriteInt32Boolean( ( (IValueWrapper<bool>)valueWrapper ).TypedValue );
                    return;

                case ValueWrapperType.Byte:
                    WriteByte( ( (IValueWrapper<byte>)valueWrapper ).TypedValue );
                    return;

                case ValueWrapperType.Int16:
                    WriteInt16( ( (IValueWrapper<short>)valueWrapper ).TypedValue );
                    return;

                case ValueWrapperType.Int32:
                    WriteInt32( ( (IValueWrapper<int>)valueWrapper ).TypedValue );
                    return;

                case ValueWrapperType.Int64:
                    WriteInt64( ( (IValueWrapper<long>)valueWrapper ).TypedValue );
                    return;

                case ValueWrapperType.Single:
                    WriteSingle( ( (IValueWrapper<float>)valueWrapper ).TypedValue );
                    return;

                case ValueWrapperType.Double:
                    WriteDouble( ( (IValueWrapper<double>)valueWrapper ).TypedValue );
                    return;

                case ValueWrapperType.DateTime:
                    WriteDateTime8( ( (IValueWrapper<DateTime>)valueWrapper ).TypedValue );
                    return;

                case ValueWrapperType.String:
                    var stringWrapper = (IValueWrapper<string>)valueWrapper;
                    if( 0 < stringWrapper.Size )
                    {
                        WriteString( stringWrapper.TypedValue, stringWrapper.Size );
                        return;
                    }
                    WriteString( stringWrapper.TypedValue );
                    return;

                case ValueWrapperType.NullableSingle:
                    {
                        bool hasValue;
                        float value;
                        ConvertUtils.FromNullable(
                            ( (IValueWrapper<float?>)valueWrapper ).TypedValue,
                            out hasValue,
                            out value );
                        WriteInt32Boolean( hasValue );
                        WriteSingle( value );
                    }
                    return;

                case ValueWrapperType.ByteArray:
                    WriteArrayContent( ( (IValueWrapper<byte[]>)valueWrapper ).TypedValue, WriteByte );
                    return;
            } // switch( valueWrapper.ValueWrapperType )
            throw new ArgumentOutOfRangeException( string.Format( "Invalid ValueWrapperType({0})",
                                                                  valueWrapper.ValueWrapperType ) );
        }

        #endregion

        #region 发送、解析命令对象

        /// <summary>
        ///     内部锁
        /// </summary>
        private static readonly object _lock = new object();

        /// <summary>
        ///     结构类型ID对自定义数据解析函数的映射
        /// </summary>
        private static readonly HashDictionary<int, Func20<ToFromBytesUtils, object>>
            _structTypeID2CustomDataReader =
                new HashDictionary<int, Func20<ToFromBytesUtils, object>>
                    {
                        // 空的自定义数据类
                        {StructTypeIDs.EmptyCustomData, toFromBytesUtils => new EmptyCustomData()},

                        // 变长字符串
                        {StructTypeIDs.VarString, toFromBytesUtils => toFromBytesUtils.ReadVarString()},

                        // 变长字符串包含json
                        {StructTypeIDs.VarStringOfJson, toFromBytesUtils => new JsonCustomData { Text = toFromBytesUtils.ReadVarString() } },

                        // 不带长度标识的字节数组
                        {
                            StructTypeIDs.ByteArray,
                            toFromBytesUtils => toFromBytesUtils.ReadBytes( toFromBytesUtils.ByteCountLeft2Read )
                        },

                        // 变长结构体数组
                        {
                            StructTypeIDs.VarStructArray,
                            toFromBytesUtils => {
                                int size = Math.Min(toFromBytesUtils.ReadInt32(), 1000); // 目前最多一次传送1000个结构体
                                int structTypeID = toFromBytesUtils.ReadInt32();
                                toFromBytesUtils.ByteCountLeft2Read -= 8;

                                VarStructArray ret = new VarStructArray{StructTypeID = structTypeID};
                                for( int i = 0; i < size; i++) ret.Add(ReadBodyMessage(toFromBytesUtils,structTypeID));
                                return ret;
                            }
                        },
                    };

        /// <summary>
        ///     结构类型ID对自定义数据发送函数的映射
        /// </summary>
        private static readonly HashDictionary<int, Action20<object, ToFromBytesUtils>>
            _structTypeID2CustomDataWriter =
                new HashDictionary<int, Action20<object, ToFromBytesUtils>>
                    {
                        // 空的自定义数据类
                        {StructTypeIDs.EmptyCustomData, ( object data, ToFromBytesUtils toFromBytesUtils ) => { }},

                        // 变长字符串
                        {
                            StructTypeIDs.VarString,
                            ( object data, ToFromBytesUtils toFromBytesUtils ) => 
                            //toFromBytesUtils.WriteVarString(data as string, StringEncodingID.ASCII)
                            // 为了下发中文设备测点名，使用utf8替代ascii码，utf8兼容ascii码
                            toFromBytesUtils.WriteVarString(data as string, StringEncodingID.UTF8)
                        },

                        // 变长字符串包含json
                        {
                            StructTypeIDs.VarStringOfJson,
                            ( object data, ToFromBytesUtils toFromBytesUtils ) =>
                            //toFromBytesUtils.WriteVarString(data as string, StringEncodingID.ASCII)
                            // 为了下发中文设备测点名，使用utf8替代ascii码，utf8兼容ascii码
                            toFromBytesUtils.WriteVarString(data as string, StringEncodingID.UTF8)
                        },

                        // 不带长度标识的字节数组
                        {
                            StructTypeIDs.ByteArray,
                            ( object data, ToFromBytesUtils toFromBytesUtils ) =>
                            toFromBytesUtils.WriteArrayContent( data as byte[], toFromBytesUtils.WriteByte )
                        },

                        // 版本数据(版本号)
                        {
                            StructTypeIDs.VersionData,
                            ( object data, ToFromBytesUtils toFromBytesUtils ) =>
                                toFromBytesUtils.WriteArrayContent( ( (VersionData)data ).VersionBytes,
                                    toFromBytesUtils.WriteByte )
                        }
                    };

        /// <summary>
        ///     设置结构类型ID对自定义数据解析函数的映射
        /// </summary>
        /// <param name="structTypeIDs">结构类型ID集合</param>
        /// <param name="handlers">代理集合</param>
        public static void SetStructTypeID2CustomDataReaders( int[] structTypeIDs,
                                                              Func20<ToFromBytesUtils, object>[] handlers )
        {
            if( CollectionUtils.IsAnyNullOrEmpty( structTypeIDs, handlers ) )
            {
                return;
            }

            if( !CollectionUtils.IsLengthEqual( structTypeIDs, handlers ) )
            {
                throw new ArgumentException( "键集合与代理集合长度不等！" );
            }

            lock( _lock )
            {
                ForUtils.ForEach( structTypeIDs, handlers,
                                  ( id, handler ) => _structTypeID2CustomDataReader[id] = handler );
            }
        }

        /// <summary>
        ///     设置结构类型ID对自定义数据写入函数的映射
        /// </summary>
        /// <param name="structTypeIDs">结构类型ID集合</param>
        /// <param name="handlers">代理集合</param>
        public static void SetStructTypeID2CustomDataWriters( int[] structTypeIDs,
                                                              Action20<object, ToFromBytesUtils>[] handlers )
        {
            if( CollectionUtils.IsAnyNullOrEmpty( structTypeIDs, handlers ) )
            {
                return;
            }

            if( !CollectionUtils.IsLengthEqual( structTypeIDs, handlers ) )
            {
                throw new ArgumentException( "键集合与代理集合长度不等！" );
            }

            lock( _lock )
            {
                ForUtils.ForEach( structTypeIDs, handlers,
                                  ( id, handler ) => _structTypeID2CustomDataWriter[id] = handler );
            }
        }

        /// <summary>
        ///     解析命令对象
        /// </summary>
        /// <returns>命令对象</returns>
        public CommandMessage ReadCommandMessage()
        {
            return ReadCommandMessage( this );
        }

        /// <summary>
        ///     发送命令对象
        /// </summary>
        /// <param name="commandMessage">命令对象</param>
        public void WriteCommandMessage( CommandMessage commandMessage )
        {
            WriteCommandMessage( commandMessage, this );
        }

        /// <summary>
        ///     解析命令对象
        /// </summary>
        /// <param name="byteBuffer">ByteBuffer</param>
        /// <returns>命令对象</returns>
        public static CommandMessage ReadCommandMessage( ByteBuffer byteBuffer )
        {
            using( MemoryStream stream = byteBuffer.CreateReadStream() )
            {
                return ReadCommandMessage(stream);
            }
        }

        /// <summary>
        ///     解析命令对象
        /// </summary>
        /// <param name="byteBuffer">ByteBuffer</param>
        /// <returns>命令对象</returns>
        public static CommandMessage ReadCommandMessage(byte[] bytes, int offset, int size, Action<ToFromBytesUtils> initHandler = null)
        {
            using (var reader = new ByteArrayReader(bytes,offset,size))
            {
                var tofromUtils = new ToFromBytesUtils(reader) { ByteCountLeft2Read = size };
                initHandler?.Invoke(tofromUtils);
                return ReadCommandMessage(tofromUtils);
            }
            //using (MemoryStream stream = new MemoryStream(bytes, offset, size))
            //{
            //    return ReadCommandMessage(stream, initHandler);
            //}
        }
        /// <summary>
        ///     解析命令对象
        /// </summary>
        /// <param name="byteBuffer">ByteBuffer</param>
        /// <returns>命令对象</returns>
        public static CommandMessage ReadCommandMessage(byte[] bytes)
        {
            return ReadCommandMessage(bytes, 0, bytes.Length);
        }

        /// <summary>
        ///     解析命令对象
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <returns>命令对象</returns>
        public static CommandMessage ReadCommandMessage(Stream stream, Action<ToFromBytesUtils> initHandler = null)
        {
            using (var reader = new BinaryRead(stream))
            {
                var tofromUtils = new ToFromBytesUtils(reader) { ByteCountLeft2Read = (int)stream.Length };
                initHandler?.Invoke(tofromUtils);
                return ReadCommandMessage(tofromUtils);
            }
        }

        /// <summary>
        ///     解析命令对象
        /// </summary>
        /// <param name="toFromBytesUtils">ToFromBytesUtils</param>
        /// <returns>命令对象</returns>
        public static CommandMessage ReadCommandMessage( ToFromBytesUtils toFromBytesUtils )
        {
            int commandID = toFromBytesUtils.ReadInt32();
            int structTypeID = toFromBytesUtils.ReadInt32();
            toFromBytesUtils.ByteCountLeft2Read -= 8;

            object data = ReadBodyMessage(toFromBytesUtils,structTypeID);
            return new CommandMessage {CommandID = commandID, StructTypeID = structTypeID, Data = data};
        }

        /// <summary>
        ///     读取一个Body段数据对象
        /// </summary>
        /// <param name="toFromBytesUtils">ToFromBytesUtils</param>
        /// <param name="structTypeID">类型ID</param>
        /// <returns>一个Body段数据对象</returns>
        private static object ReadBodyMessage(ToFromBytesUtils toFromBytesUtils, int structTypeID )
        {
            Func20<ToFromBytesUtils, object> handler = GetCustomDataReaderByStructTypeID(structTypeID);

            toFromBytesUtils.LogHandler($"tofrombytes begin handle. {structTypeID}");
            object data = handler( toFromBytesUtils );
            toFromBytesUtils.LogHandler($"tofrombytes end handle. {structTypeID}");
            return data;
        }

        /// <summary>
        /// 根据structTypeID获得自定义的读取函数
        /// </summary>
        /// <param name="structTypeID">类型ID</param>
        /// <returns></returns>
        private static Func20<ToFromBytesUtils, object> GetCustomDataReaderByStructTypeID(int structTypeID)
        {
            Func20<ToFromBytesUtils, object> handler = _structTypeID2CustomDataReader[structTypeID];
            return handler == null
                ? throw new ArgumentOutOfRangeException( string.Format( "Unkown struct type ID({0}).", structTypeID ) )
                : handler;
        }

        /// <summary>
        ///     命令对象转化为字节数组
        /// </summary>
        /// <param name="commandMessage">命令对象</param>
        /// <returns>字节数组</returns>
        public static byte[] ToBytes( CommandMessage commandMessage )
        {
            using( var stream = new MemoryStream() )
            {
                using( var writer = new BinaryWrite( stream ) )
                {
                    WriteCommandMessage( commandMessage, new ToFromBytesUtils( writer ) );
                }
                return stream.ToArray();
            }
        }

        /// <summary>
        ///     发送命令对象
        /// </summary>
        /// <param name="commandMessage">命令对象</param>
        /// <param name="toFromBytesUtils">ToFromBytesUtils</param>
        public static void WriteCommandMessage( CommandMessage commandMessage, ToFromBytesUtils toFromBytesUtils )
        {
            int structTypeID = commandMessage.StructTypeID;
            Action20<object, ToFromBytesUtils> handler = _structTypeID2CustomDataWriter[structTypeID];
            if( handler == null )
            {
                throw new ArgumentOutOfRangeException( string.Format( "无法识别的结构类型ID({0})！", structTypeID ) );
            }

            toFromBytesUtils.WriteInt32( commandMessage.CommandID );
            toFromBytesUtils.WriteInt32( structTypeID );
            handler( commandMessage.Data, toFromBytesUtils );
        }

        #endregion
    }
}