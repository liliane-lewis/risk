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
        private Game game;
        private Territory selectedTerritory = null;
        public Map()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            /*if (!isWaitingMouseClick)
            {
                this.selectedTerritory = null;
                return;
            }*/
            Bitmap b = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
            pictureBox1.DrawToBitmap(b, pictureBox1.ClientRectangle);
            Color c = b.GetPixel(e.X, e.Y);

            var t = Territory.GetTerritoryByColor(c);
            this.selectedTerritory = t;
            /*if (t == null)
                MessageBox.Show("No territory selected!");
            else
            {
                StringBuilder sb = new StringBuilder(String.Format("{0} ({1})", t.Name, t.Continent.Name));
                sb.Append(System.Environment.NewLine);
                sb.Append("Neighbours:");
                sb.Append(System.Environment.NewLine);
                foreach (Territory n in t.AdjacentTerritories)
                {
                    sb.Append("* " + n.Name);
                    sb.Append(System.Environment.NewLine);
                }
                MessageBox.Show(sb.ToString());
            }*/
        }

        public string ReadPlayerName(string playerNumber)
        {
            this.Hide();
            bool nomeValido = false;
            NewPlayerForm npf = new NewPlayerForm(playerNumber);
            do
            {
                npf.ShowDialog();
                nomeValido = npf.SelectedName != null && npf.SelectedName != string.Empty;
                if (!nomeValido)
                    MessageBox.Show("Invalid name!");
            }
            while (!nomeValido);
            this.Show();
            return npf.SelectedName;
        }

        public Territory ReadTerritory()
        {
            return this.selectedTerritory;
        }

        public void WriteCurrentPlayerInfo(Player p)
        {
            Invoke(new Action(() =>
            {
                this.lblPlayer.Text = "Current player: " + p.Name;
                this.lblAvailable.Text = "Available armies: " + p.AvailableArmies;
            }));
        }

        public void SetLabelText(Label l, string text)
        {
            Invoke(new Action(() => 
            {
                l.Text = text;
            }));
        }

        private void Map_Load(object sender, EventArgs e)
        {
            Application.DoEvents();
            game = new Game(this);
        }
    }
}
