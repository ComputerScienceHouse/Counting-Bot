#define RUN_BOT
//#define WIPCONFIGS

using c = System.Console;
using DataCerealizer;

namespace CountVonCount
{
    internal class Program
    {
        internal static Config config = (Config)Activator.CreateInstance(typeof(Config))!;
        static void Main(string[] args)
        {
            // subscribe to the process being killed and save when it happens
            var (config, highScore) = SimpleSerializer.ReadEnvar();
            AppDomain.CurrentDomain.ProcessExit += (s, e) => { SimpleSerializer.WriteEnvar(config); SimpleSerializer.WriteEnvar(Counter.HighScore); };
            Program.config = config;
            Counter.HighScore = highScore;

#if RUN_BOT
            Counter.Run().Wait();
            for (; ; )
                ; // stay alive, forever
#endif
        }
    }
}