using System;
using System.ComponentModel;
using Moons.Common20.Reflection;

namespace Moons.Common20
{
    /// <summary>
    /// 用于实体类的基类
    /// </summary>
    [Serializable]
    public class EntityBase
    {
        #region ToString

        public override string ToString()
        {
            return ReflectionUtils.Object2String( this );
        }

        #endregion
    }

    /// <summary>
    /// 带事件通知的、用于实体类的基类
    /// </summary>
    [Serializable]
    public class EntityWithEventBase : EntityBase, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region 激发属性改变事件

        /// <summary>
        /// 激发属性改变事件
        /// </summary>
        public void FirePropertyChanged()
        {
            FirePropertyChanged( string.Empty );
        }

        /// <summary>
        /// 激发属性改变事件
        /// </summary>
        /// <param name="propertyName">属性名</param>
        public void FirePropertyChanged( string propertyName )
        {
            FirePropertyChanged( new PropertyChangedEventArgs( propertyName ) );
        }

        /// <summary>
        /// 激发属性改变事件
        /// </summary>
        /// <param name="e">PropertyChangedEventArgs</param>
        public void FirePropertyChanged( PropertyChangedEventArgs e )
        {
            EventUtils.FireEvent( PropertyChanged, this, e );
        }

        #endregion
    }
}