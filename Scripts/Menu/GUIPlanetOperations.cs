using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIPlanetOperations : MonoBehaviour {
    #region UI Variables
    private GUIOverview GUIOverview;
    private GameObject[] on_off_cost = new GameObject[2]; //on or off antymatery or resources
    private Text txtMetalCost, txtCrystalCost, txtDeuterCost, txtAntymateries, txtSubjectName, txtSubjectDescription;
    private Image img;
    #endregion
    // Use this for initialization
    private void Start () {
        #region Start
        GUIOverview = GameObject.Find("Interface").GetComponent<GUIOverview>();
        on_off_cost[0] = GameObject.Find("Cost");
        on_off_cost[1] = GameObject.Find("CostAntymatery");
        txtMetalCost = GameObject.Find("txtMetalInformationBuyer").GetComponent<Text>();
        txtCrystalCost = GameObject.Find("txtCrystalInformationBuyer").GetComponent<Text>();
        txtDeuterCost = GameObject.Find("txtDeuterInformationBuyer").GetComponent<Text>();
        txtAntymateries = GameObject.Find("txtAntymaterylInformationBuyer").GetComponent<Text>();
        txtSubjectName = GameObject.Find("txtSubjectNameInformationBuyer").GetComponent<Text>();
        txtSubjectDescription = GameObject.Find("txtDescriptionInformationBuyer").GetComponent<Text>();
        img = GameObject.Find("imgInformationBuyer").GetComponent<Image>();
        #endregion
    }

    internal void Subject_Information(int MetalCost, int CrystalCost, int DeuterCost, int AntymateryCost, string SubjectName, string SubjectDescription, Sprite SubjectPhoto) //tutaj bedziemy otwierac okno z informacjami o przedmiocie => zmniejszy ilosc zmiennych
    {
        if (GUIOverview.page < 8 || GUIOverview.page >= 10)
        {
            on_off_cost[0].SetActive(true);
            on_off_cost[1].SetActive(false);
        }
        if (GUIOverview.page == 8)
        {
            on_off_cost[0].SetActive(false);
            on_off_cost[1].SetActive(true);
        }
        txtMetalCost.text = MetalCost.ToString();
        txtCrystalCost.text = CrystalCost.ToString();
        txtDeuterCost.text = DeuterCost.ToString();
        txtAntymateries.text = AntymateryCost.ToString();
        txtSubjectName.text = SubjectName;
        txtSubjectDescription.text = SubjectDescription;
        img.sprite = SubjectPhoto;
    }
    internal bool Check_Levels(string name, int level)
    {
        return PlayerPrefs.GetInt(name) >= level;
    }
}
