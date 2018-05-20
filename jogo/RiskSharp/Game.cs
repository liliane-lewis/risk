using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RiskSharp
{
    public class Game
    {
        private Map mapForm;
        private Player player1, player2, neutral;
        private ReadOnlyCollection<Player> players;

        private Player currentPlayer;
        public Player CurrentPlayer
        {
            get { return this.currentPlayer; }
            set
            { this.currentPlayer = value; }
        }

        public Game(Map mapForm)
        {
            this.mapForm = mapForm;
            this.InitializeLabels();
            string p1Name = mapForm.ReadPlayerName("1");
            string p2Name = mapForm.ReadPlayerName("2");

            this.player1 = new HumanPlayer(p1Name, this);
            this.player2 = new HumanPlayer(p2Name, this);
            this.neutral = new NeutralPlayer("Neutral", this);
            this.players = new ReadOnlyCollection<Player>(new Player[] { player1, player2, neutral });
            this.Draft();
        }

        private void Draft()
        {
            var bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler((sender, e) =>
            {
                int i;
                for (i = 0; i < Territory.Territories.Count; i++)
                {
                    this.mapForm.WriteCurrentPlayerInfo(players[i % players.Count]);
                    this.CurrentPlayer = players[i % players.Count];
                    players[i % players.Count].PlaceArmyOnUncontrolledTerritory();                    
                }

                i = 0;

                do
                {
                    this.mapForm.WriteCurrentPlayerInfo(players[i % players.Count]);
                    players[i % players.Count].PlaceArmyOnOwnTerritory();                    
                    i++;
                }
                while (player1.AvailableArmies > 0 && player2.AvailableArmies > 0 && neutral.AvailableArmies > 0);
            });
            bw.RunWorkerAsync();

        }

        public Territory ReadTerritoryByController(Player controller)
        {
            Territory t;

            do
            {
                t = this.mapForm.ReadTerritory();
            }
            while (t == null || t.Controller != controller);

            return t;
        }

        public void UpdateLabelInfo(Territory t)
        {
            string playerString;
            if (t.Controller == player1)
                playerString = "P1";
            else if (t.Controller == player2)
                playerString = "P2";
            else if (t.Controller == neutral)
                playerString = "N";
            else
                playerString = string.Empty;

            this.mapForm.SetLabelText(t.Label, playerString + System.Environment.NewLine + t.Armies.ToString());
        }

        private void InitializeLabels()
        {
            foreach (Control c in this.mapForm.Controls)
            {
                if (!(c is Label))
                    continue;
                var l = c as Label;
                foreach (Territory t in Territory.Territories)
                {
                    if (l.Name.Substring(3) == t.Name.Replace(" ", string.Empty))
                    {
                        t.Label = l;
                        l.Text = string.Empty;
                        l.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                        break;
                    }
                }
            }
        }
    }
}
