using System.IO;

namespace Moons.Common20.Serialization
{
    /// <summary>
    /// 实现IBinaryRead接口的类
    /// </summary>
    public class BinaryRead : BinaryReader, IBinaryRead
    {
        #region ctor

        public BinaryRead( Stream stream ) : base( stream )
        {
        }

        #endregion
    }
}