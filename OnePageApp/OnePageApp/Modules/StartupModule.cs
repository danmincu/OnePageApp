using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace OnePageApp.Modules
{
    public class StartupModule : IModule
    {
        IRegionManager _regionManager;
        IUnityContainer _container;

        public StartupModule(RegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            //_container.RegisterTypeForNavigation<Views... userLogin>();
            //_container.RegisterTypeForNavigation<Views... someConfiguration module>();
        }
    }
}
