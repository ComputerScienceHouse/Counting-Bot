// minimal example for sending a simple message to a channel with the SlackAPI library
// this example uses the Async method

using System;
using System.Threading.Tasks;
using System.Text;
using c = System.Console;
using SlackNet;
using SlackNet.Bot;

namespace CountVonCount
{
    internal class Counter
    {
        static readonly string start = "Alrighty ladies and gentlemen, lets get counting! The nth reply to this channel must be the nth number in the set of cardinal numbers. " +
            "Rules: You cannot reply to yourself and you must wait an hour between each of your own replies. What number can we get to without someone fucking it up.";

        internal static async Task Run(string channelID)
        {
            var botToken = Tokens.ReadToken("bot");
            var bot = new SlackBot(botToken);
            await bot.Connect();

            await bot.Send(new BotMessage
            {
                Text = start,
                Conversation = "#count",
                
            });

            bot.OnMessage += OnMessageRecieved;
        }

        private static void OnMessageRecieved(object? sender, IMessage message)
        {
            message.ReplyWith(message.Text);
            // throw new NotImplementedException();
        }
    }
}