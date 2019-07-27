using System;
using System.Collections.Generic;

namespace Moons.Common20
{
    /// <summary>
    /// 保持一定长度的队列
    /// </summary>
    /// <typeparam name="T">队列元素类型</typeparam>
    public class FixSizeQueue<T>
    {
        /// <summary>
        /// 保持数据的链表
        /// </summary>
        private readonly List<T> _datas = new List<T>();

        private FixSizeQueue()
        {
        }

        /// <summary>
        /// 队列的最大数量
        /// </summary>
        public int MaxCount { get; set; }

        /// <summary>
        /// 用于队列的最大数量的原因而被移除时调用的函数
        /// </summary>
        public Action<T> RemoveByMaxCount { private get; set; }

        /// <summary>
        /// 纠正队列元素的数量
        /// </summary>
        private void CorrectElementCount()
        {
            if( MaxCount < _datas.Count )
            {
                int removeCount = _datas.Count - MaxCount;
                ForUtils.ForCount( removeCount, index => EventUtils.FireEvent( RemoveByMaxCount, _datas[index] ) );
                _datas.RemoveRange( 0, removeCount );
            }
        }

        /// <summary>
        /// 创建队列
        /// </summary>
        /// <param name="maxCount">队列的最大数量</param>
        /// <returns>队列</returns>
        public static FixSizeQueue<T> Create( int maxCount )
        {
            var ret = new FixSizeQueue<T>();
            ret.MaxCount = maxCount;
            return ret;
        }

        /// <summary>
        /// 队列转换为数组
        /// </summary>
        /// <returns>T[]</returns>
        public T[] ToArray()
        {
            return _datas.ToArray();
        }

        /// <summary>
        /// 添加元素
        /// </summary>
        /// <param name="datas">元素集合</param>
        public void Add( params T[] datas )
        {
            _datas.AddRange( datas );
            CorrectElementCount();
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            _datas.Clear();
        }
    }
}