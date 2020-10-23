using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    public static LobbyManager lobbyManager;

    public InputField roomNameInputField;
    public Slider roomSizeSlider;
    public GameObject goButton;

    public GameObject roomListingPrefab;
    public Transform roomsPanel;

    private string roomName;
    private int roomSize;

    private void Awake()
    {
        lobbyManager = this;
    }

    private void Start()
    {
        OnRoomSizeChanged();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            goButton.GetComponent<Button>().onClick.Invoke();
        }
    }

    public void RemoveRoomListings()
    {
        while (roomsPanel.childCount != 0)
        {
            Destroy(roomsPanel.GetChild(0));
        }
    }

    public void ListRoom(RoomInfo room)
    {
        if (room.IsOpen && room.IsVisible)
        {
            GameObject tempListing = Instantiate(roomListingPrefab, roomsPanel);
            RoomButton tempButton = tempListing.GetComponent<RoomButton>();
            tempButton.roomName = room.Name;
            tempButton.roomSize = room.MaxPlayers;
            tempButton.SetRoom();
        }
    }

    public void CreateRoom()
    {
        Debug.Log("Trying to create a new room");

        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };
        PhotonNetwork.CreateRoom(roomName, roomOps);
    }

    public void OnRoomNameChanged(string nameIn)
    {
        if (roomNameInputField.text.Length > 12)
        {
            roomNameInputField.text = roomNameInputField.text.Substring(0, roomNameInputField.text.Length - 1);
        }

        nameIn = roomNameInputField.text;

        if (roomNameInputField.text.Length >= 3)
        {
            goButton.SetActive(true);
        }
        else
        {
            goButton.SetActive(false);
        }
        roomName = nameIn;
    }

    public void OnRoomSizeChanged()
    {
        roomSize = (int)roomSizeSlider.value;
    }

    public void JoinLobbyOnClick()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    #region PUN CALLBACKS

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        RemoveRoomListings();
        foreach(RoomInfo room in roomList)
        {
            ListRoom(room);
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to create a new room but failed, there must already be a room with the same name");
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("Room created. Name: " + PhotonNetwork.CurrentRoom.Name + "; Size: " + PhotonNetwork.CurrentRoom.MaxPlayers );
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("We are now in a room");

        UIManager.getInstance().CloseCurrentPanel();
        PhotonNetwork.LoadLevel(1);
    }

    #endregion

    #region UI CALLBACKS

    public void OnLeaveGameButtonClicked()
    {
        PhotonNetwork.LeaveRoom();
    }

    #endregion

}