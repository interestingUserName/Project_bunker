using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSpawner : MonoBehaviour
{
    public GameObject infoButton1;
    public GameObject infoButton2;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Invoke("SpawnGameManager", 1);
        }
    }

    void SpawnGameManager()
    {
        ShowInfoButtons();
        PhotonNetwork.Instantiate("GameManager", new Vector3(0, 0, 0), Quaternion.identity);
    }

    void ShowInfoButtons()
    {
        infoButton1.SetActive(true);
        infoButton2.SetActive(true);
    }
}
