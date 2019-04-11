using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIAdmiralPanel : MonoBehaviour
{
    public Image panelAdmiralExp;
    public Text[] textInfo_Admiral;

    private statystyki stats;
    private GUIOverview GUIOverview;
    private Operations operations = new Operations();

    private void Start()
    {
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIOverview = GameObject.Find("Interface").GetComponent<GUIOverview>();
    }
    private void Info_AdmiralPanel()
    {
        panelAdmiralExp.rectTransform.sizeDelta = new Vector2(130f * operations.Change_result(stats.Get_Data_From("Exp"), stats.Count_Exp_To_Next_Level()), 30f);

        textInfo_Admiral[0].text = "Level: " + stats.Get_Data_From("Level").ToString("N0");
        textInfo_Admiral[1].text = stats.Get_String_Data_From("Alliance_Name");
        textInfo_Admiral[2].text = stats.Get_Data_From("Destroyed_Comets").ToString("N0");
        textInfo_Admiral[3].text = stats.Get_Data_From("Spent_Resources").ToString("N0");
        textInfo_Admiral[4].text = stats.Get_Data_From("Bought_Ships").ToString("N0");
        textInfo_Admiral[5].text = stats.Get_Data_From("Destroyed_Enemy_Ships").ToString("N0");
        textInfo_Admiral[6].text = stats.Get_String_Data_From("Admiral_Name");
        textInfo_Admiral[7].text = "Points: " + stats.Get_Points().ToString();
        textInfo_Admiral[8].text = stats.Get_Data_From("Explored_Moons").ToString("N0");
        textInfo_Admiral[9].text = stats.Get_Data_From("Exp").ToString("N0") + "/" + stats.Count_Exp_To_Next_Level().ToString("N0");

        if (stats.Get_Data_From("Exp") >= stats.Count_Exp_To_Next_Level())
        {
            stats.Set_Data("Exp", stats.Get_Data_From("Exp") - stats.Count_Exp_To_Next_Level());
            stats.Set_Data("Metal", stats.Get_Data_From("Metal") + (10 * (stats.Get_Data_From("Level")+1)));
            stats.Set_Data("Crystal", stats.Get_Data_From("Crystal") + (10 * (stats.Get_Data_From("Level") + 1)));
            stats.Set_Data("Deuter", stats.Get_Data_From("Deuter") + (10 * (stats.Get_Data_From("Level") + 1)));
            textInfo_Admiral[10].text = (10 * (stats.Get_Data_From("Level") + 1)).ToString("N0");
            textInfo_Admiral[11].text = (10 * (stats.Get_Data_From("Level") + 1)).ToString("N0");
            textInfo_Admiral[12].text = (10 * (stats.Get_Data_From("Level") + 1)).ToString("N0");
            stats.Set_Data("Level", stats.Get_Data_From("Level") + 1);
            GUIOverview.View_CanvasLevelUpAdmiral("Congratulations. \nYou have reached level " + stats.Get_Data_From("Level") + ".");
        }
    }
    private void LateUpdate()
    {
        Info_AdmiralPanel();
    }
}
