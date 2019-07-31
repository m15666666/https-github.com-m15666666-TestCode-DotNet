using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Moons.Common20;

namespace SocketLib
{
    /// <summary>
    /// 通过Socket发送/接收对象的类
    /// </summary>
    public class ObjectSendReceive : ObjectSendReceiveBase
    {
        #region 变量和属性

        /// <summary>
        /// 异步读取的对象
        /// </summary>
        public object ReadObj { get; private set; }

        #endregion

        #region 序列化对象

        /// <summary>
        /// BinaryFormatter对象
        /// </summary>
        private readonly BinaryFormatter _binaryFormatter = SerializationFormatters.BinaryFormatter;

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="obj">可序列化对象</param>
        /// <returns>byte数组</returns>
        private byte[] Serialize( object obj )
        {
            return SerializationFormatters.Serialize( obj, _binaryFormatter );
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <param name="buffer">byte数组</param>
        /// <returns>可序列化对象</returns>
        private object DeserializeBinary( byte[] buffer )
        {
            return SerializationFormatters.DeserializeBinary( buffer, _binaryFormatter );
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <param name="buffer">byte数组</param>
        /// <param name="offset">偏移</param>
        /// <param name="count">字节数</param>
        /// <returns>对象</returns>
        private object DeserializeBinary( byte[] buffer, int offset, int count )
        {
            return SerializationFormatters.DeserializeBinary( buffer, offset, count, _binaryFormatter );
        }

        #endregion

        #region 异步事件的代理

        /// <summary>
        /// 异步读对象成功时调用的函数
        /// </summary>
        public Action<ObjectSendReceive> ReadObjSuccess { private get; set; }

        /// <summary>
        /// 异步读对象失败时调用的函数
        /// </summary>
        public Action<ObjectSendReceive> ReadObjFail { private get; set; }

        /// <summary>
        /// 激发事件
        /// </summary>
        /// <param name="handler">代理</param>
        private void FireEvent( Action<ObjectSendReceive> handler )
        {
            EventUtils.FireEvent( handler, this );
        }

        #endregion

        #region 创建ObjectSendReceive对象

        protected ObjectSendReceive()
        {
        }

        /// <summary>
        /// 创建ObjectSendReceive对象
        /// </summary>
        /// <param name="socketWrapper">SocketWrapper，连接已经打开</param>
        /// <returns>ObjectSendReceive对象</returns>
        public static ObjectSendReceive CreateObjectSendReceive( SocketWrapper socketWrapper )
        {
            return new ObjectSendReceive { InnerSocketWrapper = socketWrapper };
        }

        /// <summary>
        /// 创建客户端ObjectSendReceive对象，首先接受ServerReply
        /// </summary>
        /// <param name="socketWrapper">SocketWrapper，连接已经打开</param>
        /// <returns>ObjectSendReceive对象</returns>
        public static ObjectSendReceive CreateObjectSendReceive_Client( SocketWrapper socketWrapper )
        {
            ObjectSendReceive ret = CreateObjectSendReceive( socketWrapper );

            // 客户端不使用3.5特性
            ret.InnerSocketWrapper.UseFramework35Feature = false;

            ret.ReceiveServerReplay();

            return ret;
        }

        /// <summary>
        /// 创建服务器端ObjectSendReceive对象，首先发送ServerReply
        /// </summary>
        /// <param name="socketWrapper">SocketWrapper，连接已经打开</param>
        /// <returns>ObjectSendReceive对象</returns>
        public static ObjectSendReceive CreateObjectSendReceive_Server( SocketWrapper socketWrapper )
        {
            ObjectSendReceive ret = CreateObjectSendReceive( socketWrapper );
            ret.SendServerReplay();

            return ret;
        }

        #endregion

        #region 发送/接收对象

        #region 异步接收一个对象

        /// <summary>
        /// 异步接收一个对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns>一个对象</returns>
        public void BeginReceiveObj<T>() where T : class
        {
            ReadPackageSuccess = OnReceiveObjSuccess<T>;
            ReadPackageFail = OnReceiveObjFail;

            BeginReceivePackage();
        }

        /// <summary>
        /// 响应异步接收对象的成功事件
        /// </summary>
        private void OnReceiveObjSuccess<T>( ObjectSendReceiveBase byteSendReceive ) where T : class
        {
            try
            {
                ByteBuffer readByteBuffer = byteSendReceive.ReadByteBuffer;
                int count = readByteBuffer.Size2Read;
                if( count == 0 || !readByteBuffer.ReadEnough )
                {
                    throw new ArgumentException(
                        string.Format(
                            "readByteBuffer.Buffer:{0}, readByteBuffer.Offset:{1}, readByteBuffer.Size2Read:{2}",
                            readByteBuffer.Buffer == null, readByteBuffer.Offset, readByteBuffer.Size2Read ) );
                }

                ReadObj = DeserializeBinary( readByteBuffer.Buffer, readByteBuffer.Offset, count ) as T;

                // 反序列化后(也只在此处)立即释放资源，避免激发事件后函数重入导致的多线程并发问题。
                byteSendReceive.ReleaseReadByteBuffer();

                FireEvent( ReadObjSuccess );
            }
            catch( Exception ex )
            {
                OnReceiveObjFail( ex );
            }
        }

        /// <summary>
        /// 响应异步接收对象的失败事件
        /// </summary>
        private void OnReceiveObjFail( ObjectSendReceiveBase byteSendReceive )
        {
            OnReceiveObjFail( byteSendReceive.InnerException );
        }

        /// <summary>
        /// 响应异步接收对象的失败事件
        /// </summary>
        private void OnReceiveObjFail( Exception ex )
        {
            InnerException = ex;
            FireEvent( ReadObjFail );
        }

        #endregion

        /// <summary>
        /// 接收一个对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns>一个对象</returns>
        public T ReceiveObj<T>() where T : class
        {
            return DeserializeBinary( ReceivePackage() ) as T;
        }

        /// <summary>
        /// 接收一个对象
        /// </summary>
        /// <returns>一个对象</returns>
        public object ReceiveObj()
        {
            return ReceiveObj<object>();
        }

        /// <summary>
        /// 发送数据对象
        /// </summary>
        /// <param name="data">数据对象</param>
        public void SendObj( object data )
        {
            if( data != null )
            {
                SendPackage( Serialize( data ) );
            }
        }

        #endregion

        #region 包装在CommonDataPackage对象中发送/接收

        /// <summary>
        /// 接收一个CommonDataPackage对象
        /// </summary>
        /// <returns>一个CommonDataPackage对象</returns>
        public CommonDataPackage ReceiveCommonPackage()
        {
            return ReceiveObj<CommonDataPackage>();
        }

        /// <summary>
        /// 接收一个CommonDataPackage包含的对象
        /// </summary>
        /// <typeparam name="T">包含的对象的类型</typeparam>
        /// <returns>包含的对象</returns>
        public T ReceiveCommonPackage<T>() where T : class
        {
            while( true )
            {
                var package = ReceiveObj<CommonDataPackage>();
                if( package == null )
                {
                    return null;
                }

                // 心跳包，跳过
                if( package.GetData<HeartbeatData>() != null )
                {
                    continue;
                }

                var errorData = package.GetData<ErrorData>();
                if( errorData != null )
                {
                    // 有错误，抛出异常
                    if( errorData.IsError )
                    {
                        throw errorData.ToException();
                    }

                    // 没错误，且是读ErrorData，则返回。
                    if( typeof( T ) == typeof( ErrorData ) )
                    {
                        return package.GetData<T>();
                    }

                    // 没错误，且不是读ErrorData，则再读后面的一个对象。
                    continue;
                }

                return package.GetData<T>();
            } // while( true )
        }

        /// <summary>
        /// 接收一个CommonDataPackage包含的ErrorData对象，用于检查是否有错误。
        /// </summary>
        public void ReceiveCommonPackageError()
        {
            ReceiveCommonPackage<ErrorData>();
        }

        /// <summary>
        /// 包装在CommonDataPackage对象中，并且发送
        /// </summary>
        /// <param name="data">数据对象</param>
        public void Send_InCommonPackage( object data )
        {
            var package = new CommonDataPackage { Protocol = DTCProtocolType.Data, InnerData = data };
            SendObj( package );
        }

        /// <summary>
        /// 发送心跳包
        /// </summary>
        public void SendHearbeat()
        {
            Send_InCommonPackage( new HeartbeatData() );
        }

        #endregion

        #region 包装在ErrorData对象中，发送

        /// <summary>
        /// 包装在ErrorData对象中，并且发送
        /// </summary>
        /// <param name="message">错误消息</param>
        public void SendError( string message )
        {
            Send_InCommonPackage( new ErrorData { ErrorMsg = message } );
        }

        /// <summary>
        /// 包装在ErrorData对象中，并且发送
        /// </summary>
        /// <param name="ex">Exception</param>
        public void SendError( Exception ex )
        {
            var errorData = new ErrorData();
            errorData.SetException( ex );

            Send_InCommonPackage( errorData );
        }

        /// <summary>
        /// 发送登陆失败错误
        /// </summary>
        public void SendError_Login()
        {
            SendError( "登陆失败" );
        }

        /// <summary>
        /// 发送成功消息
        /// </summary>
        public void SendError_Success()
        {
            SendError( string.Empty );
        }

        #endregion

        #region 发送/接收文件

        /// <summary>
        /// 发送多个文件
        /// </summary>
        /// <param name="paths">文件路径数组</param>
        public void SendFiles( params string[] paths )
        {
            // 检查文件是否存在
            foreach( string path in paths )
            {
                if( !File.Exists( path ) )
                {
                    throw new ArgumentException( string.Format( "文件({0})不存在！", path ) );
                }
            }

            // 生成List<FileInfoData>对象
            var fileInfoDatas = new List<FileInfoData>();
            foreach( string path in paths )
            {
                var fileInfo = new FileInfo( path );
                fileInfoDatas.Add( new FileInfoData { Length = fileInfo.Length } );
            }

            // 发送List<FileInfoData>对象
            Send_InCommonPackage( fileInfoDatas );

            // 依次发送所有文件的字节
            foreach( string path in paths )
            {
                SendFileBytes( path );
            }
        }

        /// <summary>
        /// 接收多个文件，传输失败的文件被删除
        /// </summary>
        /// <param name="dir">文件保存的目录</param>
        /// <param name="paths">多个文件的路径，用于输出</param>
        public void ReceiveFiles( string dir, List<string> paths )
        {
            var allPaths = new List<string>();

            try
            {
                ReceiveFiles( dir, paths, allPaths );
            }
            catch
            {
                paths.ForEach( path => allPaths.Remove( path ) );

                allPaths.ForEach( File.Delete );

                throw;
            }
        }

        /// <summary>
        /// 接收多个文件，传输失败的文件被保留
        /// </summary>
        /// <param name="dir">文件保存的目录</param>
        /// <param name="validPaths">传输完整的、多个文件的路径，用于输出</param>
        /// <param name="allPaths">所有传输文件(包括不完整的)的路径，用于输出</param>
        public void ReceiveFiles( string dir, List<string> validPaths, List<string> allPaths )
        {
            var fileInfoDatas = ReceiveCommonPackage<List<FileInfoData>>();
            if( fileInfoDatas == null )
            {
                throw new Exception( "未收到List<FileInfoData>对象！" );
            }

            long totalLength = 0;
            fileInfoDatas.ForEach( fileInfoData => totalLength += fileInfoData.Length );

            var driveInfo = new DriveInfo( dir[0].ToString() );
            if( driveInfo.AvailableFreeSpace < totalLength )
            {
                throw new Exception( string.Format( "没有足够的磁盘空间({0} < {1})！",
                                                    driveInfo.AvailableFreeSpace,
                                                    totalLength ) );
            }

            fileInfoDatas.ForEach(
                fileInfoData =>
                    {
                        // 生成唯一的文件名
                        string path = Path.Combine( dir, Guid.NewGuid().ToString( "N" ) );

                        // 文件路径加入到列表中
                        allPaths.Add( path );

                        ReceiveFileBytes( fileInfoData.Length, path );

                        // 文件传输成功则加入到列表中
                        validPaths.Add( path );
                    }
                );
        }

        #endregion
    }
}