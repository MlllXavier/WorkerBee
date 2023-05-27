using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkerBee
{
    public partial class Form2 : Form
    {
        Button b;
        public Form2(Button button)
        {
            InitializeComponent();
            b = button;
            b.Enabled = false;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            b.Enabled = true;
        }
    }
}
