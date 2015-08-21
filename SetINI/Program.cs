using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SetINIs
{
    class Program
    {
        public static string LoadFileToString(string FileName)
        {
            StreamReader streamReader = new StreamReader(FileName);
            string text = streamReader.ReadToEnd();
            streamReader.Close();
            return text;
        }

        static void Main(string[] args)
        {
            string[] INIFiles = Directory.GetFiles(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "*.ini", SearchOption.AllDirectories);
            foreach (string ini in INIFiles)
            {
                string Contents = LoadFileToString(ini);
                StringBuilder sw=new StringBuilder();
                using (StringReader reader = new StringReader(Contents))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains("GameLanguageOverride="))
                        {
                            line = "GameLanguageOverride=CZE";
                        }
                        else if((line.Contains("Language="))&(!line.Contains("bAllowMatureLanguage=")))
                        {
                            line="Language=CZE";
                        }
                        sw.AppendLine(line);
                    }
                }
                using (StreamWriter output = new StreamWriter(ini,false))
                {
                    output.Write(sw.ToString());
                }
                Console.WriteLine(ini);
            }
            Console.Write("FINISHED!");
            Console.ReadKey();
        }
    }
}
