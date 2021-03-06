﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUISettingsAlliance : MonoBehaviour {

    private statystyki stats;
    private GUIOverview GUIOverview;
    private GUIPlanetOperations GUIPlanetOperations;
    [SerializeField] private XmlOperations xmlOperations;

    public Canvas CanvasDelete;
    public InputField[] AllianceDataInput;
    public Text[] txtLength;
    public GameObject[] panels;
    [SerializeField] private Text[] AllianceData;
    internal readonly int cost = 1;
    public RawImage Avatar;

    [SerializeField] private InputField antymateryBank;

    private void Start () {
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIOverview = GameObject.Find("Interface").GetComponent<GUIOverview>();
        GUIPlanetOperations = GameObject.Find("Interface").GetComponent<GUIPlanetOperations>();
        AllianceDataInput[2].text = stats.Get_String_Data_From("Alliance_Avatar");
        GUIPlanetOperations.SetAvatar(AllianceDataInput[2].text, "Alliance_Avatar", Avatar);
        GetAllianceData();
    }

    private void LateUpdate()
    {
        ShowActivePanels();
        txtLength[0].text = GUIPlanetOperations.ReturnLength(AllianceDataInput[0]);
        txtLength[1].text = GUIPlanetOperations.ReturnLength(AllianceDataInput[1]);
        txtLength[2].text = GUIPlanetOperations.ReturnLength(AllianceDataInput[2]);
        txtLength[3].text = GUIPlanetOperations.ReturnLength(AllianceDataInput[3]);
        AllianceData[1].text = ShowCost_SetAllianceData();
        AllianceData[2].text = ShowCost_CreateAlliance();

        AllianceData[3].text = stats.Get_Data_From("Alliance_Antymatery").ToString(); //Alliance antymatery
        AllianceData[4].text = stats.Get_Data_From("Antymatery").ToString(); // player antymatery
    }

    internal void GetAllianceData()
    {
        AllianceDataInput[0].text = stats.Get_String_Data_From("Alliance_Name");
        AllianceDataInput[1].text = stats.Get_String_Data_From("Alliance_Tag");
        AllianceDataInput[2].text = stats.Get_String_Data_From("Alliance_Avatar");

        AllianceData[0].text = stats.Get_String_Data_From("Alliance_Name");
    }
    private bool HasAlly() 
    {
        return CheckName(AllianceDataInput[0]) == true && CheckTag(AllianceDataInput[1]) == true;
    }

    internal string ShowCost_CreateAlliance()
    {
        return stats.Get_Data_From("Antymatery") + "/" + cost;
    }

    internal string ShowCost_SetAllianceData()
    {
        return stats.Get_Data_From("Alliance_Antymatery") + "/" + cost;
    }

    internal bool CheckName(InputField AllianceName)
    {
        return AllianceName.text != "no alliance" || AllianceName.text != "";
    }

    internal bool CheckTag(InputField AllianceTag)
    {
        return AllianceTag.text != "no tag" || AllianceTag.text != "";
    }

    internal bool CheckAntymateries(int value_cost)
    {
        return stats.Get_Data_From("Antymatery") >= value_cost;
    }

    public void Change_Alliance_Data()
    {
        if (HasAlly())
        {
            if (stats.Get_Data_From("Antymatery") >= cost)
            {
                stats.Set_Data("Alliance_Antymatery", stats.Get_Data_From("Alliance_Antymatery")-cost);
                stats.Set_String_Data("Alliance_Name", AllianceDataInput[0].text);
                stats.Set_String_Data("Alliance_Tag", AllianceDataInput[1].text);
                GetAllianceData();
                GUIOverview.page = 15;
            }
            else
            {
                GUIOverview.View_CanvasMessage("Too small Antymateries!");
            }
        }
        else
        {
            GUIOverview.View_CanvasMessage("You don't have alliance!");
            GUIOverview.page = 12;
        }

    }

    public void BtnLeave()
    {
        CanvasDelete.enabled = true;
    }

    public void BtnAnswer(string answer)
    {
        if (answer == "Yes")
        {
            stats.Set_String_Data("Alliance_Name", "no alliance");
            stats.Set_String_Data("Alliance_Tag", "no tag");
            stats.Set_String_Data("Alliance_Avatar", "http://www.owiki.de/images/2/28/Flottenadmiral.PNG");
            stats.Set_Data("MemberID", 0);
            stats.Set_Data("Space Base", 0);
            stats.Set_Data("Scout", 0);
            stats.Set_Data("Alliance_Antymatery", 0);
            xmlOperations.ClearFile("Allies.xml");
            GetAllianceData();
            CanvasDelete.enabled = false;
            GUIOverview.page = 12;
        }
        else
        {
            CanvasDelete.enabled = false;
        }
    }

    public void BtnCreateAlliance()
    {
        if (stats.Get_Data_From("Antymatery") >= cost)
        {
            if (CheckName(AllianceDataInput[0]) && CheckTag(AllianceDataInput[1]))
            {
                stats.Change_Antymatery(-cost);
                stats.Set_String_Data("Alliance_Name", AllianceDataInput[3].text);
                stats.Set_String_Data("Alliance_Tag", AllianceDataInput[4].text);
                stats.Set_String_Data("Alliance_Avatar", "http://www.owiki.de/images/2/28/Flottenadmiral.PNG");
                stats.Set_Data("MemberID", 0);
                stats.Set_Data("Space Base", 0);
                stats.Set_Data("Scout", 0);
                stats.Set_Data("Aliiance_Antymatery", 0);
                GetAllianceData();
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

    public void BtnPayToBank()
    {
        int antymatery = int.Parse(antymateryBank.text);
        if (antymatery >= 0)
        {
            if (CheckAntymateries(antymatery))
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

    private void ShowActivePanels()
    {
        panels[0].SetActive(SetActivePanels(GUIOverview.page));
        panels[1].SetActive(SetActivePanels(GUIOverview.page));
    }

    private bool SetActivePanels(int page)
    {
        return page >= 15 && page <= 20;//if true Show PanelBottom at Alliance
    }

    public void SetAllianceAvatar()
    {
        if (HasAlly() && AllianceDataInput[2].text != "")
        {
            GUIPlanetOperations.SetAvatar(AllianceDataInput[2].text, "Alliance_Avatar", Avatar);
        }
    }
}
