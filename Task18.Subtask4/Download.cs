using AngleSharp.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace Task18.Subtask4
{
    internal class Download : Command
    {
        YoutubeClient client;
        public Download(YoutubeClient client)
        {
            this.client = client;
        }
        public override void Cancel()
        {
            throw new NotImplementedException();
        }
        public override async Task Run(string url)
        {
            var videoInfo = client.Videos.GetAsync(url);
            var streamManifest = client.Videos.Streams.GetManifestAsync(url);
            var streamInfo = streamManifest.Result.GetMuxedStreams().GetWithHighestVideoQuality();
            string fileExtension = streamInfo.Container.Name;
            string fileName = $"{videoInfo.Result.Title}.{fileExtension}";
            fileName = string.Concat(fileName.Split(Path.GetInvalidFileNameChars()));

            var cursor = Console.GetCursorPosition();
            var progress = new Progress<double>(p => ShowProgress(cursor.Left, cursor.Top, $"{p:P0}"));
            await client.Videos.Streams.DownloadAsync(streamInfo, fileName, progress);

            Console.SetCursorPosition(cursor.Left, cursor.Top);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(cursor.Left, cursor.Top);
            Console.WriteLine(new string('-', Console.BufferWidth));
            Console.WriteLine("Готово!");
            Console.WriteLine(new string('-', Console.BufferWidth));
        }
        public static void ShowProgress(int left, int top, string progress)
        {
            Console.SetCursorPosition(left, top);
            if (Int32.Parse(progress.Substring(0,2)) % 10 == 0)
            {
                Console.Write(progress);
            }
            Console.SetCursorPosition(left, top);
        }
    }
}
