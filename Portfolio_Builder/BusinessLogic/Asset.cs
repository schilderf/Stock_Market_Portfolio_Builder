using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_Builder.BusinessLogic
{
    public class Asset
    {
        private string _symbol;

        public string Symbol
        {
            get => _symbol;
        }
        private string _name;
        public string Name
        {
            get => _name;
        }
        private string _country;
        public string Country
        {
            get => _country;
        }
        private string _sector;
        public string Sector
        {
            get => _sector;
        }

        private List<AssetDay> _marketDays;
        public List<AssetDay> MarketDays
        {
            get => _marketDays;
        }

        public Asset()
        {
            _symbol = "";
            _name = "";
            _marketDays = new List<AssetDay>();
        }
        public Asset(string symbol, string name, List<AssetDay> marketDays)
        {
            _symbol = symbol;
            _name = name;
            _marketDays = marketDays;
        }
    }
}
