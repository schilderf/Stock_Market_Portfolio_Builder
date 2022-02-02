using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_Builder.Models
{
    public class WatchlistDeleteAssetMessage: ValueChangedMessage<AssetCardModel>
    {
        public WatchlistDeleteAssetMessage(AssetCardModel value): base(value)
        {

        }
    }
}
