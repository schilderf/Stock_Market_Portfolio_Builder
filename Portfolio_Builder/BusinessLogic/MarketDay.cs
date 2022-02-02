using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_Builder.BusinessLogic
{
    public class MarketDay
    {
        private readonly DateTime _date;
        public DateTime Date
        {
            get => _date;
        }
        private readonly double _value;
        public double Value
        {
            get => _value;
        }

        public MarketDay()
        {
            _date = new DateTime();
            _value = double.NaN;
        }

        public MarketDay(DateTime date, double value)
        {
            _date = date;
            _value = value;
        }
    }
}
