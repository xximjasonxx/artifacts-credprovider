// Copyright (c) Microsoft. All rights reserved.
//
// Licensed under the MIT license.

using System.Threading;
using NuGet.Common;
using NuGet.Protocol.Plugins;

namespace NuGetCredentialProvider.Logging
{
    internal class PluginConnectionLogger : LoggerBase
    {
        private readonly IConnection connection;

        internal PluginConnectionLogger(IConnection connection)
        {
            this.connection = connection;
        }

        protected override void WriteLog(LogLevel logLevel, string message)
        {
            // intentionally not awaiting here -- don't want to block forward progress just because we tried to log.
            FireAndForgetLog(logLevel, message);
        }

        private async void FireAndForgetLog(LogLevel logLevel, string message)
        {
            try
            {
                await connection.SendRequestAndReceiveResponseAsync<LogRequest, LogResponse>(
                    MessageMethod.Log,
                    new LogRequest(logLevel, $"    {message}"),
                    CancellationToken.None);
            }
            catch
            {
                // Intentionally empty.
                // It's possible we'll fail to log if the plugin connection is in a bad state.
                // Other loggers if attached will capture the actual error. This failure to log is likely not interesting enough to log to stderr.
            }
        }
    }
}
