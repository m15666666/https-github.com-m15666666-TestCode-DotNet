using System.Collections.Generic;

namespace Moons.Common20.PromptMessage
{
    /// <summary>
    /// 系统提示信息基类
    /// </summary>
    public class PromptMsgBase
    {
        #region 变量和属性

        /// <summary>
        /// 提示信息健值对集合
        /// </summary>
        private IDictionary<string, string> _promptMsgs;

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化提示信息健值对集合数据
        /// </summary>
        /// <param name="promptMsgs">提示信息健值对集合数据</param>
        public virtual void Init( IDictionary<string, string> promptMsgs )
        {
            _promptMsgs = promptMsgs;
        }

        #endregion

        #region 根据Key获取值

        /// <summary>
        /// 根据Key获取值
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="args">提示信息中所需参数值</param>
        /// <returns></returns>
        protected string ReadPromptMsgByKey( string key, params object[] args )
        {
            if( key == null )
            {
                return null;
            }

            if( _promptMsgs.ContainsKey( key ) )
            {
                return string.Format( _promptMsgs[key], args );
            }

            key = CharCaseUtils.ToCase( key );
            if( _promptMsgs.ContainsKey( key ) )
            {
                return string.Format( _promptMsgs[key], args );
            }

            return null;
        }

        #endregion
    }
}