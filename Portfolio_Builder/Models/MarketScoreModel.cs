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
        private string _type;
        public string Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        private readonly double _gain;
        public double Gain
        {
            get => _gain;
        }

        private readonly string _direction;
        public string Direction
        {
            get => _direction;
        }

        private readonly string _gainColor;
        public string GainColor
        {
            get => _gainColor;
        }

        public MarketScoreModel()
        {
            _name = "";
            _type = "Unknown";
            _gain = 0.0;
            _gainColor = MapGainColor(_gain);
            _direction = MapValueDirection(_gain);
        }

        public MarketScoreModel(string name, double gain, string type)
        {
            _name = name;
            _gain = gain;
            _type = type;
            _gainColor = MapGainColor(_gain);
            _direction = MapValueDirection(_gain);
        }

        public static string MapGainColor(double gain)
        {
            if (gain > 5)
                return "DarkSeaGreen";
            if (gain < -5)
                return "PaleVioletRed";
            else
                return "DarkOrange";
        }

        public static string MapValueDirection(double gain)
        {
            if (gain > 5)
                return "⬆";
            if (gain < -5)
                return "⬇";
            else
                return "~";
        }
    }
}
