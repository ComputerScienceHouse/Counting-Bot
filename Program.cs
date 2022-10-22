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
            //var (config, highScore) = SimpleSerializer.ReadConfig();
            //AppDomain.CurrentDomain.ProcessExit += (s, e) => SimpleSerializer.WriteConfig(config, Counter.HighScore);
            Program.config = SimpleSerializer.defaultConfig;
            Counter.HighScore = 0; // since i cant store data rn we cant have nice things

#if RUN_BOT
            Counter.Run().Wait();
            for (; ; )
                ; // stay alive, forever
#endif
        }
    }
}