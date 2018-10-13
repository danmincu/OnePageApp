using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace OnePageApp.Modules
{
    public class ScreensModule : IModule
    {
        IRegionManager _regionManager;
        IUnityContainer _container;

        public ScreensModule(RegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterTypeForNavigation<Views.Users>();
            _container.RegisterTypeForNavigation<Views.Permissions>();
            _container.RegisterTypeForNavigation<Views.Contracts>();
            _container.RegisterTypeForNavigation<Views.Settings>();
        }
    }
}
