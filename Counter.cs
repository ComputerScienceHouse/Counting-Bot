#define DBG

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
        #if DEBUG || DBG
        internal static readonly string start = "Alrighty ladies and gentlemen, lets get counting! The nth reply to this thread must be the nth number in the set of cardinal numbers. " +
            "Rules: You cannot reply to yourself and you must wait an hour between each of your own replies. What number can we get to without someone fucking it up.";
        #endif

        internal static SlackApiClient? Client { get; private set; }
        internal static SlackBot? Bot { get; private set; }
        internal static ReactionsApi? ReactionsApi { get; private set; }
        internal static ChatApi? ChatApi { get; private set; }
        internal static ConversationsApi? ConversationsApi { get; private set; }
        //internal static EmojiApi? EmojiApi { get; private set; }

        static int count = 0;

        internal static List<Contributor> contributors = new();
        static Contributor? previousContributor;

        internal static string? CtxThread;
        internal static async Task Run()
        {
            // bot

            var botToken = "xoxb-3897780282835-3898543784995-8LQ9wyEwLDvCR3NJetGRJnLM";//Tokens.ReadToken("bot");
            Bot = new SlackBot(botToken);
            await Bot.Connect();

            // #if DEBUG && DBG
            // await Bot.Send(new()
            // {
            //     Text = start,
            //     Conversation = Program.config.Channel,
            //     
            // });
            // #endif

            // event subscription

            Bot.OnMessage += OnMessageRecieved;

            // client and apis

            Client = new(botToken);
            ReactionsApi = new(Client);
            ChatApi = new(Client);
            ConversationsApi = new(Client);
            //EmojiApi = new(Client);
        }

        private static void OnMessageRecieved(object? u, IMessage message)
        {
            // this could be soooo much better... guess ill leave it as it is
            if (message.Text[0] == '\\' && !message.User.IsBot) // slash command (slack doesnt like slashes so I used a baclskash)
                SlashCR.HandleSlashCommand(message.Text[1..], message);
            else if (message.Conversation.Name == Program.config.Channel && message.IsInThread && message.ThreadTs == (CtxThread ??= message.ThreadTs)) // counting in a valid channel only, and is in the currrent thread
                if (int.TryParse(message.Text, out int n) && n == ++count) // valid number check
                    if (contributors.Count == 0) // brand new user
                        HandleGoodCount(message, true);
                    else // users exist
                        for (int i = 0; i < contributors.Count; i++) // CS0162 here, I cant wrap my head around why and id need more ppl to help me test
                            if (contributors[i].ID == message.User.Id) // chceck if user is already contributing
                                if (message.User.Id == previousContributor!.ID) // user counted twice in a row
                                { HandleBadCount(message); return; }
                                else if (double.Parse(message.Ts) - double.Parse(contributors[i].TimeStamp) >= Program.config.WaitTimeSeconds) // has it been an hour?
                                { HandleGoodCount(message, false, i); return; }
                                else // hasnt been an hour
                                { HandleBadCount(message); return; }
                            else // new user
                            { HandleGoodCount(message, true); return; }
                else // invalid number
                    HandleBadCount(message);
        }

        internal static void AddReaction(IMessage message, bool isOkCount) =>
            ReactionsApi!.AddToMessage(isOkCount ? Program.config.OkCountEmoji : Program.config.BadCountEmoji, message.Conversation.Id, message.Ts);

        private static void HandleBadCount(IMessage message)
        {
            AddReaction(message, false);
            message.ReplyWith($"{Responses.SelectRandom(in Responses.CountMessedUp).Replace("@u", $"<@{message.User.Id}>")} Resetting count.");
            count = 0;
            contributors.Clear();
            CtxThread = null;
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

        internal static async Task ResetCount()
        {
            count = 0;
            contributors.Clear();
            await Bot!.Send(new()
            {
                Text = "Resetting count.",
                Conversation = Program.config.Channel,
            });
        }

        internal static async Task SendMessage(string channelID, string message) => await Bot!.Send(new() { Text = message, Conversation = channelID });
    }
}