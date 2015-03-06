using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace ConsoleApp1
{
    class DefLogger
    {
        private static Logger logger = null;
        public static Logger Log
        {
            get
            {
                if (logger == null)
                    logger = LogManager.GetLogger("ConsoleApp1");
                return (logger);
            }
        }
    }
}
