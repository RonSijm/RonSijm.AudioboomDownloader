using System.Threading.Tasks;
using FluentAssertions;
using RonSijm.AudioboomDownloader.AudioBoom;
using Xunit;

namespace RonSijm.AudioboomDownloader.Unittests
{
    public class AudioboomDownloadableFilesRetrieverTests
    {
        [Fact]
        public async Task GetPodcastById()
        {
            var audioBoomIdLocator = new PodcastPageParsingRetriever(TestConfig.TestChannelId);
            var result = await audioBoomIdLocator.GetAsyncEnumerator().MoveNextAsync();
            result.Should().BeFalse();
        }
    }
}
