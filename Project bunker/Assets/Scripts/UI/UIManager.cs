using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager getInstance()
    {
        if (instance == null)
            instance = GameObject.Find("UI Manager").GetComponent<UIManager>();
        return instance;
    }

    [SerializeField]
    private Dictionary<string, GameObject> panels = new Dictionary<string, GameObject>();
    [SerializeField]
    private GameObject authorizationPanel = null;
    [SerializeField]
    private GameObject menuPanel = null;
    [SerializeField]
    private GameObject privateGamePanel = null;
    [SerializeField]
    private GameObject infoPanel = null;
    [SerializeField]
    private GameObject lobbyPanel = null;

    private GameObject currentPanel = null;

    private void Start()
    {
        panels = new Dictionary<string, GameObject>();
        InitializePanels();
        Open("Authorization");
    }

    private void InitializePanels()
    {
        panels.Add("Authorization", authorizationPanel);
        panels.Add("Menu", menuPanel);
        panels.Add("Private game", privateGamePanel);
        panels.Add("Info", infoPanel);
        panels.Add("Lobby", lobbyPanel);
    }

    public void CloseCurrentPanel()
    {
        Destroy(currentPanel);
    }
    public void Open(string _panelName)
    {
        CloseCurrentPanel();
        panels.TryGetValue(_panelName, out GameObject panel);
        currentPanel = Instantiate(panel, GameObject.Find("Canvas").transform);
    }
}
