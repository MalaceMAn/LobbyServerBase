using System;
using System.Collections.Generic;
using System.Text;

namespace LobbyServer
{
    /// <summary>Sent from server to client.</summary>
    public enum ServerPackets
    {
        welcome = 1,
        LoginRequestResponse
    }

    /// <summary>Sent from client to server.</summary>
    public enum ClientPackets
    {
        welcomeReceived = 1,
        sendLoginRequest
    }
}
