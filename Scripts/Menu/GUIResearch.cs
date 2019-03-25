using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GUIResearch : MonoBehaviour {
    private class List_researches
    {
        public int id;
        public string name;
		public string description;
        public int metal;
		public int crystal;
		public int deuter;
		public int require_lvl; //wymagany poziom do odblokowania badania
        public int level;

        public List_researches(int i, string n, string de, int m, int c, int d, int rl, int l)
        {
            this.id = i;
            this.name = n;
			this.description = de;
            this.metal = m;
			this.crystal = c;
			this.deuter = d;
			this.require_lvl = rl;
            this.level = l;
        }
    }
    private List<List_researches> research = new List<List_researches>();
    private statystyki staty;
    private Ads Ads;
    private GUIPlanetOperations GUIPlanetOperations;

    public GameObject[] researches;
    public Button[] buttony;
    public Text[] text_button;
    public Text textDebugResearch;
	public int spent_resources;
	public Sprite[] SpriteResearches;
    public int activated_bonus = 0;
    public AudioClip sound_buildup;
    public AudioSource audiosource_sound_buildup;

    // Use this for initialization
    private void Start () {
        Ads = GameObject.Find("Scripts").GetComponent<Ads>();
        staty = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIPlanetOperations = GameObject.Find("Interface").GetComponent<GUIPlanetOperations>();

        research.Add(new List_researches(0, "SHIELD", "Each level of shield gives +1 life. Max lvl causes immortal for first collision with comet or enemy laser.", 100, 0, 0, 1, 1));
        research.Add(new List_researches(1, "COMBUSTION", "Each level of combustion causes -1 consumtpion from ships. Max lvl gives 10% chance for 0 consumption on start exploration of space.", 300, 0, 500, 2, 1));
		research.Add(new List_researches(2, "LASER TECHNOLOGY", "Each level of laser technology gives more lasers to buy. Max lvl causes more damage from lasers on enemy ship.", 75, 225, 0, 3, 1));
		research.Add(new List_researches(3, "MINING TECHNLOGY", "Each level of mining technology gives +2 max drop from comets. Max lvl causes +5 extra drop resource from comet.", 200, 350, 175, 4, 1));
		research.Add(new List_researches(4, "ANTYMATERY TECHNLOGY", "Each level of antymatery technology gives +1 chance drop antymatery from comets. Max lvl causes +1 max drop antymatery for watch ads.", 500, 500, 500, 5, 1));
        View_Researches(); 
    }
    private void Check_research() //nadpisuje poziomy
    {
		research[0].level = staty.Get_Data_From("Shield");
		research[1].level = (int)staty.Get_Float_Data_From("Combustion");
		research[2].level = staty.Get_Data_From("Laser_Technology");
		research[3].level = staty.Get_Data_From("Mining_Technology");
		research[4].level = staty.Get_Data_From("Antymatery_Technology");
	}
    private void Check_buttons() // zmienia tylko nazwy w tekscie
    {
        for (int ilosc = 0; ilosc < research.Count(); ilosc++)
        {
            if ((research[ilosc].level < 3) && (staty.Get_Data_From("Metal") >= (research[ilosc].metal * (research[ilosc].level + 1))) && (staty.Get_Data_From("Crystal") >= (research[ilosc].crystal * (research[ilosc].level + 1))) && (staty.Get_Data_From("Deuter") >= (research[ilosc].deuter * (research[ilosc].level + 1))))
            {//zmien
                text_button[ilosc].text = "BUY " + "(" + (research[ilosc].level + 1) + ")";
            }
            else if (research[ilosc].level >= 3)
            {
                text_button[ilosc].text = "MAX LVL";

            }
            else if ((research[ilosc].level < 3) && (staty.Get_Data_From("Metal") < (research[ilosc].metal * (research[ilosc].level + 1))) || (staty.Get_Data_From("Crystal") < (research[ilosc].crystal * (research[ilosc].level + 1))) || (staty.Get_Data_From("Deuter") < (research[ilosc].deuter * (research[ilosc].level + 1))))
            { //zarob po kliknieciu wywola reklame
                text_button[ilosc].text = "EARN " + "(" + (research[ilosc].level + 1) + ")";
            }
        }
    }
    private void View_Researches()
    {
        for (int ilosc = 0; ilosc < researches.Count(); ilosc++)
        {
                researches[ilosc].SetActive(GUIPlanetOperations.Check_Levels("Laboratory", ilosc));
        }
    }

    private void Set_Properties_Up(int nr)
    {
        staty.Set_Data("Metal", staty.Get_Data_From("Metal") - research[nr].metal * (research[nr].level + 1));
        staty.Set_Data("Crystal", staty.Get_Data_From("Crystal") - research[nr].crystal * (research[nr].level + 1));
        staty.Set_Data("Deuter", staty.Get_Data_From("Deuter") - research[nr].deuter * (research[nr].level + 1));
        spent_resources = (research[nr].metal * (research[nr].level + 1)) + (research[nr].crystal * (research[nr].level + 1)) + (research[nr].deuter * (research[nr].level + 1));
        staty.Set_Data("Spent_Resources", staty.Get_Data_From("Spent_Resources") + spent_resources);
        research[nr].level += 1;
    }
    private void Set_Technology(int nr)
    {
        if (research[nr].name == "SHIELD")
        {
            staty.Set_Data("Shield", research[nr].level);
        }
        else if (research[nr].name == "COMBUSTION")
        {
            staty.Set_Float_Data("Combustion", research[nr].level);
        }
        else if (research[nr].name == "LASER TECHNOLOGY")
        {
            staty.Set_Data("Laser_Technology", research[nr].level);
        }
        else if (research[nr].name == "MINING TECHNLOGY")
        {
            staty.Set_Data("Mining_Technology", research[nr].level);
        }
        else if (research[nr].name == "ANTYMATERY TECHNLOGY")
        {
            staty.Set_Data("Antymatery_Technology", research[nr].level);
        }
        PlayerPrefs.Save();
        textDebugResearch.text = "BOUGHT: " + research[nr].name + "(" + research[nr].level + ")";
        audiosource_sound_buildup.PlayOneShot(sound_buildup, 0.7F);
    }

    private void Turn_On_Ads(int nr)
    {
        if (Ads.pokazane == false)
        {
            Ads.Show_to_earn("resources");
            Ads.pokazane = false;

        }
        textDebugResearch.text = "EARN: " + research[nr].name + "(" + research[nr].level + ")";
    }

    public void BuyResearch(int nr)
    {
        if (research[nr].level <= 2)
        {
            if ((staty.Get_Data_From("Metal") >= research[nr].metal * (research[nr].level + 1)) && (staty.Get_Data_From("Crystal") >= research[nr].crystal * (research[nr].level + 1)) && (staty.Get_Data_From("Deuter") >= research[nr].deuter * (research[nr].level + 1)))
            {//zmien
                Set_Properties_Up(nr);
                Set_Technology(nr);
            }
            else if ((staty.Get_Data_From("Metal") < research[nr].metal * (research[nr].level + 1)) || (staty.Get_Data_From("Crystal") < research[nr].crystal * (research[nr].level + 1)) || (staty.Get_Data_From("Deuter") < research[nr].deuter * (research[nr].level + 1)))
            { 
                Turn_On_Ads(nr);
            }
            else if (research[nr].level == 3 && text_button[nr].text == "MAX LVL")
            {
                textDebugResearch.text = "MAX LVL: " + research[nr].name;
            }
        }
    }
   
    // Update is called once per frame
    private void LateUpdate () {
        Check_research();
		Check_buttons();
        View_Researches();
	}

	public void Info_researches(int nr){
        GUIPlanetOperations.Subject_Information((research[nr].metal * (research[nr].level + 1)),
        (research[nr].crystal * (research[nr].level + 1)),
        (research[nr].deuter * (research[nr].level + 1)), 0,
        research[nr].name + " (" + research[nr].level.ToString() + ")",
        research[nr].description,
        SpriteResearches[nr]);
    }
	
	}

