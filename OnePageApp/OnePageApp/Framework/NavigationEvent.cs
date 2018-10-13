using Prism.Events;

namespace OnePageApp.Framework
{
    public class NavigationEvent : PubSubEvent<string>
    {
    }

    public class NavigationInfoEvent : PubSubEvent<string>
    {
    }
}
