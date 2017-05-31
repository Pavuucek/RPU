using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProcessM3u
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var i = File.ReadAllText("aplaylist.m3u");
            var lines = Regex.Split(i, "\r\n|\r|\n");
            var aname = string.Empty;
            var sb = new StringBuilder();
            foreach (var line in lines)
            {
                if (line.StartsWith("#EXTINF:-1,"))
                {
                    aname = line.Replace("#EXTINF:-1,", string.Empty);
                    sb.AppendLine(line);
                }
                else if (line.StartsWith("http://"))
                {
                    var s = line.Replace("http://",
                        string.Format(
                            "pipe:///usr/bin/avconv -loglevel fatal -metadata service_name={0} -vcodec copy -acodec copy -f mpegts pipe:1 -i $$H$$",
                            aname));
                    sb.AppendLine(s.Replace("$$H$$", "http://"));
                }
                else sb.AppendLine(line);
            }
            File.WriteAllText("playlist.m3u",sb.ToString());

        }
    }
}
