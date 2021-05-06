using System.IO;

namespace RonSijm.AudioboomDownloader.Extensions
{
    public static class StringExtensions
    {
        public static string ReplaceInvalidChars(this string filename)
        {
            return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        }
    }
}
