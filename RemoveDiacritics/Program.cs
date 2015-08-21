using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoveDiacritics
{
    class Program
    {
        public static void Usage()
        {
            Console.WriteLine("Usage: RemoveDiacritics.exe *.CZE");
            Console.WriteLine("press any key...");
            Console.ReadKey();
        }


        public static string RemoveDiacritics(String s)
        {
            // oddělení znaků od modifikátorů (háčků, čárek, atd.)
            s = s.Normalize(System.Text.NormalizationForm.FormD);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < s.Length; i++)
            {
                // do řetězce přidá všechny znaky kromě modifikátorů
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(s[i]) != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(s[i]);
                }
            }

            // vrátí řetězec bez diakritiky
            return sb.ToString();
        }
        public static string LoadFileToString(string FileName)
        {
            StreamReader streamReader = new StreamReader(FileName);
            string text = streamReader.ReadToEnd();
            streamReader.Close();
            return text;
        }

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Usage();
                return;
            }
            string[] arrFiles = Directory.GetFiles(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), args[0], SearchOption.AllDirectories);
            foreach (string singleFile in arrFiles)
            {
                string Contents = LoadFileToString(singleFile);
                Contents = RemoveDiacritics(Contents);
                using (StreamWriter output = new StreamWriter(singleFile,false))
                {
                    output.Write(Contents);
                }
                Console.WriteLine(singleFile);
            }
            Console.WriteLine("FINISHED!");
            Console.ReadKey();
        }
    }
}
