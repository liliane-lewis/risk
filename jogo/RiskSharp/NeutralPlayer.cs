using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskSharp
{
    public class NeutralPlayer : Player
    {
        public NeutralPlayer(string name, Game game) : base(name, game) { }

        public override void PlaceArmyOnUncontrolledTerritory()
        {
            Random rnd = new Random();
            int i;
            do
            {
                i = rnd.Next(0, Territory.Territories.Count);
            }
            while (Territory.Territories[i].Controller != null);
            Territory.Territories[i].Controller = this;
            this.ControlledTerritories.Add(Territory.Territories[i]);
            Territory.Territories[i].Armies++;
            this.AvailableArmies--;
            this.Game.UpdateLabelInfo(Territory.Territories[i]);
        }

        public override void PlaceArmyOnOwnTerritory()
        {
            Random rnd = new Random();
            int i = rnd.Next(0, this.ControlledTerritories.Count);
            this.ControlledTerritories[i].Armies++;
            this.AvailableArmies--;
            this.Game.UpdateLabelInfo(this.ControlledTerritories[i]);
        }
        public override void AttackTerritory()
        {
            return;
        }
    }
}
