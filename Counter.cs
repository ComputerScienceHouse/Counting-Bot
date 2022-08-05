using System;
using System.Threading.Tasks;
using System.Text;
using c = System.Console;
using SlackNet;
using SlackNet.Bot;
using SlackNet.WebApi;

namespace CountVonCount
{
    internal class Counter
    {
        static readonly string start = "Alrighty ladies and gentlemen, lets get counting! The nth reply to this channel must be the nth number in the set of cardinal numbers. " +
            "Rules: You cannot reply to yourself and you must wait an hour between each of your own replies. What number can we get to without someone fucking it up.";

        static SlackBot? bot;
        static ReactionsApi? reactionsApi;

        static int count = 0;
        
        internal static async Task Run(string channelName)
        {
            // bot
            
            var botToken = Tokens.ReadToken("bot");
            bot = new SlackBot(botToken);
            await bot.Connect();

            await bot.Send(new BotMessage
            {
                Text = start,
                Conversation = channelName,
                
            });

            bot.OnMessage += OnMessageRecieved;

            // reactions api
            
            SlackApiClient client = new(botToken);
            reactionsApi = new(client);
        }

        private static void OnMessageRecieved(object? u, IMessage message)
        {
            if (int.TryParse(message.Text, out int i) && i == ++count)
                AddReaction(message, true);
            else
            {
                AddReaction(message, false);
                message.ReplyWith(
                    $"{Responses.SelectRandom(in Responses.CountMessedUp).Replace("@u", $"<@{message.User.Id}>")} Resetting count."
                    );
                count = 0;
            }
        }

        private static void AddReaction(IMessage message, bool isOkCount) =>
            reactionsApi!.AddToMessage(isOkCount ? "white_check_mark" : "x", message.Conversation.Id, message.Ts);
    }
}