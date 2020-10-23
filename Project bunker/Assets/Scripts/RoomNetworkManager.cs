using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomNetworkManager : MonoBehaviourPunCallbacks, IInRoomCallbacks, IPunObservable
{
    public Text roomNameText;
    public Text playersCountText;

    public PhotonView PV;

    public static RoomNetworkManager room;

    public Transform playersPanel;
    public GameObject playerListingPrefab;
    public GameObject startButton;
    public ReadyController readyController;

    private void Awake()
    {
        if (RoomNetworkManager.room == null)
        {
            RoomNetworkManager.room = this;
        }
        else
        {
            if (RoomNetworkManager.room != this)
            {
                Destroy(RoomNetworkManager.room.gameObject);
                RoomNetworkManager.room = this;
            }
        }
    }

    private void Start()
    {
        playersPanel = GameObject.Find("Players panel").transform;
        PV = GetComponent<PhotonView>();
        PhotonNetwork.Instantiate("ReadyController", new Vector3(0, 0, 0), Quaternion.identity);
        PhotonNetwork.Instantiate("SettingsController", new Vector3(0, 0, 0), Quaternion.identity);
        ClearPlayerListings();
        ListPlayers();
        SetRoomName();
        UpdatePlayersCount();
    }

    private void Update()
    {
    }

    public override void OnJoinedRoom()
    {
    }

    private void ClearPlayerListings()
    {
        for (int i = playersPanel.childCount - 1; i >= 0; i--)
        {
            Destroy(playersPanel.GetChild(i).gameObject);
        }
        if (readyController != null)
        {
            readyController.isReadyList.Clear();
        }
    }

    private void ListPlayers()
    {
        if (PhotonNetwork.InRoom)
        {
            foreach(Player player in PhotonNetwork.PlayerList)
            {
                GameObject tempListing = Instantiate(playerListingPrefab, playersPanel);
                tempListing.GetComponent<ActorNumber>().actorNumber = player.ActorNumber;
                Text tempText = tempListing.transform.GetChild(0).GetComponent<Text>();
                tempText.text = player.NickName;
                if (readyController != null)
                {
                    readyController.isReadyList.Add(player.ActorNumber, false);
                }
            }
        }
    }

    public void StartGame()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void SetRoomName()
    {
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
    }

    public void UpdatePlayersCount()
    {
        playersCountText.text = (PhotonNetwork.CurrentRoom.PlayerCount + "/" + PhotonNetwork.CurrentRoom.MaxPlayers);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log(newPlayer.NickName + "has joined the room");
        UpdatePlayersCount();
        ClearPlayerListings();
        ListPlayers();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log(otherPlayer.NickName + "has left the game");
        UpdatePlayersCount();
        ClearPlayerListings();
        ListPlayers();
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Debug.Log("You left room");
        PhotonNetwork.LoadLevel(0);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
}
