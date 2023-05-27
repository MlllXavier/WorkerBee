using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace WorkerBee
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            webBrowser1.IsWebBrowserContextMenuEnabled = false;
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.AllowWebBrowserDrop = false;
            webBrowser1.Navigate("http://y.youhui.in/");
        }
    }
}
