using Microsoft.Toolkit.Mvvm.ComponentModel;
using Portfolio_Builder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_Builder.BusinessLogic
{
    public class Market
    {
        private readonly string _name;
        public string Name
        {
            get => _name;
        }
        private readonly string _type;
        public string Type
        {
            get => _type;
        }

        private readonly List<MarketDay> _days;
        public List<MarketDay> Days
        {
            get => _days;
        }

        private readonly ObservableCollection<AssetScoreModel> _assets;
        public ObservableCollection<AssetScoreModel> Assets
        {
            get => _assets;
        }

        public Market()
        {
            _name = String.Empty;
            _type = String.Empty;
            _days = new List<MarketDay>();
            _assets = new ObservableCollection<AssetScoreModel>();
        }

        public Market(string name, string type, List<MarketDay> days, ObservableCollection<AssetScoreModel> assets)
        {
            _name = name;
            _type = type;
            _days = days;
            _assets = assets;
        }
    }
}
