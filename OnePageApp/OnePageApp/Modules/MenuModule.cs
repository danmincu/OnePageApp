using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace OnePageApp.Modules
{
    public class MenuModule : IModule
    {
        IRegionManager _regionManager;
        IUnityContainer _container;

        public MenuModule(RegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterTypeForNavigation<Views.Menu>("MenuView");
        }
    }
}
