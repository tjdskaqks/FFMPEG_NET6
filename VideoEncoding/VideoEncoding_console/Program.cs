using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Text;
using System.Runtime.InteropServices.ComTypes;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;
using System.Threading;

namespace VideoEncoding_console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            List<MetaData> metaDatas = new();
            Stopwatch stopwatch = new();
            stopwatch.Start();
            for (int i = 0; i < 100_000; i++)
            {
                var test1 = await GetMetaDataAsync(@"D:\20211012_녹화복구\1.일취월장 완성편_부족한 부분 완벽 보완_3부\원본\43864da8c461caba49fe55e054443945e070da40481dff384387e84e22939e3a.mp4");
            }
            
            stopwatch.Stop();
            //Console.WriteLine(test1);
            Console.WriteLine(stopwatch.ElapsedMilliseconds + Environment.NewLine);

            //stopwatch.Restart();
            //var test2 = await GetMetaDataAsync(@"D:\20211012_녹화복구\1.일취월장 완성편_부족한 부분 완벽 보완_3부\수정\4-1.mp4");
            //stopwatch.Stop();
            //Console.WriteLine(test2);
            //Console.WriteLine(stopwatch.ElapsedMilliseconds + Environment.NewLine);

            //stopwatch.Restart();
            //var test3 = await GetMetaDataAsync(@"D:\20211012_녹화복구\2.일취월장 응용편_장중 급등 및 눌림 매매 기법_3부\원본\54c022d0cd9f3d31e2b9b3170d12e4d22374749ff43bab4679e54d30c8e3fad7.mp4");
            //stopwatch.Stop();
            //Console.WriteLine(test3);
            //Console.WriteLine(stopwatch.ElapsedMilliseconds + Environment.NewLine);

            //stopwatch.Restart();
            //var test4 = await GetMetaDataAsync(@"D:\20211012_녹화복구\2.일취월장 응용편_장중 급등 및 눌림 매매 기법_3부\수정\5-1.mp4");
            //stopwatch.Stop();
            //Console.WriteLine(test4);
            //Console.WriteLine(stopwatch.ElapsedMilliseconds + Environment.NewLine);

            //stopwatch.Restart();
            //var test5 = await GetMetaDataAsync(@"C:\Users\tjdsk\Desktop\ed97dad20ef370904c790102d2fec112949ce8e987f10817ab7773c7ffc9b669.mp4");
            //stopwatch.Stop();
            //Console.WriteLine(test5);
            //Console.WriteLine(stopwatch.ElapsedMilliseconds + Environment.NewLine);

            //stopwatch.Restart();
            //var test6 = await GetMetaDataAsync(@"C:\Users\tjdsk\Desktop\converted_ed97dad20ef370904c790102d2fec112949ce8e987f10817ab7773c7ffc9b669.mp4");
            //stopwatch.Stop();
            //Console.WriteLine(test6);
            //Console.WriteLine(stopwatch.ElapsedMilliseconds + Environment.NewLine);

            //metaDatas.Add(test1);
            //metaDatas.Add(test2);
            //metaDatas.Add(test3);
            //metaDatas.Add(test4);
            //metaDatas.Add(test5);
            //metaDatas.Add(test6);

            foreach (var metaData in metaDatas.Where(metadata => metadata.FileFormat.Equals("f4v") && metadata.CompatibleBrands.Equals("isommp42m4v")))
            {
                //stopwatch.Restart();
                //await ConvertingMP4Async(metaData.FullFilename, Path.Combine(@"D:\ffmpegTest\Video", "converted" + metaData.Filename));
                //await ConvertingMP4Async(metaData);
                //stopwatch.Stop();
                //Console.WriteLine($"second : {stopwatch.ElapsedTicks / System.Diagnostics.Stopwatch.Frequency}");

                //await Task.Delay(TimeSpan.FromSeconds(1));
            }

            Console.ReadLine();
        }

        private static async Task<MetaData> GetMetaDataAsync(string fileFullPath)
        {
            // https://www.adobe.com/content/dam/acom/en/devnet/flv/video_file_format_spec_v10.pdf
            // https://videocube.tistory.com/entry/MP4-%EB%B6%84%EC%84%9D-%ED%95%98%EA%B8%B0-MPEG4-%ED%8C%8C%ED%8A%B8-14

            if (!System.IO.File.Exists(fileFullPath))
                return null;

            MetaData result = new();

            using FileStream fileStream = new(fileFullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            if (fileStream.Length >= 4)
            {
                try
                {
                    byte[] ftypbytesSize = new byte[4];
                    await fileStream.ReadAsync(ftypbytesSize.AsMemory(0, 4)); // ftyp
                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(ftypbytesSize);

                    int ftypBoxSize = BitConverter.ToInt32(ftypbytesSize, 0); // ftyp 데이터 사이즈
                    if (ftypBoxSize > 0)
                    {
                        byte[] ftypbytes = new byte[ftypBoxSize];
                        int ftypReadbytes = await fileStream.ReadAsync(ftypbytes.AsMemory(0, ftypBoxSize)); // ftyp 데이터 읽기

                        string fileFormat = string.Empty;
                        string compatibleBrands = string.Empty;
                        string duration = string.Empty;

                        if (ftypReadbytes > 0)
                        {
                            fileFormat = Encoding.UTF8.GetString(ftypbytes, 4, 4).Trim(); // f4v 인지 isom 인지
                            compatibleBrands = Encoding.UTF8.GetString(ftypbytes, 12, 16).Replace("\0", "").Replace("\b", "").Trim(); // isommp42m4v 인지 isomiso2avc1mp41

                            fileStream.Position += 4; // mdat 파일 사이즈 부분으로 이동.
                            var isMdatorMoovBytes = new byte[20];
                            await fileStream.ReadAsync(isMdatorMoovBytes.AsMemory(0, 20)); // mdat 사이즈 읽기
                            bool isMdat = Encoding.UTF8.GetString(isMdatorMoovBytes, 0, 20).Trim().Contains("mdat") ? true : false;

                            byte[] mdatbytesSize = null;
                            if (isMdat)
                            {
                                fileStream.Position -= 20; // mdat 파일 사이즈 부분으로 이동.
                                mdatbytesSize = new byte[4];
                                await fileStream.ReadAsync(mdatbytesSize.AsMemory(0, 4)); // mdat 사이즈 읽기
                                if (BitConverter.IsLittleEndian)
                                    Array.Reverse(mdatbytesSize);

                                int mdatBoxSize = BitConverter.ToInt32(mdatbytesSize, 0);
                                mdatBoxSize = BitConverter.ToInt32(mdatbytesSize, 0);
                                if (mdatBoxSize > 0)
                                {
                                    fileStream.Position = fileStream.Position + mdatBoxSize + 4 + 4 + 4; // mdat 데이터 부분 + moov + mvhd 사이즈 + mvhd
                                }
                            }
                            else
                            {
                                fileStream.Position -= 12; // mdat 파일 사이즈 부분으로 이동.
                                result.FaststartFlag = true;
                            }

                            byte[] mvhdVersionbytes = new byte[1];
                            await fileStream.ReadAsync(mvhdVersionbytes.AsMemory(0, 1)); // Movie Header Atom 의 버전, 1: 64bits 0: 32: bits(시간)

                            if (mvhdVersionbytes[0] == 0) // 32비트 시간
                            {
                                fileStream.Position = fileStream.Position + 3 + 4 + 4; // Flags + Creation-time + Modification-tim

                                byte[] timescalebytes = new byte[4];
                                await fileStream.ReadAsync(timescalebytes.AsMemory(0, 4)); // timescale

                                byte[] durationbytes = new byte[4];
                                await fileStream.ReadAsync(durationbytes.AsMemory(0, 4)); // duration

                                if (BitConverter.IsLittleEndian)
                                {
                                    Array.Reverse(timescalebytes);
                                    Array.Reverse(durationbytes);
                                }

                                int timeScaleValue = BitConverter.ToInt32(timescalebytes, 0);
                                int durationValue = BitConverter.ToInt32(durationbytes, 0);
                                duration = $"{durationValue / timeScaleValue}";
                            }
                            else if (mvhdVersionbytes[0] == 1) // 64비트 시간
                            {
                                fileStream.Position = fileStream.Position + 3 + 8 + 8; // Flags + Creation-time + Modification-tim

                                byte[] timescalebytes = new byte[4];
                                await fileStream.ReadAsync(timescalebytes.AsMemory(0, 4)); // timescale

                                byte[] durationbytes = new byte[8];
                                await fileStream.ReadAsync(durationbytes.AsMemory(0, 8)); // duration

                                if (BitConverter.IsLittleEndian)
                                {
                                    Array.Reverse(timescalebytes);
                                    Array.Reverse(durationbytes);
                                }

                                int timeScaleValue = BitConverter.ToInt32(timescalebytes, 0);
                                long durationValue = BitConverter.ToInt64(durationbytes, 0);
                                duration = $"{durationValue / timeScaleValue}";
                            }
                            
                            result.FullFilename = fileStream.Name;
                            result.Filename = Path.GetFileName(fileStream.Name);
                            result.FileSize = fileStream.Length.ToString();
                            result.Duration = duration;
                            result.FileFormat = fileFormat;
                            result.CompatibleBrands = compatibleBrands;
                        }
                    }
                }
                catch (TaskCanceledException tce)
                {

                }
            }

            return string.IsNullOrEmpty(result.Filename) ? null : result;
        }

        static async Task ConvertingMP4Async(MetaData metaData)
        {
            if (!System.IO.File.Exists(metaData.FullFilename))
                return;

            string outputPath = Path.Combine(@"D:\ffmpegTest\Video", "converted" + metaData.Filename);

            if (!System.IO.Directory.Exists(Path.GetDirectoryName(outputPath)))
                System.IO.Directory.CreateDirectory(Path.GetDirectoryName(outputPath));

            //if (System.IO.File.Exists(outputPath))
            //{
            //    var metadata = await GetMetaDataAsync(outputPath);
            //    if (metadata.FileFormat.Equals("isom") && metadata.CompatibleBrands.Equals("isomiso2avc1mp41"))
            //    {
            //        // 리네임
            //        Console.WriteLine($"이미 파일이 존재함. {outputPath}");
            //        return;
            //    }
            //}

            using var process = new Process();
            process.StartInfo.FileName = @"D:\ffmpegTest\ffmpeg-4.4-essentials_build\bin\ffmpeg.exe";
            process.StartInfo.Arguments = $"-i \"{metaData.FullFilename}\" \"{outputPath}\" -y";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.StandardErrorEncoding = Encoding.UTF8;
            
            process.Start();

            process.StandardInput.AutoFlush = true;
            process.StandardInput.Close();

            string ffmpeg라인읽기 = string.Empty;
            double 동영상길이 = Convert.ToDouble(metaData.Duration);
            var Duration = TimeSpan.FromSeconds(동영상길이);

            while (!process.StandardError.EndOfStream)
            {
                ffmpeg라인읽기 = await process.StandardError.ReadLineAsync();
                if (ffmpeg라인읽기.Contains("frame="))
                {
                    // frame=  481 fps=118 q=23.0 size=    2304kB time=00:01:38.24 bitrate= 192.1kbits/s speed=  24x
                    // frame=  481 fps=118 q=23.0 Lsize=    2304kB time=00:01:38.24 bitrate= 192.1kbits/s speed=  24x
                    // frame= 1772 fps=224 q=-1.0 Lsize=    6648kB time=00:00:58.96 bitrate= 923.5kbits/s dup=1417 drop=0 speed=7.47x

                    var logSplits = ffmpeg라인읽기.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    string combine = string.Empty;
                    foreach (var item in logSplits)
                        combine += item;

                    if (combine.Contains("frame=") && combine.Contains("size=") && combine.Contains("time=") && combine.Contains("bitrate=") && combine.Contains("speed=")) // 해당 문자열이 포함되어 있을 때만
                    {
                        int ffmepgFrame = Convert.ToInt32(combine.Substring(combine.IndexOf("frame=") + 6, combine.IndexOf("fps=") - 6));
                        string ffmepgSize = combine[(combine.IndexOf("size=") + 5)..combine.IndexOf("time=")];
                        string ffmepgTime = combine[(combine.IndexOf("time=") + 5)..combine.IndexOf("bitrate=")][..8];
                        string ffmepgbitrate;
                        if (combine.Contains("dup="))
                            ffmepgbitrate = combine[(combine.IndexOf("bitrate=") + 8)..combine.IndexOf("dup=")];
                        else
                            ffmepgbitrate = combine[(combine.IndexOf("bitrate=") + 8)..combine.IndexOf("speed=")];
                        string speed = combine[(combine.IndexOf("speed=") + 6)..].Replace("x", "");

                        var ffmepgTimes = ffmepgTime.Split(':');
                        double dConvertTime = (Convert.ToDouble(ffmepgTimes[0]) * 3600) + (Convert.ToDouble(ffmepgTimes[1]) * 60) + (Convert.ToDouble(ffmepgTimes[2]));
                        
                        Console.WriteLine($"frame={ffmepgFrame}, ConvertingFileSize={ffmepgSize}, bitrate={ffmepgbitrate}, OriTime = {Duration} time={ffmepgTime}, speed={speed}x, Estimated Time Remaining = {TimeSpan.FromSeconds(동영상길이 - dConvertTime) / Convert.ToDouble(speed)}");
                    }
                }
            }
        }
    }

    public class MetaData
    {
        public string FullFilename { get; set; }
        public string Filename { get; set; }
        public string Duration { get; set; }
        public string FileSize { get; set; }
        public string FileFormat { get; set; }
        public string CompatibleBrands { get; set; }
        public bool FaststartFlag { get; set; } = false;
        public override string ToString() => $"Filename : {Filename}{Environment.NewLine}Duration: {Duration}{Environment.NewLine}Filesize: {FileSize}{Environment.NewLine}FileFormat: {FileFormat}{Environment.NewLine}CompatibleBrands: {CompatibleBrands}";
    }
}
