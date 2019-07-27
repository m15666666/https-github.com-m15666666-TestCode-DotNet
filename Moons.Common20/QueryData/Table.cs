namespace Moons.Common20.QueryData
{
    /// <summary>
    /// 数据表
    /// </summary>
    public class Table
    {
        /// <summary>
        /// 别名
        /// </summary>
        private string _alias;

        /// <summary>
        /// 名称或内容
        /// </summary>
        private string _name;

        /// <summary>
        /// 名称或内容
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = StringUtils.Trim( value ); }
        }

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias
        {
            get { return _alias; }
            set { _alias = StringUtils.Trim( value ); }
        }
    }
}