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

        public override void AttackTerritory()
        {
            Territory t1 = null;
            do
            {
                t1 = this.Game.ReadTerritoryByController(this);
            }
            while (t1 == null || t1.Armies < 2);
            //TODO: colocar label com informações

            Territory t2 = null;
            do
            {
                t2 = this.Game.ReadTerritory();
            }
            while (t2 == null || t2.Controller == this || !t1.AdjacentTerritories.Contains(t2));

            int armiesToAttack = this.Game.ReadNumberOfArmies(t1.Armies - 1);
            int armiesToDefend = t2.Armies;

            int diceAttack = armiesToAttack > 3 ? 3 : armiesToAttack;
            int diceDefend = armiesToDefend > 3 ? 3 : armiesToDefend;

            Die[] diceAtk = new Die[diceAttack];
            Die[] diceDef = new Die[diceDefend];

            for (int i = 0; i < diceAttack; i++)
            {
                diceAtk[i] = new Die();
                diceAtk[i].Roll();
            }

            for (int i = 0; i < diceDefend; i++)
            {
                diceDef[i] = new Die();
                diceDef[i].Roll();
            }

            diceAtk = diceAtk.OrderByDescending(d => d.Result).ToArray<Die>();
            diceDef = diceDef.OrderByDescending(d => d.Result).ToArray<Die>();

            for (int i = 0; i < Math.Min(diceAttack, diceDefend); i++)
            {
                if (diceAtk[i].Result > diceDef[i].Result)
                {
                    t2.Armies--;
                }
                else
                {
                    t1.Armies--;
                }
                this.Game.UpdateLabelInfo(t1);
                this.Game.UpdateLabelInfo(t2);
                if(t2.Armies == 0)
                {
                    t2.Controller.ControlledTerritories.Remove(t2);
                    t2.Controller = this;
                    this.ControlledTerritories.Add(t2);
                    t2.Armies++;
                    t1.Armies--;
                    return;
                }
            }
        }
    }
}
