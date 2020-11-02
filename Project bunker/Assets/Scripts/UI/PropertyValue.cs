using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertyValue : MonoBehaviour
{
    public int min;
    public int max;
    public int step;
    public int value;
    public int propertyIndex;
    public GameSettings gameSettings;

    private void Start()
    {
        GetComponent<Text>().text = value.ToString();
    }

    void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            switch (propertyIndex)
            {
                case 0:
                    value = gameSettings.cards;
                    break;
                case 1:
                    value = gameSettings.prepareTime;
                    break;
                case 2:
                    value = gameSettings.showTime;
                    break;
                case 3:
                    value = gameSettings.discussTime;
                    break;
                case 4:
                    value = gameSettings.sayTime;
                    break;
                case 5:
                    value = gameSettings.allTime;
                    break;
            }
        }

        if (value > max)
        {
            value = max;
        }
        if (value < min)
        {
            value = min;
        }
        GetComponent<Text>().text = value.ToString();
    }

    public void Add(bool isPositive)
    {
        if (isPositive)
        {
            value += step;
        }
        else
        {
            value -= step;
        }
        GetComponent<Text>().text = value.ToString();
    }
}
