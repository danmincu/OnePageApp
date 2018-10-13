using System;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.SimpleChildWindow;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Prism.Mvvm;

namespace OnePageApp.Modules.ViewModels
{
    public interface IChildWindowLauncher
    {
        Task<bool> ShowChildWindowAsync(ChildWindowViewModelBase viewModel);
    }

    public class ChildWindowLanucher : IChildWindowLauncher
    {
        public Task<bool> ShowChildWindowAsync(ChildWindowViewModelBase viewModel)
        {
            string pageName = String.Empty;
            
            //create a model and a user control to be "found" by the following code 
            int index = viewModel.GetType().Name.IndexOf("Model", StringComparison.Ordinal);
            pageName = $"OnePageApp.Modules.Views.ChildWindows.{viewModel.GetType().Name.Remove(index)}Window";

            var type = Type.GetType(pageName);
            if (!(ServiceLocator.Current.GetInstance(type) is ChildWindow child))
                return null;

            child.DataContext = viewModel;

            return Application.Current.MainWindow.ShowChildWindowAsync<bool>(child);
        }
    }

    public abstract class ChildWindowViewModelBase : BindableBase
    {
        private DelegateCommand okCommand;

        public DelegateCommand OkCommand
        {
            get => okCommand;
            set => SetProperty(ref okCommand, (DelegateCommand)value);
        }

        public ChildWindowViewModelBase()
        {
            OkCommand = new DelegateCommand(ExecuteOk, CanExecuteOk);
        }

        protected abstract void ExecuteOk();

        protected virtual bool CanExecuteOk()
        {
            return true;
        }

        public abstract string GetTitleForFailure();
        public abstract string GetMessageForFailure();

    }
}
