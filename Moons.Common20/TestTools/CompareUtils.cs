using System;
using System.Reflection;
using Moons.Common20.Reflection;

namespace Moons.Common20.TestTools
{
    /// <summary>
    /// 测试时比较的实用工具类
    /// </summary>
    public static class CompareUtils
    {
        static CompareUtils()
        {
            FloatValueWithin = 1e-6;
        }

        #region 是否相等

        /// <summary>
        /// 当对象的属性不等时激发事件
        /// </summary>
        public static Action<PropertyNotEqualException> PropertyNotEqual { get; set; }

        /// <summary>
        /// 浮点数允许的误差，该误差可能由数据库引入
        /// </summary>
        public static double FloatValueWithin { get; set; }

        /// <summary>
        /// 判断两个对象是否一定不等
        /// </summary>
        /// <param name="expected">期望值</param>
        /// <param name="actual">实际值</param>
        /// <returns>true：不等，false：无法断定不等</returns>
        public static bool AbsoluteNotEqual( object expected, object actual )
        {
            // 如果是相同的实例，或者如果二者都为空则相等
            if( ReferenceEquals( expected, actual ) )
            {
                return false;
            }

            // 有一个为空则不等
            if( expected == null || actual == null )
            {
                return true;
            }

            // 类型不一致则不等
            if( expected.GetType() != actual.GetType() )
            {
                return true;
            }

            // 无法断定绝对不等
            return false;
        }

        /// <summary>
        /// 比较两个系统内置类型对象是否相等
        /// </summary>
        /// <param name="expected">期望值</param>
        /// <param name="actual">实际值</param>
        /// <returns>true：相等，false：不等</returns>
        public static bool SystemTypeAreEqual( object expected, object actual )
        {
            bool areEqual = Equals( expected, actual );
            if( areEqual )
            {
                return true;
            }

            // 绝对不相等
            if( AbsoluteNotEqual( expected, actual ) )
            {
                return false;
            }

            // 只比较字节数组
            if( expected is Array )
            {
                if( !( expected is byte[] ) )
                {
                    return false;
                }

                return ArrayUtils.Equal( (byte[])expected, (byte[])actual );
            }

            // 比较浮点数的误差
            double? expectedValue = null;
            double? actualValue = null;
            if( expected is double )
            {
                expectedValue = (double)expected;
                actualValue = (double)actual;
            }
            else if( expected is float )
            {
                expectedValue = (float)expected;
                actualValue = (float)actual;
            }
            else if( expected is double? )
            {
                expectedValue = (double?)expected;
                actualValue = (double?)actual;
            }
            else if( expected is float? )
            {
                expectedValue = (float?)expected;
                actualValue = (float?)actual;
            }

            if( expectedValue.HasValue && actualValue.HasValue )
            {
                if( Math.Abs( expectedValue.Value - actualValue.Value ) <= FloatValueWithin )
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 比较两个对象是否相等
        /// </summary>
        /// <param name="expected">期望值</param>
        /// <param name="actual">实际值</param>
        /// <returns>true：相等，false：不等</returns>
        public static bool AreEqual( object expected, object actual )
        {
            // 如果是相同的实例，或者如果二者都为空则相等
            if( ReferenceEquals( expected, actual ) )
            {
                return true;
            }

            // 有一个为空则不等
            if( expected == null || actual == null )
            {
                return false;
            }

            // 类型不一致则不等
            Type expectedType = expected.GetType();
            Type actualType = actual.GetType();
            if( expectedType != actualType )
            {
                return false;
            }

            // 系统内置类型则直接比较
            if( ReflectionUtils.IsSystemType( expectedType ) )
            {
                return SystemTypeAreEqual( expected, actual );
            }

            // 使用IComparable接口比较
            var expectedComparable = expected as IComparable;
            if( expectedComparable != null )
            {
                return expectedComparable.CompareTo( actual ) == 0;
            }

            PropertyInfoCollection properties = ReflectionUtils.GetPublicCanReadProperties( expectedType );
            foreach( PropertyInfo property in properties )
            {
                // 不比较非基本类型
                if( !ReflectionUtils.IsSystemType( property.PropertyType ) )
                {
                    continue;
                }

                object expectedValue = ReflectionUtils.GetPropertyValue( property, expected );
                object actualValue = ReflectionUtils.GetPropertyValue( property, actual );
                if( !SystemTypeAreEqual( expectedValue, actualValue ) )
                {
                    EventUtils.FireEvent( PropertyNotEqual,
                                          new PropertyNotEqualException( string.Format( "{0}不等！", property.Name ) )
                                              { PropertyInfo = property, Expected = expected, Actual = actual } );
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}