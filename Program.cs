//#define WRITE_KEY
#define RUN_BOT

using CountVonCount;
using c = System.Console;



#if RUN_BOT
// default config:
Config.Channel = "counting";
Config.WaitTimeSeconds = 3600;
Config.OkCountEmoji = "white_check_mark";
Config.BadCountEmoji = "x";
    
// TODO: serializable config

Counter.Run().Wait();
c.WriteLine("Press any key to stop the bot...");
c.ReadKey();
#endif

#if WRITE_KEY
c.WriteLine("Enter a token type, and then the token:");
Tokens.CreateToken(c.ReadLine()!, c.ReadLine()!);
#endif
