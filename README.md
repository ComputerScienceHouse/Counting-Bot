# Count Von Count
This is a counting bot for the #counting channel in slack.

## Contributing:
* Firstly, download and install the latest version of the .NET SDK from [here](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).
* Download the latest version of the .NET runtime [here](https://dotnet.microsoft.com/en-us/download).
> (If you already have the SDK and runtime installed you can skip to this step.)

* Clone the repository with `git clone` or through Github Desktop. 
* CD to the project directory with the terminal of your choice, and run `dotnet restore`.

## Building:
* With the terminal of your choice, CD to the project directory and run `dotnet build -c Release`.

## Running:
* With the terminal of your choice, CD to `./bin/Release/net6.0`
* Run the command `dotnet CountVonCount.dll`


### **!! Please note a `bot.pems` file is required to run the bot. !!**
You can generate one by changing the preprocessor directives at the top of `Program.cs`.
The top of file should look like this:
```cs
#define WRITE_KEY
// #define RUN_BOT
```
Rebuildng the program and running it will prompt you with inputs. 

* For the first one, enter `bot` 

* For the second one, paste the Bot User OAuth Token from slack (it should start with xoxb) into the second input.

> (When you are finished make sure to restore the preprocessor directives to the way they were.)

## What if I dont have a slack bot set up?
You can create a slack classic app [here](https://api.slack.com/apps?new_classic_app=1). Follow the websites instructions to setup the OAuth scopes of the bot.
It should have the following permissions:
* bot
* channels:history
* channels:read
* chanels:write
* chat:write:bot
* commands
* emoji:read
* files:read
* reactions:read
* reactions:write
