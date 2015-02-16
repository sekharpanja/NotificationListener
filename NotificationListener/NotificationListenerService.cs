using Topshelf.Logging;

namespace NotificationListener
{
    public class NotificationListenerService
    {
        static readonly LogWriter _log = HostLogger.Get<NotificationListenerService>();
        private readonly INotificationListener _listener;

        public NotificationListenerService(INotificationListener listener)
        {
            _listener = listener;
        }

        public bool Start()
        {
            _log.Debug("Sample Service Started.");
            _log.DebugFormat("Dependency: {0}", _listener);
            _listener.StartListening();
            return _listener != null;
        }

        public bool Stop()
        {
            _listener.StopListening();
            return _listener != null;
        }
    }
}
