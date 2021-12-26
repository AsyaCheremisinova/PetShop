using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using System.Collections.ObjectModel;

namespace BLL.Interfaces
{
    public interface IOrderService
    {
        bool MakeOrder(Order_Model orderDto, ObservableCollection<Product_Model> products /*List<int> items*//*, Order_line_Model lines*/);
    }
}
