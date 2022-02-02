using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_Builder.BusinessLogic
{
    public class Market
    {
        private readonly string _name;
        public string Name
        {
            get => _name;
        }
        private readonly string _type;
        public string Type
        {
            get => _type;
        }
    }
}
