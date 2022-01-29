using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_Builder.Models
{
    public class AssetScoreModel : ObservableObject
    {
        private readonly string _symbol;
        public string Symbol
        {
            get => _symbol;
        }
        private readonly string _category;
        public string Category
        {
            get => _category;
        }
        private readonly string _name;
        public string Name
        {
            get => _name;
        }

        private readonly string _categoryColor;
        public string CategoryColor
        {
            get => _categoryColor;
        }

        public AssetScoreModel()
        {
            _symbol = string.Empty;
            _category = string.Empty;
            _name = string.Empty;
            _categoryColor = MapCategoryColor(_category);
        }

        public AssetScoreModel(string symbol, string categoory, string name)
        {
            _symbol = symbol;
            _category = categoory;
            _name = name;
            _categoryColor = MapCategoryColor(_category);
        }

        private static string MapCategoryColor(string category)
        {
            return category switch
            {
                "God Tier" => "Gold",
                "SSS Tier" => "Gold",
                "SS Tier" => "Gold",
                "S Tier" => "Gold",
                "A Tier" => "MediumSeaGreen",
                "B Tier" => "DarkSeaGreen",
                "C Tier" => "DarkCyan",
                "D Tier" => "DarkViolet",
                "F Tier" => "PaleVioletRed",
                "FF Tier" => "PaleVioletRed",
                "FFF Tier" => "PaleVioletRed",
                "Garbage Tier" => "DarkRed",
                "Growth Monster" => "DarkGoldenrod",
                "Price Underdog" => "DarkGoldenrod",
                "Puppy 101" => "DarkGoldenrod",
                _ => ""
            };
        }
    }
}
