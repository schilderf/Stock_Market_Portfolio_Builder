using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Portfolio_Builder.BusinessLogic;
using Portfolio_Builder.Models;
using Portfolio_Builder.Views;
using System.Collections.ObjectModel;

namespace Portfolio_Builder.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
        private readonly WatchlistManagement watchlist = new();
        private string _marketHeadline;

        public string MarketHeadline
        {
            get => _marketHeadline;
            set => SetProperty(ref _marketHeadline, value);
        }

        private ObservableCollection<MarketCardModel> _marketCardCollection;
        public ObservableCollection<MarketCardModel> MarketCardCollection
        {
            get => _marketCardCollection;
            set => SetProperty(ref _marketCardCollection, value);
        }

        private string _stockHeadline;

        public string StockHeadline
        {
            get => _stockHeadline;
            set => SetProperty(ref _stockHeadline, value);
        }

        private ObservableCollection<AssetCardModel> _stockCardCollection;
        public ObservableCollection<AssetCardModel> StockCardCollection
        {
            get => _stockCardCollection;
            set => SetProperty(ref _stockCardCollection, value);
        }

        public MainWindowViewModel()
        {
            _marketHeadline = "Globale Märkte";
            _marketCardCollection = watchlist.MarketCardsOnWatchlist;

            _stockHeadline = "Einzelne Aktien";
            _stockCardCollection = watchlist.AssetCardsOnWatchlist;

            _openAssetScreenerCommand = new RelayCommand(() => OpenAssetScreener());

            Messenger.Register<WatchlistAddAssetMessage>(this, (r, m) =>
            {
                watchlist.AddAssetToWatchlist(m.Value);
                StockCardCollection = watchlist.AssetCardsOnWatchlist;
            });

            Messenger.Register<WatchlistDeleteAssetMessage>(this, (r, m) =>
            {
                watchlist.DeleteAssetFromWatchlist(m.Value);
                StockCardCollection= watchlist.AssetCardsOnWatchlist;
            });

            Messenger.Register<WatchlistAddMarketMessage>(this, (r, m) =>
            {
                watchlist.AddMarketToWatchlist(m.Value);
                MarketCardCollection = watchlist.MarketCardsOnWatchlist;
            });

            Messenger.Register<WatchlistDeleteMarketMessage>(this, (r, m) =>
            {
                watchlist.DeleteMarketFromWatchlist(m.Value);
                MarketCardCollection = watchlist.MarketCardsOnWatchlist;
            });
        }

        private RelayCommand _openAssetScreenerCommand;
        public RelayCommand OpenAssetScreenerCommand
        {
            get => _openAssetScreenerCommand;
            set => SetProperty(ref _openAssetScreenerCommand, value);
        }

        public static void OpenAssetScreener()
        {
            AssetScreenerView assetScreenerView = new();
            assetScreenerView.ShowDialog();
        }
    }
}
