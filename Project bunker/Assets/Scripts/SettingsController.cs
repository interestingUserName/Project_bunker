using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour, IPunObservable
{
    public Transform settingPanelTransform;
    public PhotonView PV;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        settingPanelTransform = GameObject.Find("Settings panel").transform;
        PV = GetComponent<PhotonView>();
        GameObject.Find("SettingsControllerPointer").GetComponent<SettingsControllerPointer>().controller = this;
    }

    [PunRPC]
    public void RPC_UpdateSettingUp(int child)
    {
        settingPanelTransform.GetChild(child).GetChild(1).gameObject.GetComponent<PropertyValue>().Add(true);
    }

    [PunRPC]
    public void RPC_UpdateSettingDown(int child)
    {
        settingPanelTransform.GetChild(child).GetChild(1).gameObject.GetComponent<PropertyValue>().Add(false);
    }

    public void UpdateSettingUp(int child)
    {
        PV.RPC("RPC_UpdateSettingUp", RpcTarget.AllBuffered, child);
    }

    public void UpdateSettingDown(int child)
    {
        PV.RPC("RPC_UpdateSettingDown", RpcTarget.AllBuffered, child);
    }
}
