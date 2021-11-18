using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CreateThumbnail
{
    public partial class Form1 : Form
    {
        const int THUMBNAIL_WEIGHT = 140;
        const int THUMBNAIL_HEIGHT = 90;
        private const int FILE_CEL_COUNT = 60;

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Click += Button1_Click;
            textBox1.Text = @"D:\20211012_녹화복구\3.test\ed97dad20ef370904c790102d2fec112949ce8e987f10817ab7773c7ffc9b669.mp4";
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                Debug.WriteLine("경로 입력하세요.");
                return;
            }

            if (!File.Exists(textBox1.Text))
            {
                Debug.WriteLine("파일이 없습니다.");
                return;
            }
            await GenerateThumbnail(textBox1.Text);
        }

        private async Task GenerateThumbnail(string path)
        {
            string fileName = Path.GetFileNameWithoutExtension(path);
            string directoryName = Path.GetDirectoryName(path);
            string outputPath = Path.Combine(directoryName, fileName);
            using var process = new Process();
            process.StartInfo.FileName = @"D:\ffmpegTest\ffmpeg-4.4-essentials_build\bin\ffmpeg.exe";
            process.StartInfo.Arguments = $"-vsync 2 -i \"{path}\" -vf \"fps=1,scale={THUMBNAIL_WEIGHT}:{THUMBNAIL_HEIGHT}\" -qscale:v 0 -y \"{outputPath}_%3d.png\"";

            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.StandardErrorEncoding = Encoding.UTF8;

            process.Start();

            process.StandardInput.AutoFlush = true;
            process.StandardInput.Close();

            while (!process.StandardError.EndOfStream)
            {
                Debug.WriteLine(await process.StandardError.ReadLineAsync());
            }
            //await process.WaitForExitAsync();

            var arrayThumbnail = GetThumbnailArray(path);
            GenerateToTalThumbnail(arrayThumbnail);
        }

        private string[] GetThumbnailArray(string mp4FullFileName) => Directory.GetFiles(Path.GetDirectoryName(mp4FullFileName), $"{Path.GetFileNameWithoutExtension(mp4FullFileName)}_*.png");

        private void GenerateToTalThumbnail(string[] ThumbnailArray)
        {
            int ThumbnailArrayLength = ThumbnailArray.Length;
            if (ThumbnailArrayLength > 0)
            {
                int totalThumbnail_Celcount = ThumbnailArrayLength >= FILE_CEL_COUNT ? FILE_CEL_COUNT : ThumbnailArrayLength;
                int totalThumbnail_Rowcount;
                if (totalThumbnail_Celcount == FILE_CEL_COUNT)
                    totalThumbnail_Rowcount = (ThumbnailArrayLength / FILE_CEL_COUNT) + 1;
                else if (totalThumbnail_Celcount > FILE_CEL_COUNT)
                    totalThumbnail_Rowcount = ThumbnailArrayLength / FILE_CEL_COUNT;
                else
                    totalThumbnail_Rowcount = 1;

                using Bitmap totalThumbnail = new(THUMBNAIL_WEIGHT * totalThumbnail_Celcount, THUMBNAIL_HEIGHT * totalThumbnail_Rowcount, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                using Graphics mygraphics = Graphics.FromImage(totalThumbnail);

                int fileNameIndex = 0;
                for (int i = 0; i < totalThumbnail_Rowcount; i++)
                {
                    for (int j = 0; j < totalThumbnail_Celcount; j++)
                    {
                        Debug.WriteLine($"fileNameIndex : {fileNameIndex}");
                        if (fileNameIndex == ThumbnailArrayLength && ThumbnailArrayLength > FILE_CEL_COUNT)
                        {
                            Debug.WriteLine($"i : {i}, j : {j}, break : {fileNameIndex}");
                            break;
                        }

                        using Bitmap drawingBitmap = new($"{ThumbnailArray[fileNameIndex]}");//jpg 불러오기
                        mygraphics.DrawImage(drawingBitmap, j * THUMBNAIL_WEIGHT, i * THUMBNAIL_HEIGHT, THUMBNAIL_WEIGHT, THUMBNAIL_HEIGHT);//좌표값에 그리기
                        fileNameIndex++;
                    }
                }

                string totalThumbnailFilename = ThumbnailArray[0].Replace("_001", "");
                totalThumbnail.Save(totalThumbnailFilename, System.Drawing.Imaging.ImageFormat.Png);

                if (File.Exists(totalThumbnailFilename))
                {
                    foreach (var item in ThumbnailArray)
                    {
                        File.Delete(item);
                    }
                }
            }
        }
    }
}
