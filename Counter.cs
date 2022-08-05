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
        
        static List<Contributor> contributors = new();
        static Contributor? previousContributor;
        
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
            // this could be better...
            ;
            if (int.TryParse(message.Text, out int n) && n == ++count) // valid number check
                if (contributors.Count == 0) // brand new user
                    HandleGoodCount(message, true);
                else // users exist
                    for (int i = 0; i < contributors.Count; i++)
                        if (contributors[i].ID == message.User.Id) // chceck if user is already contributing
                            if (message.User.Id == previousContributor!.ID) // user counted twice in a row
                            { HandleBadCount(message); return; }
                            else if (double.Parse(message.Ts) - double.Parse(contributors[i].TimeStamp) >= /*3600*/ 30) // has it been an hour?
                            { HandleGoodCount(message, false, i); return; }
                            else // hasnt been an hour
                            { HandleBadCount(message); return; }
                        else // new user
                        { HandleGoodCount(message, true); return; }
            else // invalid number
                HandleBadCount(message);
        }

        private static void AddReaction(IMessage message, bool isOkCount) =>
            reactionsApi!.AddToMessage(isOkCount ? "white_check_mark" : "x", message.Conversation.Id, message.Ts);

        private static void HandleBadCount(IMessage message)
        {
            AddReaction(message, false);
            message.ReplyWith($"{Responses.SelectRandom(in Responses.CountMessedUp).Replace("@u", $"<@{message.User.Id}>")} Resetting count.");
            count = 0;
            contributors.Clear();
        }
        
        private static void HandleGoodCount(IMessage message, bool newUser, int? userIndex = null)
        {
            AddReaction(message, true);
            previousContributor = new(message.User, message.Ts);
            if (newUser)
                contributors.Add(new(message.User, message.Ts));
            else
                contributors[userIndex ?? throw new NullReferenceException()].TimeStamp = message.Ts;
        }
    }
}