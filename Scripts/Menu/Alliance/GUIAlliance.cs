using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIAlliance : MonoBehaviour {

    private statystyki stats;
    private GUIOverview GUIOverview;

    public GameObject[] panels;
    public Text Alliance_Name;

    private void Start()
    {
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIOverview = GameObject.Find("Interface").GetComponent<GUIOverview>();
    }

    private void Update()
    {
        ActivePanels();
        Get_Alliance_Name();
    }

    public void BtnLeave()
    {
        stats.Set_String_Data("Alliance_Name", "no alliance");
        stats.Set_String_Data("Alliance_Tag", "no tag");
    }
    private bool SetActivePanels(int page)
    {
        return page >= 15 && page <= 18;//panelgorny dolny sojusz
    }
    private void ActivePanels()
    {
        panels[0].SetActive(SetActivePanels(GUIOverview.page));
        panels[1].SetActive(SetActivePanels(GUIOverview.page));
    }

    private void Get_Alliance_Name()
    {
        Alliance_Name.text = stats.Get_String_Data_From("Alliance_Name");
    }
}
