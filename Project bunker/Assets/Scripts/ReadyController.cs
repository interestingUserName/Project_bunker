using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ReadyController : MonoBehaviourPunCallbacks, IPunObservable
{
    public PhotonView PV;
    public bool isReady = false;
    public Text readyText;
    public GameObject startButton;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        GameObject.Find("Room network manager").GetComponent<RoomNetworkManager>().readyController = this;
        readyText = GameObject.Find("ReadyText").GetComponent<Text>();
        startButton = GameObject.Find("StartButton");

        if (PV.IsMine)
        {
            startButton.SetActive(false);
        }

        PV.RPC("RPC_UpdateIsReady", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.ActorNumber, isReady);

    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine && Input.GetKeyDown(KeyCode.Space))
        {
            if (isReady)
            {
                isReady = false;
                readyText.text = "space - ready";
            }
            else
            {
                isReady = true;
                readyText.text = "space - not ready";
            }
            PV.RPC("RPC_UpdateIsReady", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.ActorNumber, isReady);
        }
        AllReadyCheck();
    }

    public void AllReadyCheck()
    {
        if (!PhotonNetwork.IsMasterClient || !PV.IsMine)
        {
            return;
        }

        bool allReady = true;
        GameObject playersPanel = GameObject.Find("Players panel");
        for (int i = 0; i < playersPanel.transform.childCount; i++)
        {
            if (!playersPanel.transform.GetChild(i).GetChild(1).gameObject.activeSelf)
            {
                allReady = false;
            }
        }
        if (allReady)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
    }

    [PunRPC]
    void RPC_UpdateIsReady(int actorNumber, bool _isReady)
    {
        Debug.Log(actorNumber.ToString() + " is " + _isReady.ToString());
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

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        isReady = false;
        readyText.text = "space - ready";
    }
}
    