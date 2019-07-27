using System;
using System.Collections.Generic;

namespace Moons.Common20.ValueWrapper
{
    /// <summary>
    ///     包装的接口
    /// </summary>
    public interface IValueWrapper
    {
        /// <summary>
        ///     类型
        /// </summary>
        ValueWrapperType ValueWrapperType { get; }

        /// <summary>
        ///     是否为变长字符串
        /// </summary>
        bool IsVarString { get; set; }

        /// <summary>
        ///     大小，多用于字符串
        /// </summary>
        int Size { get; set; }

        /// <summary>
        ///     值
        /// </summary>
        object Value { get; set; }
    }

    /// <summary>
    ///     包装的接口
    /// </summary>
    /// <typeparam name="T">被包装的类型</typeparam>
    public interface IValueWrapper<T> : IValueWrapper
    {
        T TypedValue { get; set; }
    }

    /// <summary>
    ///     包含IValueWrapper集合的接口
    /// </summary>
    public interface IValueWrappersContainer
    {
        IValueWrapper[] ValueWrappers { get; }
    }

    /// <summary>
    ///     包装的基类
    /// </summary>
    /// <typeparam name="TValue">被包装的类型</typeparam>
    /// <typeparam name="TWrapper">包装器类型</typeparam>
    [Serializable]
    public abstract class ValueWrapperBase<TValue, TWrapper> where TWrapper : ValueWrapperBase<TValue, TWrapper>
    {
        #region 变量和属性

        /// <summary>
        ///     值
        /// </summary>
        public TValue Value { get; set; }

        #endregion

        #region 操作符重载

        public static implicit operator TValue( ValueWrapperBase<TValue, TWrapper> wrapper )
        {
            return wrapper.Value;
        }

        #endregion
    }

    /// <summary>
    ///     包装的基类
    /// </summary>
    /// <typeparam name="TValue">被包装的类型</typeparam>
    [Serializable]
    public abstract class ValueWrapperBase<TValue> : ValueWrapperBase<TValue, ValueWrapperBase<TValue>>,
                                                     IValueWrapper<TValue>
    {
        static ValueWrapperBase()
        {
            InitValueWrapperType();
        }

        #region 变量和属性

        /// <summary>
        ///     包装类的类型
        /// </summary>
        public static ValueWrapperType ValueWrapperType { get; set; }

        #endregion

        #region 操作符重载

        //public static implicit operator TValue( ValueWrapperBase<TValue> wrapper )
        //{
        //    return wrapper.Value;
        //}

        #endregion

        /// <summary>
        ///     初始化包装类的类型
        /// </summary>
        private static void InitValueWrapperType()
        {
            Type type = typeof( TValue );

            var map = new Dictionary<Type, ValueWrapperType>
                {
                    {typeof( bool ), ValueWrapperType.Boolean},
                    {typeof( byte ), ValueWrapperType.Byte},
                    {typeof( short ), ValueWrapperType.Int16},
                    {typeof( int ), ValueWrapperType.Int32},
                    {typeof( long ), ValueWrapperType.Int64},
                    {typeof( float ), ValueWrapperType.Single},
                    {typeof( double ), ValueWrapperType.Double},
                    {typeof( DateTime ), ValueWrapperType.DateTime},
                    {typeof( string ), ValueWrapperType.String},
                    {typeof( float? ), ValueWrapperType.NullableSingle},
                    {typeof( byte[] ), ValueWrapperType.ByteArray},
                };

            ForUtils.UntilFalse4Each( map, item =>
                {
                    if( type == item.Key )
                    {
                        ValueWrapperType = item.Value;
                        return false;
                    }
                    return true;
                } );
        }

        #region IValueWrapper 成员

        ValueWrapperType IValueWrapper.ValueWrapperType
        {
            get { return ValueWrapperType; }
        }

        public bool IsVarString { get; set; }

        public int Size { get; set; }

        object IValueWrapper.Value
        {
            get { return Value; }
            set { Value = value != null ? (TValue)value : default( TValue ); }
        }

        #endregion

        #region IValueWrapper<TValue> 成员

        TValue IValueWrapper<TValue>.TypedValue
        {
            get { return Value; }
            set { Value = value; }
        }

        #endregion
    }

    /// <summary>
    ///     值的包装类
    /// </summary>
    /// <typeparam name="TValue">被包装的类型</typeparam>
    [Serializable]
    public class ValueWrapper<TValue> : ValueWrapperBase<TValue>
    {
        #region 操作符重载

        public static implicit operator ValueWrapper<TValue>( TValue value )
        {
            return new ValueWrapper<TValue> {Value = value};
        }

        #endregion
    }

    /// <summary>
    ///     可以进行Push和Pop操作的值的包装类
    /// </summary>
    /// <typeparam name="TValue">被包装的类型</typeparam>
    public class StackValueWrapper<TValue> : ValueWrapperBase<TValue, StackValueWrapper<TValue>>
    {
        #region 操作符重载

        public static implicit operator StackValueWrapper<TValue>( TValue value )
        {
            return new StackValueWrapper<TValue> {Value = value};
        }

        #endregion

        #region 数据压栈相关

        /// <summary>
        ///     上一个值
        /// </summary>
        private TValue _lastValue;

        /// <summary>
        ///     堆栈内部元素个数
        /// </summary>
        private int _stackCount;

        /// <summary>
        ///     将上一个数据压栈，并且赋值
        /// </summary>
        /// <param name="value">值</param>
        public void PushAndSetValue( TValue value )
        {
            if( _stackCount == 1 )
            {
                throw new InvalidOperationException( "Can only push one time!" );
            }
            ++_stackCount;

            _lastValue = Value;
            Value = value;
        }

        /// <summary>
        ///     恢复上一个值
        /// </summary>
        public void RestoreLastValue()
        {
            if( _stackCount == 0 )
            {
                throw new InvalidOperationException( "Can only restore one time!" );
            }
            --_stackCount;

            Value = _lastValue;
            _lastValue = default( TValue );
        }

        #endregion
    }
}