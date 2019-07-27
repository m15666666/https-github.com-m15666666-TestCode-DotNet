namespace Moons.Common20.StringResources
{
    /// <summary>
    /// 使用字典实现IGetString接口
    /// </summary>
    public class DictionaryGetString : IgnoreCaseKeyDictionary<string>, IGetString
    {
    }
}