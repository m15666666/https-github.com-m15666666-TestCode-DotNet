namespace Moons.Common20.Serialization
{
    /// <summary>
    /// 命令消息类
    /// </summary>
    public class CommandMessage : EntityBase
    {
        /// <summary>
        /// 命令ID
        /// </summary>
        public int CommandID { get; set; }

        /// <summary>
        /// 结构类型ID，用于调试
        /// </summary>
        public int StructTypeID { get; set; }

        /// <summary>
        /// 自定义数据
        /// </summary>
        public object Data { get; set; }

        #region 创建命令

        /// <summary>
        /// 创建命令，默认的StructTypeID、Data指向EmptyCustomData对象。
        /// </summary>
        /// <param name="commandID">命令ID</param>
        /// <returns>CommandMessage对象</returns>
        public static CommandMessage CreateCommand( int commandID )
        {
            return new CommandMessage
                       {
                           CommandID = commandID,
                           StructTypeID = StructTypeIDs.EmptyCustomData,
                           Data = new EmptyCustomData()
                       };
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            return $"{nameof(CommandID)}:{CommandID},{nameof(StructTypeID)}:{StructTypeID},Data: {Data}";
        }

        #endregion
    }
}