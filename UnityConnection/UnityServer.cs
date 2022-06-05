using System;
using RiptideNetworking;
using RiptideNetworking.Utils;
using Astrostrider.Classes;

public enum ServerToClientId : ushort
{
    SwitchScene = 1,
    Close = 2
}

public enum ClientToServerId : ushort
{
    Quit = 1
}

namespace Astrostrider.UnityConnection
{
    public class UnityServer : NetworkManager
    {
        private static NetworkManager _instance;
        public static NetworkManager Instance
        {
            get { return _instance; }
            private set
            {
                if (_instance is null)
                {
                    _instance = value;
                }
                else
                {
                    _instance = value;
                }
            }
        }

        private ClientInfo _unityClientInfo;
        public ClientInfo UnityClientInfo { get { return _unityClientInfo; } set { _unityClientInfo = value; } }

        #region "Constructors"

        public UnityServer(ushort port, ushort maxClientCount) : base(port, maxClientCount) { }

        #endregion

        #region "Methods"

        public override void Start()
        {
            base.Start();
            UnityClientInfo = new ClientInfo();
        }

        public override void Stop()
        {
            base.Stop();
        }

        #endregion

        #region "Events Handlers"

        public override void Connected(object sender, EventArgs e)
        {
            SwitchUnityScene(ClientInfo.UnityScenesId.Ground);
        }

        #endregion

        #region "Messages"

        public void SwitchUnityScene(ClientInfo.UnityScenesId unitySceneId)
        {
            Message message = Message.Create(MessageSendMode.reliable, (ushort)ServerToClientId.SwitchScene);
            message.AddInt((int)unitySceneId);
            Server.Send(message, (ushort)ServerToClientId.SwitchScene);
        }

        public void CloseUnityApplication()
        {
            Message message = Message.Create(MessageSendMode.reliable, (ushort)ServerToClientId.Close);
            Server.Send(message, (ushort)ServerToClientId.Close);
        }

        [MessageHandler((ushort)ClientToServerId.Quit)]
        private static void CloseUnity(Message message)
        {
            Instance.Disconnect();
        }

        #endregion
    }

    public struct ClientInfo
    {
        public enum UnityScenesId
        {
            Ground = 0,
            SampleScene = 1,
            LUNA = 2
        }
    }
}
