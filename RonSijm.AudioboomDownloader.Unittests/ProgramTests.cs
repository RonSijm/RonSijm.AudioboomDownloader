using System.Threading.Tasks;
using Xunit;

namespace RonSijm.AudioboomDownloader.Unittests
{
    public class ProgramTests
    {
        [Fact]
        public async Task CallProgramWithParameters()
        {
            var commandLine = $"podcast -p {TestConfig.TestChannelUrlId} -o {TestConfig.TestOutputPath}";
            var args = commandLine.Split(' ');

            await Program.Main(args);
        }

    }
}
