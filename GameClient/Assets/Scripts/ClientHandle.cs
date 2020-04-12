using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
namespace LobbyClient
{
    public class ClientHandle : MonoBehaviour
    {
        public static void Welcome(Packet _packet)
        {
            string _msg = _packet.ReadString();
            int _myId = _packet.ReadInt();

            Debug.Log($"Message from server: {_msg}");
            Client.instance.myId = _myId;
            ClientSend.WelcomeReceived();
            //At this point both test are complete. The client is able to communicate with the server by both TCP and UDP.
            UIManager.instance.serverStatusText.color = Color.green;
            UIManager.instance.serverStatusText.text = "Online";
            UIManager.instance.serverStatusText.GetComponentInChildren<Image>().color = Color.green;
            Client.instance.isConnected = true;
            Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
        }

        public static void LoginRequestResponse(Packet _packet)
        {
            string _msg = _packet.ReadString();
            Debug.Log($"Received response from login attempt: {_msg}");
            //if login good proceed to lobby menu
            if(_msg == "Login:Accepted")
            {
                UIManager.instance.currentMenu = UIManager.instance.menus[1];
                UIManager.instance.UpdateMenu();
            }
            else//else present error dialog.
            {
                UIManager.instance.ShowLoginError(0);
            }
            
        }
    }
}