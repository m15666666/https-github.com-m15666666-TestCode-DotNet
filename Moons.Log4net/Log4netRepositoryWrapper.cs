using log4net;
using log4net.Repository;
using Moons.Common20;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moons.Log4net
{
    /// <summary>
    /// 使用log4net实现ILogNetRepository
    /// </summary>
    public class Log4netRepositoryWrapper : ILogNetRepository
    {
        private readonly ILoggerRepository _loggerRepository;
        public Log4netRepositoryWrapper(ILoggerRepository loggerRepository)
        {
            _loggerRepository = loggerRepository;
        }

        ILogNet ILogNetRepository.GetLogger(string name)
        {
            return new Log4netWrapper(LogManager.GetLogger(_loggerRepository.Name, name));
        }
    }
}
