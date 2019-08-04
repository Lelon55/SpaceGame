using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;

public class GUIAdmiralPanel : MonoBehaviour
{
    public Image panelAdmiralExp;
    public RawImage Avatar;
    public Text[] textInfo_Admiral;
    public InputField path;

    private statystyki stats;
    private GUIOverview GUIOverview;
    private GUIPlanetOperations GUIPlanetOperations;

    private void Start()
    {
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIOverview = GameObject.Find("Interface").GetComponent<GUIOverview>();
        GUIPlanetOperations = GameObject.Find("Interface").GetComponent<GUIPlanetOperations>();
        path.text = stats.Get_String_Data_From("Admiral_Avatar");
        GUIPlanetOperations.SetAvatar(path.text, "Admiral_Avatar", Avatar);
    }

    private void Exp_AdmiralPanel()
    {
        panelAdmiralExp.rectTransform.sizeDelta = GUIPlanetOperations.CountVector("Exp", stats.Count_Exp_To_Next_Level(), 30f);
    }

    private int RewardLevel()
    {
        return 10 * (stats.Get_Data_From("Level") + 1);
    }

    private void LevelUp()
    {
        if (stats.Get_Data_From("Exp") >= stats.Count_Exp_To_Next_Level())
        {
            stats.Set_Data("Exp", stats.Get_Data_From("Exp") - stats.Count_Exp_To_Next_Level());
            stats.Set_Data("Metal", stats.Get_Data_From("Metal") + RewardLevel());
            stats.Set_Data("Crystal", stats.Get_Data_From("Crystal") + RewardLevel());
            stats.Set_Data("Deuter", stats.Get_Data_From("Deuter") + RewardLevel());
            textInfo_Admiral[10].text = RewardLevel().ToString("N0");
            textInfo_Admiral[11].text = RewardLevel().ToString("N0");
            textInfo_Admiral[12].text = RewardLevel().ToString("N0");
            stats.Set_Data("Level", stats.Get_Data_From("Level") + 1);
            GUIOverview.View_CanvasLevelUpAdmiral("Congratulations. \nYou have reached level " + stats.Get_Data_From("Level") + ".");
        }
    }

    private void Info_AdmiralPanel()
    {
        textInfo_Admiral[0].text = "Level: " + stats.Get_Data_From("Level").ToString("N0");
        textInfo_Admiral[1].text = "Alliance: " + stats.Get_String_Data_From("Alliance_Name");
        textInfo_Admiral[2].text = stats.Get_Data_From("Destroyed_Comets").ToString("N0");
        textInfo_Admiral[3].text = stats.Get_Data_From("Spent_Resources").ToString("N0");
        textInfo_Admiral[4].text = stats.Get_Data_From("Bought_Ships").ToString("N0");
        textInfo_Admiral[5].text = stats.Get_Data_From("Destroyed_Enemy_Ships").ToString("N0");
        textInfo_Admiral[6].text = stats.Get_String_Data_From("Admiral_Name");
        textInfo_Admiral[7].text = "Points: " + stats.Get_Points().ToString();
        textInfo_Admiral[8].text = stats.Get_Data_From("Explored_Moons").ToString("N0");
        textInfo_Admiral[9].text = stats.Get_Data_From("Exp").ToString("N0") + "/" + stats.Count_Exp_To_Next_Level().ToString("N0");
        textInfo_Admiral[13].text = stats.Get_Data_From("Wins").ToString("N0") + "/" + stats.Get_Data_From("Loses").ToString("N0");
        textInfo_Admiral[14].text = GUIPlanetOperations.ReturnLength(path);
    }

    private void LateUpdate()
    {
        Exp_AdmiralPanel();
        Info_AdmiralPanel();
        LevelUp();
    }

    public void ChangeAvatar()
    {
        if (path.text != "")
        {
            GUIPlanetOperations.SetAvatar(path.text, "Admiral_Avatar", Avatar);
        }
    }
}

