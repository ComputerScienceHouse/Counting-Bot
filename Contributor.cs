using SlackNet;

namespace CountVonCount
{
    internal class Contributor
    {
        internal User User { get; private set; }
        internal string ID { get; private set; }
        internal string TimeStamp { get; set; }
        
        public Contributor(User user, string timestamp)
        {
            User = user;
            ID = user.Id;
            TimeStamp = timestamp;
        }
    }
}