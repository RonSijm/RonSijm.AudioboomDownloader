using System;
using System.Diagnostics;

namespace RonSijm.AudioboomDownloader.Models
{
    [DebuggerDisplay("{EpisodeNumber} - {EpisodeDate} - {EpisodeName}")]
    public class PodcastDownloadModel
    {
        public string DownloadUrl { get; set; }
        public string LocalFilePath { get; set; }
        public bool FileExists { get; set; }
        public int EpisodeNumber { get; set; }
        public DateTimeOffset? EpisodeDate { get; set; }
        public string EpisodeName { get; set; }
    }
}