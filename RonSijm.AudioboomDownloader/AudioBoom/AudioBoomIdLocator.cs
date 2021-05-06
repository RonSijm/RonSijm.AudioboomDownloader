using System;
using System.Linq;

namespace RonSijm.AudioboomDownloader.AudioBoom
{
    public static class AudioBoomIdLocator
    {
        public static int GetId(string podcast)
        {
            if (string.IsNullOrWhiteSpace(podcast))
            {
                throw new Exception("Invalid Podcast");
            }

            var isNumeric = int.TryParse(podcast, out var podcastId);
            if (isNumeric)
            {
                return podcastId;
            }

            if (podcast.Contains("https://audioboom.com/channels/"))
            {
                var result = podcast.Split(new[] { "https://audioboom.com/channels/", "." }, StringSplitOptions.RemoveEmptyEntries);
                return int.Parse(result[0]);
            }

            if (!podcast.Contains("https://audioboom.com/channel/"))
            {
                return GetChannelIdByName(podcast);
            }

            var cleanerUrl = podcast.Split(new[] { "https://audioboom.com/channel/", "/", "?" }, StringSplitOptions.RemoveEmptyEntries);
            var name = cleanerUrl[0];

            return GetChannelIdByName(name);
        }

        private static int GetChannelIdByName(string name)
        {
            var overPageUrl = $"https://audioboom.com/channel/{name}";
            var episodeOverviewPage = CachedPageDownloader.Load(overPageUrl);

            var podcastId = episodeOverviewPage.DocumentNode.Descendants().SelectMany(x => x.Attributes).Where(y => y.Name == "data-model-id").Select(z => z.Value).FirstOrDefault();

            if (podcastId == null)
            {
                throw new Exception("Podcast not found.");
            }

            return int.Parse(podcastId);
        }
    }
}