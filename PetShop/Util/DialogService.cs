using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;

namespace PetShop.Util
{
    public class DialogService: IDialogService
    {
        public void OpenShop(ICRUD crudService, IDialogService dialogServ, ITypeProductService type, IOrderService orderServ, IHistoryOrders historyOrd, IPrintCheck printCheck, int UserId)
        {
            Shop shop = new Shop(crudService, dialogServ, type, orderServ, historyOrd, printCheck, UserId);
            shop.Show();
        }
    }
}
