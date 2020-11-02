using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviourPunCallbacks, IPunObservable
{
    public Transform settingPanelTransform;
    public PhotonView PV;
    public GameSettings gameSettings;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        settingPanelTransform = GameObject.Find("Settings panel").transform;
        gameSettings = GameObject.Find("GameSettings").GetComponent<GameSettings>();
        PV = GetComponent<PhotonView>();
        GameObject.Find("SettingsControllerPointer").GetComponent<SettingsControllerPointer>().controller = this;
    }

    [PunRPC]
    public void RPC_UpdateSettings(int value1, int value2, int value3, int value4, int value5, int value6)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            gameSettings.cards = value1;
            gameSettings.prepareTime = value2;
            gameSettings.showTime = value3;
            gameSettings.discussTime = value4;
            gameSettings.sayTime = value5;
            gameSettings.allTime = value6;
        }
    }

    public void UpdateSettingUp(int child)
    {
        settingPanelTransform.GetChild(child).GetChild(1).gameObject.GetComponent<PropertyValue>().Add(true);
        gameSettings.Update();
        PV.RPC("RPC_UpdateSettings", RpcTarget.AllBuffered, gameSettings.cards, gameSettings.prepareTime, gameSettings.showTime, gameSettings.discussTime, gameSettings.sayTime, gameSettings.allTime);
    }

    public void UpdateSettingDown(int child)
    {
        settingPanelTransform.GetChild(child).GetChild(1).gameObject.GetComponent<PropertyValue>().Add(false);
        gameSettings.Update();
        PV.RPC("RPC_UpdateSettings", RpcTarget.AllBuffered, gameSettings.cards, gameSettings.prepareTime, gameSettings.showTime, gameSettings.discussTime, gameSettings.sayTime, gameSettings.allTime);
    }

    public void UpdateSettings()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PV.RPC("RPC_UpdateSettings", RpcTarget.AllBuffered, gameSettings.cards, gameSettings.prepareTime, gameSettings.showTime, gameSettings.discussTime, gameSettings.sayTime, gameSettings.allTime);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Invoke("UpdateSettings", 0.5f);
    }
}
