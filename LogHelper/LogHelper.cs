using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Reflection;
using System.Text;

namespace LogHelper
{
    public static class SerilogHelper
    {
        public static Serilog.ILogger Logger { get; private set; }

        public static void AddSerilogSetup(this ILoggingBuilder builder, LoggerConfiguration config)
        {
            Logger = config.CreateLogger();
        }

    }
}
