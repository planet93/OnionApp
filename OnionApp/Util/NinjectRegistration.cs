using Ninject.Modules;
using OnionApp.Domain.Interfaces;
using OnionApp.Infrastructure.Business;
using OnionApp.Infrastructure.Data;
using OnionApp.Services.Interfaces;

namespace OnionApp.Util
{
    public class NinjectRegistration:NinjectModule
    {
        public override void Load()
        {
            Bind<IBookRepository>().To<BookRepository>();
            Bind<IOrder>().To<CacheOrder>();
        }
    }
}