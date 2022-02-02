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
    public class WatchlistManagement : ObservableObject
    {
        private readonly CardFactory cardFactory = new();
        private readonly DatabaseManagement databaseManagement = new();

        private ObservableCollection<AssetCardModel> _assetCardsOnWatchlist;
        public ObservableCollection<AssetCardModel> AssetCardsOnWatchlist
        {
            get => _assetCardsOnWatchlist;
            set => SetProperty(ref _assetCardsOnWatchlist, value);
        }

        private ObservableCollection<MarketCardModel> _marketCardsOnWatchlist;
        public ObservableCollection<MarketCardModel> MarketCardsOnWatchlist
        {
            get => _marketCardsOnWatchlist;
            set => SetProperty(ref _marketCardsOnWatchlist, value);
        }

        public WatchlistManagement()
        {
            _assetCardsOnWatchlist = cardFactory.CreateWatchlistAssetCardCollection();
            _marketCardsOnWatchlist = cardFactory.CreateWatchlistMarketCardCollection();
        }

        public void AddAssetToWatchlist(string name)
        {
            databaseManagement.AddItemToWatchlist(name, "Asset");
            try
            { 
                AssetCardsOnWatchlist.Add(cardFactory.CreateAssetCard(name));
            }
            catch
            {

            }
        }
        public void DeleteAssetFromWatchlist(AssetCardModel asset)
        {
            databaseManagement.DeleteItemFromWatchlist(asset.Symbol, "Asset");
            try
            {
                AssetCardsOnWatchlist.Remove(asset);
            }
            catch
            {

            }
        }
        public void AddMarketToWatchlist(string name)
        {
            databaseManagement.AddItemToWatchlist(name, "Market");
            try
            {
                MarketCardsOnWatchlist.Add(cardFactory.CreateMarketCard(name));
            }
            catch
            {

            }
        }

        public void DeleteMarketFromWatchlist(MarketCardModel market)
        {
            databaseManagement.DeleteItemFromWatchlist(market.Name, "Market");
            try
            {
                MarketCardsOnWatchlist.Remove(market);
            }
            catch
            {

            }
        }
    }
}
