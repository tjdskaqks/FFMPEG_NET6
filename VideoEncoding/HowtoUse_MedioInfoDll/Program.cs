// See https://aka.ms/new-console-template for more information
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using MediaInfoLib;

namespace HowtoUse_MedioInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            MediaInfo mediaInfo = new();

            Stopwatch stopwatch = new();
            stopwatch.Start();
            var test1 = mediaInfo.Open(@"D:\20211012_녹화복구\1.일취월장 완성편_부족한 부분 완벽 보완_3부\원본\43864da8c461caba49fe55e054443945e070da40481dff384387e84e22939e3a.mp4");
            var info = mediaInfo.Inform();
            var info2 = GetMetaData(info);  
            stopwatch.Stop();
            Console.WriteLine(test1);
            Console.WriteLine(stopwatch.ElapsedMilliseconds + Environment.NewLine);

            stopwatch.Restart();
            var test2 = mediaInfo.Open(@"D:\20211012_녹화복구\1.일취월장 완성편_부족한 부분 완벽 보완_3부\수정\4-1.mp4");
            //info = mediaInfo.Inform();
            stopwatch.Stop();
            Console.WriteLine(test2);
            Console.WriteLine(stopwatch.ElapsedMilliseconds + Environment.NewLine);

            stopwatch.Restart();
            var test3 = mediaInfo.Open(@"D:\20211012_녹화복구\2.일취월장 응용편_장중 급등 및 눌림 매매 기법_3부\원본\54c022d0cd9f3d31e2b9b3170d12e4d22374749ff43bab4679e54d30c8e3fad7.mp4");
            //info = mediaInfo.Inform();
            stopwatch.Stop();
            Console.WriteLine(test3);
            Console.WriteLine(stopwatch.ElapsedMilliseconds + Environment.NewLine);

            stopwatch.Restart();
            var test4 = mediaInfo.Open(@"D:\20211012_녹화복구\2.일취월장 응용편_장중 급등 및 눌림 매매 기법_3부\수정\5-1.mp4");
            //info = mediaInfo.Inform();
            stopwatch.Stop();
            Console.WriteLine(test4);
            Console.WriteLine(stopwatch.ElapsedMilliseconds + Environment.NewLine);

            stopwatch.Restart();
            var test5 = mediaInfo.Open(@"C:\Users\tjdsk\Desktop\ed97dad20ef370904c790102d2fec112949ce8e987f10817ab7773c7ffc9b669.mp4");
            //info = mediaInfo.Inform();
            stopwatch.Stop();
            Console.WriteLine(test5);
            Console.WriteLine(info);
            Console.WriteLine(stopwatch.ElapsedMilliseconds + Environment.NewLine);

            stopwatch.Restart();
            var test6 = mediaInfo.Open(@"C:\Users\tjdsk\Desktop\converted_ed97dad20ef370904c790102d2fec112949ce8e987f10817ab7773c7ffc9b669.mp4");
            //info = mediaInfo.Inform();
            stopwatch.Stop();
            Console.WriteLine(test6);
            Console.WriteLine(stopwatch.ElapsedMilliseconds + Environment.NewLine);

            
            //var ToDisplay = mediaInfo.Option("Info_Version", "0.7.0.0;MediaInfoDLL_Example_CS;0.7.0.0");
            //mediaInfo.Open(@"C:\Users\tjdsk\Desktop\converted_ed97dad20ef370904c790102d2fec112949ce8e987f10817ab7773c7ffc9b669.mp4");
            
            //mediaInfo.Get(StreamKind.Video, 0, null);

            Console.WriteLine(info);
            mediaInfo.Close();
            Console.ReadLine();
        }
        static string GetMetaData(string info)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(info))
                return result;

            if (info.Contains("General") && info.Contains("Video") && info.Contains("Audio"))
            {
                var splits = info.Split(new char[]{'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries);
                string formatProfile = splits[4].Substring(splits[4].IndexOf(':') + 1, splits[4].Length - splits[4].IndexOf(':') + 1);
                string CodecId = string.Empty;
                string duration = string.Empty;

                foreach (var line in splits)
                {
                    if (line.Contains("Format profile"))
                    {

                    }
                    else if(line.Contains("Codec ID"))
                    {

                    }
                    else if (line.Contains("Format profile"))
                    {

                    }

                }
            }

            return result;
        }
    }
}
