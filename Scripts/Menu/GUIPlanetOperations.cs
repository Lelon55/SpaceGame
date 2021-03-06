﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GUIPlanetOperations : MonoBehaviour
{
    #region Variables
    [SerializeField] private AudioClip sound_complete;
    [SerializeField] private AudioSource audiosource_complete;
    internal GUIOperations GUIoper;

    private Ads Ads;
    private statystyki stats;
    private GameObject[] on_off_cost = new GameObject[2]; //on or off antymatery or resources
    private Text txtMetalCost, txtCrystalCost, txtDeuterCost, txtAntymateries, txtSubjectName, txtSubjectDescription;
    private Image Image;
    [SerializeField] private Canvas InformationBuyer;
    #endregion

    private void Start()
    {
        #region Start
        GUIoper = GameObject.Find("Interface").GetComponent<GUIOperations>();
        Ads = GameObject.Find("Scripts").GetComponent<Ads>();
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        on_off_cost[0] = GameObject.Find("Cost");
        on_off_cost[1] = GameObject.Find("CostAntymatery");
        txtMetalCost = GameObject.Find("txtMetalInformationBuyer").GetComponent<Text>();
        txtCrystalCost = GameObject.Find("txtCrystalInformationBuyer").GetComponent<Text>();
        txtDeuterCost = GameObject.Find("txtDeuterInformationBuyer").GetComponent<Text>();
        txtAntymateries = GameObject.Find("txtAntymaterylInformationBuyer").GetComponent<Text>();
        txtSubjectName = GameObject.Find("txtSubjectNameInformationBuyer").GetComponent<Text>();
        txtSubjectDescription = GameObject.Find("txtDescriptionInformationBuyer").GetComponent<Text>();
        Image = GameObject.Find("imgInformationBuyer").GetComponent<Image>();
        #endregion
    }

    internal void Subject_Information(int MetalCost, int CrystalCost, int DeuterCost, int AntymateryCost, string SubjectName, string SubjectDescription, Sprite SubjectPhoto) //tutaj bedziemy otwierac okno z informacjami o przedmiocie => zmniejszy nr zmiennych
    {
        if (MetalCost > 0 || CrystalCost > 0 || DeuterCost > 0)
        {
            on_off_cost[0].SetActive(true);
            on_off_cost[1].SetActive(false);
        }
        if (AntymateryCost > 0)
        {
            on_off_cost[0].SetActive(false);
            on_off_cost[1].SetActive(true);
        }
        if (MetalCost <= 0 && CrystalCost <= 0 && DeuterCost <= 0 && AntymateryCost <= 0)
        {
            on_off_cost[0].SetActive(false);
            on_off_cost[1].SetActive(false);
        }
        txtMetalCost.text = MetalCost.ToString();
        txtCrystalCost.text = CrystalCost.ToString();
        txtDeuterCost.text = DeuterCost.ToString();
        txtAntymateries.text = AntymateryCost.ToString();
        txtSubjectName.text = SubjectName;
        txtSubjectDescription.text = SubjectDescription;
        Image.sprite = SubjectPhoto;

        GUIoper.BtnOpen(InformationBuyer);
    }

    internal bool Check_Levels(string name, int level)
    {
        return PlayerPrefs.GetInt(name) >= level;
    }

    #region Buyer
    internal void View_Subject(GameObject[] gameobject, string subject)
    {
        for (int nr = 0; nr < gameobject.Count(); nr++)
        {
            gameobject[nr].SetActive(Check_Levels(subject, nr));
        }
    }

    internal void View_Available_Subject(Text[] txtButton, int nr, string text, Color color)
    {
        txtButton[nr].text = text;
        txtButton[nr].color = color;
    }

    internal string SetButtonText(int level, bool value)
    {
        if (value)
        {
            return "BUY " + "(" + level + ")";
        }
        return "EARN " + "(" + level + ")";
    }

    internal string CheckLevel(int level, int maxLevel, int metalCost, int crystalCost, int deuterCost)
    {
        if (level < maxLevel)
        {
            return SetButtonText(level + 1, CheckCost(metalCost, crystalCost, deuterCost));
        }
        return "MAX LVL";
    }

    internal string CheckLevel(int level, int maxLevel, int antymatery)
    {
        if (level < maxLevel)
        {
            return SetButtonText(level + 1, CheckCost(antymatery));
        }
        return "MAX LVL";
    }

    private bool CheckCost(int antymatery)
    {
        return stats.Get_Data_From("Alliance_Antymatery") >= antymatery;
    }

    private bool CheckCost(int metal, int crystal, int deuter)
    {
        return stats.Get_Data_From("Metal") >= metal && stats.Get_Data_From("Crystal") >= crystal && stats.Get_Data_From("Deuter") >= deuter;
    }

    internal void Check_buttons(bool have, Text[] txtButton, int nr, int antymatery, int price)
    {
        if (have)
        {
            View_Available_Subject(txtButton, nr, "USING", new Color(.105f, .375f, .105f, 255f));
        }
        else if (!have && antymatery >= price)
        {
            View_Available_Subject(txtButton, nr, "CHANGE", new Color(255f, 255f, 255f, 255f));
        }
        else if (!have && antymatery < price)
        {
            View_Available_Subject(txtButton, nr, "EARN", new Color(255f, 255f, 255f, 255f));
        }
    }

    internal void Turn_On_Ads(string earn_type)
    {
        if (!Ads.pokazane)
        {
            Ads.Show_to_earn(earn_type);
            Ads.pokazane = false;
        }
    }

    internal bool Check_HasItem(int id, int subject_id)
    {
        return id == subject_id;
    }

    internal int CountSpentResources(int metalCost, int crystalCost, int deuterCost)
    {
        return metalCost + crystalCost + deuterCost;
    }

    internal void SetSpentResources(int metal, int crystal, int deuter)
    {
        stats.Set_Data("Metal", stats.Get_Data_From("Metal") - metal);
        stats.Set_Data("Crystal", stats.Get_Data_From("Crystal") - crystal);
        stats.Set_Data("Deuter", stats.Get_Data_From("Deuter") - deuter);
        stats.Set_Data("Spent_Resources", stats.Get_Data_From("Spent_Resources") + CountSpentResources(metal, crystal, deuter));
    }

    #endregion

    #region Overview

    internal Vector2 CountVector(string value1, string value2, float height)
    {
        return new Vector2(150f * Change_result(stats.Get_Data_From(value1), stats.Get_Data_From(value2)), height);
    }

    internal Vector2 CountVector(string value1, float value2, float height)
    {
        return new Vector2(150f * Change_result(stats.Get_Data_From(value1), value2), height);
    }

    internal float Change_result(float value1, float value2)
    {
        float result = Calculate_details(value1, value2);
        if (result > 1f)
        {
            return 1f;
        }
        return result;
    }

    internal float Calculate_details(float value_detail, float max_detail)
    {
        return value_detail / max_detail;
    }

    public void BtnClearInputField(InputField inputText)
    {
        inputText.text = "";
    }

    internal Color Set_Color(float value1, float value2)
    {
        if (value1 >= value2)
        {
            return new Color(255f, 0f, 0f, 255f);
        }
        return new Color(255f, 255f, 255f, 255f);
    }

    #endregion

    #region Avatar
    internal void SetAvatar(string path, string data, RawImage Avatar)
    {
        StartCoroutine(SaveAvatar(path, data, Avatar));
    }

    internal IEnumerator SaveAvatar(string path, string data, RawImage Avatar)
    {
        using (WWW www = new WWW(path))
        {
            yield return www;
            stats.Set_String_Data(data, path);
            Avatar.texture = www.texture;
        }
    }
    #endregion

    internal bool CheckLength(InputField value)
    {
        return value.text.Length >= 1;
    }

    internal string ReturnLength(InputField TextLength)
    {
        return TextLength.text.Length.ToString() + "/" + TextLength.characterLimit;
    }

    internal void PlaySound_Complete()
    {
        audiosource_complete.PlayOneShot(sound_complete, 0.7F);
    }
}
