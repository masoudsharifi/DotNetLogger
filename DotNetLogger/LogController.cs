using DotNetLogger.Interfaces;
using System;

namespace DotNetLogger
{
    public class LogController
    {
        private ILogger _Logger = null;
        public LogController(ILogger logger)
        {
            this._Logger = logger;
        }
    }
}
