﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateAlliance : MonoBehaviour
{
    public Text[] AllianceData;
    public InputField[] AllianceDataInput;

    private GUISettingsAlliance Setting_Alliance;
    private statystyki stats;
    private GUIOverview GUIOverview;

    private void Start()
    {
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIOverview = GameObject.Find("Interface").GetComponent<GUIOverview>();
        Setting_Alliance = GameObject.Find("CanvasesAlliance").GetComponent<GUISettingsAlliance>();
    }
    private void LateUpdate()
    {
       AllianceData[0].text = Setting_Alliance.Return_Length(AllianceDataInput[0]);
       AllianceData[1].text = Setting_Alliance.Return_Length(AllianceDataInput[1]);
       AllianceData[2].text = Setting_Alliance.ShowCost_CreateAllianceData();
    }

    public void BtnCreateAlliance()
    {
        if (stats.Get_Data_From("Antymatery") >= Setting_Alliance.cost)
        {
            if (Setting_Alliance.Check_Name(AllianceDataInput[0]) && Setting_Alliance.Check_Tag(AllianceDataInput[1]))
            {
                stats.Change_Antymatery(-Setting_Alliance.cost);
                stats.Set_String_Data("Alliance_Name", AllianceDataInput[0].text);
                stats.Set_String_Data("Alliance_Tag", AllianceDataInput[1].text);
                stats.Set_String_Data("Alliance_PhotoUrl", "no url");
                stats.Set_Data("Base", 0);
                stats.Set_Data("Scout", 0);
                stats.Set_Data("Aliiance_Antymatery", 0);
                Setting_Alliance.Get_Alliance_Data();
                GUIOverview.page = 15;
            }
            else
            {
                GUIOverview.View_CanvasMessage("Empty Name or Tag!");
            }
        }
        else
        {
            GUIOverview.View_CanvasMessage("Too small Antymateries!");
        }
    }
}
