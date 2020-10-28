using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildChecker : MonoBehaviour
{
    private Transform content;
    private Text myText;

    // Start is called before the first frame update
    void Start()
    {
        content = gameObject.transform.parent.GetChild(1);
        myText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (content.childCount == 0)
        {
            myText.enabled = true;
        }
        else
        {
            myText.enabled = false;
        }
    }
}
