using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Portfolio_Builder.BusinessLogic;
using Portfolio_Builder.Models;
using Portfolio_Builder.Views;
using System.Collections.ObjectModel;

namespace Portfolio_Builder.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
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
            _marketCardCollection = new();

            _stockHeadline = "Einzelne Aktien";
            _stockCardCollection = new();

            _addAssetCardCommand = new RelayCommand(() => OpenAssetScreener());
        }

        private RelayCommand _addAssetCardCommand;
        public RelayCommand AddAssetCardCommand
        {
            get => _addAssetCardCommand;
            set => SetProperty(ref _addAssetCardCommand, value);
        }

        public static void OpenAssetScreener()
        {
            AssetScreenerView assetScreenerView = new();
            assetScreenerView.Show();
        }
    }
}
