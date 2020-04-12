using System;
using System.Collections.Generic;
using System.Text;

namespace LobbyServer
{
    class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();

            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
            if (_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }
            // TODO: send player into game
        }

        public static void ProcessLoginRequest(int _fromClient, Packet _packet)
        {
            
            string _username = _packet.ReadString();
            string _password = _packet.ReadString();
            Console.WriteLine($"Processing Login Request on credentials Username:{_username} Password:{_password}");
            if (Server.loginManager.CheckForValidCredentials(_username, _password))
            {
                ServerSend.LoginRequestResponse(_fromClient, "Login:Accepted");
                Console.WriteLine($"Result of Pass on Login Request on credentials Username:{_username} Password:{_password}");
            }
            else
            {
                ServerSend.LoginRequestResponse(_fromClient, "Login:Denied");
                Console.WriteLine($"Result of FAIL on Login Request on credentials Username:{_username} Password:{_password}");
            }
        }
    }
}
