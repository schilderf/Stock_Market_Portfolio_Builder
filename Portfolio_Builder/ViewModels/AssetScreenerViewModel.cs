using Microsoft.Toolkit.Mvvm.ComponentModel;
using Portfolio_Builder.BusinessLogic;
using Portfolio_Builder.Models;
using Portfolio_Builder.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_Builder.ViewModels
{
    public class AssetScreenerViewModel : ObservableObject
    {
        private static readonly ScoreFactory scoreFactory = new();

        private ObservableCollection<MarketScoreModel> _marketScoreCollection;
        public ObservableCollection<MarketScoreModel> MarketScoreCollection
        {
            get => _marketScoreCollection;
            set => SetProperty(ref _marketScoreCollection, value);
        }

        private ObservableCollection<string> test = new();
        public ObservableCollection<string> Test
        {
            get => test;
            set => SetProperty(ref test, value);
        }

        public AssetScreenerViewModel()
        {
            _marketScoreCollection = scoreFactory.CreateMarketScoreModelsByType("Industry");
        }
    }
}
