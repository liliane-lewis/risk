using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RiskSharp
{
    public partial class NewPlayerForm : Form
    {
        public string SelectedName = string.Empty;
        public NewPlayerForm(string playerNumber)
        {
            InitializeComponent();
            this.label1.Text = String.Format(this.label1.Text, playerNumber);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.SelectedName = textBox1.Text;
            textBox1.Clear();
            this.Close();
        }
    }
}
