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
        private readonly CardFactory cardFactory;
        private readonly DatabaseManagement databaseManagement;

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
            cardFactory = new();
            databaseManagement = new();
            _assetCardsOnWatchlist = cardFactory.CreateWatchlistAssetCardCollection("All Assets");
            _marketCardsOnWatchlist = cardFactory.CreateWatchlistMarketCardCollection("All Assets");
        }

        public void UpdateCardsOnWatchlist()
        {
            cardFactory.UpdateAssetCardCollection(AssetCardsOnWatchlist);
            cardFactory.UpdateMarketCardCollection(MarketCardsOnWatchlist);
        }

        public ObservableCollection<string> GetAvalableWatchlists()
        {
            return databaseManagement.GetAvalableWatchlists();
        }
        public void DeleteWatchlist(string watchlist)
        {
            databaseManagement.DeleteWatchlist(watchlist);
        }
        public void ChangeWatchlist(string watchlist)
        {
            AssetCardsOnWatchlist = cardFactory.CreateWatchlistAssetCardCollection(watchlist);
            MarketCardsOnWatchlist = cardFactory.CreateWatchlistMarketCardCollection(watchlist);
        }
        public void AddAssetToWatchlist(string name, string watchlist)
        {
            databaseManagement.AddItemToWatchlist(name, "Asset", watchlist);
            try
            { 
                AssetCardsOnWatchlist.Add(cardFactory.CreateAssetCard(name));
            }
            catch
            {

            }
        }
        public void DeleteAssetFromWatchlist(AssetCardModel asset, string watchlist)
        {
            databaseManagement.DeleteItemFromWatchlist(asset.Symbol, "Asset", watchlist);
            try
            {
                AssetCardsOnWatchlist.Remove(asset);
            }
            catch
            {

            }
        }
        public void AddMarketToWatchlist(string name, string watchlist)
        {
            databaseManagement.AddItemToWatchlist(name, "Market", watchlist);
            try
            {
                MarketCardsOnWatchlist.Add(cardFactory.CreateMarketCard(name));
            }
            catch
            {

            }
        }

        public void DeleteMarketFromWatchlist(MarketCardModel market, string watchlist)
        {
            databaseManagement.DeleteItemFromWatchlist(market.Name, "Market", watchlist);
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
