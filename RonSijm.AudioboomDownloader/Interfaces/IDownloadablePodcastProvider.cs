using System.Collections.Generic;
using RonSijm.AudioboomDownloader.Models;

namespace RonSijm.AudioboomDownloader.Interfaces
{
    public interface IDownloadablePodcastProvider : IAsyncEnumerable<PodcastDownloadModel>
    {
    }
}
