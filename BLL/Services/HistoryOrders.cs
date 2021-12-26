using DAL;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using BLL.Interfaces;
using DAL.EF;

namespace BLL.Services
{
    public class HistoryOrders : IHistoryOrders
    {
        IDbRepos dataBase;
        public HistoryOrders(IDbRepos dbRepository)
        {
            dataBase = dbRepository;
        }
        public List<HistoryProduct_Model> GetHistory(int id)
        {
            return dataBase.HistoryProduct.GetHistoryProducts(id).Select(i => new HistoryProduct_Model { order_id = i.order_id, cost = i.cost, name = i.name, number = i.number, order_line_id = i.order_line_id, picture = i.picture }).ToList();
        }
    }
}
