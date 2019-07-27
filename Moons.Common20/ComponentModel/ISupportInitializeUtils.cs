using System;
using System.ComponentModel;

namespace Moons.Common20.ComponentModel
{
    /// <summary>
    /// ISupportInitialize接口的实用工具类
    /// </summary>
    public class ISupportInitializeUtils : IDisposable
    {
        private readonly ISupportInitialize[] _iSupportInitializes;

        public ISupportInitializeUtils( ISupportInitialize[] iSupportInitializes )
        {
            _iSupportInitializes = iSupportInitializes;

            foreach( ISupportInitialize iSupportInitialize in _iSupportInitializes )
            {
                iSupportInitialize.BeginInit();
            }
        }

        #region IDisposable 成员

        void IDisposable.Dispose()
        {
            foreach( ISupportInitialize iSupportInitialize in _iSupportInitializes )
            {
                iSupportInitialize.EndInit();
            }
        }

        #endregion
    }
}