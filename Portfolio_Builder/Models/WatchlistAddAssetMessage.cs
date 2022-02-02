using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_Builder.Models
{
    public class WatchlistAddAssetMessage: ValueChangedMessage<string>
    {
        public WatchlistAddAssetMessage(string value) : base(value)
        {

        }
    }
}
