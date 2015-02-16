using System;
using System.IO;
using System.ServiceModel;
using Topshelf.Logging;
using Ninject.Modules;

namespace NotificationListener
{
    [ServiceBehavior(Name = "Listener", Namespace = "http://webservices.registration.com/notification", IncludeExceptionDetailInFaults = true)]
    public class LogListenerService : NotificationPortType
    {
        static readonly LogWriter _log = HostLogger.Get<LogListenerService>();

        [OperationBehavior]
        public void Notify(System.Nullable<NotificationActions> action, string reference)
        {
            _log.Info(string.Format("{0}\t{1}\t{2} ", action, reference, DateTime.Now));
        }
    }

}