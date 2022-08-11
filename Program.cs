using CountVonCount;
using c = System.Console;




Config.Channel = "counting";
Config.WaitTimeSeconds = 3600;
Config.OkCountEmoji = "white_check_mark";
Config.BadCountEmoji = "x";

// TODO: serializable config

Counter.Run().Wait();
while (true) {
    // Stay alive, forever
}