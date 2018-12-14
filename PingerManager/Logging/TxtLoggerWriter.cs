﻿using System.IO;
using System.Threading.Tasks;

namespace PingerManager.Logging
{
    public class TxtLoggerWriter : ITxtLoggerWriter
    {
        public async Task Write(LogParams logParams, TextWriter textWriter)
        {
            await textWriter.WriteLineAsync(logParams.MessageType + ": " + logParams.Message);
            await textWriter.FlushAsync();
        }
    }
}
