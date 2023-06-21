# HazzysMediaDownloader
Hazzy's Media Downloader provides a quick and easy way to upload videos you find online to discord.

## Synopsis
Find a funny youtube video? Something you want to share with your friends from twitter? A hillarious meme you find on tiktok?
Instead of going to "downloadyoutubemp3totallylegitnovirus.com", use this free, reliable bot. You can skip the process of downloading, uploading, and storing.
Just use the /download command to get a fresh video, straight on discord for you to share.

This would be my sales pitch if I were to actually host this bot, but im broke as hell and dont want to deal with hosting a c# discord bot online (yuck)

## How it works
This bot takes advantage of a free service called [Cobalt](https://co.wukko.me)
The person who made it graciously hosts this service which allows for a quick, clean media downloading experience. And hosts a great API. 
What this bot does is get the link you inputted, sends it off to cobalt to handle all the conversion, gets the link they send back, downloads it to a file on the host's computer, then uploads it all right to discord.
It's hasstle free, and quick, and you dont even need to be the one to host it. 

## How to use
Use is relatively simple. As of right now, you do need to download the source code and recompile, but I'll upload a release and change a bit of the code to make it seamless in the future. 

1. Download the source code and open the .sln file with any IDE you so choose
2. Find the part where I register the slash commands in the Bot.cs file
3. Replace the guildId's I add with the guildID's of whatever server you want, or register all (NOT RECOMMENDED, WILL TAKE HOURS)
4. Build the solution, and find the build location (bin > Debug > net7.0)
5. Add a file called "config.JSON"
6. Copy the json string below into it (Replacing "(YOUR TOKEN)" with your bot's token)
7. Run the .exe!

{
  "token": "(YOUR TOKEN)",
  "prefix": "/"
}
