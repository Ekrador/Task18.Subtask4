using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace Task18.Subtask4
{
    internal class GetInfo : Command
    {
        YoutubeClient client;
        public GetInfo(YoutubeClient client)
        {
            this.client = client;
        }
        public override void Cancel()
        {
            throw new NotImplementedException();
        }

        public override async Task Run(string url)
        {
            var video = await client.Videos.GetAsync(url);
            var streamManifest = client.Videos.Streams.GetManifestAsync(url);
            var streamInfo = streamManifest.Result.GetMuxedStreams().GetWithHighestVideoQuality();

            Console.WriteLine(new string('-', Console.BufferWidth));
            Console.WriteLine($"Видео: \"{video.Title}\"");
            Console.WriteLine($"Канал: \"{video.Author}\"");
            Console.WriteLine($"Размер: {streamInfo.Size}");
            Console.WriteLine(new string('-', Console.BufferWidth));
        }
    }
}
