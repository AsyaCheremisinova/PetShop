using BLL.Interfaces;
using Ninject.Modules;
using BLL;
using BLL.Services;
namespace PetShop.Util
{
    class NinjectReg : NinjectModule
    {
        public override void Load()
        {
            Bind<ICRUD>().To<CRUD>();
            Bind<ITypeProductService>().To<TypeProductService>();
            Bind<IOrderService>().To<OrderService>();
            Bind<IHistoryOrders>().To<HistoryOrders>();
            Bind<IAuthorizationService>().To<AuthorizationService>();
            Bind<IPrintCheck>().To<PrintCheck>();
        }
    }
}