using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
using Photon.Pun;
using System;

public class AuthorizationManager : MonoBehaviourPunCallbacks
{
    public InputField nameInputField;
    public Text nameInputLog;

    public GameObject goButton;

    private string playerName;
    public void SetPlayerName()
    {
        PhotonNetwork.NickName = playerName;
        Debug.Log("Player name set to: " + PhotonNetwork.NickName);
    }

    public void ConnectPlayer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnNameInputChanged()
    {
        if (nameInputField.text.Length > 16)
        {
            nameInputField.text = nameInputField.text.Substring(0, nameInputField.text.Length - 1);
        }

        playerName = nameInputField.text;

        if (nameInputField.text.Length >= 3)
        {
            goButton.SetActive(true);
        }
        else
        {
            goButton.SetActive(false);
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to the Photon master server");
        PhotonNetwork.AutomaticallySyncScene = true;
        UIManager.getInstance().Open("Menu");
    }
}
