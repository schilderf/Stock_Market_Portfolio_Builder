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

        public ObservableCollection<MarketScoreModel> CreateMarketScoreModelsByType(string MarketType)
        {
            return databaseManagement.GetMarketScoreModelsByType(MarketType);
        }

        public ObservableCollection<MarketScoreModel> CreateAllMarketScoreModels()
        {
            List<string> marketTypes = new() { "Industry", "Sector", "Country" };
            ObservableCollection<MarketScoreModel> allMarketScores = new();
            foreach (string marketType in marketTypes)
            {
                foreach (var item in allMarketScores.Concat(CreateMarketScoreModelsByType(marketType)))
                {
                    allMarketScores.Add(item);
                }
            }

            return allMarketScores;
        }
    }
}
