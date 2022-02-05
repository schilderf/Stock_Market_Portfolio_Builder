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
        private ObservableCollection<string> _watchlistCollection;
        public ObservableCollection<string> WatchlistCollection
        {
            get => _watchlistCollection;
            set => SetProperty(ref _watchlistCollection, value);
        }
        private string _selectedWatchlist;
        public string SelectedWatchlist
        {
            get => _selectedWatchlist;
            set
            {
                SetProperty(ref _selectedWatchlist, value);
                watchlist.ChangeWatchlist(_selectedWatchlist);
                StockCardCollection = watchlist.AssetCardsOnWatchlist;
                MarketCardCollection = watchlist.MarketCardsOnWatchlist;
            } 
                
        }
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

            _selectedWatchlist = "Standard";
            _watchlistCollection = watchlist.GetAvalableWatchlists();
            _marketHeadline = "Globale Märkte";
            _marketCardCollection = watchlist.MarketCardsOnWatchlist;

            _stockHeadline = "Einzelne Aktien";
            _stockCardCollection = watchlist.AssetCardsOnWatchlist;

            _openAssetScreenerCommand = new RelayCommand(() => OpenAssetScreener());
            _openWatchlistTextDialogCommand = new RelayCommand(() => OpenWatchlistTextDialog());
            _deleteWatchlistDialogCommand = new RelayCommand(() => DeleteWatchlistDialog());

            Messenger.Register<WatchlistAddAssetMessage>(this, (r, m) =>
            {
                AddAssetToWatchlist(m.Value);
                StockCardCollection = watchlist.AssetCardsOnWatchlist;
            });

            Messenger.Register<WatchlistDeleteAssetMessage>(this, (r, m) =>
            {
                DeleteAssetFromWatchlist(m.Value);
                StockCardCollection= watchlist.AssetCardsOnWatchlist;
            });

            Messenger.Register<WatchlistAddMarketMessage>(this, (r, m) =>
            {
                AddMarketToWatchlist(m.Value);
                MarketCardCollection = watchlist.MarketCardsOnWatchlist;
            });

            Messenger.Register<WatchlistDeleteMarketMessage>(this, (r, m) =>
            {
                DeleteMarketFromWatchlist(m.Value);
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

        private RelayCommand _openWatchlistTextDialogCommand;
        public RelayCommand OpenWatchlistTextDialogCommand
        {
            get => _openWatchlistTextDialogCommand;
            set => SetProperty(ref _openWatchlistTextDialogCommand, value);
        }
        public void OpenWatchlistTextDialog()
        {
            TextInputPromptView text = new();
            if (text.ShowDialog() ?? false)
            {
                WatchlistCollection.Add(text.Response);
                SelectedWatchlist = text.Response;
            }
        }

        private RelayCommand _deleteWatchlistDialogCommand;
        public RelayCommand DeleteWatchlistDialogCommand
        {
            get => _deleteWatchlistDialogCommand;
            set => SetProperty(ref _deleteWatchlistDialogCommand, value);
        }
        public void DeleteWatchlistDialog()
        {
            if (new ConfirmPromptView().ShowDialog() ?? false)
            {
                watchlist.DeleteWatchlist(SelectedWatchlist);
                WatchlistCollection.Remove(SelectedWatchlist);
            }
        }

        public void AddAssetToWatchlist(string assetSymbol)
        {
            watchlist.AddAssetToWatchlist(assetSymbol, SelectedWatchlist);
        }
        public void DeleteAssetFromWatchlist(AssetCardModel assetCard)
        {
            watchlist.DeleteAssetFromWatchlist(assetCard, SelectedWatchlist);
        }
        public void AddMarketToWatchlist(string marketName)
        {
            watchlist.AddMarketToWatchlist(marketName, SelectedWatchlist);
        }
        public void DeleteMarketFromWatchlist(MarketCardModel marketCard)
        {
            watchlist.DeleteMarketFromWatchlist(marketCard, SelectedWatchlist);
        }
    }
}
