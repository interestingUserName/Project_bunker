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
    private Dictionary<string, GameObject> panels;
    [SerializeField]
    private GameObject authorizationPanel;
    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private GameObject privateGamePanel;
    [SerializeField]
    private GameObject infoPanel;
    [SerializeField]
    private GameObject lobbyPanel;

    private GameObject currentPanel;

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

    private void CloseCurrentPanel()
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
