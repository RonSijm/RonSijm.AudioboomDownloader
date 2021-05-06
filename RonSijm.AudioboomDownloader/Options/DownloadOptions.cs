using CommandLine;

namespace RonSijm.AudioboomDownloader.Options
{
    [Verb(PodcastVerb, HelpText = "Settings for which podcast to download.")]
    public class DownloadOptions
    {
        public const string PodcastVerb = "podcast";

        [Option('p', "Podcast", Required = true, HelpText = "Set channel url to save locally.")]
        public string Podcast { get; set; }

        [Option('o', "Output", Required = true, HelpText = "Set output path to save files.")]
        public string OutputPath { get; set; }

        [Option('s', "save", Required = false, HelpText = "Indicates where you want to save the settings file.", Default = "settings.json")]
        public string Path { get; set; }
    }
}