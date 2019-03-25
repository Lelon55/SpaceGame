using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateAlliance : MonoBehaviour
{
    public Text[] AllianceData;

    private statystyki stats;
    private GUIOverview GUIOverview;
    private const int cost = 1;

    private void Start()
    {
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIOverview = GameObject.Find("Interface").GetComponent<GUIOverview>();
    }

    private bool Check_Name()
    {
        if (AllianceData[0].text == "no alliance" || AllianceData[0].text == "")
        {
            return false;
        }
        return true;
    }
    private bool Check_Tag()
    {
        if (AllianceData[1].text == "no tag" || AllianceData[1].text == "")
        {
            return false;
        }
        return true;
    }
    private bool Check_Antymateries()
    {
        if (stats.Get_Data_From("Antymatery") >= cost)
        {
            return true;
        }
        return false;
    }

    public void BtnCreateAlliance()
    {
        if (Check_Antymateries() == true)
        {
            if (Check_Name() == true && Check_Tag() == true)
            {
                stats.Change_Antymatery(-cost);
                stats.Set_String_Data("Alliance_Name", AllianceData[0].text);
                stats.Set_String_Data("Alliance_Tag", AllianceData[1].text);
                GUIOverview.page = 15;
            }
            else
            {
                Debug.Log("wprowadz dane");
            }
        }
        else
        {
            Debug.Log("malo anty");
        }
    }
}
