using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GUIBuildings : MonoBehaviour
{
    private class List_buildings
    {
        public int id, metal, crystal, deuter, income, capacity, level;
        public string name, description;
        public float factor;

        public List_buildings(int i, string n, string de, int m, int c, int d, int inc, int ca, float fa, int l)
        {
            this.id = i;
            this.name = n;
            this.description = de;
            this.metal = m;
            this.crystal = c;
            this.deuter = d;
            this.income = inc;
            this.capacity = ca;
            this.factor = fa;
            this.level = l;
        }
    }

    private List<List_buildings> buildings = new List<List_buildings>();
    private statystyki stats;
    private GUIPlanetOperations GUIPlanetOperations;

    public Text[] text_button;
    public Sprite[] SpriteBuildings;

    private void Start()
    {
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIPlanetOperations = GameObject.Find("Interface").GetComponent<GUIPlanetOperations>();

        buildings.Add(new List_buildings(0, "METAL MINE", "Each level of metal mine gives +5/30 sec income metal.", 24, 13, 0, 5, 0, 1.2f, 1));
        buildings.Add(new List_buildings(1, "CRYSTAL MINE", "Each level of crystal mine gives +5/30 sec income crystal.", 26, 17, 0, 5, 0, 1.2f, 1));
        buildings.Add(new List_buildings(2, "DEUTER SINTETIZER", "Each level of deuter sintetizer gives +5/30 sec income deuter.", 31, 23, 0, 5, 0, 1.2f, 1));
        buildings.Add(new List_buildings(3, "LABORATORY", "Each level laboratory gives more researches to learn.", 75, 100, 45, 0, 0, 1.8f, 1));
        buildings.Add(new List_buildings(4, "HANGAR", "Each level of hangar gives more ships and lasers to buy.", 100, 50, 75, 0, 0, 1.8f, 1));
        buildings.Add(new List_buildings(5, "METAL STORE", "Each level of metal store gives +1000 free space of magazine.", 100, 0, 0, 0, 1000, 1.5f, 1));
        buildings.Add(new List_buildings(6, "CRYSTAL STORE", "Each level of crystal store gives +1000 free space of magazine.", 75, 50, 0, 0, 1000, 1.5f, 1));
        buildings.Add(new List_buildings(7, "DEUTER STORE", "Each level of deuter store gives +1000 free space of magazine.", 75, 50, 50, 0, 1000, 1.5f, 1));
        buildings.Add(new List_buildings(8, "TERRAFORMER", "Each level of terraformer gives +15 free space of planet to build new buildings.", 0, 450, 600, 0, 0, 1.2f, 1));
        CheckButtons();
    }

    private void CheckBuildings() //nadpisuje poziomy
    {
        buildings[0].level = stats.Get_Data_From("Metal_Mine");
        buildings[1].level = stats.Get_Data_From("Crystal_Mine");
        buildings[2].level = stats.Get_Data_From("Deuter_Sintetizer");
        buildings[3].level = stats.Get_Data_From("Laboratory");
        buildings[4].level = stats.Get_Data_From("Hangar");
        buildings[5].level = stats.Get_Data_From("Metal_Store");
        buildings[6].level = stats.Get_Data_From("Crystal_Store");
        buildings[7].level = stats.Get_Data_From("Deuter_Store");
        buildings[8].level = stats.Get_Data_From("Terraformer");
    }

    private void CheckButtons()
    {
        for (int nr = 0; nr < buildings.Count; nr++)
        {
            if ((stats.Get_Data_From("Free_Field") >= 1) && stats.Get_Data_From("Metal") >= MetalCost(nr) && stats.Get_Data_From("Crystal") >= CrystalCost(nr) && stats.Get_Data_From("Deuter") >= DeuterCost(nr))
            {//zmien
                text_button[nr].text = "BUY " + "(" + (buildings[nr].level + 1) + ")";
            }
            else if ((stats.Get_Data_From("Free_Field") <= 0) && (buildings[nr].name != "TERRAFORMER")) //zrobic pozniej aby nie zaliczalo terraformera
            {
                text_button[nr].text = "EMPTY FIELD";
            }
            else if ((stats.Get_Data_From("Free_Field") >= 1) && stats.Get_Data_From("Metal") < MetalCost(nr) || stats.Get_Data_From("Crystal") < CrystalCost(nr) || stats.Get_Data_From("Deuter") < DeuterCost(nr))
            {
                text_button[nr].text = "EARN " + "(" + (buildings[nr].level + 1) + ")";
            }
            else if ((stats.Get_Data_From("Free_Field") <= 0) && (buildings[nr].name == "TERRAFORMER") && stats.Get_Data_From("Metal") >= MetalCost(nr) && stats.Get_Data_From("Crystal") >= CrystalCost(nr) && stats.Get_Data_From("Deuter") >= DeuterCost(nr))//zrobic pozniej aby nie zaliczalo terraformera
            {
                text_button[nr].text = "BUY " + "(" + (buildings[nr].level + 1) + ")";
            }
        }
    }
    private void Set_Properties_Up(int nr)
    {
        stats.Set_Data("Metal", stats.Get_Data_From("Metal") - MetalCost(nr));
        stats.Set_Data("Crystal", stats.Get_Data_From("Crystal") - CrystalCost(nr));
        stats.Set_Data("Deuter", stats.Get_Data_From("Deuter") - DeuterCost(nr));
        stats.Set_Data("Spent_Resources", stats.Get_Data_From("Spent_Resources") + GUIPlanetOperations.CountSpentResources(MetalCost(nr), CrystalCost(nr), DeuterCost(nr)));
        buildings[nr].level += 1;
        stats.Set_Data("Bought_Field_Planet", stats.Get_Data_From("Bought_Field_Planet") + 1);
    }

    private void Set_Free_Field(int value)
    {
        stats.Set_Data("Free_Field", stats.Get_Data_From("Free_Field") + value);
        PlayerPrefs.Save();
    }

    private void Show_Information(int nr, string text_return)
    {
        GUIPlanetOperations.Subject_Information(MetalCost(nr), CrystalCost(nr), DeuterCost(nr), 0,
        buildings[nr].name + " (" + buildings[nr].level.ToString() + ")", text_return, SpriteBuildings[nr]);
    }

    public void BuyBuilding(int nr)
    {
        if (stats.Get_Data_From("Free_Field") >= 1 && stats.Get_Data_From("Metal") >= MetalCost(nr) && stats.Get_Data_From("Crystal") >= CrystalCost(nr) && stats.Get_Data_From("Deuter") >= DeuterCost(nr))
        {
            Set_Properties_Up(nr);
            switch (nr)
            {
                case 0:
                    stats.Set_Data("Metal_Mine", buildings[nr].level);
                    stats.Set_Data("Income_Metal", buildings[nr].income * buildings[nr].level);
                    break;
                case 1:
                    stats.Set_Data("Crystal_Mine", buildings[nr].level);
                    stats.Set_Data("Income_Crystal", buildings[nr].income * buildings[nr].level);
                    break;
                case 2:
                    stats.Set_Data("Deuter_Sintetizer", buildings[nr].level);
                    stats.Set_Data("Income_Deuter", buildings[nr].income * buildings[nr].level);
                    break;
                case 3:
                    stats.Set_Data("Laboratory", buildings[nr].level);
                    break;
                case 4:
                    stats.Set_Data("Hangar", buildings[nr].level);
                    break;
                case 5:
                    stats.Set_Data("Metal_Store", buildings[nr].level);
                    stats.Set_Data("Capacity_Metal", stats.Get_Data_From("Capacity_Metal") + buildings[nr].capacity);
                    break;
                case 6:
                    stats.Set_Data("Crystal_Store", buildings[nr].level);
                    stats.Set_Data("Capacity_Crystal", stats.Get_Data_From("Capacity_Crystal") + buildings[nr].capacity);
                    break;
                case 7:
                    stats.Set_Data("Deuter_Store", buildings[nr].level);
                    stats.Set_Data("Capacity_Deuter", stats.Get_Data_From("Capacity_Deuter") + buildings[nr].capacity);
                    break;
            }
            Set_Free_Field(-1);
            Show_Information(nr, "Bought!");
            GUIPlanetOperations.PlaySound_Complete();
        }
        else if (stats.Get_Data_From("Free_Field") >= 1 && (stats.Get_Data_From("Metal") < MetalCost(nr) || stats.Get_Data_From("Crystal") < CrystalCost(nr) || stats.Get_Data_From("Deuter") < DeuterCost(nr)))
        {
            GUIPlanetOperations.Turn_On_Ads("resources");
            Show_Information(nr, "Earn!");
        }
    }

    public void BuyTerraformer(int nr)
    {
        if (stats.Get_Data_From("Free_Field") >= 0 && stats.Get_Data_From("Metal") >= MetalCost(nr) && stats.Get_Data_From("Crystal") >= CrystalCost(nr) && stats.Get_Data_From("Deuter") >= DeuterCost(nr))
        {
            Set_Properties_Up(nr);
            stats.Set_Data("Terraformer", buildings[nr].level);
            Set_Free_Field(15);
            Show_Information(nr, "Bought!");
            GUIPlanetOperations.PlaySound_Complete();
        }
        else if (stats.Get_Data_From("Free_Field") >= 0 && (stats.Get_Data_From("Metal") < MetalCost(nr) || stats.Get_Data_From("Crystal") < CrystalCost(nr) || stats.Get_Data_From("Deuter") < DeuterCost(nr)))
        {
            GUIPlanetOperations.Turn_On_Ads("resources");
            Show_Information(nr, "Earn!");
        }
    }

    private void LateUpdate()
    {
        CheckBuildings();
        CheckButtons();
    }

    public void Info_buildings(int nr)
    {
        Show_Information(nr, buildings[nr].description);
    }

    private int MetalCost(int nr)
    {
        return (int)Cost(buildings[nr].metal, buildings[nr].factor, buildings[nr].level);
    }

    private int CrystalCost(int nr)
    {
        return (int)Cost(buildings[nr].crystal, buildings[nr].factor, buildings[nr].level);
    }

    private int DeuterCost(int nr)
    {
        return (int)Cost(buildings[nr].deuter, buildings[nr].factor, buildings[nr].level);
    }

    private float Cost(float cost, float factor, float level)
    {
        return (int)(cost * Mathf.Exp(Mathf.Log(factor) * level));
    }
}
