using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyController : MonoBehaviour, IPunObservable
{
    public Dictionary<int, bool> isReadyList = new Dictionary<int, bool>();
    public PhotonView PV;
    public bool isReady = false;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        GameObject.Find("Room network manager").GetComponent<RoomNetworkManager>().readyController = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine && Input.GetKeyDown(KeyCode.Space))
        {
            if (isReady)
            {
                isReady = false;
            }
            else
            {
                isReady = true;
            }
            PV.RPC("RPC_UpdateIsReady", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.ActorNumber, isReady);
        }
    }

    [PunRPC]
    void RPC_UpdateIsReady(int actorNumber, bool _isReady)
    {
        Debug.Log(actorNumber.ToString() + " is " + _isReady.ToString());
        isReadyList.Remove(actorNumber);
        isReadyList.Add(actorNumber, _isReady);
        GameObject playersPanel = GameObject.Find("Players panel");
        for (int i = 0; i < playersPanel.transform.childCount; i++)
        {
            if (playersPanel.transform.GetChild(i).gameObject.GetComponent<ActorNumber>().actorNumber == actorNumber)
            {
                if (_isReady)
                {
                    playersPanel.transform.GetChild(i).GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    playersPanel.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
                }
            }
        }
    }
}
