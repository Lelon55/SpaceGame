using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUISettingsAlliance : MonoBehaviour {

    private statystyki stats;
    private GUIOverview GUIOverview;

    // Use this for initialization
    private void Start () {
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIOverview = GameObject.Find("Interface").GetComponent<GUIOverview>();
    }
	
	// Update is called once per frame
	private void Update () {
       
    }

    private bool Has_Ally() //jesli false to mam klan, jesli true czyli ze no clan to nie mam klanu
    {
        return stats.Get_String_Data_From("Alliance_Name") == "no alliance";
    }

    private void Change_Name_Alliance()
    {
      
    }

    private void Change_Tag_Alliance()
    {
        
    }

    private void Remove_Alliance()
    {

    }

    private void Set_Description() // tylko tekst
    {

    }

    private void Set_Photo()
    {

    }

    private void Set_Threshold_Point()//prog pkt
    {

    }
}


