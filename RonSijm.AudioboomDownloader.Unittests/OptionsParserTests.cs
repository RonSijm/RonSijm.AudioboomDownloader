using FluentAssertions;
using RonSijm.AudioboomDownloader.Options;
using Xunit;

namespace RonSijm.AudioboomDownloader.Unittests
{
    public class OptionsParserTests
    {
        [Fact]
        public void CreateOptionsWithNull()
        {
            var result = OptionsParser.GetOptions(null);
            result.LoadOptions.Should().NotBeNull();
        }

        [Fact]
        public void CreateOptionsForPodcast()
        {
            var cmdLine = new[] { DownloadOptions.PodcastVerb, "-p", TestConfig.TestPodcastName, "-o", TestConfig.TestOutputPath };
            var result = OptionsParser.GetOptions(cmdLine);

            result.DownloadOptions.Should().NotBeNull();
        }

        [Fact]
        public void CreateOptionsAndSave()
        {
            var cmdLine = new[] { DownloadOptions.PodcastVerb, "-p", TestConfig.TestPodcastName, "-o", TestConfig.TestOutputPath, "save" };
            var result = OptionsParser.GetOptions(cmdLine);

            result.DownloadOptions.Should().NotBeNull();
        }

        [Fact]
        public void CreateOptionsIncorrectParams()
        {
            var cmdLine = new[] { "-p", TestConfig.TestPodcastName, "-o", TestConfig.TestOutputPath };
            var result = OptionsParser.GetOptions(cmdLine);

            result.DownloadOptions.Should().BeNull();
        }
    }
}