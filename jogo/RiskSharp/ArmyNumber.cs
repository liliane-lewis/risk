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
    public partial class ArmyNumber : Form
    {
        public int Number;
        public ArmyNumber()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(textBox1.Text, out this.Number))
            {
                this.textBox1.Clear();
                this.Close();
            }

        }
    }
}
