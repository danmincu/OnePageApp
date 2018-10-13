using System.Collections.Generic;
using System.Linq;
using OnePageApp.Framework;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;

namespace OnePageApp.Modules
{
    public class FlowOrchestratorModule : IModule
    {
        private IEventAggregator eventAggregator;
        IRegionManager regionManager;
        private IRegion allScreenRegion;
        Stack<string> frmStack = new Stack<string>();
        private bool isSettings = false;


        public FlowOrchestratorModule(RegionManager regionManager, IEventAggregator eventAggregator)
        {
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
        }

        public void Initialize()
        {
            this.allScreenRegion = this.regionManager.Regions[RegionConstants.AllScreenRegion];

            this.Navigate(string.Empty);

            //note: subscribing with the ThreadOption.UIThread, true options is quite important, the subscription gets lost otherwise due to GC 
            this.eventAggregator.GetEvent<NavigationEvent>().Subscribe(this.Navigate, ThreadOption.UIThread, true);
        }

        private string InternalPush(string frameName)
        {
            if (frmStack.Count == 0 || frmStack.Peek() != frameName)
                frmStack.Push(frameName);
            return frameName;
        }

        private void Navigate(string navigationLabel)
        {
            var views = this.allScreenRegion.Views.ToList();
            foreach (var view in views)
            {
                this.allScreenRegion.Remove(view);
            }

            if (this.regionManager.Regions[RegionConstants.MenuRegion].ActiveViews.FirstOrDefault() == null)
            {
                this.regionManager.RequestNavigate(RegionConstants.MenuRegion, "MenuView", NavigationCallback);
            }
            switch (navigationLabel)
            {
                case "NavigateBack":
                    if (frmStack.Any())
                    {
                        frmStack.Pop();
                    }

                    if (frmStack.Any())
                    {
                        this.regionManager.RequestNavigate(RegionConstants.RightRegion, frmStack.Peek(), NavigationCallback);
                    }
                    break;

                case nameof(Views.Users):
                case nameof(Views.Permissions):
                case nameof(Views.Contracts):
                case nameof(Views.Settings):
                    this.regionManager.RequestNavigate(RegionConstants.RightRegion, this.InternalPush(navigationLabel), NavigationCallback);
                    break;
                default:
                    break;
            }

            //publish the event to allow/forbid back navigation
            this.eventAggregator.GetEvent<ControlBackFlowEvent>().Publish(frmStack.Count > 1);
        }

        private void NavigationCallback(NavigationResult obj)
        {
            if (obj.Result == false)
            {
                // navigation errors
                Logs.Log.Add(obj.Error);
            }
            else
            {
                this.eventAggregator.GetEvent<NavigationInfoEvent>().Publish(obj.Context.Uri.ToString());
            }
        }
    }
}
