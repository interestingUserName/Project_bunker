using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerPointer : MonoBehaviour
{
    public void Open(string panelName)
    {
        UIManager.getInstance().Open(panelName);
    }
}
