using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Drawing;

namespace RiskSharp
{
    public class Territory
    {
        #region Color Dictionary
        private static readonly Dictionary<Int32, Territory> dict = new Dictionary<Int32, Territory>(42);
        private static bool dictIsInitialized = false;
        private static void InitializeDictionary()
        {
            //NORTH AMERICA
            dict.Add(Color.FromArgb(128, 128, 0).ToArgb(), Alaska);
            dict.Add(Color.FromArgb(255, 255, 0).ToArgb(), Alberta);
            dict.Add(Color.FromArgb(255, 255, 128).ToArgb(), CentralAmerica);
            dict.Add(Color.FromArgb(131, 131, 0).ToArgb(), EasternUSA);
            dict.Add(Color.FromArgb(250, 250, 0).ToArgb(), Greenland);
            dict.Add(Color.FromArgb(90, 90, 39).ToArgb(), NorthwestTerritory);
            dict.Add(Color.FromArgb(148, 148, 73).ToArgb(), Ontario);
            dict.Add(Color.FromArgb(255, 255, 110).ToArgb(), Quebec);
            dict.Add(Color.FromArgb(80, 80, 39).ToArgb(), WesternUSA);

            //SOUTH AMERICA
            dict.Add(Color.FromArgb(255, 0, 0).ToArgb(), Argentina);
            dict.Add(Color.FromArgb(128, 64, 64).ToArgb(), Brazil);
            dict.Add(Color.FromArgb(128, 0, 0).ToArgb(), Peru);
            dict.Add(Color.FromArgb(255, 128, 128).ToArgb(), Venezuela);

            //EUROPE
            dict.Add(Color.FromArgb(0, 64, 128).ToArgb(), GreatBritain);
            dict.Add(Color.FromArgb(0, 0, 255).ToArgb(), Iceland);
            dict.Add(Color.FromArgb(0, 0, 240).ToArgb(), NorthernEurope);
            dict.Add(Color.FromArgb(0, 128, 255).ToArgb(), Scandinavia);
            dict.Add(Color.FromArgb(0, 58, 132).ToArgb(), SouthernEurope);
            dict.Add(Color.FromArgb(0, 0, 128).ToArgb(), Ukraine);
            dict.Add(Color.FromArgb(0, 130, 240).ToArgb(), WesternEurope);

            //AFRICA
            dict.Add(Color.FromArgb(174, 87, 0).ToArgb(), Congo);
            dict.Add(Color.FromArgb(255, 128, 0).ToArgb(), EastAfrica);
            dict.Add(Color.FromArgb(128, 64, 0).ToArgb(), Egypt);
            dict.Add(Color.FromArgb(166, 93, 0).ToArgb(), Madagascar);
            dict.Add(Color.FromArgb(255, 145, 91).ToArgb(), NorthAfrica);
            dict.Add(Color.FromArgb(135, 71, 0).ToArgb(), SouthAfrica);

            //ASIA
            dict.Add(Color.FromArgb(128, 255, 128).ToArgb(), Afghanistan);
            dict.Add(Color.FromArgb(0, 128, 64).ToArgb(), China);
            dict.Add(Color.FromArgb(0, 128, 128).ToArgb(), India);
            dict.Add(Color.FromArgb(128, 255, 0).ToArgb(), Irkutsk);
            dict.Add(Color.FromArgb(117, 236, 73).ToArgb(), Japan);
            dict.Add(Color.FromArgb(0, 147, 65).ToArgb(), Kamchatka);
            dict.Add(Color.FromArgb(0, 128, 0).ToArgb(), MiddleEast);
            dict.Add(Color.FromArgb(0, 64, 0).ToArgb(), Mongolia);
            dict.Add(Color.FromArgb(149, 242, 140).ToArgb(), Siam);
            dict.Add(Color.FromArgb(0, 151, 31).ToArgb(), Siberia);
            dict.Add(Color.FromArgb(51, 85, 13).ToArgb(), Ural);
            dict.Add(Color.FromArgb(0, 105, 61).ToArgb(), Yakutsk);

            //AUSTRALIA
            dict.Add(Color.FromArgb(64, 0, 64).ToArgb(), EasternAustralia);
            dict.Add(Color.FromArgb(128, 0, 255).ToArgb(), Indonesia);
            dict.Add(Color.FromArgb(255, 0, 255).ToArgb(), NewGuinea);
            dict.Add(Color.FromArgb(128, 0, 64).ToArgb(), WesternAustralia);
            dictIsInitialized = true;
        }
        #endregion

        private string name;
        public string Name
        {
            get { return this.name; }
        }

        private ReadOnlyCollection<Territory> adjacentTerritories = null;
        public ReadOnlyCollection<Territory> AdjacentTerritories
        {
            get
            {
                if (!adjacentTerritoriesAreInitialized)
                    InitializeAdjacentTerritories();
                return this.adjacentTerritories;
            }
        }

        private Continent continent;
        public Continent Continent
        {
            get { return this.continent; }
        }

        private int armies;
        public int Armies
        {
            get { return this.armies; }
            set { this.armies = value; }
        }

        public static Territory GetTerritoryByColor(Color c)
        {
            if (!dictIsInitialized)
                InitializeDictionary();

            Territory t = null;
            dict.TryGetValue(c.ToArgb(), out t);
            return t;
        }

        private Territory(string name, Continent continent)
        {
            this.name = name;
            this.continent = continent;
            this.armies = 0;
        }

        #region InitializeAdjacentTerritories
        private static bool adjacentTerritoriesAreInitialized = false;
        private static void InitializeAdjacentTerritories()
        {
            if (adjacentTerritoriesAreInitialized)
                return;

            //NORTH AMERICA
            Alaska.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { Alberta, Greenland, Kamchatka });
            Alberta.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { Alaska, NorthwestTerritory, Ontario, Quebec });
            CentralAmerica.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { EasternUSA, WesternUSA, Venezuela });
            EasternUSA.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { CentralAmerica, Ontario, Quebec, WesternUSA });
            Greenland.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { NorthwestTerritory, Ontario, Quebec, Iceland });
            NorthwestTerritory.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { Alaska, Alberta, Greenland, Ontario });
            Ontario.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { Alberta, EasternUSA, Greenland, NorthwestTerritory, Quebec, WesternUSA });
            Quebec.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { EasternUSA, Greenland, Ontario });
            WesternUSA.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { Alberta, CentralAmerica, EasternUSA });

            //SOUTH AMERICA
            Argentina.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { Brazil, Peru });
            Brazil.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { Argentina, Peru, Venezuela, NorthAfrica });
            Peru.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { Argentina, Brazil, Venezuela });
            Venezuela.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { Brazil, Peru, CentralAmerica });

            //EUROPE
            GreatBritain.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { Iceland, NorthernEurope, Scandinavia, WesternEurope });
            Iceland.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { GreatBritain, Scandinavia, Alaska });
            NorthernEurope.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { GreatBritain, Scandinavia, SouthernEurope, Ukraine, WesternEurope });
            Scandinavia.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { GreatBritain, Iceland, NorthernEurope, Ukraine });
            SouthernEurope.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { NorthernEurope, Ukraine, WesternEurope, MiddleEast, Egypt, NorthAfrica });
            Ukraine.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { NorthernEurope, Scandinavia, SouthernEurope, Afghanistan, MiddleEast, Ural });
            WesternEurope.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { GreatBritain, NorthernEurope, SouthernEurope, NorthAfrica });

            //AFRICA
            Congo.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { EastAfrica, NorthAfrica, SouthAfrica });
            EastAfrica.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { Congo, Egypt, Madagascar, NorthAfrica, SouthAfrica, MiddleEast });
            Egypt.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { EastAfrica, NorthAfrica, MiddleEast, SouthernEurope });
            Madagascar.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { EastAfrica, SouthAfrica });
            NorthAfrica.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { Congo, EastAfrica, Egypt, SouthAfrica, SouthernEurope, WesternEurope, Brazil });
            SouthAfrica.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { Congo, EastAfrica, Madagascar });

            //ASIA
            Afghanistan.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { China, India, MiddleEast, Ural, Ukraine });
            China.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { Afghanistan, India, Mongolia, Siam });
            India.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { Afghanistan, China, MiddleEast, Siam });
            Irkutsk.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { Kamchatka, Mongolia, Siberia, Yakutsk });
            Japan.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { Kamchatka, Mongolia });
            Kamchatka.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { Irkutsk, Japan, Mongolia, Yakutsk, Alaska });
            MiddleEast.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { Afghanistan, India, Ukraine, SouthernEurope, Egypt, EastAfrica });
            Mongolia.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { China, Irkutsk, Japan, Kamchatka, Siberia });
            Siam.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { China, India, Indonesia });
            Siberia.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { Irkutsk, Mongolia, Ural, Yakutsk });
            Ural.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { Afghanistan, China, Siberia, Ukraine });
            Yakutsk.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { Irkutsk, Kamchatka, Siberia });

            //AUSTRALIA
            EasternAustralia.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { Indonesia, NewGuinea, WesternAustralia });
            Indonesia.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { NewGuinea, WesternAustralia, Siam });
            NewGuinea.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { EasternAustralia, Indonesia, WesternAustralia });
            WesternAustralia.adjacentTerritories = new ReadOnlyCollection<Territory>(new Territory[] { EasternAustralia, Indonesia, NewGuinea });
            adjacentTerritoriesAreInitialized = true;
        }
        #endregion

        #region Territories
        #region North America
        public readonly static Territory Alaska = new Territory("Alaska", Continent.NorthAmerica);
        public readonly static Territory Alberta = new Territory("Alberta", Continent.NorthAmerica);
        public readonly static Territory CentralAmerica = new Territory("Central America", Continent.NorthAmerica);
        public readonly static Territory EasternUSA = new Territory("Eastern United States", Continent.NorthAmerica);
        public readonly static Territory Greenland = new Territory("Greenland", Continent.NorthAmerica);
        public readonly static Territory NorthwestTerritory = new Territory("Northwest Territory", Continent.NorthAmerica);
        public readonly static Territory Ontario = new Territory("Ontario", Continent.NorthAmerica);
        public readonly static Territory Quebec = new Territory("Quebec", Continent.NorthAmerica);
        public readonly static Territory WesternUSA = new Territory("Western United States", Continent.NorthAmerica);
        #endregion

        #region South America
        public readonly static Territory Argentina = new Territory("Argentina", Continent.SouthAmerica);
        public readonly static Territory Brazil = new Territory("Brazil", Continent.SouthAmerica);
        public readonly static Territory Peru = new Territory("Peru", Continent.SouthAmerica);
        public readonly static Territory Venezuela = new Territory("Venezuela", Continent.SouthAmerica);
        #endregion

        #region Europe
        public readonly static Territory GreatBritain = new Territory("Great Britain", Continent.Europe);
        public readonly static Territory Iceland = new Territory("Iceland", Continent.Europe);
        public readonly static Territory NorthernEurope = new Territory("Northern Europe", Continent.Europe);
        public readonly static Territory Scandinavia = new Territory("Scandinavia", Continent.Europe);
        public readonly static Territory SouthernEurope = new Territory("Southern Europe", Continent.Europe);
        public readonly static Territory Ukraine = new Territory("Ukraine", Continent.Europe);
        public readonly static Territory WesternEurope = new Territory("Western Europe", Continent.Europe);
        #endregion

        #region Africa
        public readonly static Territory Congo = new Territory("Congo", Continent.Africa);
        public readonly static Territory EastAfrica = new Territory("East Africa", Continent.Africa);
        public readonly static Territory Egypt = new Territory("Egypt", Continent.Africa);
        public readonly static Territory Madagascar = new Territory("Madagascar", Continent.Africa);
        public readonly static Territory NorthAfrica = new Territory("North Africa", Continent.Africa);
        public readonly static Territory SouthAfrica = new Territory("South Africa", Continent.Africa);
        #endregion

        #region Asia
        public readonly static Territory Afghanistan = new Territory("Afghanistan", Continent.Asia);
        public readonly static Territory China = new Territory("China", Continent.Asia);
        public readonly static Territory India = new Territory("India", Continent.Asia);
        public readonly static Territory Irkutsk = new Territory("Irkutsk", Continent.Asia);
        public readonly static Territory Japan = new Territory("Japan", Continent.Asia);
        public readonly static Territory Kamchatka = new Territory("Kamchatka", Continent.Asia);
        public readonly static Territory MiddleEast = new Territory("Middle East", Continent.Asia);
        public readonly static Territory Mongolia = new Territory("Mongolia", Continent.Asia);
        public readonly static Territory Siam = new Territory("Siam", Continent.Asia);
        public readonly static Territory Siberia = new Territory("Siberia", Continent.Asia);
        public readonly static Territory Ural = new Territory("Ural", Continent.Asia);
        public readonly static Territory Yakutsk = new Territory("Yakutsk", Continent.Asia);
        #endregion

        #region Australia
        public readonly static Territory EasternAustralia = new Territory("Eastern Australia", Continent.Australia);
        public readonly static Territory Indonesia = new Territory("Indonesia", Continent.Australia);
        public readonly static Territory NewGuinea = new Territory("New Guinea", Continent.Australia);
        public readonly static Territory WesternAustralia = new Territory("Western Australia", Continent.Australia);
        #endregion
        #endregion
    }
}
