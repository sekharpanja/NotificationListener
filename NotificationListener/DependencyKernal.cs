using Ninject.Modules;

namespace NotificationListener
{
    public class DependencyKernal : NinjectModule
    {
        public override void Load()
        {
            Bind<INotificationListener>().To<NotificationListener>();
            Bind<NotificationPortType>().To<LogListenerService>();
        }
    }
}
