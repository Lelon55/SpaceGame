using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIOverview : MonoBehaviour
{
    public Canvas CanvasMessage, CanvasAdmiralName;
    public GameObject Earth;
    [Space]
    public Canvas[] Canvases;
    public GameObject[] antymatery_field_planet;
    public Text[] resources, Antymatery;

    [SerializeField] internal int page;

    public InputField planet_name;
    public InputField admiral_name;
    public Text txt_planet_name, txt_admiral_name, txt_planet_name_Overview, textMessage;

    public AudioClip sound_message;
    public AudioSource audiosource_sound_message;

    public Text[] txt_Length; //0 admiral, 1 planet

    private int luck = 0;
    private const int cost = 1;
    private statystyki staty;
    private GUIOperations GUIoper;
    private GUIPlanetOperations GUIPlanetOperations;
    private float rotation_y;

    private void Start()
    {
        GUIPlanetOperations = GameObject.Find("Interface").GetComponent<GUIPlanetOperations>();
        staty = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIoper = GameObject.Find("Interface").GetComponent<GUIOperations>();

        txt_planet_name_Overview.text = staty.Get_String_Data_From("Planet_Name");
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
        else if (page == 0 &&  staty.Get_String_Data_From("Admiral_Name") == "set admiral name")
        {
            Canvases.enabled = false;
            page = 0;
        }
    }

    private void Show_Chars_Limit()
    {
        txt_Length[0].text = GUIPlanetOperations.ReturnLength(planet_name);
        txt_Length[1].text = GUIPlanetOperations.ReturnLength(admiral_name);
    }

    private void MovePlanet()
    {
        if (page == 1)
        {
            Earth.SetActive(true);
            rotation_y += Time.deltaTime * 60;
            Earth.transform.rotation = Quaternion.Euler(0, rotation_y, -30f);
        }
        else
        {
            Earth.SetActive(false);
        }
    }

    private void Update()
    {
        MovePlanet();
        Show_Chars_Limit();
        GUIoper.Steer_Canvas(Canvases, page);
        Steer_Panel_Resources();
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

    internal void View_CanvasMessage(string text)
    {
        CanvasMessage.enabled = true;
        textMessage.text = text;
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
        Check_Level("Laboratory", "Build laboratory", 4);
    }

    public void BtnShop()
    {
        Check_Level("Hangar", "Build Hangar", 7);
    }

    public void BtnScout()
    {
        Check_Level("Scout", "Build structure Scout", 20);
    }

    private void Check_Level(string subject, string message, int nr_page)
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
        Handheld.Vibrate();
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
            txt_planet_name_Overview.text = planet_name.text;
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
        //PlayerPrefs.DeleteAll();
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

    private int LuckyConsumption()
    {
        if (luck < 90)
        {
            return (int)staty.Get_Consumption();
        }
        return 0;
    }

    public void BtnExploration()
    {
        if (staty.Get_Data_From("Deuter") >= (int)staty.Get_Consumption())
        {
            staty.Set_Data("Deuter", staty.Get_Data_From("Deuter") - LuckyConsumption());
            BtnOpenScene("Game");
        }
        else
        {
            View_CanvasMessage("Not enough deuter");
        }
    }

    public void EarnAntymatery()
    {
        GUIPlanetOperations.Turn_On_Ads("antymatery");
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
        if (staty.free_exploration == 1)
        {
            luck = Random.Range(1, 100);
            Debug.Log("" + luck);
        }
    }

    private void LateUpdate()
    {
        Check_capacity();
    }
}
