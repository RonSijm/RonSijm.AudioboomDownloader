using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using RonSijm.AudioboomDownloader.Extensions;
using RonSijm.AudioboomDownloader.Interfaces;
using RonSijm.AudioboomDownloader.Models;
using RonSijm.AudioboomDownloader.Options;

namespace RonSijm.AudioboomDownloader
{
    public class DownloaderFacade
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public string State { get; set; }
        public Action<int> TotalItems { get; set; }
        public Action ItemDownloading { get; set; }

        private readonly DownloadOptions _options;
        private readonly IDownloadablePodcastProvider _downloadablePodcastProvider;

        public DownloaderFacade(DownloadOptions options, IDownloadablePodcastProvider downloadablePodcastProvider)
        {
            _downloadablePodcastProvider = downloadablePodcastProvider;
            _options = options;
        }

        public async Task Download()
        {
            var podcastModels = new List<PodcastDownloadModel>();

            // Can't give a progress bar at this point, not sure how many podcasts there are. But loading the pages doesn't take that long anyways
            State = "Loading Podcast pages...";
            
            await foreach (var item in _downloadablePodcastProvider)
            {
                podcastModels.Add(item);
            }

            podcastModels = podcastModels.OrderBy(x => x.EpisodeDate).ToList();

            CheckIfFileExists(podcastModels);

            foreach (var podcastDownloadModel in podcastModels)
            {
                DownloadFile(podcastDownloadModel);
            }

        }

        private void CheckIfFileExists(IReadOnlyList<PodcastDownloadModel> podcastModels)
        {
            for (var index = 0; index < podcastModels.Count; index++)
            {
                var podcastModel = podcastModels[index];

                if (podcastModel.EpisodeNumber == 0)
                {
                    podcastModel.EpisodeNumber = index + 1;
                }

                CheckFilePath(podcastModel);
            }

            TotalItems?.Invoke(podcastModels.Count(x => !x.FileExists));
        }

        private void DownloadFile(PodcastDownloadModel podcastDownloadModel)
        {
            if (podcastDownloadModel.FileExists)
            {
                return;
            }

            State = podcastDownloadModel.EpisodeName;
            ItemDownloading?.Invoke();

            using (var client = new WebClient())
            {
                client.DownloadFile(podcastDownloadModel.DownloadUrl, podcastDownloadModel.LocalFilePath);
            }
        }

        private void CheckFilePath(PodcastDownloadModel podcastDownloadModel)
        {
            var timeStampAppend = string.Empty;

            if (podcastDownloadModel.EpisodeDate.HasValue)
            {
                timeStampAppend = $" - [{podcastDownloadModel.EpisodeDate.Value.DateTime:yyyy-MM-dd}]";
            }

            var fileName = $"{podcastDownloadModel.EpisodeNumber}{timeStampAppend} - {podcastDownloadModel.EpisodeName}";
            var fileExtension = podcastDownloadModel.DownloadUrl.Split(".").Last();

            var fileNameWithExtension = $"{fileName}.{fileExtension}".ReplaceInvalidChars();

            var localFile = $"{_options.OutputPath}\\{fileNameWithExtension}";

            var exists = File.Exists(localFile);

            podcastDownloadModel.LocalFilePath = localFile;
            podcastDownloadModel.FileExists = exists;
        }
    }
}
