using Microsoft.Toolkit.Mvvm.ComponentModel;
using Portfolio_Builder.Models;
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
        private ObservableCollection<MarketCardModel> _marketCardCollection;
        public ObservableCollection<MarketCardModel> MarketCardCollection
        {
            get => _marketCardCollection;
            set => SetProperty(ref _marketCardCollection, value);
        }

        public AssetScreenerViewModel()
        {
            _marketCardCollection = new();
        }
    }
}
