using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Portfolio_Builder.BusinessLogic;
using Portfolio_Builder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_Builder.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private CardFactory assetCardCreator = new CardFactory();

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

        public MainWindowViewModel ()
        {
            _marketHeadline = "Globale Märkte";
            _marketCardCollection = new ObservableCollection<MarketCardModel>();

            _stockHeadline = "Einzelne Aktien";
            _stockCardCollection = new ObservableCollection<AssetCardModel>();
            //AddAssetCard("AMZN");
            //AddAssetCard("AAPL");
            //AddAssetCard("GOOG");
            //AddAssetCard("AMZN");


            _addAssetCardCommand = new RelayCommand(() => AddAssetCard("GOOG"));
        }

        private RelayCommand _addAssetCardCommand;
        public RelayCommand AddAssetCardCommand
        {
            get => _addAssetCardCommand;
            set => SetProperty(ref _addAssetCardCommand, value);
        }

        public void AddAssetCard(string assetTickerSymbol)
        {
           StockCardCollection.Add(assetCardCreator.CreateAssetCard(assetTickerSymbol));
        }
    }
}
