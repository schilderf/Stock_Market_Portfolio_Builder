using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_Builder.Models
{
    public class MarketScoreModel : ObservableObject
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private readonly double _gain;
        public double Gain
        {
            get => _gain;
        }

        private readonly string _gainColor;
        public string GainColor
        {
            get => _gainColor;
        }

        public MarketScoreModel()
        {
            _name = "";
            _gain = 0.0;
            _gainColor = MapGainColor(_gain);
        }

        public MarketScoreModel(string name, double gain)
        {
            _name = name;
            _gain = gain;
            _gainColor = MapGainColor(_gain);
        }

        public static string MapGainColor(double gain)
        {
            if (gain > 0)
                return "Green";
            if (gain < 0)
                return "Red";
            else
                return "White";
        }
    }
}
