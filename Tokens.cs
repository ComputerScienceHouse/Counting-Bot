using System;
using System.IO;
using System.Text;

namespace CountVonCount
{
    internal static class Tokens
    {
        internal static string ReadToken(string type) => Environment.GetEnvironmentVariable("SLACK_KEY");
    }
}
