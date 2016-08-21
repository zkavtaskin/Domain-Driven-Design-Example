using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.ApplicationLayer.History
{
    public interface IHistoryService
    {
        HistoryDto GetHistory();
    }
}
