using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace md5
{
    public partial class Form1 : Form
    {
        static List<char> СписокСимволов=new List<char>();
        static char firstChar;
        static char lastChar;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = md5(textBox1.Text);
        }

        public string md5(string pass)
        {
            MD5 _md5 = new MD5CryptoServiceProvider();
            byte[] checkSum = _md5.ComputeHash(Encoding.UTF8.GetBytes(pass));
            string result = BitConverter.ToString(checkSum).Replace("-", String.Empty);
            return result;
        }

        private string next(string str)
        {
            char[] ar = new char[str.Length];
            int end = str.Length - 1;
            for (int i = 0; i < str.Length; i++)
            {
                ar[i] = str[i];
            }
            
            while (end >= 0)
            {
                if (ar[end] == lastChar)
                {
                    ar[end] = firstChar;
                    end--;
                }
                else
                {
                    ar[end] = СписокСимволов[СписокСимволов.IndexOf(ar[end]) + 1];
                    break;
                }
            }

            str = String.Concat<char>(ar);

            if (end == -1) { str = firstChar + str; }

            return str;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int from=Convert.ToInt32(textBox5.Text);
            int to=Convert.ToInt32(textBox6.Text);

            string pass="";
            string finish="";
            string md5_pass="";
            string md5_password=textBox3.Text;

 
            if (checkBox3.Checked == true) // 0..9
            {
                for (int код = 48; код <= 57; код++)
                {
                    СписокСимволов.Add((char)код);
                }
            }
            if (checkBox2.Checked == true) //A...Z
            {
                for (int код = 65; код <= 90; код++)
                {
                    СписокСимволов.Add((char)код);
                }
            }
            if (checkBox1.Checked == true) // a..z
            {
                for (int код = 97; код <= 122; код++)
                {
                    СписокСимволов.Add((char)код);
                }
            }
            //richtextbox1
            string simbols=richTextBox1.Text;
            if (simbols != "") 
            {
                 foreach (char c in simbols) { СписокСимволов.Add(c); }
            }

            firstChar = СписокСимволов[0];
            lastChar = СписокСимволов[СписокСимволов.Count-1];

            for (int i = 1; i <= from; i++) 
            {
                pass+=firstChar;
            }
            for (int i = 1; i <= to; i++) 
            {
                finish+=lastChar;
            }
            while (pass != finish) 
            {
                md5_pass=md5(pass);
                if (md5_pass == md5_password) 
                {
                    textBox4.Text=pass;
                    break;
                }
                pass=next(pass);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string md5_password = textBox7.Text;
            System.IO.StreamReader sr = new System.IO.StreamReader("English.txt", Encoding.Default);
            string line = null;
            while ((line = sr.ReadLine()) != null)
            {
                if (md5(line) == md5_password)
                {
                    textBox8.Text = line;
                    return;
                }
            }
            MessageBox.Show("not found!");
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            /*int from=Convert.ToInt32(textBox10.Text);
            int to=Convert.ToInt32(textBox9.Text);

            if (checkBox4.Checked==true) 
            {
            }
           // File.WriteAllText("");*/
        }

        public class Pair
        {
            string md5;
            string pass;

            public Pair(string md5, string pass) 
            {
                this.md5=md5;
                this.pass=pass;
            }
        }
    }
}
    

