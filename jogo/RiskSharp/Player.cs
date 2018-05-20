using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskSharp
{
    public abstract class Player
    {
        private string name;
        public string Name
        {
            get { return this.name; }
        }

        private IList<Territory> controlledTerritories;
        public IList<Territory> ControlledTerritories
        {
            get { return this.controlledTerritories; }
        }

        private int availableArmies;
        public int AvailableArmies
        {
            get { return this.availableArmies; }
            set { this.availableArmies = value; }
        }

        private Game game;
        public Game Game
        {
            get { return this.game; }
        }

        public int TotalArmies
        {
            get
            {
                int total = availableArmies;
                foreach(Territory t in controlledTerritories)
                {
                    total += t.Armies;
                }
                return total;
            }
        }

        public Player(string name, Game game)
        {
            this.name = name;
            this.controlledTerritories = new List<Territory>(Territory.Territories.Count);
            this.availableArmies = 35;
            this.game = game;
        }

        public abstract void PlaceArmyOnUncontrolledTerritory();
        public abstract void PlaceArmyOnOwnTerritory();
        public abstract void AttackTerritory();
    }
}
