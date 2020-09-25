using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager getInstance()
    {
        if (instance == null)
            instance = new UIManager();
        return instance;
    }

    [SerializeField]
    private Dictionary<string, GameObject> Panels = new Dictionary<string, GameObject>();
    private GameObject currentPanel;
    [SerializeField]
    private GameObject autorizationPanel;

    private void Start()
    {
        InitializePanels();
        Open("Autorization");
    }

    private void InitializePanels()
    {
        Panels.Add("Autorization", autorizationPanel);
    }

    public void Open(string panelName)
    {
        CloseCurrentPanel();
        Panels.TryGetValue(panelName, out GameObject panel);
        currentPanel = Instantiate(panel, GameObject.Find("Canvas").transform);
    }

    private void CloseCurrentPanel()
    {
        Destroy(currentPanel);
    }
}
