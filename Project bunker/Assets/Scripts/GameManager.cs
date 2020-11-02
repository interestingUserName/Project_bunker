using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public InfoStorage infoStorage;
    public PhotonView PV;
    public Text apocalypseNameText;
    public Text apocalypseDescriptionText;
    public Text bunkerDescriptionText;

    public int apocalypseIndex;
    public int population = 0;
    public int destruction = 0;

    public int bunkerDescriptionIndex;
    public int bunkerSpace = 0;
    public int bunkerTime = 0;
    public int bunkerEquipmentIndex1;
    public int bunkerEquipmentIndex2;
    public int bunkerStuffIndex1;
    public int bunkerStuffIndex2;
    public int bunkerStuffIndex3;
    public int bunkerResidentsIndex;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        infoStorage = GameObject.Find("InfoStorage").GetComponent<InfoStorage>();
        apocalypseNameText = GameObject.Find("ApocalypseInfoPanel").transform.GetChild(0).gameObject.GetComponent<Text>();
        apocalypseDescriptionText = GameObject.Find("ApocalypseInfoPanel").transform.GetChild(1).gameObject.GetComponent<Text>();
        bunkerDescriptionText = GameObject.Find("ApocalypseInfoPanel").transform.GetChild(3).gameObject.GetComponent<Text>();
        if (PhotonNetwork.IsMasterClient)
        {
            InitializeApocalypseInfo();
            InitializeBunkerInfo();


            SetApocalyplseInfo(apocalypseIndex, population, destruction);
            SetBunkerInfo(bunkerDescriptionIndex, bunkerSpace, bunkerTime, bunkerEquipmentIndex1, bunkerEquipmentIndex2, bunkerStuffIndex1, bunkerStuffIndex2, bunkerStuffIndex3, bunkerResidentsIndex);
        }
    }

    private void InitializeApocalypseInfo()
    {
        apocalypseIndex = Random.Range(0, infoStorage.apocalypses.Length);
        population = Random.Range(0, 99);
        destruction = Random.Range(0, 99);
    }
    private void InitializeBunkerInfo()
    {
        bunkerDescriptionIndex = Random.Range(0, infoStorage.bunkerDescriptions.Length);
        bunkerSpace = Random.Range(10, 201);
        bunkerTime = Random.Range(1, 13);
        bunkerEquipmentIndex1 = Random.Range(0, infoStorage.bunkerEquipment.Length);
        bunkerEquipmentIndex2 = Random.Range(0, infoStorage.bunkerEquipment.Length);
        while (bunkerEquipmentIndex2 == bunkerEquipmentIndex1)
        {
            bunkerEquipmentIndex2 = Random.Range(0, infoStorage.bunkerEquipment.Length);
        }

        bunkerStuffIndex1 = Random.Range(0, infoStorage.bunkerStuff.Length);
        bunkerStuffIndex2 = Random.Range(0, infoStorage.bunkerStuff.Length);
        while (bunkerStuffIndex2 == bunkerStuffIndex1)
        {
            bunkerStuffIndex2 = Random.Range(0, infoStorage.bunkerStuff.Length);
        }
        bunkerStuffIndex3 = Random.Range(0, infoStorage.bunkerStuff.Length);
        while (bunkerStuffIndex3 == bunkerStuffIndex2 || bunkerStuffIndex3 == bunkerStuffIndex1)
        {
            bunkerStuffIndex3 = Random.Range(0, infoStorage.bunkerStuff.Length);
        }
        bunkerResidentsIndex = Random.Range(0, infoStorage.bunkerResidents.Length);
    }

    private void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        UpdateApocalypseInfoUI();
        UpdateBunkerInfoUI();
    }

    private void UpdateApocalypseInfoUI()
    {
        apocalypseNameText.text = infoStorage.apocalypses[apocalypseIndex].name;
        apocalypseDescriptionText.text = infoStorage.apocalypses[apocalypseIndex].description + "\r\n" + "\r\n";
        apocalypseDescriptionText.text += "Остаток выжившего населения: " + population.ToString() + "%" + "\r\n";
        apocalypseDescriptionText.text += "Разрушения на поверхности: " + destruction.ToString() + "%";
    }

    private void UpdateBunkerInfoUI()
    {
        bunkerDescriptionText.text = infoStorage.bunkerDescriptions[bunkerDescriptionIndex].description + "\r\n" + "\r\n";
        bunkerDescriptionText.text += "Площади убежища: " + bunkerSpace.ToString() + " квадратных метров" + "\r\n" + "\r\n";
        bunkerDescriptionText.text += "Время нахождения в убежище (Еда и питье рассчитаны на весь период пребывания): " + bunkerTime.ToString();
        if (bunkerTime == 1)
        {
            bunkerDescriptionText.text += " месяц" + "\r\n" + "\r\n";
        }
        else if (bunkerTime < 5)
        {
            bunkerDescriptionText.text += " месяца" + "\r\n" + "\r\n";
        }
        else
        {
            bunkerDescriptionText.text += " месяцев" + "\r\n" + "\r\n";
        }
        bunkerDescriptionText.text += "В убежище оборудовано: " + infoStorage.bunkerEquipment[bunkerEquipmentIndex1].equipment + "\r\n";
        bunkerDescriptionText.text += "В убежище оборудовано: " + infoStorage.bunkerEquipment[bunkerEquipmentIndex2].equipment + "\r\n" + "\r\n";
        bunkerDescriptionText.text += "В убежище есть: " + infoStorage.bunkerStuff[bunkerStuffIndex1].stuff + "\r\n";
        bunkerDescriptionText.text += "В убежище есть: " + infoStorage.bunkerStuff[bunkerStuffIndex2].stuff + "\r\n";
        bunkerDescriptionText.text += "В убежище есть: " + infoStorage.bunkerStuff[bunkerStuffIndex3].stuff + "\r\n" + "\r\n";
        bunkerDescriptionText.text += "В убежище живут: " + infoStorage.bunkerResidents[bunkerResidentsIndex].resident;
    }

    public void SetApocalyplseInfo(int _apocalypseIndex, int _population, int _destruction)
    {
        PV.RPC("RPC_SetApocalyplseInfo", RpcTarget.AllBuffered, _apocalypseIndex, _population, _destruction);
    }

    [PunRPC]
    public void RPC_SetApocalyplseInfo(int _apocalypseIndex, int _population, int _destruction)
    {
        apocalypseIndex = _apocalypseIndex;
        population = _population;
        destruction = _destruction;
    }

    public void SetBunkerInfo(int _bunkerDescriptionIndex, int _bunkerSpace, int _bunkerTime, int _bunkerEquipmentIndex1, int _bunkerEquipmentIndex2, int _bunkerStuffIndex1, int _bunkerStuffIndex2, int _bunkerStuffIndex3, int _bunkerResidentsIndex)
    {
        PV.RPC("RPC_SetBunkerInfo", RpcTarget.AllBuffered, _bunkerDescriptionIndex, _bunkerSpace, _bunkerTime, _bunkerEquipmentIndex1, _bunkerEquipmentIndex2, _bunkerStuffIndex1, _bunkerStuffIndex2, _bunkerStuffIndex3, _bunkerResidentsIndex);
    }

    [PunRPC]
    public void RPC_SetBunkerInfo(int _bunkerDescriptionIndex, int _bunkerSpace, int _bunkerTime, int _bunkerEquipmentIndex1, int _bunkerEquipmentIndex2, int _bunkerStuffIndex1, int _bunkerStuffIndex2, int _bunkerStuffIndex3, int _bunkerResidentsIndex)
    {
        bunkerDescriptionIndex = _bunkerDescriptionIndex;
        bunkerSpace = _bunkerSpace;
        bunkerTime = _bunkerTime;
        bunkerEquipmentIndex1 = _bunkerEquipmentIndex1;
        bunkerEquipmentIndex2 = _bunkerEquipmentIndex2;
        bunkerStuffIndex1 = _bunkerStuffIndex1;
        bunkerStuffIndex2 = _bunkerStuffIndex2;
        bunkerStuffIndex3 = _bunkerStuffIndex3;
        bunkerResidentsIndex = _bunkerResidentsIndex;
    }
}
