using System;
using System.Windows.Threading;
using RiptideNetworking;
using RiptideNetworking.Utils;

namespace Astrostrider.Classes
{
    public class NetworkManager
    {
        #region "Fields"

        private Server _server;
        private ushort _port;
        private ushort _maxClientCount;
        private DispatcherTimer _timer;

        #endregion

        #region "Properties"

        public Server Server { get { return _server; } private set { _server = value; } }
        public ushort Port { get { return _port; } set { _port = value; } }
        public ushort MaxClientCount { get { return _maxClientCount; } set { _maxClientCount = value; } }

        #endregion

        public NetworkManager(ushort port, ushort maxClientCount)
        {
            Port = port;
            MaxClientCount = maxClientCount;
            Server = new Server();
        }

        #region "Methods"
        public virtual void Start()
        {
            Server.Start(Port, MaxClientCount);

            Server.ClientConnected += Connected;
            Server.ClientDisconnected += Disconnected;

            _timer = new DispatcherTimer();

            _timer.Tick += new EventHandler((sender, args) => {
                Server.Tick();
            });
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Start();
        }

        public virtual void Stop()
        {
            Server.Stop();
            _timer.Stop();
        }

        public virtual void Disconnect()
        {
            Server.DisconnectClient(0);
        }

        public virtual void Connected(object sender, EventArgs e)
        {

        }

        public virtual void Disconnected(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
