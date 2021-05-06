using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using HtmlAgilityPack;
using RonSijm.AudioboomDownloader.Interfaces;
using RonSijm.AudioboomDownloader.Models;

namespace RonSijm.AudioboomDownloader.AudioBoom
{
    public class PodcastPageParsingRetriever : IDownloadablePodcastProvider
    {
        private readonly int _podcastName;
        
        public PodcastPageParsingRetriever(int podcast)
        {
            _podcastName = podcast;
        }

        public async IAsyncEnumerator<PodcastDownloadModel> GetAsyncEnumerator([EnumeratorCancellation] CancellationToken cancellationToken = new CancellationToken())
        {
            var page = 0;

            do
            {
                page++;

                var overPageUrl = $"https://audioboom.com/channels/{_podcastName}?page={page}";
                var episodeOverviewPage = await new HtmlWeb().LoadFromWebAsync(overPageUrl, cancellationToken);

                var podcastUrls = episodeOverviewPage.DocumentNode.Descendants().Where(n => n.HasClass("l-abs-fill-all")).SelectMany(x => x.Attributes.Where(htmlAttribute => htmlAttribute.Name == "href").Select(y => y.Value)).ToList();

                if (!podcastUrls.Any())
                {
                    yield break;
                }

                foreach (var downloadModel in podcastUrls.Select(CachedPageDownloader.Load).Select(podcastPage => new PodcastDownloadModel
                {
                    DownloadUrl = AudioboomDataExtractor.ParseAudioUrl(podcastPage),
                    EpisodeName = AudioboomDataExtractor.ParseTitleNumber(podcastPage),
                    EpisodeNumber = AudioboomDataExtractor.ParseEpisodeNumber(podcastPage),
                    EpisodeDate = AudioboomDataExtractor.ParseEpisodeDate(podcastPage)
                }))
                {
                    yield return downloadModel;
                }

            } while (true);
        }
    }
}
