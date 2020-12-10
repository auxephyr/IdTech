using System;
using System.IO;
using System.Text.Json;

namespace Auxephyr.IdTech.Infrastructure
{
    public static class Log
    {
        public static TextWriter Target { get; set; }

        public static void Write(params object[] messages)
        {
            if (Target == null)
                return;

            foreach (var message in messages)
                Target.WriteLineAsync(JsonSerializer.Serialize(message));
        }
    }
}