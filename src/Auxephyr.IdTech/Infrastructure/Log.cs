using System.IO;
using System.Text.Json;

namespace Auxephyr.IdTech.Infrastructure
{
    public class Log : ILog
    {
        public static ILog Default { get; } = new Log();
        
        public TextWriter Target { get; set; }

        public void Write(params object[] messages)
        {
            if (Target == null)
                return;

            foreach (var obj in messages)
                Target.WriteLineAsync(obj is string str ? str : JsonSerializer.Serialize(obj));
        }
    }
}