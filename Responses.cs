namespace CountVonCount
{
    internal class Responses
    {
        // Yall feel free to add any responses you want in here lol
        // use @u whenever you want to mention the person responsible 
        internal static readonly string[] CountMessedUp = new[] {
            "Hey everyone! Get a load of @u, they cant count LMAO",
            "You ruined it for everyone...",
            "I'm sorry @u, I'm afraid you can't do that", // copilot suggested this one 🤖
            "You can code, but you cant count. Unreal",
            "Petition to ban @u?",
            "Looks like your counting isn't production ready yet...",
            "@u, my genuine response: 0_O",
            "I'm sure ehouse would love to have @u!",
            "L, imagine not being able to count, get ratio'ed",
            "There's some elementary schoolers in Georgia (the country) laughing at the thought of @u rn",
            "You fucking idiot you stupid dummy I'm going to kill your parents if you mistcount again. Fuck You.",
            "Wake up your APU's 2nd guinea pig.",
            "I bet this guy can't even read Weird Clock 🤣🤣 (<https://weird-clock.csh.rit.edu/>)",
            "@u L + Ratio + Skill Issue + You Can't Count + Git Gud + Cringe",
            $"@u I think you meant {Counter.Count}. Get it right next time.",
            "@u probably writes python.",
            "@u probably uses Windows =(", // this one sucks
            "Who invited @u?"
        };
        
        internal static string SelectRandom(in string[] table) => table[new Random().Next(table.Length - 1)];
    }
}
