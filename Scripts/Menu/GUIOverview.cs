using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIOverview : MonoBehaviour
{
    public Canvas CanvasMessage, CanvasAdmiralName;
    [Space]
    public Canvas[] Canvases;
    public GameObject[] antymatery_field_planet;
    public Text[] Antymatery;

    [SerializeField] internal int page;

    public InputField planet_name;
    public InputField admiral_name;
    public Text PlanetName, TextMessage;

    public AudioClip sound_message;
    public AudioSource audiosource_sound_message;

    public Text[] txt_Length; //0 admiral, 1 planet

    internal const int cost = 1;
    private statystyki staty;
    private GUIOperations GUIoper;
    private GUIPlanetOperations GUIPlanetOperations;

    private void Start()
    {
        GUIPlanetOperations = GameObject.Find("Interface").GetComponent<GUIPlanetOperations>();
        staty = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIoper = GameObject.Find("Interface").GetComponent<GUIOperations>();

        PlanetName.text = staty.Get_String_Data_From("Planet_Name");
        SetAdmiralName();
    }

    private void SetAdmiralName()
    {
        if (staty.Get_String_Data_From("Admiral_Name") == "set admiral name")
        {
            CanvasAdmiralName.enabled = true;
            page = 0;
        }
        else if (staty.Get_String_Data_From("Admiral_Name") != "set admiral name")
        {
            CanvasAdmiralName.enabled = false;
            page = 1;
        }
    }

    public void BtnFirstName(Canvas Canvases)
    {
        if (page == 0 && staty.Get_String_Data_From("Admiral_Name") != "set admiral name")
        {
            Canvases.enabled = false;
            page = 1;
        }
        else if (page == 0 && staty.Get_String_Data_From("Admiral_Name") == "set admiral name")
        {
            Canvases.enabled = false;
            page = 0;
        }
    }

    private void ShowCharsLimit()
    {
        txt_Length[0].text = GUIPlanetOperations.ReturnLength(planet_name);
        txt_Length[1].text = GUIPlanetOperations.ReturnLength(admiral_name);
    }

    private void Update()
    {
        ShowCharsLimit();
        GUIoper.Steer_Canvas(Canvases, page);
        SteerUpperPanel();
    }

    private void SetVisibilityResources(bool resources, bool fieldPlanet, bool antymatery)
    {
        antymatery_field_planet[0].SetActive(resources);
        antymatery_field_planet[1].SetActive(fieldPlanet);
        antymatery_field_planet[2].SetActive(antymatery);
    }

    private void SteerUpperPanel()
    {
        if (page == 0 || page == 1 || page >= 12)
        {
            SetVisibilityResources(false, false, false);
        }
        else if (page >= 2 && page <= 5)
        {
            SetVisibilityResources(true, true, false);
        }
        else if (page >= 6 && page <= 11)
        {
            SetVisibilityResources(true, false, true);
        }
    }

    internal void View_CanvasMessage(string text)
    {
        CanvasMessage.enabled = true;
        TextMessage.text = text;
        audiosource_sound_message.PlayOneShot(sound_message, 0.7F);
    }

    internal void View_CanvasLevelUpAdmiral(string text)
    {
        View_CanvasMessage(text);
        GUIoper.RewardLvlUp.SetActive(true);
    }

    public void BtnOpenAlliance()
    {
        if (staty.Get_String_Data_From("Alliance_Name") == "no alliance" && staty.Get_String_Data_From("Alliance_Tag") == "no tag")
        {
            page = 14;
        }
        else
        {
            page = 15;
        }
    }

    public void BtnResearch()
    {
        CheckLevel("Laboratory", "Build laboratory", 4);
    }

    public void BtnShop()
    {
        CheckLevel("Hangar", "Build Hangar", 7);
    }

    public void BtnScout()
    {
        CheckLevel("Scout", "Build structure Scout", 20);
    }

    public void BtnExploration()
    {
        if (staty.Get_Data_From("Deuter") >= staty.LuckyConsumption()) //albo 0 jak ma szczescie, albo zwraca wartosc GetConsumption
        {
            staty.Set_Data("Deuter", staty.Get_Data_From("Deuter") - staty.LuckyConsumption());
            BtnOpenScene("Game");
        }
        else
        {
            View_CanvasMessage("Not enough deuter");
        }
    }

    private void CheckLevel(string subject, string message, int nr_page)
    {
        if (staty.Get_Data_From(subject) >= 1)
        {
            page = nr_page;
        }
        else
        {
            View_CanvasMessage(message);
        }
    }

    public void BtnOpenScene(string name_scene)
    {
        SceneManager.LoadScene(name_scene);
        staty.Set_Data("ticks", staty.ticks);
        PlayerPrefs.Save();
    }

    public void BtnPVP()
    {
        if (staty.Get_Data_From("Antymatery") >= 5)
        {
            staty.Change_Antymatery(-5);
            BtnOpenScene("PVP");
        }
        else if (staty.Get_Data_From("Antymatery") < 5)
        {
            View_CanvasMessage("Min. 5 antymatery to fight");
        }
    }

    public void ChangePlanetName()
    {
        if (GUIPlanetOperations.CheckLength(planet_name) && staty.Get_Data_From("Antymatery") >= cost)
        {
            staty.Set_String_Data("Planet_Name", planet_name.text);
            PlanetName.text = planet_name.text;
            staty.Change_Antymatery(-cost);
            View_CanvasMessage("Changed name");
        }
        else if (staty.Get_Data_From("Antymatery") < cost)
        {
            View_CanvasMessage("Not enough antymatery");
        }
        else if (!GUIPlanetOperations.CheckLength(planet_name))
        {
            View_CanvasMessage("Please enter the planet name");
        }
       // PlayerPrefs.DeleteAll();
    }

    public void ChangeAdmiralName()
    {
        if (GUIPlanetOperations.CheckLength(admiral_name))
        {
            staty.Set_String_Data("Admiral_Name", admiral_name.text);
            CanvasAdmiralName.enabled = false;
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
        else if (!GUIPlanetOperations.CheckLength(admiral_name))
        {
            View_CanvasMessage("Please enter the admiral name");
        }
    }

    public void EarnAntymatery()
    {
        GUIPlanetOperations.Turn_On_Ads("antymatery");
    }

    public void AddAntymateryForFB()
    {
        if (staty.Get_Data_From("Collected_Antymatery") == 0)
        {
            staty.Change_Antymatery(10);
            staty.Set_Data("Collected_Antymatery", 1);
        }
        else if (staty.Get_Data_From("Collected_Antymatery") >= 1)
        {
            View_CanvasMessage("Just 1 time!");
        }
    }

    public void BtnPages(int number)
    {
        page = number;
    }

    public void LoadAntymatery()//after clicked on Button PVP or Change Planet Name
    {
        Antymatery[0].text = staty.Get_Data_From("Antymatery") + " / " + cost;
        Antymatery[1].text = staty.Get_Data_From("Antymatery") + " / " + 5;
    }

    public void LoadCostExploration()
    {
        staty.LoadLuckyForConsumption();
    }
}
