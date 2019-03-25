using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Quest : MonoBehaviour {
private class List_quest
    {
        public int id;
        public string name_quest;
        public string description;
		public string category_quest;
		public int require_explored_moons; //ilosc zekplorowanych ksiezycy
        public int require_comet; //ilosc komet do zbicia w grze
        public int require_enemy_ships; //ilosc zniszczonych wrogich statkow
        public int require_bought_ships; //ilosc kupionych statkow
		public int require_metal_mine; //poziom kopalni metalu
		public int require_crystal_mine; //poziom kopalni kryształu
		public int require_deuter_sintetizer; //poziom kopalni deuteru
		public int require_laboratory; //poziom laboratorium
		public int require_hangar; //poziom stoczni
		public int require_terraformer; //poziom terraformera
		public int require_field_planet; //ilosc zabudowanego pola planety
		public int require_spent_resources; //ilosc wydanych surowcow
		public int require_antymatery; //ilosc zebranej antymaterii
		public int reward_metal; //nagroda w metalu
		public int reward_crystal; //nagroda w krysztale
		public int reward_deuter; //nagroda w deuterze
		public int reward_exp; //nagroda w exp
        public bool done;

        public List_quest(int i, string nq, string dc, string cq, int r_explored_moons, int r_comet, int r_enemy, int r_bought, int r_metal_mine, int r_crystal_mine, int r_deuter_sintetizer, int r_laboratory, int r_hangar, int r_terraformer, int r_field, int r_spent_resources, int r_antymatery, int re_metal, int re_crystal, int re_deuter, int re_exp, bool done)
        {
            this.id = i;
            this.name_quest = nq;
            this.description = dc;
			this.category_quest = cq;
			this.require_explored_moons = r_explored_moons;
            this.require_comet = r_comet;
            this.require_enemy_ships = r_enemy;
            this.require_bought_ships = r_bought;
			this.require_metal_mine = r_metal_mine;
            this.require_crystal_mine = r_crystal_mine;
			this.require_deuter_sintetizer = r_deuter_sintetizer;
            this.require_laboratory = r_laboratory;
			this.require_hangar = r_hangar;
            this.require_terraformer = r_terraformer;
			this.require_field_planet = r_field;
			this.require_spent_resources = r_spent_resources;
			this.require_antymatery = r_antymatery;
			this.reward_metal = re_metal;
			this.reward_crystal = re_crystal;
			this.reward_deuter = re_deuter;
			this.reward_exp = re_exp;
			this.done = done;
        }
    }
    private List<List_quest> quest = new List<List_quest>();
	public Text text_reward_metal;
    public Text text_reward_crystal;
    public Text text_reward_deuter;
	public Text text_reward_exp;
	public Text text_name_quest;
	public Text text_description;
	public Button btn_reward_quest;
	public Sprite[] SpriteCategoryQuest;
	public Image imgCategoryQuest;

    private GUIOverview GUIOverview;
    private statystyki staty;
    // Use this for initialization
    private void Start () {
        GUIOverview = GameObject.Find("Interface").GetComponent<GUIOverview>();
        staty = GameObject.Find("Scripts").GetComponent<statystyki>();

        quest.Add(new List_quest(0, "metal mine", "build metal mine (1)", "planet", 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 25, 10, 0, 1, false)); 
		quest.Add(new List_quest(1, "crystal mine", "build crystal mine (1)", "planet", 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 20, 15, 0, 1, false)); 
		quest.Add(new List_quest(2, "deuter sintetizer", "build deuter sintetizer (1)", "planet", 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 30, 20, 0, 1, false)); 
		quest.Add(new List_quest(3, "metal mine", "build metal mine (2)", "planet", 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 40, 20, 0, 5, false)); 
		quest.Add(new List_quest(4, "crystal mine", "build crystal mine (2)", "planet", 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 40, 30, 0, 5, false)); 
		quest.Add(new List_quest(5, "deuter sintetizer", "build deuter sintetizer (2)", "planet", 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 50, 35, 0, 5, false)); 
		quest.Add(new List_quest(6, "exploration", "destroy comets (25)", "cosmos", 0, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 25, 25, 25, 5, false)); 
		quest.Add(new List_quest(7, "exploration", "explore (2) moons", "cosmos", 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 50, 50, 50, 10, false)); 
		quest.Add(new List_quest(8, "exploration", "destroy enemy ships (1)", "cosmos", 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 50, 50, 25, 10, false)); 
		quest.Add(new List_quest(9, "laboratory", "build laboratory (1)", "planet", 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 50, 70, 30, 5, false)); 
		quest.Add(new List_quest(10, "hangar", "build hangar (1)", "planet", 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 70, 50, 30, 5, false));
		quest.Add(new List_quest(11, "metal mine", "build metal mine (8)", "planet", 0, 0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 250, 150, 0, 10, false)); 
		quest.Add(new List_quest(12, "crystal mine", "build crystal mine (6)", "planet", 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 200, 170, 0, 10, false)); 
		quest.Add(new List_quest(13, "deuter sintetizer", "build deuter sintetizer (6)", "planet", 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0, 250, 200, 0, 10, false)); 
		quest.Add(new List_quest(14, "laboratory", "build laboratory (3) to buy new technology", "planet", 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 100, 200, 50, 15, false)); 
		quest.Add(new List_quest(15, "field planet", "purchase 30 fields of the planet", "planet", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 30, 0, 0, 0, 200, 400, 75, false));
		quest.Add(new List_quest(16, "terraformer", "small planet? build terraformer (1)", "planet", 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 150, 300, 50, false));
		quest.Add(new List_quest(17, "resources", "spent 10.000 resources", "planet", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10000, 0, 500, 250, 125, 25, false));
		quest.Add(new List_quest(18, "hangar", "build hangar (3)", "planet", 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 250, 150, 100, 30, false));
		quest.Add(new List_quest(19, "antymatery", "get antymatery (20) from comets or ads", "antymatery", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 20, 500, 250, 250, 25, false));
		quest.Add(new List_quest(20, "ship", "buy ship", "ship", 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 100, 50, 75, 10, false));
		quest.Add(new List_quest(21, "exploration", "destroy comets (150)", "cosmos", 0, 150, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 150, 150, 150, 10, false)); 
		quest.Add(new List_quest(22, "exploration", "destroy enemy ships (5)", "cosmos", 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 250, 250, 50, 35, false)); 
		quest.Add(new List_quest(23, "exploration", "explore (10) moons", "cosmos", 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 500, 500, 250, 50, false));
		quest.Add(new List_quest(24, "metal mine", "build metal mine (16)", "planet", 0, 0, 0, 0, 16, 0, 0, 0, 0, 0, 0, 0, 0, 500, 380, 0, 50, false)); 
		quest.Add(new List_quest(25, "crystal mine", "build crystal mine (14)", "planet", 0, 0, 0, 0, 0, 14, 0, 0, 0, 0, 0, 0, 0, 500, 400, 0, 50, false)); 
		quest.Add(new List_quest(26, "deuter sintetizer", "build deuter sintetizer (14)", "planet", 0, 0, 0, 0, 0, 0, 14, 0, 0, 0, 0, 0, 0, 700, 450, 0, 50, false)); 	
		quest.Add(new List_quest(27, "field planet", "purchase 70 fields of the planet", "planet", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 70, 0, 0, 0, 1000, 2000, 150, false));
		quest.Add(new List_quest(28, "terraformer", "small planet? build terraformer (3)", "planet", 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 2000, 4000, 150, false));
		quest.Add(new List_quest(29, "antymatery", "get antymatery (30) from comets or ads", "antymatery", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 30, 500, 250, 250, 25, false));
		quest.Add(new List_quest(30, "ship", "buy ship", "ship", 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 300, 100, 50, 25, false));	
	}

    private void Info_quest(int nr)
    {
        text_name_quest.text = quest[nr].name_quest;
        text_description.text = quest[nr].description;
        text_reward_metal.text = quest[nr].reward_metal.ToString("N0");
        text_reward_crystal.text = quest[nr].reward_crystal.ToString("N0");
        text_reward_deuter.text = quest[nr].reward_deuter.ToString("N0");
        text_reward_exp.text = quest[nr].reward_exp.ToString("N0");

    }
    private void Change_photo_quest(int nr)
    {
        if (quest[nr].category_quest == "planet")
        {
            imgCategoryQuest.sprite = SpriteCategoryQuest[0];
        }
        else if (quest[nr].category_quest == "cosmos")
        {
            imgCategoryQuest.sprite = SpriteCategoryQuest[1];
        }
        else if (quest[nr].category_quest == "antymatery")
        {
            imgCategoryQuest.sprite = SpriteCategoryQuest[2];
        }
        else if (quest[nr].category_quest == "ship")
        {
            imgCategoryQuest.sprite = SpriteCategoryQuest[3];
        }
    }

    private void On_off_BtnQuest(int nr)
    {
        if (staty.Get_Data_From("Destroyed_Comets") >= quest[nr].require_comet && staty.Get_Data_From("Destroyed_Enemy_Ships") >= quest[nr].require_enemy_ships && staty.Get_Data_From("Bought_Ships") >= quest[nr].require_bought_ships && staty.Get_Data_From("Metal_Mine") >= quest[nr].require_metal_mine && staty.Get_Data_From("Crystal_Mine") >= quest[nr].require_crystal_mine && staty.Get_Data_From("Deuter_Sintetizer") >= quest[nr].require_deuter_sintetizer && staty.Get_Data_From("Laboratory") >= quest[nr].require_laboratory && staty.Get_Data_From("Hangar") >= quest[nr].require_hangar && staty.Get_Data_From("Terraformer") >= quest[nr].require_terraformer && staty.Get_Data_From("Bought_Field_Planet") >= quest[nr].require_field_planet && staty.Get_Data_From("Spent_Resources") >= quest[nr].require_spent_resources && staty.Get_Data_From("Antymatery") >= quest[nr].require_antymatery && staty.Get_Data_From("Explored_Moons") >= quest[nr].require_explored_moons)
        {
            btn_reward_quest.gameObject.SetActive(true);
        }
        else if (staty.Get_Data_From("Destroyed_Comets") < quest[nr].require_comet || staty.Get_Data_From("Destroyed_Enemy_Ships") < quest[nr].require_enemy_ships || staty.Get_Data_From("Bought_Ships") < quest[nr].require_bought_ships || staty.Get_Data_From("Metal_Mine") < quest[nr].require_metal_mine || staty.Get_Data_From("Crystal_Mine") < quest[nr].require_crystal_mine || staty.Get_Data_From("Deuter_Sintetizer") < quest[nr].require_deuter_sintetizer || staty.Get_Data_From("Laboratory") < quest[nr].require_laboratory || staty.Get_Data_From("Hangar") < quest[nr].require_hangar || staty.Get_Data_From("Terraformer") < quest[nr].require_terraformer || staty.Get_Data_From("Bought_Field_Planet") < quest[nr].require_field_planet || staty.Get_Data_From("Spent_Resources") < quest[nr].require_spent_resources || staty.Get_Data_From("Antymatery") < quest[nr].require_antymatery || staty.Get_Data_From("Explored_Moons") < quest[nr].require_explored_moons)
        {
            btn_reward_quest.gameObject.SetActive(false);
        }
    }

    private void Last_quest()
    {
        if (staty.Get_Data_From("Quest") > quest.Count())
        {
            staty.Set_Data("Quest", staty.Get_Data_From("Quest") + 0);
        }
        else if (staty.Get_Data_From("Quest") <= quest.Count())
        {
            staty.Set_Data("Quest", staty.Get_Data_From("Quest") + 1);
        }
    }
    public void BtnCheck_quest()
    {
        int nr = staty.Get_Data_From("Quest");
        //ify >= sprawdzajace kazdy parametr
        if (staty.Get_Data_From("Destroyed_Comets") >= quest[nr].require_comet && staty.Get_Data_From("Destroyed_Enemy_Ships") >=
            quest[nr].require_enemy_ships && staty.Get_Data_From("Bought_Ships") >= quest[nr].require_bought_ships && staty.Get_Data_From("Metal_Mine") >= quest[nr].require_metal_mine && staty.Get_Data_From("Crystal_Mine") >= quest[nr].require_crystal_mine && staty.Get_Data_From("Deuter_Sintetizer") >= quest[nr].require_deuter_sintetizer && staty.Get_Data_From("Laboratory") >= quest[nr].require_laboratory && staty.Get_Data_From("Hangar") >= quest[nr].require_hangar && staty.Get_Data_From("Terraformer") >= quest[nr].require_terraformer && staty.Get_Data_From("Bought_Field_Planet") >= quest[nr].require_field_planet && staty.Get_Data_From("Spent_Resources") >= quest[nr].require_spent_resources && staty.Get_Data_From("Antymatery") >= quest[nr].require_antymatery && staty.Get_Data_From("Explored_Moons") >= quest[nr].require_explored_moons)
        {
            staty.Set_Data("Metal", staty.Get_Data_From("Metal") + quest[nr].reward_metal);
            staty.Set_Data("Crystal", staty.Get_Data_From("Crystal") + quest[nr].reward_crystal);
            staty.Set_Data("Deuter", staty.Get_Data_From("Deuter") + quest[nr].reward_deuter);
            staty.Set_Data("Exp", staty.Get_Data_From("Exp") + quest[nr].reward_exp);
            Last_quest();
            PlayerPrefs.Save();
            ViewMessage("Quest complete");
        }
        else if (staty.Get_Data_From("Destroyed_Comets") < quest[nr].require_comet || staty.Get_Data_From("Destroyed_Enemy_Ships") < quest[nr].require_enemy_ships || staty.Get_Data_From("Bought_Ships") < quest[nr].require_bought_ships || staty.Get_Data_From("Metal_Mine") < quest[nr].require_metal_mine || staty.Get_Data_From("Crystal_Mine") < quest[nr].require_crystal_mine || staty.Get_Data_From("Deuter_Sintetizer") < quest[nr].require_deuter_sintetizer || staty.Get_Data_From("Laboratory") < quest[nr].require_laboratory || staty.Get_Data_From("Hangar") < quest[nr].require_hangar || staty.Get_Data_From("Terraformer") < quest[nr].require_terraformer || staty.Get_Data_From("Bought_Field_Planet") < quest[nr].require_field_planet || staty.Get_Data_From("Spent_Resources") < quest[nr].require_spent_resources || staty.Get_Data_From("Antymatery") < quest[nr].require_antymatery || staty.Get_Data_From("Explored_Moons") < quest[nr].require_explored_moons)
        {
            ViewMessage("Check require list");
        }
    }
    private void ViewMessage(string text)
    {
        GUIOverview.CanvasMessage.enabled = true;
        GUIOverview.textMessage.text = text;
        GUIOverview.audiosource_sound_message.PlayOneShot(GUIOverview.sound_message, 0.7F);
    }

	// Update is called once per frame
	private void LateUpdate () {
		On_off_BtnQuest(staty.Get_Data_From("Quest"));
		Info_quest(staty.Get_Data_From("Quest"));
		Change_photo_quest(staty.Get_Data_From("Quest"));
	}
}
