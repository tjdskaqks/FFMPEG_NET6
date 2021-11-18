using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP4VideoEncoding
{
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
