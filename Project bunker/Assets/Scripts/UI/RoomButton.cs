using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomButton : MonoBehaviour
{
    public Text nameText;
    public Text sizeText;

    public string roomName;
    public int roomSize;

    private Outline outline;

    private void Start()
    {
        outline = GetComponent<Outline>();
    }

    public void SetRoom()
    {
        nameText.text = roomName;
        sizeText.text = roomSize.ToString();
    }

    public void JoinRoomOnClick()
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    void OnMouseEnter()
    {
        outline.enabled = true;
    }

    public void OnMouseExit()
    {
        outline.enabled = false;
    }
}
