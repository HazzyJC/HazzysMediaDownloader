using System.Text;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using HazzysMediaDownloader.Commands;
using Newtonsoft.Json;

namespace HazzysMediaDownloader;

public class Bot 
{
    public DiscordClient client { get; private set; }
    public InteractivityExtension interactivity { get; private set; }
    public CommandsNextExtension commands { get; private set; }
    public SlashCommandsExtension slashExtention { get; private set; }

    public async Task RunAsync()
    {
        // reading bot token from config.json
        var json = string.Empty;
        using (var fs = File.OpenRead("config.json"))
        using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
            json = await sr.ReadToEndAsync();

        var configJson = JsonConvert.DeserializeObject<ConfigJSON>(json);

        var config = new DiscordConfiguration()
        {
            Intents = DiscordIntents.All,
            Token = configJson.Token,
            TokenType = TokenType.Bot,
            AutoReconnect = true,
        };

        client = new DiscordClient(config);
        client.UseInteractivity(new InteractivityConfiguration()
        {
            Timeout = TimeSpan.FromMinutes(2)
        });
        
        // registering slash commands. i register them in 2 private servers. If you want to use this code, replace my id's with the id's of the servers you want to register them in.
        var slash = client.UseSlashCommands();
        slash.RegisterCommands<SlashCommands>(769430592114524181);
        slash.RegisterCommands<SlashCommands>(1120788294561697892);

        await client.ConnectAsync();
        await Task.Delay(-1);
    }

    private Task OnClientReady(ReadyEventArgs e)
    {
        return Task.CompletedTask;
    }

}