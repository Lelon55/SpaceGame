using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUISettingsAlliance : MonoBehaviour {

    private statystyki stats;
    private GUIOverview GUIOverview;
    private GUIAllianceLog GUIAllianceLog;

    public InputField[] AllianceData;
    public Text[] txtLength;
    private const int cost = 1;

    // Use this for initialization
    private void Start () {
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIOverview = GameObject.Find("Interface").GetComponent<GUIOverview>();
        GUIAllianceLog = GameObject.Find("CanvasesAlliance").GetComponent<GUIAllianceLog>();
        Get_Alliance_Data();
    }

    // Update is called once per frame
    private void Update()
    {
        Check_Length();
    }
    private void Check_Length()
    {
        txtLength[0].text = AllianceData[0].text.Length.ToString() + "/" + AllianceData[0].characterLimit;
        txtLength[1].text = AllianceData[1].text.Length.ToString() + "/" + AllianceData[1].characterLimit;
    }

    private void Get_Alliance_Data()
    {
        AllianceData[0].text = stats.Get_String_Data_From("Alliance_Name");
        AllianceData[1].text = stats.Get_String_Data_From("Alliance_Tag");
        AllianceData[2].text = stats.Get_String_Data_From("Alliance_Descritpion");
        AllianceData[3].text = stats.Get_String_Data_From("Alliance_PhotoUrl");
        AllianceData[4].text = stats.Get_Data_From("Alliance_Threshold_Point").ToString();
    }
    private bool Has_Ally() //jesli true to mam klan, jesli false czyli ze no clan to nie mam klanu
    {
        return Check_Name() == true && Check_Tag() == true;
    }
    private bool Check_Name()
    {
        return AllianceData[0].text != "no alliance" || AllianceData[0].text != "";
    }
    private bool Check_Tag()
    {
        return AllianceData[1].text != "no tag" || AllianceData[1].text != "";
    }
    public void Change_Alliance_Data()
    {
        if (Has_Ally()==true)
        {
            stats.Set_String_Data("Alliance_Name", AllianceData[0].text);
            stats.Set_String_Data("Alliance_Tag", AllianceData[1].text);
        }
        else
        {
            Debug.Log("sss");//brak tekstu lub no name, no tag
        }
    }

    public void Btn_Remove_Alliance()
    {
        if (Has_Ally() == true)
        {
            stats.Set_String_Data("Alliance_Name", "no alliance");
            stats.Set_String_Data("Alliance_Tag", "no tag");
            GUIOverview.page = 12;
        }
        else
        {
            Debug.Log("nic nie robie");
        }
    }

    private void Set_Description() // tylko tekst
    {
        if (Has_Ally() == true)
        {
            stats.Set_String_Data("Alliance_Description", AllianceData[2].text);
        }
    }

    private void Set_Photo()
    {
        if (Has_Ally() == true)
        {
            stats.Set_String_Data("Alliance_PhotoUrl", AllianceData[3].text);
        }
    }

    public void Set_Threshold_Point()//prog pkt
    {
        if (Has_Ally() == true)
        {
            stats.Set_Data("Alliance_Threshold_Point", int.Parse(AllianceData[4].text));
            GUIAllianceLog.Add_New_Entry("Threshold Point change by: " + stats.Get_String_Data_From("Admiral_Name"));
        }
    }
}



