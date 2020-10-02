using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class MenuNetworkManager : MonoBehaviourPunCallbacks
{
    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log("Player was disconnected");
        UIManager.getInstance().Open("Authorization");
    }
}
