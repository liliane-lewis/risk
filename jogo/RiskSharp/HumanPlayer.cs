using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RiskSharp
{
    public class HumanPlayer : Player
    {
        public HumanPlayer(string name, Game game) : base(name, game) { }

        public override void PlaceArmyOnUncontrolledTerritory()
        {
            Territory t = null;
            do
            {
                t = this.Game.ReadTerritoryByController(null);
            }
            while (t == null);

            t.Controller = this;
            this.ControlledTerritories.Add(t);
            t.Armies++;
            this.AvailableArmies--;
            this.Game.UpdateLabelInfo(t);
        }

        public override void PlaceArmyOnOwnTerritory()
        {

            Territory t = null;
            do
            {
                t = this.Game.ReadTerritoryByController(this);
            }
            while (t == null);

            t.Armies++;
            this.AvailableArmies--;
            this.Game.UpdateLabelInfo(t);
        }
    }
}
