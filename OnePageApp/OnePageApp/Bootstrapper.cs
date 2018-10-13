using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using OnePageApp.Framework;
using OnePageApp.Modules.ViewModels;
using Prism.Modularity;

namespace OnePageApp
{
    public class Bootstrapper: Prism.Unity.UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return this.Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            App.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<IChildWindowLauncher, ChildWindowLanucher>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IDialogCoordinator, DialogCoordinator>(new ContainerControlledLifetimeManager());
            Container.RegisterType<AppSettings>(new ContainerControlledLifetimeManager(),
                new InjectionFactory(c =>
                {
                    var a = new AppSettings();
                    a.Load();
                    return a;
                }));

            //// in order to share the same model between Views you I need to make view models as singleton
            //// Note: a better approach would be to separated the models and just pass data around
            //Container.RegisterType<SomeViewModel>(new ContainerControlledLifetimeManager());

        }


        protected override void ConfigureServiceLocator()
        {
            base.ConfigureServiceLocator();

            var csl = new UnityServiceLocator(this.Container);
            ServiceLocator.SetLocatorProvider(() => csl);
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            var moduleCatalog = (ModuleCatalog)this.ModuleCatalog;

            moduleCatalog.AddModule(typeof(Modules.StartupModule));
            moduleCatalog.AddModule(typeof(Modules.MenuModule));
            moduleCatalog.AddModule(typeof(Modules.ScreensModule));
            moduleCatalog.AddModule(typeof(Modules.FlowOrchestratorModule));
        }

    }
}
