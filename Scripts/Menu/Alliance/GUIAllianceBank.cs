using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIAllianceBank : MonoBehaviour {

    [SerializeField] private InputField Value_Antymatery;
    [SerializeField] private Text[] AllianceData;

    private GUISettingsAlliance Setting_Alliance;
    private statystyki stats;
    private GUIOverview GUIOverview;

    private void Start () {
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIOverview = GameObject.Find("Interface").GetComponent<GUIOverview>();
        Setting_Alliance = GameObject.Find("CanvasesAlliance").GetComponent<GUISettingsAlliance>();
    }
	
	private void LateUpdate () {
        AllianceData[0].text = stats.Get_Data_From("Alliance_Antymatery").ToString(); //Alliance antymatery
        AllianceData[1].text = stats.Get_Data_From("Antymatery").ToString(); // player antymatery
    }

    public void BtnPay()
    {
        //wplac do banku
        int antymatery = int.Parse(Value_Antymatery.text);
        if (antymatery >= 0)
        {
            if (Setting_Alliance.Check_Antymateries(antymatery) == true) //true wykonuje akcje
            {
                stats.Change_Antymatery(-antymatery);
                stats.Set_Data("Alliance_Antymatery", stats.Get_Data_From("Alliance_Antymatery") + antymatery);
                GUIOverview.View_CanvasMessage("You paid: " + antymatery);
            }
            else
            {
                GUIOverview.View_CanvasMessage("Too small Antymateries!");
            }
        }
        else
        {
            GUIOverview.View_CanvasMessage("Haha. Funny!");
        }
    }
}
