using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsControllerPointer : MonoBehaviour
{
    public SettingsController controller;

    public void UpdateUp(int child)
    {
        controller.UpdateSettingUp(child);
    }
    public void UpdateDown(int child)
    {
        controller.UpdateSettingDown(child);
    }

}
