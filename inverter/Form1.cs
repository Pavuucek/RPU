using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var invertedList = new List<short>();
            listBox1.Items.Clear();
            button1.Text = short.MaxValue.ToString();
            using (var reader = new BinaryReader(File.Open(@"d:\xxx\wh", FileMode.OpenOrCreate)))
            {
                using (var writer = new BinaryWriter(File.Open(@"d:\xxx\wh.correct", FileMode.OpenOrCreate)))
                {
                    writer.Seek(0, SeekOrigin.Begin);
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        var i = reader.ReadInt16();
                        int i2;
                        //if (i < 0) i2 = i + short.MaxValue; // ne takhle to je blbe
                        //else i2 = i - short.MaxValue;
                        i2 = i*-1; // obratit znaminko
                        var i3 = Convert.ToInt16(i2);
                        invertedList.Add(i3);
                        listBox1.Items.Add(i + ":" + i2 + ":" + i3);
                        //writer.Write(i3);
                    }
                    button2.Text = listBox1.Items.Count.ToString();
                    for (int i4 = invertedList.Count-1; i4 >= 0; i4--)
                    {
                        // zapsat soubor LIFO
                        writer.Write(invertedList[i4]);
                    }
                    
                }
            }

            MessageBox.Show("a");
        }
    }
}