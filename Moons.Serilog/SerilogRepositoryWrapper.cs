using Moons.Common20;
using System.Collections.Generic;

namespace Moons.Serilogs
{
    /// <summary>
    /// 使用Serilog实现ILogNetRepository
    /// https://serilog.net/
    /// </summary>
    public class SerilogRepositoryWrapper : ILogNetRepository
    {
        private readonly IDictionary<string, Serilog.ILogger> _name2Loggers;
        public SerilogRepositoryWrapper(IDictionary<string, Serilog.ILogger> name2Loggers )
        {
            _name2Loggers = name2Loggers;
        }

        ILogNet ILogNetRepository.GetLogger(string name)
        {
            return new SerilogWrapper(_name2Loggers.ContainsKey(name) ? _name2Loggers[name] : Serilog.Log.Logger);
        }
    }
}
