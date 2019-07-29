using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GUIResearch : MonoBehaviour
{
    private class List_researches
    {
        public int id, metal, crystal, deuter, require_lvl, level;
        public string name, description;

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
    private statystyki stats;
    private GUIPlanetOperations GUIPlanetOperations;

    public GameObject[] researches;
    public Text[] text_button;
    public Sprite[] SpriteResearches;

    private void Start()
    {
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIPlanetOperations = GameObject.Find("Interface").GetComponent<GUIPlanetOperations>();

        research.Add(new List_researches(0, "Shield", "Each level of shield gives +1 life. Max lvl causes immortal for first collision with comet or enemy laser.", 100, 0, 0, 1, 1));
        research.Add(new List_researches(1, "Combustion", "Each level of combustion causes -1 consumtpion from ships. Max lvl gives 10% chance for 0 consumption on start exploration of space.", 300, 0, 500, 2, 1));
        research.Add(new List_researches(2, "Laser Technology", "Each level of laser technology gives more lasers to buy. Max lvl causes more damage from lasers on enemy ship.", 75, 225, 0, 3, 1));
        research.Add(new List_researches(3, "Mining Technology", "Each level of mining technology gives +2 max drop from comets. Max lvl causes +5 extra drop resource from comet.", 200, 350, 175, 4, 1));
        research.Add(new List_researches(4, "Antymatery Technology", "Each level of antymatery technology gives +1 chance drop antymatery from comets. Max lvl causes +1 max drop antymatery for watch ads.", 500, 500, 500, 5, 1));
        GUIPlanetOperations.View_Subject(researches, "Laboratory");
    }

    private void CheckResearch()
    {
        research[0].level = stats.Get_Data_From("Shield");
        research[1].level = (int)stats.Get_Float_Data_From("Combustion");
        research[2].level = stats.Get_Data_From("Laser Technology");
        research[3].level = stats.Get_Data_From("Mining Technology");
        research[4].level = stats.Get_Data_From("Antymatery Technology");
    }

    private void CheckButtons() // zmienia tylko nazwy w tekscie
    {
        for (int nr = 0; nr < research.Count(); nr++)
        {
            if (research[nr].level < 3)
            {
                if (stats.Get_Data_From("Metal") >= MetalCost(nr) && stats.Get_Data_From("Crystal") >= CrystalCost(nr) && stats.Get_Data_From("Deuter") >= DeuterCost(nr))
                {
                    text_button[nr].text = "BUY " + "(" + (research[nr].level + 1) + ")";
                }
                else
                {
                    text_button[nr].text = "EARN " + "(" + (research[nr].level + 1) + ")";
                }
            }
            else
            {
                text_button[nr].text = "MAX LVL";
            }
        }
    }

    private void Set_Properties_Up(int nr)
    {
        stats.Set_Data("Metal", stats.Get_Data_From("Metal") - MetalCost(nr));
        stats.Set_Data("Crystal", stats.Get_Data_From("Crystal") - CrystalCost(nr));
        stats.Set_Data("Deuter", stats.Get_Data_From("Deuter") - DeuterCost(nr));
        stats.Set_Data("Spent_Resources", stats.Get_Data_From("Spent_Resources") + GUIPlanetOperations.CountSpentResources(MetalCost(nr), CrystalCost(nr), DeuterCost(nr)));
        research[nr].level += 1;
    }

    private void Show_Information(int nr, string description)
    {
        GUIPlanetOperations.Subject_Information(MetalCost(nr), CrystalCost(nr), DeuterCost(nr), 0,
        research[nr].name.ToUpper() + " (" + research[nr].level.ToString() + ")", description, SpriteResearches[nr]);
    }

    private void Set_Technology(int nr)
    {
        switch (research[nr].name)
        {
            case "Combustion":
                stats.Set_Float_Data("Combustion", research[nr].level);
                break;
            default:
                stats.Set_Data(research[nr].name, research[nr].level);
                break;
        }
        PlayerPrefs.Save();
        Show_Information(nr, "Bought!");
        GUIPlanetOperations.PlaySound_Complete();
    }

    public void BuyResearch(int nr)
    {
        if (research[nr].level <= 2)
        {
            if (stats.Get_Data_From("Metal") >= MetalCost(nr) && stats.Get_Data_From("Crystal") >= CrystalCost(nr) && stats.Get_Data_From("Deuter") >= DeuterCost(nr))
            {
                Set_Properties_Up(nr);
                Set_Technology(nr);
            }
            else if (stats.Get_Data_From("Metal") < MetalCost(nr) || stats.Get_Data_From("Crystal") < CrystalCost(nr) || stats.Get_Data_From("Deuter") < DeuterCost(nr))
            {
                GUIPlanetOperations.Turn_On_Ads("resources");
                Show_Information(nr, "Earn!");
            }
        }
        else if (research[nr].level == 3)
        {
            Show_Information(nr, "MAX LVL!");
        }
    }

    private void LateUpdate()
    {
        CheckResearch();
        CheckButtons();
        GUIPlanetOperations.View_Subject(researches, "Laboratory");
    }

    public void Info_researches(int nr)
    {
        Show_Information(nr, research[nr].description);
    }

    private int MetalCost(int nr)
    {
       return research[nr].metal * (research[nr].level + 1);
    }

    private int CrystalCost(int nr)
    {
        return research[nr].crystal * (research[nr].level + 1);
    }

    private int DeuterCost(int nr)
    {
        return research[nr].deuter * (research[nr].level + 1);
    }
}

