using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_Builder.Models
{
    public class PerformanceCardModel
    {
        private int _kpiFontSize;
        public int KpiFontSize
        {
            get => _kpiFontSize;
        }
        private int _captionFontSize;
        public int CaptionFontSize
        {
            get => _captionFontSize;
        }
        private int _width;
        public int Width
        {
            get => _width;
        }
        private int _height;
        public int Height
        {
            get => _height;
        }
        private string _value;
        public string Value
        {
            get => _value;
            set => _value = value;
        }
        private string _timeframe;
        public string Timeframe
        {
            get => _timeframe;
            set => _timeframe = value;
        }
        public PerformanceCardModel(double value, string timeframe, string sign)
        {
            _value = $"{value}{sign}";
            _timeframe = timeframe;
            _kpiFontSize = 20;
            _captionFontSize = 10;
            _width = 100;
            _height = 30;
        }
    }
}
