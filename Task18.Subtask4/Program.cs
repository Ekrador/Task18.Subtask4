using AngleSharp.Dom;
using System;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace Task18.Subtask4
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            YoutubeClient client = new YoutubeClient();
            Sender sender = new Sender();
            Command getInfo = new GetInfo(client);
            Command download = new Download(client);
            Console.WriteLine("Здравствуйте!");

            while (true)
            {
                var url = GetUrl();
                sender.SetCommand(getInfo);
                try
                {
                    await sender.Run(url);
                }
                catch (Exception ex) when (ex.Message.Contains("Invalid YouTube video ID or URL"))
                {
                    Console.WriteLine("Введен неправильный адрес");
                    continue;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\nПопробуйте еще раз.");
                    continue;
                }
                Console.WriteLine("Качаем это видео? (Y/N)");
                var answer = Console.ReadLine().ToUpper();
                if (answer.Equals("Y"))
                {
                    sender.SetCommand(download);
                    await sender.Run(url);
                }
            }
        }

        static string GetUrl()
        {
            Console.WriteLine("Введите ссылку на Youtube-видео для скачивания или клавишу \"Esc\" для выхода");
            if(Console.ReadKey().Key == ConsoleKey.Escape) { Environment.Exit(0); }
            return Console.ReadLine();
        }
    }
}