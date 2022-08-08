using SlackNet.Bot;
using System.Diagnostics;

namespace CountVonCount
{
    internal static class SlashCR
    {
        internal static void HandleSlashCommand(string command, IMessage message)
        {
            var args = command.Split(' ');
            switch (args[0])
            {
                case "ping":
                    // uhhhhhhhhh
                    Thread pingThread = new(() => {
                        int? elapsedMs = null;
                        var dm = Counter.ConversationsApi!.OpenAndReturnInfo(new string[] { message.User.Id });
                        Stopwatch stopwatch = new();
                        stopwatch.Restart();
                        stopwatch.Start();
                        Counter.Bot!.Send(new()
                        {
                            Text = "testing",
                            Conversation = dm.Result.Channel.Id
                        });
                        stopwatch.Stop();
                        elapsedMs = (int)stopwatch.ElapsedMilliseconds;
                        
                        HandleDm(message, $"pong! {(elapsedMs is null ? "latency unavailable" : elapsedMs)}").Wait();
                    });
                    pingThread.Start();
                    break;
                case "reset":
                    Counter.ResetCount().Wait();
                    break;
                case "kys":
                    HandleDm(message, "Terminating... Mrs. Obama, its been an honor.").Wait();
                    Environment.Exit(-2);
                    break;
                default:
                    HandleDm(message, "Unknown command.").Wait();
                    break;
            }
            //DeleteMsg(message).Wait();
            Counter.ReactionsApi!.AddToMessage("ok", message.Conversation.Id, message.Ts).Wait();
        }

        private static async Task HandleDm(IMessage message, string text)
        {
            var dm = await Counter.ConversationsApi!.OpenAndReturnInfo(new string[] { message.User.Id });
            await Counter.ChatApi!.PostMessage(new()
            {
                Channel = dm.Channel.Id,
                Text = text
            });
        }

        private static async Task DeleteMsg(IMessage message) => await Counter.ChatApi!.Delete(message.Ts, message.Conversation.Id);
    }
}
