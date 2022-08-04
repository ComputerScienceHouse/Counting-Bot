using System;
using System.IO;
using System.Text;

namespace CountVonCount
{
    internal static class Tokens
    {
        internal static string ReadToken(string type) => Encoding.ASCII.GetString(File.ReadAllBytes($"{type}.pems"));
        internal static void CreateToken(string type, string token) => File.WriteAllBytes($"{type}.pems", Encoding.ASCII.GetBytes(token));
    }
}
