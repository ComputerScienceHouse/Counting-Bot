//#define WRITE_KEY
#define RUN_BOT

using CountVonCount;
using c = System.Console;

#if RUN_BOT
var b = new Counter();
Counter.Run("C03SSEW3T7B").Wait();
c.WriteLine("Press any key to continue...");
c.ReadKey();
#endif

#if WRITE_KEY
c.WriteLine("Enter a token type, and then the token:");
Tokens.CreateToken(c.ReadLine()!, c.ReadLine()!);
#endif