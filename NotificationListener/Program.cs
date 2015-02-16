using Topshelf;
using Topshelf.Ninject;
using log4net.Config;

namespace NotificationListener
{
    class Program
    {
        static void Main()
        {
            XmlConfigurator.Configure();

            HostFactory.Run(c =>
            {
                c.UseNinject(new DependencyKernal()); //Initiates Ninject and consumes Modules
                c.UseLog4Net(); //Instructs to use Log4Net

                c.Service<NotificationListenerService>(s =>
                {
                    //Specifies that Topshelf should delegate to Ninject for construction
                    s.ConstructUsingNinject();
                    s.WhenStarted((service, control) => service.Start());
                    s.WhenStopped((service, control) => service.Stop());
                });
            });
        }
    }
}
