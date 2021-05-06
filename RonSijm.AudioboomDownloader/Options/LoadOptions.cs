using CommandLine;

namespace RonSijm.AudioboomDownloader.Options
{
    [Verb(Command, HelpText = HelpText)]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class LoadOptions
    {
        private const string Command = "load";
        private const string HelpText = "Save and load the program settings.";

        [Option('p', "Path", Required = false, HelpText = "Load a specific settings file.", Default = "settings.json")]
        public string Path { get; set; }
    }
}