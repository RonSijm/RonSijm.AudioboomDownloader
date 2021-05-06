using System.IO;
using HtmlAgilityPack;

namespace RonSijm.AudioboomDownloader
{
    public static class CachedPageDownloader
    {
        public static HtmlDocument Load(string pageUrl)
        {
            var directory = Path.Combine(Directory.GetCurrentDirectory(), "cache");

            if (!Directory.Exists(directory))
            {
                var di = Directory.CreateDirectory(directory);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }

            var httpWeb = new HtmlWeb
            {
                CachePath = directory,
                UsingCache = true,
                UsingCacheIfExists = true
            };

            var result = httpWeb.Load(pageUrl);

            return result;
        }
    }
}
