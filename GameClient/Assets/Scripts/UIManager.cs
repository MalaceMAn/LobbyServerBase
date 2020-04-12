using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace LobbyClient
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        public List<CanvasGroup> menus = new List<CanvasGroup>();
        public CanvasGroup currentMenu;
        public List<CanvasGroup> dialog = new List<CanvasGroup>();

        public TMP_InputField usernameField;
        public TMP_InputField passwordField;
        public TextMeshProUGUI serverStatusText;
        private Color onlineColor = Color.green;
        private Color offlineColor = Color.red;

        private void Awake()
        {

            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Debug.Log("Instance already exists, destroying object!");
                Destroy(this);
            }

            currentMenu = menus[0];
            UpdateMenu();
        }

        public void LoginToServerButtonClick()
        {
           // Client.instance.ClientLoginRequest(usernameField.text, passwordField.text);
           ClientSend.SendLoginRequest();
        }
        public void ServerOnline()
        {
            serverStatusText.text = "Online";
            serverStatusText.color = onlineColor;

        }
        public void ServerOffline()
        {
            serverStatusText.text = "Offline";
            serverStatusText.color = offlineColor;
        }

        public void UpdateMenu()
        {
            foreach (CanvasGroup cG in menus)
            {
                if (cG == currentMenu)
                {
                    cG.alpha = 1;
                    cG.interactable = true;
                    cG.blocksRaycasts = true;
                }
                else
                {
                    cG.alpha = 0;
                    cG.interactable = false;
                    cG.blocksRaycasts = false;
                }
            }
        }

        public void ShowLoginError(int _errorCode)
        {
            dialog[_errorCode].alpha = 1;
            dialog[_errorCode].interactable = true;
            dialog[_errorCode].blocksRaycasts = true;
        }
        public void HideDialog()
        {
            foreach (CanvasGroup cG in dialog)
            {
                cG.alpha = 0;
                cG.interactable = false;
                cG.blocksRaycasts = false;
            }
        }

    }
}
