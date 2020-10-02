using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomNetworkManager : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public Text roomNameText;
    public Text playersCountText;

    public static RoomNetworkManager room;
    private PhotonView PV;

    public bool isGameLoaded;
    public int currentScene;

    Player[] photonPlayers;
    public int playersInRoom;
    public int myNumberInRoom;

    public int playerInGame;

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
        SetRoomName();
        UpdatePlayersCount();
        PV = GetComponent<PhotonView>();

        ClearPlayerListings();
        ListPlayers();

        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom = photonPlayers.Length;
        myNumberInRoom = playersInRoom;
    }

    private void Update()
    {
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("We are now in a room");

        ClearPlayerListings();
        ListPlayers();

        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom = photonPlayers.Length;
        myNumberInRoom = playersInRoom;
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
        isGameLoaded = true;
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
        UpdatePlayersCount();
        Debug.Log(newPlayer.NickName + "has joined the room");
        ClearPlayerListings();
        ListPlayers();
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom++;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log(otherPlayer.NickName + "has left the game");
        playersInRoom--;
        ClearPlayerListings();
        ListPlayers();
        UpdatePlayersCount();
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Debug.Log("You left room");
        UIManager.getInstance().Open("Menu");
    }
}
