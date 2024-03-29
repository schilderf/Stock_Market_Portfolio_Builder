﻿using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
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
    public class AssetScreenerViewModel : ObservableRecipient
    {
        private static readonly ScoreFactory scoreFactory = new();

        private ObservableCollection<MarketScoreModel> _marketScoreCollection;
        public ObservableCollection<MarketScoreModel> MarketScoreCollection
        {
            get => _marketScoreCollection;
            set => SetProperty(ref _marketScoreCollection, value);
        }
        private MarketScoreModel? _selectedMarketScore;
        public MarketScoreModel SelectedMarketScore
        {
            get => _selectedMarketScore ?? new();
            set 
            {
                SetProperty(ref _selectedMarketScore, value);

                if (_selectedMarketScore != null)
                    AssetScoreCollection = scoreFactory.CreateAssetScoreModels(_selectedMarketScore.Type, _selectedMarketScore.Name);
            }
        }

        private ObservableCollection<AssetScoreModel> _assetScoreCollection;
        public ObservableCollection<AssetScoreModel> AssetScoreCollection
        {
            get => _assetScoreCollection;
            set => SetProperty(ref _assetScoreCollection, value);
        }

        private AssetScoreModel? _selectedAssetScore;
        public AssetScoreModel SelectedAssetScore
        {
            get => _selectedAssetScore ?? new();
            set => SetProperty(ref _selectedAssetScore, value);
        }

        private RelayCommand _addAssetToWatchlistCommand;
        public RelayCommand AddAssetToWatchlistCommand
        {
            get => _addAssetToWatchlistCommand;
            set => SetProperty(ref _addAssetToWatchlistCommand, value);
        }

        private RelayCommand _addMarketToWatchlistCommand;
        public RelayCommand AddMarketToWatchlistCommand
        {
            get => _addMarketToWatchlistCommand;
            set => SetProperty(ref _addMarketToWatchlistCommand, value);
        }

        public AssetScreenerViewModel()
        {
            _marketScoreCollection = scoreFactory.CreateMarketScoreModels();
            _assetScoreCollection = new();

            _addAssetToWatchlistCommand = new(() => AddAssetToWatchlist());
            _addMarketToWatchlistCommand = new(() => AddMarketToWatchlist());
        }

        private void AddAssetToWatchlist()
        {
            Messenger.Send(new WatchlistAddAssetMessage(SelectedAssetScore.Symbol));
        }

        private void AddMarketToWatchlist()
        {
            Messenger.Send(new WatchlistAddMarketMessage(SelectedMarketScore.Name));
        }
    }
}
