using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIShipPanel : MonoBehaviour
{
    public Text Txtship_name;
    public Image imgShip;
    public Sprite[] skin_ship;
    public Image panelConsumption;
    public Image panelLife;
    public Image panelChanceDrop;

    public GameObject[] Bonus_Panel;
    private int activate_bonus;

    private statystyki stats;
    private Operations operations = new Operations();

    private void Start()
    {
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
    }
   
    private void Info_ShipPanel()
    {
        imgShip.sprite = skin_ship[stats.Get_Data_From("Ship_Id")];
        Txtship_name.text = stats.Get_String_Data_From("Ship_Name");
        panelConsumption.rectTransform.sizeDelta = new Vector2(150f * operations.Change_result(stats.Get_Consumption(), 15), 20f);
        panelLife.rectTransform.sizeDelta = new Vector2(150f * operations.Change_result(stats.Get_Life(), 6), 20f);
        panelChanceDrop.rectTransform.sizeDelta = new Vector2(150f * operations.Change_result(stats.Get_Chance_Drop(), 18), 20f);
    }
    private void Info_BonusPanel()
    {
        if (activate_bonus >= 1)
        {
            Bonus_Panel[0].SetActive(true);
        }
        else
        {
            Bonus_Panel[0].SetActive(false);
        }
        View_Bonus("Shield", 1);
        View_Bonus("Combustion", 2);
        View_Bonus("Laser_Technology", 3);
        View_Bonus("Mining_Technology", 4);
        View_Bonus("Antymatery_Technology", 5);
    }
    private void View_Bonus(string technology, int nr)
    {
        if (stats.Get_Float_Data_From(technology) == 3f || stats.Get_Data_From(technology) == 3)
        {
            activate_bonus += 1;
            Bonus_Panel[nr].SetActive(true);
        }
        else
        {
            Bonus_Panel[nr].SetActive(false);
        }
    }
    private void LateUpdate()
    {
        Info_ShipPanel();
        Info_BonusPanel();
    }
}