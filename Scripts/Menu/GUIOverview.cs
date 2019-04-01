using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIOverview : MonoBehaviour
{
    public Canvas CanvasMessage, CanvasNameOfAdmiral, CanvasLevelUpAdmiral;

    public Canvas[] Canvases;
    public GameObject[] antymatery_field_planet;
    public Text[] resources;

    internal int page;

    public InputField planet_name;
    public InputField admiral_name;
    public Text txt_planet_name, txt_admiral_name, txt_planet_name_Overview, textAntymatery, textMessage;

    public AudioClip sound_message;
    public AudioSource audiosource_sound_message;

    public Text[] txt_Length; //0 admiral, 1 planet

    private int luck = 0;
    private bool random = false;
    private const int cost = 1;
    private Ads Ads;
    private statystyki staty;
    private GUIOperations GUIoper;

    // Use this for initialization
    private void Start()
    {
        Ads = GameObject.Find("Scripts").GetComponent<Ads>();
        staty = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIoper = GameObject.Find("Interface").GetComponent<GUIOperations>();

        txt_planet_name_Overview.text = staty.Get_String_Data_From("Planet_Name");
        textAntymatery.text = "" + staty.Get_Data_From("Antymatery") + " / " + cost;
        if (staty.Get_String_Data_From("Admiral_Name") == "" || staty.Get_String_Data_From("Admiral_Name") == "set admiral name")
        {
            CanvasNameOfAdmiral.enabled = true;
            page = 0;
        }
        else if (staty.Get_String_Data_From("Admiral_Name") != "" && staty.Get_String_Data_From("Admiral_Name") != "set admiral name")
        {
            CanvasNameOfAdmiral.enabled = false;
            page = 1;
        }

    }
    public void BtnFirstName(Canvas Canvases)
    {
        if (page == 0 && (staty.Get_String_Data_From("Admiral_Name") != "" || staty.Get_String_Data_From("Admiral_Name") != "set admiral name"))
        {
            Canvases.enabled = false;
            page = 1;
        }
        else if (page == 0 && (staty.Get_String_Data_From("Admiral_Name") == "" || staty.Get_String_Data_From("Admiral_Name") == "set admiral name"))
        {
            Canvases.enabled = false;
        }
    }
    // Update is called once per frame
    private void Update()
    {
        txt_Length[0].text = planet_name.text.Length.ToString() + "/" + planet_name.characterLimit;
        txt_Length[1].text = admiral_name.text.Length.ToString() + "/" + admiral_name.characterLimit;
        Steer_Canvases();
        Steer_Panel_Resources();
    }
    private void Steer_Canvases()
    {
        for (int ilosc = 1; ilosc < Canvases.Length; ilosc++)
        {
            if (page == ilosc)
            {
                Canvases[ilosc].enabled = true;
            }
            else if (page != ilosc)
            {
                Canvases[ilosc].enabled = false;
            }
        }
    }
    private void Steer_Panel_Resources()
    {
        if (page == 0 || page == 1 || page >= 12)//nothing
        {
            antymatery_field_planet[0].SetActive(false);
            antymatery_field_planet[1].SetActive(false);
            antymatery_field_planet[2].SetActive(false);
        }
        else if (page >= 2 && page <= 5)//resources
        {
            antymatery_field_planet[0].SetActive(true);
            antymatery_field_planet[1].SetActive(true);
            antymatery_field_planet[2].SetActive(false);
        }
        else if (page >= 6 && page <= 11)//antymatery
        {
            antymatery_field_planet[0].SetActive(true);
            antymatery_field_planet[1].SetActive(false);
            antymatery_field_planet[2].SetActive(true);
        }
    }
    private void Check_capacity()
    {
        //set color after checked which value is greater
        resources[0].color = GUIoper.Set_Color(staty.Get_Data_From("Metal"), staty.Get_Data_From("Capacity_Metal"));
        resources[1].color = GUIoper.Set_Color(staty.Get_Data_From("Crystal"), staty.Get_Data_From("Capacity_Crystal"));
        resources[2].color = GUIoper.Set_Color(staty.Get_Data_From("Deuter"), staty.Get_Data_From("Capacity_Deuter"));

        resources[0].text = staty.Get_Data_From("Metal").ToString("N0");
        resources[1].text = staty.Get_Data_From("Crystal").ToString("N0");
        resources[2].text = staty.Get_Data_From("Deuter").ToString("N0");
        resources[3].text = staty.Get_Data_From("Free_Field").ToString("N0");
        resources[4].text = staty.Get_Data_From("Antymatery").ToString("N0");
    }
    private void View_CanvasMessage(string text)
    {
        CanvasMessage.enabled = true;
        textMessage.text = text;
        audiosource_sound_message.PlayOneShot(sound_message, 0.7F);
    }
    public void BtnOpenAlliance()
    {
        if(staty.Get_String_Data_From("Alliance_Name") == "no alliance" && staty.Get_String_Data_From("Alliance_Tag") == "no tag")
        {
            page = 14;
        }else
        {
            page = 15;
        }
    }
    public void BtnResearch()
    {
        if (staty.Get_Data_From("Laboratory") >= 1)
        {
            page = 4;
        }
        else
        {
            View_CanvasMessage("Build laboratory");
        }
    }
    public void BtnShop()
    {
        if (staty.Get_Data_From("Hangar") >= 1)
        {
            page = 7;
        }
        else
        {
            View_CanvasMessage("Build Hangar");
        }
    }
    public void BtnOpenScene(string name_scene)
    {
        Handheld.Vibrate();
        SceneManager.LoadScene(name_scene);
        staty.Set_Data("ticks", staty.ticks);
        PlayerPrefs.Save();
    }
    public void BtnMultiWar()
    {
        if (staty.Get_Data_From("Antymatery") >= 5)
        {
            staty.Change_Antymatery(-5);
            BtnOpenScene("Game");
        }
        else if (staty.Get_Data_From("Antymatery") < 5)
        {
            View_CanvasMessage("Min. 5 antymatery to fight");
        }
    }
    private bool CheckChars(string value)
    {
        return value.Length >= 1;
    }
    public void ChangeNamePlanetCorrect()
    {
        if (CheckChars(planet_name.text) == true && staty.Get_Data_From("Antymatery") >= cost)
        {
            staty.Set_String_Data("Planet_Name", planet_name.text);
            txt_planet_name_Overview.text = planet_name.text;
            staty.Change_Antymatery(-cost);
            View_CanvasMessage("Changed name");
        }
        else if (staty.Get_Data_From("Antymatery") < cost)
        {
            View_CanvasMessage("Not enough antymatery");
        }
        else if (CheckChars(planet_name.text) == false)
        {
            View_CanvasMessage("Please enter the planet name");
        }
        //PlayerPrefs.DeleteAll();
    }
    public void ChangeNameOfAdmiralCorrect()
    {
        if (CheckChars(admiral_name.text) == true)
        {
            staty.Set_String_Data("Admiral_Name", admiral_name.text);
            CanvasNameOfAdmiral.enabled = false;
            if (staty.Get_String_Data_From("message_on_start") == "false")
            {
                View_CanvasMessage("Dear " + admiral_name.text + ".\n On start you received 100 all resources to upgrade your planet.");
                staty.Set_String_Data("message_on_start", "true");
            }
            else if (staty.Get_String_Data_From("message_on_start") == "true")
            {
                View_CanvasMessage("Changed name");
            }
        }
        else if (CheckChars(admiral_name.text) == false)
        {
            View_CanvasMessage("Please enter the admiral name");
        }
    }

    public void BtnExploration()
    {
        if (staty.Get_Data_From("Deuter") >= (int)staty.Get_Consumption())
        {
            if (luck < 90)
            {
                staty.Set_Data("Deuter", staty.Get_Data_From("Deuter") - (int)staty.Get_Consumption());
            }
            else if (luck >= 90)
            {
                staty.Set_Data("Deuter", staty.Get_Data_From("Deuter") - 0);
            }
            BtnOpenScene("Game");
        }
        else
        {
            View_CanvasMessage("Not enough deuter");
        }
    }
    public void EarnAntymatery()
    {
        if (Ads.pokazane == false)
        {
            Ads.Show_to_earn("antymatery");
            Ads.pokazane = false;
        }
    }
    public void Add_antymatery_for_FB()
    {
        if (staty.Get_Data_From("Collected_Antymatery") == 0)
        {
            staty.Change_Antymatery(10);
            staty.Set_Data("Collected_Antymatery", 1);
        }
        else if (staty.Get_Data_From("Collected_Antymatery") >= 1)
        {
            Debug.Log("juz raz pobrales");
        }
    }

    public void BtnPages(int number)
    {
        page = number;
    }
    private void LateUpdate()
    {
        Check_capacity();
        textAntymatery.text = staty.Get_Data_From("Antymatery") + " / " + cost;
        if (staty.free_exploration == 1 && random == false)
        {
            luck = Random.Range(1, 100);
            random = true;
            Debug.Log("" + luck);
        }
    }
}
