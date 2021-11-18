using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Concurrent;

namespace MP4VideoEncoding
{
    public partial class Form1 : Form
    {
        readonly string INIPATH    = Path.Combine(System.Windows.Forms.Application.StartupPath, "setup.ini");
        readonly string FFMPEGPATH = Path.Combine(System.Windows.Forms.Application.StartupPath, "ffmpeg.exe");

        int AllCount = 0;
        int jobCount = 0;
        bool isAutoStart = false;
        readonly HashSet<string> directoryList = new();
        CancellationTokenSource tokenSource = new();

        public Form1(string[] args)
        {
            InitializeComponent();

            if (args.Length >= 2)
            {
                foreach (string arg in args)
                {
                    if (arg.Contains("-autostart", StringComparison.OrdinalIgnoreCase))
                        isAutoStart = true;
                }
            }

            LoadIni();
            SetControlEvent();
            SetUI();
        }

        private void LoadIni()
        {
            IniFile inifile = new();
            if (File.Exists(INIPATH))
            {
                inifile.Load(INIPATH);
                if (!isAutoStart)
                    isAutoStart = inifile["Setup"]["AutoStart"].ToBool(false);
                
                int DirectoriesCount = inifile["Setup"]["Count"].ToInt(0);
                if (DirectoriesCount > 0)
                {
                    for (int i = 0; i < DirectoriesCount; i++)
                    {
                        string iniDirectoryPath = inifile["List"][$"dir{i}"].ToString();
                        if (!string.IsNullOrEmpty(iniDirectoryPath) && Directory.Exists(iniDirectoryPath))
                        {
                            string fullPath = Path.GetFullPath(iniDirectoryPath);
                            if (iniDirectoryPath[^1] == '\\')
                                fullPath = Path.GetFullPath(iniDirectoryPath[0..^1]);
                            directoryList.Add(fullPath);
                        }    
                    }
                }
            }
            else
            {
                inifile["Setup"]["Count"] = 0;
                inifile.Save(INIPATH);
            }
        }

        private void SetUI()
        {
            ExtensionMethods.DoubleBuffered(lv_DirectoryList, true);
            lv_DirectoryList.BeginUpdate();
            lv_DirectoryList.Clear();
            lv_DirectoryList.View = View.Details;
            lv_DirectoryList.GridLines = true;
            lv_DirectoryList.Columns.Add("디렉토리명", 400, HorizontalAlignment.Center);
            lv_DirectoryList.Columns.Add("파일용량", 100, HorizontalAlignment.Center);
            lv_DirectoryList.Columns.Add("파일개수", 120, HorizontalAlignment.Center);

            foreach (var directory in directoryList)
            {
                ListViewItem item = new(directory.Trim());
                item.SubItems.Add("");
                item.SubItems.Add("");
                lv_DirectoryList.Items.Add(item);
            }
            lv_DirectoryList.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            lv_DirectoryList.EndUpdate();

            cb_AutoStart.Checked = isAutoStart;

            lbl_Filename.Text          = "";
            lbl_OriFileSize.Text       = "";
            lbl_OriDuration.Text       = "";
            lbl_AllJobCount.Text       = "0";
            lbl_JobCount.Text          = "0";
            lbl_ConvFileSize.Text      = "";
            lbl_ConvDuration.Text      = "";
            lbl_ConvFrame.Text         = "";
            lbl_ConvSpeed.Text         = "";
            lbl_ConvEstimatedTime.Text = "";
        }

        private void SetControlEvent()
        {
            this.Shown += Form1_Shown;
            this.FormClosed += Form1_FormClosed;
            tokenSource.Token.Register(() => 
            {
                tb_InputDirecotyPath.ReadOnly = false;
                btn_AddDirectoryPath.Enabled  = true;
                btn_SaveDirectories.Enabled   = true;
                btn_StartJob.Enabled          = true;
                btn_EndJob.Enabled            = false;
            });

            btn_AddDirectoryPath.Click += Btn_AddDirectoryPath_Click;
            btn_SaveDirectories.Click += Btn_SaveDirectories_Click;

            btn_StartJob.Click += Btn_StartJob_Click;
            btn_EndJob.Click += Btn_EndJob_Click;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (cb_AutoStart.Checked)
                Btn_StartJob_Click(sender, e);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Btn_EndJob_Click(sender, e);
        }

        private void Btn_AddDirectoryPath_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_InputDirecotyPath.Text) && Directory.Exists(tb_InputDirecotyPath.Text))
            {
                string fullPath = Path.GetFullPath(tb_InputDirecotyPath.Text);
                if (tb_InputDirecotyPath.Text[^1] == '\\')
                    fullPath = Path.GetFullPath(tb_InputDirecotyPath.Text[0..^1]);

                ListViewItem item = new(fullPath);
                item.SubItems.Add("");
                item.SubItems.Add("");

                bool isExists = false;

                if (lv_DirectoryList.Items.Count > 0)
                {
                    for (int i = 0; i < lv_DirectoryList.Items.Count; i++)
                    {
                        if (lv_DirectoryList.Items[i].Text.Equals(fullPath, StringComparison.OrdinalIgnoreCase))
                        {
                            isExists = true;
                            break;
                        }
                    }
                }

                if (!isExists)
                    lv_DirectoryList.Items.Add(item);
            }
            else
            {
                MessageBox.Show("폴더 경로가 존재하지 않습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tb_InputDirecotyPath.Focus();
            }
        }

        private void Btn_SaveDirectories_Click(object sender, EventArgs e)
        {
            try
            {
                IniFile inifile = new();
                inifile.Clear();
                inifile["Setup"]["AutoStart"] = cb_AutoStart.Checked;
                inifile["Setup"]["Count"] = lv_DirectoryList.Items.Count;
                if (lv_DirectoryList.Items.Count > 0)
                    for (int i = 0; i < lv_DirectoryList.Items.Count; i++)
                        inifile["List"][$"dir{i}"] = lv_DirectoryList.Items[i].Text;
                inifile.Save(INIPATH);

                MessageBox.Show("저장 성공", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e1)
            {
                MessageBox.Show($"저장 실패!{Environment.NewLine}{e1.Message}", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void Btn_StartJob_Click(object sender, EventArgs e)
        {
            tb_InputDirecotyPath.ReadOnly = true;
            btn_AddDirectoryPath.Enabled  = false;
            btn_SaveDirectories.Enabled   = false;
            btn_StartJob.Enabled          = false;
            btn_EndJob.Enabled            = true;

            tokenSource                   = new();
            tokenSource.Token.Register(() =>
            {
                tb_InputDirecotyPath.ReadOnly = false;
                btn_AddDirectoryPath.Enabled  = true;
                btn_SaveDirectories.Enabled   = true;
                btn_StartJob.Enabled          = true;
                btn_EndJob.Enabled            = false;
                AllCount                      = 0;
                jobCount                      = 0;
            });
            await StartJob(tokenSource.Token);
        }

        private void Btn_EndJob_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
            
        }

        private async Task StartJob(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (lv_DirectoryList.Items.Count > 0)
                    {
                        HashSet<ListViewItem> listViewItems = new();
                        foreach (ListViewItem item in lv_DirectoryList.Items)
                            listViewItems.Add(item);

                        ConcurrentQueue<MetaData> filelist = await GetJobFileListAsync(listViewItems, tokenSource.Token);

                        if (filelist is not null)
                        {
                            AllCount = filelist.Count;
                            ControlExtensions.InvokeOnUiThreadIfRequired(this, () => { lbl_AllJobCount.Text = $"{AllCount}"; });
                            while (!filelist.IsEmpty)
                            {
                                if (cancellationToken.IsCancellationRequested)
                                    break;

                                if (filelist.TryDequeue(out MetaData metaData))
                                {
                                    await ConvertingMP4Async(metaData, tokenSource.Token);
                                }
                            }
                        }
                    }

                    await Task.Delay(TimeSpan.FromMilliseconds(300), cancellationToken);
                }
            }
            catch (TaskCanceledException tce)
            {
                Debug.WriteLine($"작업 취소, {tce.Message}");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }  
        }

        private async Task<ConcurrentQueue<MetaData>> GetJobFileListAsync(HashSet<ListViewItem> listViewItems, CancellationToken cancellationToken)
        {
            ConcurrentQueue<MetaData> filelist = new();

            foreach (var item in listViewItems)
            {
                var list = Directory.GetFiles(item.Text, $"*.mp4");
                foreach (var file in list)
                {
                    if (cancellationToken.IsCancellationRequested)
                        return null;
                    
                    var metaData = await GetMetaDataAsync(file, cancellationToken);
                    if (metaData is not null)
                        if (metaData.FileFormat.Equals("f4v") && metaData.CompatibleBrands.Equals("isommp42m4v"))
                            filelist.Enqueue(metaData);
                }
            }

            return !filelist.IsEmpty ? filelist : null;
        }

        private async Task<MetaData> GetMetaDataAsync(string fileFullPath, CancellationToken cancellationToken)
        {
            // https://www.adobe.com/content/dam/acom/en/devnet/flv/video_file_format_spec_v10.pdf
            // https://videocube.tistory.com/entry/MP4-%EB%B6%84%EC%84%9D-%ED%95%98%EA%B8%B0-MPEG4-%ED%8C%8C%ED%8A%B8-14

            if (!System.IO.File.Exists(fileFullPath))
                return null;

            MetaData result = new();

            using FileStream fileStream = new(fileFullPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);

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

        private async Task ConvertingMP4Async(MetaData metaData, CancellationToken cancellationToken)
        {
            if (!System.IO.File.Exists(metaData.FullFilename))
                return;
           
            string outputPath = Path.Combine(Path.GetDirectoryName(metaData.FullFilename), "converted_" + metaData.Filename);

            if (!System.IO.Directory.Exists(Path.GetDirectoryName(outputPath)))
                System.IO.Directory.CreateDirectory(Path.GetDirectoryName(outputPath));

            if (System.IO.File.Exists(outputPath))
            {
                var metadata = await GetMetaDataAsync(outputPath, cancellationToken);
                if (metadata is not null)
                {
                    if (metadata.FileFormat.Equals("isom") && metadata.CompatibleBrands.Equals("isomiso2avc1mp41"))
                    {
                        // 리네임
                        Console.WriteLine($"이미 파일이 존재함. {outputPath}");

                        if (AllCount == jobCount)
                        {
                            jobCount = 0;
                            ControlExtensions.InvokeOnUiThreadIfRequired(this, () => { lbl_JobCount.Text = $"{jobCount}"; });
                        }
                        else
                            ControlExtensions.InvokeOnUiThreadIfRequired(this, () => { lbl_JobCount.Text = $"{++jobCount}"; });
                        return;
                    }
                }
            }

            if (!File.Exists(FFMPEGPATH))
            {
                tokenSource.Cancel();
                MessageBox.Show($"{FFMPEGPATH}가 존재하지 않습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using var process = new Process();
            process.StartInfo.FileName = Path.Combine(Application.StartupPath, "ffmpeg.exe");
            process.StartInfo.Arguments = $"-i \"{metaData.FullFilename}\" -threads 0 -movflags faststart \"{outputPath}\"";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.StandardErrorEncoding = Encoding.UTF8;

            process.Start();

            process.StandardInput.AutoFlush = true;
            //process.StandardInput.Close();

            string ffmpeg라인읽기 = string.Empty;
            double 동영상길이 = Convert.ToDouble(metaData.Duration);
            var Duration = TimeSpan.FromSeconds(동영상길이);

            while (!process.StandardError.EndOfStream)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    process.Kill();
                    
                    if (File.Exists(outputPath))
                    {
                        var outputMetaData = await GetMetaDataAsync(outputPath, cancellationToken);
                        if (outputMetaData is not null && outputMetaData.FileFormat.Equals("f4v") && outputMetaData.CompatibleBrands.Equals("isommp42m4v"))
                        {
                            File.Delete(outputPath);
                        }
                    }
                    return;
                }
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
                        int ffmepgFrame   = Convert.ToInt32(combine.Substring(combine.IndexOf("frame=") + 6, combine.IndexOf("fps=") - 6));
                        string ffmepgSize = combine[(combine.IndexOf("size=") + 5)..combine.IndexOf("time=")];
                        string ffmepgTime = combine[(combine.IndexOf("time=") + 5)..combine.IndexOf("bitrate=")][..8];
                        string ffmepgbitrate;
                        if (combine.Contains("dup="))
                            ffmepgbitrate = combine[(combine.IndexOf("bitrate=") + 8)..combine.IndexOf("dup=")];
                        else
                            ffmepgbitrate = combine[(combine.IndexOf("bitrate=") + 8)..combine.IndexOf("speed=")];
                        string speed = combine[(combine.IndexOf("speed=") + 6)..].Replace("x", "");

                        var ffmepgTimes     = ffmepgTime.Split(':');
                        double dConvertTime = (Convert.ToDouble(ffmepgTimes[0]) * 3600) + (Convert.ToDouble(ffmepgTimes[1]) * 60) + (Convert.ToDouble(ffmepgTimes[2]));

                        ControlExtensions.InvokeOnUiThreadIfRequired(this, () =>
                        {
                            lbl_Filename.Text          = $"{metaData.Filename}";
                            lbl_OriFileSize.Text       = $"{Ext.ToPrettySize(Convert.ToInt32(metaData.FileSize), 3)}";
                            lbl_OriDuration.Text       = $"{Duration}";
                            lbl_ConvFileSize.Text      = $"{Ext.ToPrettySize(Convert.ToInt32(ffmepgSize.Replace("Kb", "", StringComparison.OrdinalIgnoreCase)) * 1024, 3)}";;
                            lbl_ConvDuration.Text      = $"{ffmepgTime}";
                            lbl_ConvFrame.Text         = $"{ffmepgFrame}";
                            lbl_ConvSpeed.Text         = $"{speed}x";
                            lbl_ConvEstimatedTime.Text = $"{TimeSpan.FromSeconds(동영상길이 - dConvertTime) / Convert.ToDouble(speed):hh\\:mm\\:ss}";
                        });
                        //Console.WriteLine($"frame={ffmepgFrame}, ConvertingFileSize={ffmepgSize}, bitrate={ffmepgbitrate}, OriTime = {Duration} time={ffmepgTime}, speed={speed}x, Estimated Time Remaining = {TimeSpan.FromSeconds(동영상길이 - dConvertTime) / Convert.ToDouble(speed)}");
                    }
                }
            }

            //process.StartInfo.Arguments = $" -vsync 2 -i \"{metaData.FullFilename}\" -vf \"fps=1,scale=240:-1\" -qscale:v 2  -y \"{outputPath}_%3d.jpg\"";

            //process.Start();

            //process.StandardInput.AutoFlush = true;

            //while (!process.StandardError.EndOfStream)
            //{
            //    Debug.WriteLine(await process.StandardError.ReadLineAsync());
            //}
            //await process.WaitForExitAsync(cancellationToken);

            ControlExtensions.InvokeOnUiThreadIfRequired(this, () => { lbl_JobCount.Text = $"{++jobCount}"; });
        }
    }

    public static class ExtensionMethods
    {
        public static void DoubleBuffered(this Control ctl, bool setting) => ctl.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(ctl, setting, null);
    }

    public static class ControlExtensions
    {
        /// <summary>
        /// Executes the Action asynchronously on the UI thread, does not block execution on the calling thread.
        /// </summary>
        /// <param name="control">the control for which the update is required</param>
        /// <param name="action">action to be performed on the control</param>
        public static void InvokeOnUiThreadIfRequired(this Control control, Action action)
        {
            //If you are planning on using a similar function in your own code then please be sure to
            //have a quick read over https://stackoverflow.com/questions/1874728/avoid-calling-invoke-when-the-control-is-disposed
            //No action
            if (control.Disposing || control.IsDisposed || !control.IsHandleCreated)
                return;

            if (control.InvokeRequired)
                control.BeginInvoke(action);
            else
                action.Invoke();
        }
    }

    public static class Ext
    {
        private const long OneKb = 1024;
        private const long OneMb = OneKb * 1024;
        private const long OneGb = OneMb * 1024;
        private const long OneTb = OneGb * 1024;

        public static string ToPrettySize(this int value, int decimalPlaces = 0) => ((ulong)value).ToPrettySize(decimalPlaces);
        public static string ToPrettySize(this long value, int decimalPlaces = 0) => ((ulong)value).ToPrettySize(decimalPlaces);

        public static string ToPrettySize(this ulong value, int decimalPlaces = 0)
        {
            var asTb = Math.Round((double)value / OneTb, decimalPlaces);
            var asGb = Math.Round((double)value / OneGb, decimalPlaces);
            var asMb = Math.Round((double)value / OneMb, decimalPlaces);
            var asKb = Math.Round((double)value / OneKb, decimalPlaces);

            string chosenValue = asTb > 1 ? $"{asTb}Tb"
                : asGb > 1 ? $"{asGb}Gb"
                : asMb > 1 ? $"{asMb}Mb"
                : asKb > 1 ? $"{asKb}Kb"
                : string.Format("{0}B", Math.Round((double)value, decimalPlaces));

            return chosenValue;
        }
    }
}
