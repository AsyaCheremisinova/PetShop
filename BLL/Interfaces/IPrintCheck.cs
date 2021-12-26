using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using System.Collections.ObjectModel;

namespace BLL.Interfaces
{
    public interface IPrintCheck
    {
        void GetCheck(Order_Model order);
        void GetReport(ObservableCollection<Order_Model> orders, int UserId, DateTime CostsDate1, DateTime CostsDate2);
    }
}
