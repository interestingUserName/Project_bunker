using ExitGames.Client.Photon;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Photon.Pun.Demo.Asteroids
{
    public class LobbyManager : MonoBehaviourPunCallbacks, ILobbyCallbacks
    {
        public static LobbyManager lobbyManager;

        public InputField roomNameInputField;
        public InputField roomSizeInputField;

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
            PhotonNetwork.ConnectUsingSettings();
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
            roomName = nameIn;
        }

        public void OnRoomSizeChanged(string sizeIn)
        {
            roomSize = int.Parse(sizeIn);
        }

        public void JoinLobbyOnClick()
        {
            if (!PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinLobby();
            }
        }

        #region PUN CALLBACKS

        public override void OnConnectedToMaster()
        {
            Debug.Log("Player has connected to the Photon master server");
            PhotonNetwork.AutomaticallySyncScene = true;
        }

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
            Debug.Log("Room created");
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            Debug.Log("You joined lobby");
        }

        #endregion

        #region UI CALLBACKS

        public void OnLeaveGameButtonClicked()
        {
            PhotonNetwork.LeaveRoom();
        }

        #endregion

    }
}