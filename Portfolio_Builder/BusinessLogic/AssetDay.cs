using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_Builder.BusinessLogic
{
    public class AssetDay
    {
        private readonly DateTime _date;
        public DateTime Date
        {
            get => _date;
        }
        private readonly double _closingPrice;
        public double ClosingPrice
        {
            get => _closingPrice;
        }

        public AssetDay()
        {
            _date = new DateTime();
            _closingPrice = 0.00;
        }

        public AssetDay(DateTime date, double closingPrice)
        {
            _date = date;
            _closingPrice = closingPrice;
        }
    }
}
