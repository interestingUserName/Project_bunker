using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    public int cards;
    public int prepareTime;
    public int showTime;
    public int discussTime;
    public int sayTime;
    public int allTime;

    public Text cardsText;
    public Text prepareTimeText;
    public Text showTimeText;
    public Text discussTimeText;
    public Text sayTimeText;
    public Text allTimeText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        if (cardsText == null)
        {
            return;
        }
        if (PhotonNetwork.IsMasterClient)
        {
            cards = Convert.ToInt32(cardsText.text);
            prepareTime = Convert.ToInt32(prepareTimeText.text);
            showTime = Convert.ToInt32(showTimeText.text);
            discussTime = Convert.ToInt32(discussTimeText.text);
            sayTime = Convert.ToInt32(sayTimeText.text);
            allTime = Convert.ToInt32(allTimeText.text);
        }
    }
}
