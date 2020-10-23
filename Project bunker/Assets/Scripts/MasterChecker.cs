using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterChecker : MonoBehaviour
{
    private void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            gameObject.SetActive(false);
        }
    }
}
