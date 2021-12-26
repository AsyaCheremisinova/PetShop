using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using System.Collections.ObjectModel;

namespace BLL.Interfaces
{
    public interface IHistoryOrders
    {
        List<HistoryProduct_Model> GetHistory(int id);
    }
}
