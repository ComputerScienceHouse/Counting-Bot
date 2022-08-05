namespace CountVonCount
{
    internal class Responses
    {
        // Yall feel free to add any responses you want in here lol
        // use @u whenever you want to mention the person responsible 
        internal static readonly string[] CountMessedUp = new string[] {
            "Hey everyone! Get a load of @u, they cant count LMAO",
            "You ruined it for everyone...",
            "I'm sorry @u, I'm afraid you can't do that", // copilot suggested this one 🤖
            "You can code, but you cant count. Unreal",
            "Petition to ban @u?"
        };
        
        internal static string SelectRandom(in string[] table) => table[new Random().Next(table.Length - 1)];
    }
}
