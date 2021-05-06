using System;
using System.IO;
using System.Text.Json;
using CommandLine;
using CommandLine.Text;

namespace RonSijm.AudioboomDownloader.Options
{
    public static class OptionsParser
    {
        public static Options GetOptions(string[] args)
        {
            args ??= Array.Empty<string>();

            var allOptions = new Options();

            var parser = new Parser(x => x.AutoHelp = false);
            var result = parser.ParseArguments<LoadOptions, DownloadOptions>(args)
                    .WithParsed<LoadOptions>(options => allOptions.LoadOptions = options)
                    .WithParsed<DownloadOptions>(options => allOptions.DownloadOptions = options);

            if (allOptions.DownloadOptions != null)
            {
                return allOptions;
            }

            if (result.Tag == ParserResultType.NotParsed)
            {
                parser.ParseArguments<LoadOptions, DownloadOptions>(new[] { "load" })
                    .WithParsed<LoadOptions>(options => allOptions.LoadOptions = options)
                    .WithParsed<DownloadOptions>(options => allOptions.DownloadOptions = options);
            }

            var isDirectory = Path.IsPathFullyQualified(allOptions.LoadOptions.Path);

            if (!isDirectory)
            {
                allOptions.LoadOptions.Path = Path.Combine(Directory.GetCurrentDirectory(), allOptions.LoadOptions.Path);
            }

            if (File.Exists(allOptions.LoadOptions.Path))
            {
                var jsonSettingsString = File.ReadAllText(allOptions.LoadOptions.Path);
                allOptions.DownloadOptions = JsonSerializer.Deserialize<DownloadOptions>(jsonSettingsString);
            }

            if (allOptions.DownloadOptions != null)
            {
                return allOptions;
            }

            var helpOptions = HelpText.AutoBuild(result);
            Console.WriteLine(helpOptions.ToString());

            return allOptions;
        }
    }
}
