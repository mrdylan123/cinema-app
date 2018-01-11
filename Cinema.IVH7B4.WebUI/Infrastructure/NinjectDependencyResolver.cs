using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cinema.IVH7B4.Domain.Abstract;
using Cinema.IVH7B4.Domain.Concrete;
using Ninject;

namespace Cinema.IVH7B4.WebUI.Infrastructure {
    public class NinjectDependencyResolver : IDependencyResolver {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam) {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType) {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return kernel.GetAll(serviceType);
        }

        public void AddBindings() {
            kernel.Bind<IFilmRepository>().To<FilmRepository>();
            kernel.Bind<IShowingRepository>().To<ShowingRepository>();
            kernel.Bind<IFilmOverviewRepository>().To<FilmOverviewRepository>();
            kernel.Bind<ICashRegisterLoginRepository>().To<CashRegisterLoginRepository>();
            kernel.Bind<ILostAndFoundRepository>().To<LostAndFoundRepository>();
            kernel.Bind<IManagerLoginRepository>().To<ManagerLoginRepository>();
            kernel.Bind<IReviewRepository>().To<ReviewRepository>();
            kernel.Bind<IBackOfficeRepository>().To<BackOfficeRepository>();
            kernel.Bind<ITenRidesCardRepository>().To<TenRidesCardRepository>();
            kernel.Bind<IWeekDayCardRepository>().To<WeekDayCardRepository>();
            kernel.Bind<INewsLetterRegistrationRepository>().To<NewsLetterRegistrationRepository>();
            kernel.Bind<ISubscriptionHolderRepository>().To<SubscriptionHolderRepository>();
        }
    }
}