using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace LoyalityWebAPI.Logging
{
    public class Log : ILog
    {
        private  readonly ILogger logger;// = LogManager.GetCurrentClassLogger();
        //var logger = new LoggerConfiguration();


        public Log()
        {
            logger = new LoggerConfiguration()
.Enrich.FromLogContext()
//.WriteTo.Console()
//.WriteTo.Seq("http://localhost:5341") // <- Added
.CreateLogger();

           // logger = Serilog.Log.Logger;
        }

        public void Information(string message)
        {
            logger.Information(message);
        }

        public void Warning(string message)
        {
            logger.Warning(message);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }
    }
}
