using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomNetworkManager : MonoBehaviourPunCallbacks, IInRoomCallbacks, IPunObservable
{
    public Text roomNameText;
    public Text playersCountText;

    private PhotonView PV;

    public static RoomNetworkManager room;

    private bool isReady = false;

    public Transform playersPanel;
    public GameObject playerListingPrefab;
    public GameObject startButton;

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
        ClearPlayerListings();
        ListPlayers();
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && Input.GetKeyDown(KeyCode.Space))
        {
            if (isReady)
            {
                isReady = false;
            }
            else
            {
                isReady = true;
            }
        }
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
    }

    private void ListPlayers()
    {
        if (PhotonNetwork.InRoom)
        {
            foreach(Player player in PhotonNetwork.PlayerList)
            {
                GameObject tempListing = Instantiate(playerListingPrefab, playersPanel);
                Text tempText = tempListing.transform.GetChild(0).GetComponent<Text>();
                tempText.text = player.NickName;
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
