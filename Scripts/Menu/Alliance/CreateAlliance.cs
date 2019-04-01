using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateAlliance : MonoBehaviour
{
    public Text[] AllianceData;
    public InputField[] AllianceDataInput;

    private statystyki stats;
    private GUIOverview GUIOverview;
    private const int cost = 1;

    private void Start()
    {
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIOverview = GameObject.Find("Interface").GetComponent<GUIOverview>();
    }
    private void Update()
    {
        Check_Length();
    }
    private void Check_Length()
    {
        AllianceData[2].text = AllianceDataInput[0].text.Length.ToString() + "/" + AllianceDataInput[0].characterLimit;
        AllianceData[3].text = AllianceDataInput[1].text.Length.ToString() + "/" + AllianceDataInput[1].characterLimit;
    }
    private bool Check_Name()
    {
        return AllianceData[0].text != "no alliance" || AllianceData[0].text != "";
    }
    private bool Check_Tag()
    {
        return AllianceData[1].text != "no tag" || AllianceData[1].text != "";
    }
    private bool Check_Antymateries()
    {
        return stats.Get_Data_From("Antymatery") >= cost;
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
