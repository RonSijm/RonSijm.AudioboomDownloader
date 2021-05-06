using FluentAssertions;
using RonSijm.AudioboomDownloader.AudioBoom;
using Xunit;

namespace RonSijm.AudioboomDownloader.Unittests
{
    public class AudioBoomIdLocatorTests
    {
        [Fact]
        public void GetPodcastById()
        {
            var result = AudioBoomIdLocator.GetId(TestConfig.TestChannelId.ToString());
            result.Should().Be(TestConfig.TestChannelId);
        }

        [Fact]
        public void GetPodcastByUrlId()
        {
            var result = AudioBoomIdLocator.GetId(TestConfig.TestChannelUrlId);
            result.Should().Be(TestConfig.TestChannelId);
        }

        [Fact]
        public void GetPodcastByChannelUrl()
        {
            var result = AudioBoomIdLocator.GetId(TestConfig.TestChannelUrlName);
            result.Should().Be(TestConfig.TestChannelId);
        }

        [Fact]
        public void GetPodcastByPodcastName()
        {
            var result = AudioBoomIdLocator.GetId(TestConfig.TestPodcastName);
            result.Should().Be(TestConfig.TestChannelId);
        }
    }
}
