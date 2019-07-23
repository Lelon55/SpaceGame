using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUISettingsAlliance : MonoBehaviour {

    private statystyki stats;
    private GUIOverview GUIOverview;
    private GUIPlanetOperations GUIPlanetOperations;

    public Canvas CanvasDelete;
    public InputField[] AllianceDataInput;
    public Text[] txtLength;
    public GameObject[] panels;
    [SerializeField] private Text[] AllianceData;
    internal readonly int cost = 1;

    // Use this for initialization
    private void Start () {
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIOverview = GameObject.Find("Interface").GetComponent<GUIOverview>();
        GUIPlanetOperations = GameObject.Find("Interface").GetComponent<GUIPlanetOperations>();
        Get_Alliance_Data();
    }

    private void LateUpdate()
    {
        ActivePanels();
        txtLength[0].text = GUIPlanetOperations.ReturnLength(AllianceDataInput[0]);
        txtLength[1].text = GUIPlanetOperations.ReturnLength(AllianceDataInput[1]);
        AllianceData[1].text = ShowCost_SetAllianceData();
    }

    internal void Get_Alliance_Data()
    {
        AllianceDataInput[0].text = stats.Get_String_Data_From("Alliance_Name");
        AllianceDataInput[1].text = stats.Get_String_Data_From("Alliance_Tag");
        AllianceDataInput[2].text = stats.Get_String_Data_From("Alliance_PhotoUrl");

        AllianceData[0].text = stats.Get_String_Data_From("Alliance_Name");
    }
    private bool Has_Ally() //jesli true to mam klan, jesli false czyli ze no clan to nie mam klanu
    {
        return Check_Name(AllianceDataInput[0]) == true && Check_Tag(AllianceDataInput[1]) == true;
    }

    internal string ShowCost_CreateAllianceData()
    {
        return stats.Get_Data_From("Antymatery") + "/" + cost;
    }

    internal string ShowCost_SetAllianceData()
    {
        return stats.Get_Data_From("Alliance_Antymatery") + "/" + cost;
    }

    internal bool Check_Name(InputField AllianceName)
    {
        return AllianceName.text != "no alliance" || AllianceName.text != "";
    }

    internal bool Check_Tag(InputField AllianceTag)
    {
        return AllianceTag.text != "no tag" || AllianceTag.text != "";
    }

    internal bool Check_Antymateries(int value_cost)
    {
        return stats.Get_Data_From("Antymatery") >= value_cost;
    }

    public void Change_Alliance_Data()
    {
        if (Has_Ally())
        {
            if (stats.Get_Data_From("Antymatery") >= cost)
            {
                stats.Set_Data("Alliance_Antymatery", stats.Get_Data_From("Alliance_Antymatery")-cost);
                stats.Set_String_Data("Alliance_Name", AllianceDataInput[0].text);
                stats.Set_String_Data("Alliance_Tag", AllianceDataInput[1].text);
                Get_Alliance_Data();
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
            stats.Set_String_Data("Alliance_PhotoUrl", "no url");
            stats.Set_Data("MemberID", 1);
            stats.Set_Data("Base", 0);
            stats.Set_Data("Scout", 0);
            stats.Set_Data("Alliance_Antymatery", 0);
            Get_Alliance_Data();
            CanvasDelete.enabled = false;
            GUIOverview.page = 12;
        }
        else
        {
            CanvasDelete.enabled = false;
        }
    }

    private void ActivePanels()
    {
        panels[0].SetActive(SetActivePanels(GUIOverview.page));
        panels[1].SetActive(SetActivePanels(GUIOverview.page));
    }

    private bool SetActivePanels(int page)
    {
        return page >= 15 && page <= 20;//if true Show PanelBottom at Alliance
    }

    public void Set_Photo()
    {
        if (Has_Ally())
        {
            stats.Set_String_Data("Alliance_PhotoUrl", AllianceDataInput[2].text);
        }
    }
}



