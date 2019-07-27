namespace Moons.Common20
{

    #region 自定义代理

    /// <summary>
    /// 用于.net framework 2.0的无参Action版本。3.0以上已经包含了这个版本。
    /// </summary>
    public delegate void Action20();

    /// <summary>
    /// 用于.net framework 2.0的Action版本。3.0以上已经包含了这个版本。
    /// </summary>
    public delegate void Action20<in TArg1, in TArg2>( TArg1 arg1, TArg2 arg2 );

    /// <summary>
    /// 用于.net framework 2.0的Action版本。3.0以上已经包含了这个版本。
    /// </summary>
    public delegate void Action20<in TArg1, in TArg2, in TArg3>( TArg1 arg1, TArg2 arg2, TArg3 arg3 );

    /// <summary>
    /// 用于.net framework 2.0的Func版本。3.0以上已经包含了这个版本。
    /// </summary>
    public delegate TRet Func20<out TRet>();

    /// <summary>
    /// 用于.net framework 2.0的Func版本。3.0以上已经包含了这个版本。
    /// </summary>
    public delegate TRet Func20<in TArg1, out TRet>( TArg1 arg1 );

    /// <summary>
    /// 用于.net framework 2.0的Func版本。3.0以上已经包含了这个版本。
    /// </summary>
    public delegate TRet Func20<in TArg1, in TArg2, out TRet>( TArg1 arg1, TArg2 arg2 );

    /// <summary>
    /// 创建TRet对象的代理
    /// </summary>
    /// <typeparam name="TRet">对象的类</typeparam>
    /// <returns>TRet对象</returns>
    public delegate TRet CreateObjectHandler<out TRet>();

    #region 操作字节数组的代理

    /// <summary>
    /// 操作字节数组的代理
    /// </summary>
    /// <param name="buffer">缓冲区</param>
    /// <param name="offset">偏移量</param>
    /// <param name="size">字节数</param>
    public delegate void ByteBufferHandler( byte[] buffer, int offset, int size );

    /// <summary>
    /// 操作字节数组的代理
    /// </summary>
    /// <typeparam name="T">数据的来源对象的类型</typeparam>
    /// <param name="source">数据的来源</param>
    /// <param name="buffer">缓冲区</param>
    /// <param name="offset">偏移量</param>
    /// <param name="size">字节数</param>
    public delegate void ByteBufferHandler<in T>( T source, byte[] buffer, int offset, int size );

    #endregion

    #endregion
}