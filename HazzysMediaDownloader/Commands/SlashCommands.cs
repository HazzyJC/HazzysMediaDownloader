using System.Net;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Newtonsoft.Json;

namespace HazzysMediaDownloader.Commands;

public class SlashCommands : ApplicationCommandModule
{
    [SlashCommand("download", "Sends and embeds a file from whatever social media site you linked")]
    public async Task DownloadCommand(InteractionContext ctx, [Option("link", "your inputted link")] string link, [Option("AudioOnly", "Select if you only want to download audio")] bool mp3)
    {
        string recievedUrl;
        await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
        System.GC.Collect(); 
        System.GC.WaitForPendingFinalizers();
        Console.WriteLine("deleting previous files");

        // Deleting Video File
        // this is my path to my video/audio files. If you plan to use this code, replace these paths with your paths. optimally the same path where your config.json is stored
        if (File.Exists(@"D:\\DiscordBotFIles\\video.mp4"))
        {
            File.Delete(@"D:\\DiscordBotFIles\\video.mp4");
        }
        else
        {
            Console.WriteLine("video file does not exist.");
        }
        
        // Deleting Audio File
        if (File.Exists(@"D:\\DiscordBotFIles\\audio.mp3"))
        {
            File.Delete(@"D:\\DiscordBotFIles\\audio.mp3");
        }
        else
        {
            Console.WriteLine("audio file does not exist.");
        }

        Console.WriteLine("DONE!");
        
        string encodedUrl = Uri.EscapeUriString(link);
        // creating cobalt web request
        HttpWebRequest request = (HttpWebRequest) WebRequest.Create("https://co.wuk.sh/api/json");
        request.Method = "POST";
        request.ContentType = "application/json";
        request.Accept = "application/json";
        Console.WriteLine("DONE SETTING UP WEB REQUEST");
        
        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        {
            if (mp3)
            {
                string json = JsonConvert.SerializeObject(new CommandExtensions.RequestData()
                {
                    url = encodedUrl,
                    isAudioOnly = true
                });
                streamWriter.Write(json);
            }
            else
            {
                string json = JsonConvert.SerializeObject(new CommandExtensions.RequestData()
                {
                    url = encodedUrl,
                    isAudioOnly = false
                });
                streamWriter.Write(json);
            }
        }
        
        HttpWebResponse response;

        try
        {
            response = (HttpWebResponse) request.GetResponse();
        }
        catch (WebException e)
        {
            await ctx.EditResponseAsync(
                new DiscordWebhookBuilder().WithContent("the inputted link did not return a download"));
            throw;
        }
        Console.WriteLine("about to do the final download/upload bit");
        using (var streamReader = new StreamReader(response.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
            // Parse json
            var responseData = JsonConvert.DeserializeObject<CommandExtensions.ResponseData>(result);
            // Get the video url
            string videoUrl = responseData.url;
            // Link the video
            recievedUrl = videoUrl;
            WebClient webClient = new WebClient();
            
            // Download Video
            FileStream fileObj;
            if (!mp3)
            {
                webClient.DownloadFile(recievedUrl, @"D:/DiscordBotFIles/video.mp4");
                fileObj = new FileStream(@"D:/DiscordBotFIles/video.mp4", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            }
            else
            {
                webClient.DownloadFile(recievedUrl, @"D:/DiscordBotFIles/audio.mp3");
                fileObj = new FileStream(@"D:/DiscordBotFIles/audio.mp3", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            }
            Console.WriteLine("downloaded file");
                
            if (fileObj.Length > 25000000)
            {
                await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Resulting file was too big!"));
            }
            else
            {
                Console.WriteLine("First bit done");
                await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddFile(fileObj));
                fileObj = null;
            }
        }
    }
}