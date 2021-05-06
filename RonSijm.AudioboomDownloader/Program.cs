using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using RonSijm.AudioboomDownloader.AudioBoom;
using RonSijm.AudioboomDownloader.Options;
using ShellProgressBar;

namespace RonSijm.AudioboomDownloader
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var options = OptionsParser.GetOptions(args);

            if (options.DownloadOptions == null)
            {
                Console.WriteLine("invalid options.");
                return;
            }

            if (options.DownloadOptions?.Path != null)
            {
                var jsonString = JsonSerializer.Serialize(options.DownloadOptions);
                await File.WriteAllLinesAsync(options.DownloadOptions.Path, new[] { jsonString });
            }

            var progressBar = new ProgressBar(1, "Loading available downloads...", new ProgressBarOptions { EnableTaskBarProgress = true });

            var id = AudioBoomIdLocator.GetId(options.DownloadOptions.Podcast);

            var audioboomDownloadableFilesRetriever = new PodcastPageParsingRetriever(id);

            var downloaderFacade = new DownloaderFacade(options.DownloadOptions, audioboomDownloadableFilesRetriever)
            {
                TotalItems = i =>
                {
                    progressBar.MaxTicks = i + 1;
                }
            };


            downloaderFacade.ItemDownloading = () =>
            {
                if (downloaderFacade.State != null)
                {
                    progressBar.Tick(downloaderFacade.State);
                }
            };

            Directory.CreateDirectory(options.DownloadOptions.OutputPath);
            await downloaderFacade.Download();
        }
    }
}