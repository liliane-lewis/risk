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
    public partial class Map : Form
    {
        public Map()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Bitmap b = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
            pictureBox1.DrawToBitmap(b, pictureBox1.ClientRectangle);
            Color c = b.GetPixel(e.X, e.Y);

            var t = Territory.GetTerritoryByColor(c);
            if (t == null)
                MessageBox.Show("No territory selected!");
            else
            {
                StringBuilder sb = new StringBuilder(String.Format("{0} ({1})", t.Name, t.Continent.Name));
                sb.Append(System.Environment.NewLine);
                sb.Append("Neighbours:");
                sb.Append(System.Environment.NewLine);
                foreach(Territory n in t.AdjacentTerritories)
                {
                    sb.Append("* " + n.Name);
                    sb.Append(System.Environment.NewLine);
                }
                MessageBox.Show(sb.ToString());
            }
        }
    }
}
