using System;
using System.Linq;
using HtmlAgilityPack;

namespace RonSijm.AudioboomDownloader.AudioBoom
{
    public static class AudioboomDataExtractor
    {
        public static string ParseAudioUrl(HtmlDocument podcastPage)
        {
            var audioUrl = podcastPage.DocumentNode.SelectSingleNode("//meta[@property='og:audio']").Attributes["content"].Value;
            return audioUrl;
        }

        public static string ParseTitleNumber(HtmlDocument podcastPage)
        {
            var episodeName = podcastPage.DocumentNode.SelectSingleNode("//meta[@name='audio_title']").Attributes["content"].Value;
            return episodeName;
        }

        public static int ParseEpisodeNumber(HtmlDocument podcastPage)
        {
            var episodeNumberNode = podcastPage.DocumentNode.Descendants().FirstOrDefault(x => x.Attributes.FirstOrDefault(n => n.Name == "data-episode") != null);
            if (episodeNumberNode == null)
            {
                return 0;
            }

            var episodeNumber = int.Parse(episodeNumberNode.Attributes.First(x => x.Name == "data-episode").Value);
            return episodeNumber;
        }

        public static DateTimeOffset? ParseEpisodeDate(HtmlDocument podcastPage)
        {
            var episodeDateNode = podcastPage.DocumentNode.Descendants().FirstOrDefault(n => n.HasClass("js-time"));
            if (episodeDateNode == null)
            {
                return null;
            }

            var timestamp = int.Parse(episodeDateNode.Attributes.First(x => x.Name == "data-epochtime").Value);
            var dateTime = DateTimeOffset.FromUnixTimeSeconds(timestamp);

            return dateTime;
        }
    }
}
