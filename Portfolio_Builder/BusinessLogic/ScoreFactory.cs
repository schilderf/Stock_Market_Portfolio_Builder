using Portfolio_Builder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_Builder.BusinessLogic
{
    public class ScoreFactory
    {
        private readonly DatabaseManagement databaseManagement = new();

        public ObservableCollection<MarketScoreModel> CreateMarketScoreModels()
        {
            return databaseManagement.GetMarketScoreModels();
        }

        public ObservableCollection<AssetScoreModel> CreateAssetScoreModels(string type, string typeName)
        {
            return databaseManagement.GetAssetScoreModels(type, typeName);
        }
    }
}
