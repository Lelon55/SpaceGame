using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIAlliance : MonoBehaviour {

    private statystyki stats;
    private GUIOverview GUIOverview;

    public GameObject[] panels;

    private void Start()
    {
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIOverview = GameObject.Find("Interface").GetComponent<GUIOverview>();
    }

    private void Update()
    {
        ActivePanels();
    }

    public void BtnLeave()
    {
        stats.Set_String_Data("Alliance_Name", "no alliance");
        stats.Set_String_Data("Alliance_Tag", "no tag");
    }
    private bool SetActivePanels(int page)
    {
        if (page >= 15 && page <= 17)//panelgorny dolny sojusz
        {
            return true;
        }
        return false;
    }
    private void ActivePanels()
    {
        panels[0].SetActive(SetActivePanels(GUIOverview.page));
        panels[1].SetActive(SetActivePanels(GUIOverview.page));
    }
}
