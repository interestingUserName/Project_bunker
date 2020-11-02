using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSpawner : MonoBehaviour
{
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
        PhotonNetwork.Instantiate("GameManager", new Vector3(0, 0, 0), Quaternion.identity);
    }
}
