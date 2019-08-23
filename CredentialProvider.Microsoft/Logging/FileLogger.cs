// Copyright (c) Microsoft. All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.IO;
using NuGet.Common;

namespace NuGetCredentialProvider.Logging
{
    internal class FileLogger : ILogger, IDisposable
    {
        Lazy<StreamWriter> lazyWriter;

        internal FileLogger(string filePath)
        {
            lazyWriter = new Lazy<StreamWriter>(() => new StreamWriter(new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)));
        }

        public void SetLogLevel(LogLevel newLogLevel)
        {
            // If enabled, FileLogger always logs all messages for diagnostic purposes
        }

        public void Log(LogLevel logLevel, string message)
        {
            lazyWriter.Value.WriteLine($"[{logLevel}] {message}");
        }

        #region IDisposable Support
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing && lazyWriter.IsValueCreated)
                {
                    lazyWriter.Value.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
