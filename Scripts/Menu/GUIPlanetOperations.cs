using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GUIPlanetOperations : MonoBehaviour {
    #region UI Variables
    private Ads Ads;
    private GameObject[] on_off_cost = new GameObject[2]; //on or off antymatery or resources
    private Text txtMetalCost, txtCrystalCost, txtDeuterCost, txtAntymateries, txtSubjectName, txtSubjectDescription;
    private Image img;
    #endregion
    // Use this for initialization
    private void Start () {
        #region Start
        Ads = GameObject.Find("Scripts").GetComponent<Ads>();
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
        if (MetalCost > 0 || CrystalCost > 0 || DeuterCost > 0)
        {
            on_off_cost[0].SetActive(true);
            on_off_cost[1].SetActive(false);
        }
        if (AntymateryCost>0)
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

    #region Buyer
    internal void View_Subject(GameObject[] gameobject, string subject)
    {
        for (int ilosc = 0; ilosc < gameobject.Count(); ilosc++)
        {
            gameobject[ilosc].SetActive(Check_Levels(subject, ilosc));
        }
    }

    internal void View_Available_Subject(Text[] txtButton, int nr, string text, Color color)
    {
        txtButton[nr].text = text;
        txtButton[nr].color = color;
    }

    internal void Check_buttons(bool have, Text[] txtButton, int ilosc, int antymatery, int price)
    {
        if (have == true)
        {
            View_Available_Subject(txtButton, ilosc, "USING", new Color(.105f, .375f, .105f, 255f));
        }
        else if (have == false && antymatery >= price)
        {
            View_Available_Subject(txtButton, ilosc, "CHANGE", new Color(255f, 255f, 255f, 255f));
        }
        else if (have == false && antymatery < price)
        {
            View_Available_Subject(txtButton, ilosc, "EARN", new Color(255f, 255f, 255f, 255f));
        }
    }

    internal void Turn_On_Ads(string earn_type)
    {
        if (Ads.pokazane == false)
        {
            Ads.Show_to_earn(earn_type);
            Ads.pokazane = false;
        }
    }

    internal bool Check_HasItem(int id, int subject_id)
    {
        return id == subject_id;
    }

    #endregion

    internal bool CheckLength(InputField value)
    {
        return value.text.Length >= 1;
    }
}
