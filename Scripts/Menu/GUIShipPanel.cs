using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIShipPanel : MonoBehaviour
{
    public Text ShipName;
    public Image imgShip, imgLaser;
    public Image panelConsumption, panelLife, panelChanceDrop;

    public GameObject[] Bonus_Panel;
    private int activateBonus;

    private statystyki stats;
    [SerializeField] private Skins skin;
    private GUIPlanetOperations GUIPlanetOperations;

    private void Start()
    {
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIPlanetOperations = GameObject.Find("Interface").GetComponent<GUIPlanetOperations>();
    }

    private void InformationShip()
    {
        imgShip.sprite = skin.skin_statku[stats.Get_Data_From("Ship_Id")];
        imgLaser.sprite = skin.skin_laseru[stats.Get_Data_From("Laser")];
        ShipName.text = stats.Get_String_Data_From("Ship_Name");
        panelConsumption.rectTransform.sizeDelta = new Vector2(150f * GUIPlanetOperations.Change_result(stats.Get_Consumption(), 15), 20f);
        panelLife.rectTransform.sizeDelta = new Vector2(150f * GUIPlanetOperations.Change_result(stats.Get_Life(), 6), 20f);
        panelChanceDrop.rectTransform.sizeDelta = new Vector2(150f * GUIPlanetOperations.Change_result(stats.Get_Chance_Drop(), 18), 20f);
    }

    private void InformationBonus()
    {
        Bonus_Panel[0].SetActive(GetBonusPanel());
        ViewBonus("Shield", 1);
        ViewBonus("Combustion", 2);
        ViewBonus("Laser Technology", 3);
        ViewBonus("Mining Technology", 4);
        ViewBonus("Antymatery Technology", 5);
    }

    private bool GetBonusPanel()
    {
        return activateBonus >= 1;
    }

    private void SetBonusPanel()
    {
        activateBonus += 1;
    }

    private void ViewBonus(string technology, int nr)
    {
        if (stats.Get_Float_Data_From(technology) == 3f || stats.Get_Data_From(technology) == 3)
        {
            SetBonusPanel();
            Bonus_Panel[nr].SetActive(true);
        }
        else
        {
            Bonus_Panel[nr].SetActive(false);
        }
    }

    private void LateUpdate()
    {
        InformationShip();
        InformationBonus();
    }
}