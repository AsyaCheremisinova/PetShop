using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;

namespace PetShop.Util
{
    public interface IDialogService
    {
        void OpenShop(ICRUD crudService, IDialogService dialogServ, ITypeProductService type, IOrderService orderServ, IHistoryOrders historyOrd, IPrintCheck printCheck,  int UserId);

    }
}
