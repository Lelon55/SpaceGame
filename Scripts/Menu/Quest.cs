using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Quest : MonoBehaviour {
    private class ListQuest
    {
        public int id;
        public string quest_name, description, category_quest;
        public int require_explored_moons, require_comet, require_enemy_ships, require_bought_ships;
        public int require_metal_mine, require_crystal_mine, require_deuter_sintetizer, require_laboratory, require_hangar;
        public int require_terraformer, require_field_planet, require_spent_resources, require_antymatery;
        public int reward_metal, reward_crystal, reward_deuter, reward_exp;
        public bool done;

        public ListQuest(int i, string nq, string dc, string cq, int r_explored_moons, int r_comet, int r_enemy, int r_bought, int r_metal_mine, int r_crystal_mine, int r_deuter_sintetizer, int r_laboratory, int r_hangar, int r_terraformer, int r_field, int r_spent_resources, int r_antymatery, int re_metal, int re_crystal, int re_deuter, int re_exp, bool done)
        {
            this.id = i;
            this.quest_name = nq;
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

    private List<ListQuest> quest = new List<ListQuest>();
	public Text text_reward_metal, text_reward_crystal, text_reward_deuter, text_reward_exp, text_quest_name, text_description;
	public Button btn_reward_quest;
	public Sprite[] SpriteCategoryQuest;
	public Image imgCategoryQuest;

    private GUIOverview GUIOverview;
    private statystyki staty;

    private void Start () {
        GUIOverview = GameObject.Find("Interface").GetComponent<GUIOverview>();
        staty = GameObject.Find("Scripts").GetComponent<statystyki>();

        quest.Add(new ListQuest(0, "metal mine", "build metal mine (1)", "planet", 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 25, 10, 0, 1, false)); 
		quest.Add(new ListQuest(1, "crystal mine", "build crystal mine (1)", "planet", 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 20, 15, 0, 1, false)); 
		quest.Add(new ListQuest(2, "deuter sintetizer", "build deuter sintetizer (1)", "planet", 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 30, 20, 0, 1, false)); 
		quest.Add(new ListQuest(3, "metal mine", "build metal mine (2)", "planet", 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 40, 20, 0, 5, false)); 
		quest.Add(new ListQuest(4, "crystal mine", "build crystal mine (2)", "planet", 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 40, 30, 0, 5, false)); 
		quest.Add(new ListQuest(5, "deuter sintetizer", "build deuter sintetizer (2)", "planet", 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 50, 35, 0, 5, false)); 
		quest.Add(new ListQuest(6, "exploration", "destroy comets (25)", "cosmos", 0, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 25, 25, 25, 5, false)); 
		quest.Add(new ListQuest(7, "exploration", "explore (2) moons", "cosmos", 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 50, 50, 50, 10, false)); 
		quest.Add(new ListQuest(8, "exploration", "destroy enemy ships (1)", "cosmos", 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 50, 50, 25, 10, false)); 
		quest.Add(new ListQuest(9, "laboratory", "build laboratory (1)", "planet", 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 50, 70, 30, 5, false)); 
		quest.Add(new ListQuest(10, "hangar", "build hangar (1)", "planet", 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 70, 50, 30, 5, false));
		quest.Add(new ListQuest(11, "metal mine", "build metal mine (8)", "planet", 0, 0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 250, 150, 0, 10, false)); 
		quest.Add(new ListQuest(12, "crystal mine", "build crystal mine (6)", "planet", 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 200, 170, 0, 10, false)); 
		quest.Add(new ListQuest(13, "deuter sintetizer", "build deuter sintetizer (6)", "planet", 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0, 250, 200, 0, 10, false)); 
		quest.Add(new ListQuest(14, "laboratory", "build laboratory (3) to buy new technology", "planet", 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 100, 200, 50, 15, false)); 
		quest.Add(new ListQuest(15, "field planet", "purchase 30 fields of the planet", "planet", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 30, 0, 0, 0, 200, 400, 75, false));
		quest.Add(new ListQuest(16, "terraformer", "small planet? build terraformer (1)", "planet", 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 150, 300, 50, false));
		quest.Add(new ListQuest(17, "resources", "spent 10.000 resources", "planet", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10000, 0, 500, 250, 125, 25, false));
		quest.Add(new ListQuest(18, "hangar", "build hangar (3)", "planet", 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 250, 150, 100, 30, false));
		quest.Add(new ListQuest(19, "antymatery", "get antymatery (25) from comets or ads", "antymatery", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 25, 500, 250, 250, 25, false));
		quest.Add(new ListQuest(20, "ship", "buy ship", "ship", 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 100, 50, 75, 10, false));
		quest.Add(new ListQuest(21, "exploration", "destroy comets (150)", "cosmos", 0, 150, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 150, 150, 150, 10, false)); 
		quest.Add(new ListQuest(22, "exploration", "destroy enemy ships (5)", "cosmos", 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 250, 250, 50, 35, false)); 
		quest.Add(new ListQuest(23, "exploration", "explore (10) moons", "cosmos", 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 500, 500, 250, 50, false));
        quest.Add(new ListQuest(24, "metal mine", "build metal mine (16)", "planet", 0, 0, 0, 0, 16, 0, 0, 0, 0, 0, 0, 0, 0, 500, 380, 0, 50, false)); 
		quest.Add(new ListQuest(25, "crystal mine", "build crystal mine (14)", "planet", 0, 0, 0, 0, 0, 14, 0, 0, 0, 0, 0, 0, 0, 500, 400, 0, 50, false)); 
		quest.Add(new ListQuest(26, "deuter sintetizer", "build deuter sintetizer (14)", "planet", 0, 0, 0, 0, 0, 0, 14, 0, 0, 0, 0, 0, 0, 700, 450, 0, 50, false)); 	
		quest.Add(new ListQuest(27, "field planet", "purchase 70 fields of the planet", "planet", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 70, 0, 0, 0, 1000, 2000, 150, false));
		quest.Add(new ListQuest(28, "terraformer", "small planet? build terraformer (3)", "planet", 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 2000, 4000, 150, false));
		quest.Add(new ListQuest(29, "antymatery", "get antymatery (30) from comets or ads", "antymatery", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 30, 500, 250, 250, 25, false));
		quest.Add(new ListQuest(30, "ship", "buy ship", "ship", 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 300, 100, 50, 25, false));
        quest.Add(new ListQuest(31, "resources", "spent 25.000 resources", "ship", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 25000, 0, 1500, 1000, 750, 30, false));
        quest.Add(new ListQuest(32, "laboratory", "build laboratory (5) to buy new technology", "planet", 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 650, 800, 500, 15, false));
        quest.Add(new ListQuest(33, "exploration", "destroy comets (350)", "cosmos", 0, 350, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 350, 350, 350, 15, false));
        quest.Add(new ListQuest(34, "exploration", "explore (20) moons", "cosmos", 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 750, 750, 500, 45, false));
        quest.Add(new ListQuest(35, "exploration", "destroy enemy ships (15)", "cosmos", 0, 0, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 500, 500, 150, 30, false));
    }

    private void QuestInformation(int nr)
    {
        text_quest_name.text = quest[nr].quest_name;
        text_description.text = quest[nr].description;
        text_reward_metal.text = quest[nr].reward_metal.ToString("N0");
        text_reward_crystal.text = quest[nr].reward_crystal.ToString("N0");
        text_reward_deuter.text = quest[nr].reward_deuter.ToString("N0");
        text_reward_exp.text = quest[nr].reward_exp.ToString("N0");
    }

    private void SetQuestPhoto(int nr)
    {
        switch (quest[nr].category_quest)
        {
            case "planet":
                imgCategoryQuest.sprite = SpriteCategoryQuest[0];
                break;
            case "cosmos":
                imgCategoryQuest.sprite = SpriteCategoryQuest[1];
                break;
            case "antymatery":
                imgCategoryQuest.sprite = SpriteCategoryQuest[2];
                break;
            case "ship":
                imgCategoryQuest.sprite = SpriteCategoryQuest[3];
                break;
        }
    }

    private void SetActiveBtnQuest(int nr)
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

    private void LastQuest()
    {
        if (staty.Get_Data_From("Quest") >= quest.Count())
        {
            staty.Set_Data("Quest", staty.Get_Data_From("Quest"));
        }
        else if (staty.Get_Data_From("Quest") < quest.Count() - 1)
        {
            staty.Set_Data("Quest", staty.Get_Data_From("Quest") + 1);
        }
    }

    private void QuestReward(int nr)
    {
        staty.Set_Data("Metal", staty.Get_Data_From("Metal") + quest[nr].reward_metal);
        staty.Set_Data("Crystal", staty.Get_Data_From("Crystal") + quest[nr].reward_crystal);
        staty.Set_Data("Deuter", staty.Get_Data_From("Deuter") + quest[nr].reward_deuter);
        staty.Set_Data("Exp", staty.Get_Data_From("Exp") + quest[nr].reward_exp);
    }

    public void CheckQuest()
    {
        int nr = staty.Get_Data_From("Quest");
        //ify >= sprawdzajace kazdy parametr
        if (staty.Get_Data_From("Destroyed_Comets") >= quest[nr].require_comet && staty.Get_Data_From("Destroyed_Enemy_Ships") >=
            quest[nr].require_enemy_ships && staty.Get_Data_From("Bought_Ships") >= quest[nr].require_bought_ships && staty.Get_Data_From("Metal_Mine") >= quest[nr].require_metal_mine && staty.Get_Data_From("Crystal_Mine") >= quest[nr].require_crystal_mine && staty.Get_Data_From("Deuter_Sintetizer") >= quest[nr].require_deuter_sintetizer && staty.Get_Data_From("Laboratory") >= quest[nr].require_laboratory && staty.Get_Data_From("Hangar") >= quest[nr].require_hangar && staty.Get_Data_From("Terraformer") >= quest[nr].require_terraformer && staty.Get_Data_From("Bought_Field_Planet") >= quest[nr].require_field_planet && staty.Get_Data_From("Spent_Resources") >= quest[nr].require_spent_resources && staty.Get_Data_From("Antymatery") >= quest[nr].require_antymatery && staty.Get_Data_From("Explored_Moons") >= quest[nr].require_explored_moons)
        {
            QuestReward(nr);
            LastQuest();
            PlayerPrefs.Save();
            GUIOverview.View_CanvasMessage("Quest complete");
        }
        else if (staty.Get_Data_From("Destroyed_Comets") < quest[nr].require_comet || staty.Get_Data_From("Destroyed_Enemy_Ships") < quest[nr].require_enemy_ships || staty.Get_Data_From("Bought_Ships") < quest[nr].require_bought_ships || staty.Get_Data_From("Metal_Mine") < quest[nr].require_metal_mine || staty.Get_Data_From("Crystal_Mine") < quest[nr].require_crystal_mine || staty.Get_Data_From("Deuter_Sintetizer") < quest[nr].require_deuter_sintetizer || staty.Get_Data_From("Laboratory") < quest[nr].require_laboratory || staty.Get_Data_From("Hangar") < quest[nr].require_hangar || staty.Get_Data_From("Terraformer") < quest[nr].require_terraformer || staty.Get_Data_From("Bought_Field_Planet") < quest[nr].require_field_planet || staty.Get_Data_From("Spent_Resources") < quest[nr].require_spent_resources || staty.Get_Data_From("Antymatery") < quest[nr].require_antymatery || staty.Get_Data_From("Explored_Moons") < quest[nr].require_explored_moons)
        {
            GUIOverview.View_CanvasMessage("Check require list");
        }
    }

	private void LateUpdate () {
		SetActiveBtnQuest(staty.Get_Data_From("Quest"));
		QuestInformation(staty.Get_Data_From("Quest"));
		SetQuestPhoto(staty.Get_Data_From("Quest"));
	}
}
