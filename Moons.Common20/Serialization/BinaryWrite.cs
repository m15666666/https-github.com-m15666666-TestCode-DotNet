using System.IO;

namespace Moons.Common20.Serialization
{
    /// <summary>
    /// 实现IBinaryWrite接口的类
    /// </summary>
    public class BinaryWrite : BinaryWriter, IBinaryWrite
    {
        #region ctor

        public BinaryWrite( Stream stream ) : base( stream )
        {
        }

        #endregion
    }
}