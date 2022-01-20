using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_Builder.BusinessLogic
{
    public class Asset
    {
        private readonly string _symbol;

        public string Symbol
        {
            get => _symbol;
        }
        private readonly string _name;
        public string Name
        {
            get => _name;
        }
        private readonly string _country;
        public string Country
        {
            get => _country;
        }
        private readonly string _sector;
        public string Sector
        {
            get => _sector;
        }

        private readonly string _industry;
        public string Industry
        {
            get => _industry;
        }

        private readonly List<AssetDay> _marketDays;
        public List<AssetDay> MarketDays
        {
            get => _marketDays;
        }

        public Asset()
        {
            _symbol = "";
            _name = "";
            _sector = "";
            _industry = "";
            _country = "";
            _marketDays = new List<AssetDay>();
        }
        public Asset(string symbol, string name, string sector, string industry, string country, List<AssetDay> marketDays)
        {
            _symbol = symbol;
            _name = name;
            _industry = industry;
            _sector = sector;
            _country = country;
            _marketDays = marketDays;
        }
    }
}
