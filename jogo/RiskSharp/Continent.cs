using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskSharp
{
    public class Continent
    {
        private string name;
        public string Name
        {
            get { return this.name; }
        }

        private int reinforcementArmies;
        public int ReinforcementArmies
        {
            get { return this.reinforcementArmies; }
        }

        private ReadOnlyCollection<Territory> territories;
        public ReadOnlyCollection<Territory> Territories
        {
            get { return this.territories; }
        }

        public Continent(string name, int reinforcementArmies, IList<Territory> territories)
        {
            this.name = name;
            this.reinforcementArmies = reinforcementArmies;
            this.territories = new ReadOnlyCollection<Territory>(territories);
        }

        public readonly static Continent NorthAmerica = new Continent("North America", 5, new Territory[] { Territory.Alaska, Territory.Alberta, Territory.CentralAmerica, Territory.EasternUSA, Territory.Greenland, Territory.NorthwestTerritory, Territory.Ontario, Territory.Quebec, Territory.WesternUSA });
        public readonly static Continent SouthAmerica = new Continent("South America", 2, new Territory[] { Territory.Argentina, Territory.Brazil, Territory.Peru, Territory.Venezuela });
        public readonly static Continent Europe = new Continent("Europe", 5, new Territory[] { Territory.GreatBritain, Territory.Iceland, Territory.NorthernEurope, Territory.Scandinavia, Territory.SouthernEurope, Territory.Ukraine, Territory.WesternEurope });
        public readonly static Continent Africa = new Continent("Africa", 3, new Territory[] { Territory.Congo, Territory.EastAfrica, Territory.Egypt, Territory.Madagascar, Territory.NorthAfrica, Territory.SouthAfrica });
        public readonly static Continent Asia = new Continent("Asia", 7, new Territory[] { Territory.Afghanistan, Territory.China, Territory.India, Territory.Irkutsk, Territory.Japan, Territory.Kamchatka, Territory.MiddleEast, Territory.Mongolia, Territory.Siam, Territory.Siberia, Territory.Ural, Territory.Yakutsk });
        public readonly static Continent Australia = new Continent("Australia", 2, new Territory[] { Territory.EasternAustralia, Territory.India, Territory.NewGuinea, Territory.WesternAustralia });
    }
}
