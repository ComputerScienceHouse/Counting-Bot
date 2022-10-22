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
#if WIPCONFIGS
            if (!File.Exists("config.cereal"))
            {
                config.Channel = "counting";
                config.WaitTimeSeconds = 3600;
                config.OkCountEmoji = "white_check_mark";
                config.BadCountEmoji = "x";
                File.WriteAllBytes("config.cereal", DataCerealizer<Config>.Serialize(config, "Count Von Count default config."));
            }
            else
            {
                var bconf = File.ReadAllBytes("config.cereal");
                config = DataCerealizer<Config>.Deserialize(bconf); 
            }


            // TODO: serializable config
#else
            config = new()
            {
                Channel = "counting",
                WaitTimeSeconds = 3600,
                OkCountEmoji = "white_check_mark",
                BadCountEmoji = "x"
            };
#endif

#if RUN_BOT
            Counter.Run().Wait();
            for (;;)
                ; // stay alive, forever
        }
#endif
    }
}