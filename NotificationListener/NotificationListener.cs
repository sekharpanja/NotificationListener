using System;
using System.ServiceModel;
using Topshelf.Logging;

namespace NotificationListener
{
    public interface INotificationListener
    {
        void StartListening();
        void StopListening();
        void Close();
    }

    public class NotificationListener : INotificationListener
    {
        static readonly LogWriter _log = HostLogger.Get<NotificationListener>();
        private ServiceHost _serviceHost;
        private NotificationPortType _listenerType;
        private const string _endpointAddress = "http://localhost:12526/ConsoleListener";
        private const string _endpointNamespace = "http://webservices.registration.com/notification";

        public NotificationListener(NotificationPortType listenerType)
        {
            _listenerType = listenerType;
        }

        public void StartListening()
        {
            _log.Debug("Setting up listener...");

            _serviceHost = new ServiceHost(_listenerType.GetType(), new Uri(_endpointAddress));
            _serviceHost.UnknownMessageReceived += new EventHandler<UnknownMessageReceivedEventArgs>(UnknownMessage);
            _serviceHost.AddServiceEndpoint(typeof(NotificationPortType),
                                            new BasicHttpBinding() { Namespace = _endpointNamespace }, 
                                            new Uri(_endpointAddress));

            _log.Debug("ConsoleListener starting...");

            _serviceHost.Open();
        }

        private void UnknownMessage(object sender, UnknownMessageReceivedEventArgs e)
        {
            _log.ErrorFormat("*** UNKNOWN MESSAGE *** \r\n Header.To: {0}\r\nAddressing: {1}\r\nEnvelope: {2}\r\nBody: {3}\r\n", 
                                        e.Message.Headers.To, e.Message.Version.Addressing, e.Message.Version.Envelope, e.Message);
        }

        public void StopListening()
        {
            _log.Debug("Stopping ConsoleListener");
            _serviceHost.Close();
        }

        public void Close()
        {
            _serviceHost = null;
        }
    }
}
