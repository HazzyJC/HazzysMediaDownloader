namespace HazzysMediaDownloader.Commands;

public class CommandExtensions
{
    public class ResponseData
    {
        public string url;
    }

    public class RequestData
    {
        public string url = null;
        public string vCodec = "h264";
        public string vQuality = "720";
        public string aFormat = "mp3";
        public bool isAudioOnly = false;
        public bool isNoTTWatermark = true;
        public bool isTTFullAudio = false;
        public bool isAudioMuted = false;
        public bool dubLang = false;
    }
}