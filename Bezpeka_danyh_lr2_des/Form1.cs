using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Windows.Forms;

namespace Bezpeka_danyh_lr2_des
{
    public partial class Form1 : Form
    {
        static byte[] bytes;
        protected string toScript;
        protected string fromScript;
        public Form1()
        {
            InitializeComponent();
            toScript = "";
            fromScript = "";
            bytes = ASCIIEncoding.ASCII.GetBytes("keywords");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            toScript = textBox1.Text;
            fromScript = Encrypt(toScript);
            textBox3.Text = "";
            textBox3.Text = fromScript;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string result = Decrypt(textBox2.Text);
            textBox4.Text = "";
            textBox4.Text = result;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0)
                button1.Enabled = true;
            else
                button1.Enabled = false;
            textBox3.Clear();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length != 0)
                button2.Enabled = true;
            else
                button2.Enabled = false;
            textBox4.Clear();
        }

        public static string Encrypt(string originalString)
        {
            if (String.IsNullOrEmpty(originalString))
            {
                throw new ArgumentNullException
                       ("The string which needs to be encrypted can not be null.");
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cryptoStream);
            writer.Write(originalString);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();
            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }

        public static string Decrypt(string cryptedString)
        {
            if (String.IsNullOrEmpty(cryptedString))
            {
                throw new ArgumentNullException
                   ("The string which needs to be decrypted can not be null.");
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cryptedString));
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);
            return reader.ReadToEnd();
        }
    }
}
