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

    private void Start()
    {
        GetComponent<Text>().text = value.ToString();
    }

    void Update()
    {
        if (value > max)
        {
            value = max;
            GetComponent<Text>().text = value.ToString();
        }
        if (value < min)
        {
            value = min;
            GetComponent<Text>().text = value.ToString();
        }
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
