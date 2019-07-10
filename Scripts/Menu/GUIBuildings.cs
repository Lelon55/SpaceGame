using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GUIBuildings : MonoBehaviour {
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
        private statystyki staty;
        private GUIPlanetOperations GUIPlanetOperations;

        public Text[] text_button;
        public Text textDebugBuildings;
        private int spent_resources;
        public Sprite[] SpriteBuildings;
        public AudioClip sound_buildup;
        public AudioSource audiosource_sound_buildup;
    

    // Use this for initialization
    private void Start() {
        staty = GameObject.Find("Scripts").GetComponent<statystyki>();
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
        Check_buttons();
    }
    private void Check_buildings() //nadpisuje poziomy
    {
        buildings[0].level = staty.Get_Data_From("Metal_Mine");
        buildings[1].level = staty.Get_Data_From("Crystal_Mine");
        buildings[2].level = staty.Get_Data_From("Deuter_Sintetizer");
        buildings[3].level = staty.Get_Data_From("Laboratory");
        buildings[4].level = staty.Get_Data_From("Hangar");
        buildings[5].level = staty.Get_Data_From("Metal_Store");
        buildings[6].level = staty.Get_Data_From("Crystal_Store");
        buildings[7].level = staty.Get_Data_From("Deuter_Store");
        buildings[8].level = staty.Get_Data_From("Terraformer");
    }
    private void Check_buttons() // zmienia tylko nazwy w tekscie
    {
        for (int ilosc = 0; ilosc < buildings.Count; ilosc++)
        {
            if ((staty.Get_Data_From("Free_Field") >= 1) && (staty.Get_Data_From("Metal") >= Cost(buildings[ilosc].metal, buildings[ilosc].factor, buildings[ilosc].level)) && (staty.Get_Data_From("Crystal") >= Cost(buildings[ilosc].crystal, buildings[ilosc].factor, buildings[ilosc].level)) && (staty.Get_Data_From("Deuter") >= Cost(buildings[ilosc].deuter, buildings[ilosc].factor, buildings[ilosc].level)))
            {//zmien
                text_button[ilosc].text = "BUY " + "(" + (buildings[ilosc].level + 1) + ")";
            }
            else if ((staty.Get_Data_From("Free_Field") <= 0) && (buildings[ilosc].name != "TERRAFORMER")) //zrobic pozniej aby nie zaliczalo terraformera
            {
                text_button[ilosc].text = "EMPTY FIELD";
            }
            else if ((staty.Get_Data_From("Free_Field") >= 1) && (staty.Get_Data_From("Metal") < Cost(buildings[ilosc].metal, buildings[ilosc].factor, buildings[ilosc].level)) || (staty.Get_Data_From("Crystal") < Cost(buildings[ilosc].crystal, buildings[ilosc].factor, buildings[ilosc].level)) || (staty.Get_Data_From("Deuter") < Cost(buildings[ilosc].deuter, buildings[ilosc].factor, buildings[ilosc].level)))
            {
                text_button[ilosc].text = "EARN " + "(" + (buildings[ilosc].level + 1) + ")";
            }
            else if ((staty.Get_Data_From("Free_Field") <= 0) && (buildings[ilosc].name == "TERRAFORMER") && (staty.Get_Data_From("Metal") >= Cost(buildings[ilosc].metal, buildings[ilosc].factor, buildings[ilosc].level)) && (staty.Get_Data_From("Crystal") >= Cost(buildings[ilosc].crystal, buildings[ilosc].factor, buildings[ilosc].level)) && (staty.Get_Data_From("Deuter") >= Cost(buildings[ilosc].deuter, buildings[ilosc].factor, buildings[ilosc].level)))//zrobic pozniej aby nie zaliczalo terraformera
            {
                text_button[ilosc].text = "BUY " + "(" + (buildings[ilosc].level + 1) + ")";
            }
            else if ((staty.Get_Data_From("Free_Field") <= 0) && (buildings[ilosc].name == "TERRAFORMER") && (staty.Get_Data_From("Metal") < Cost(buildings[ilosc].metal, buildings[ilosc].factor, buildings[ilosc].level)) || (staty.Get_Data_From("Crystal") < Cost(buildings[ilosc].crystal, buildings[ilosc].factor, buildings[ilosc].level)) || (staty.Get_Data_From("Deuter") < Cost(buildings[ilosc].deuter, buildings[ilosc].factor, buildings[ilosc].level)))//zrobic pozniej aby nie zaliczalo terraformera
            {
                text_button[ilosc].text = "BUY " + "(" + (buildings[ilosc].level + 1) + ")";
            }
        }
    }
    private void Set_Properties_Up(int nr)
    {
        staty.Set_Data("Metal", staty.Get_Data_From("Metal") - (int)Cost(buildings[nr].metal, buildings[nr].factor, buildings[nr].level));
        staty.Set_Data("Crystal", staty.Get_Data_From("Crystal") - (int)Cost(buildings[nr].crystal, buildings[nr].factor, buildings[nr].level));
        staty.Set_Data("Deuter", staty.Get_Data_From("Deuter") - (int)Cost(buildings[nr].deuter, buildings[nr].factor, buildings[nr].level));
        spent_resources = (int)Cost(buildings[nr].metal, buildings[nr].factor, buildings[nr].level) + (int)Cost(buildings[nr].crystal, buildings[nr].factor, buildings[nr].level) + (int)Cost(buildings[nr].deuter, buildings[nr].factor, buildings[nr].level);
        buildings[nr].level += 1;
        staty.Set_Data("Spent_Resources", staty.Get_Data_From("Spent_Resources") + spent_resources);
        staty.Set_Data("Bought_Field_Planet", staty.Get_Data_From("Bought_Field_Planet") + 1);
    }
    private void Set_Free_Field(int value){
        
        staty.Set_Data("Free_Field", staty.Get_Data_From("Free_Field") + value);
        PlayerPrefs.Save();
    }
    private void Set_Properties_Down(int nr)
    {
        textDebugBuildings.text = "BOUGHT: " + buildings[nr].name + "(" + buildings[nr].level + ")";
        audiosource_sound_buildup.PlayOneShot(sound_buildup, 0.7F);
    }

    private void Earn_Information(int nr)
    {
        textDebugBuildings.text = "EARN: " + buildings[nr].name + "(" + (buildings[nr].level + 1) + ")";
    }

    public void Buybuildings0(int nr)
    {
        if ((staty.Get_Data_From("Free_Field") >= 1) && (staty.Get_Data_From("Metal") >= Cost(buildings[nr].metal, buildings[nr].factor, buildings[nr].level)) && (staty.Get_Data_From("Crystal") >= Cost(buildings[nr].crystal, buildings[nr].factor, buildings[nr].level)) && (staty.Get_Data_From("Deuter") >= Cost(buildings[nr].deuter, buildings[nr].factor, buildings[nr].level)))
        {
            Set_Properties_Up(nr);
            if (nr == 0)
            {
                staty.Set_Data("Metal_Mine", buildings[nr].level);
                staty.Set_Data("Income_Metal", buildings[nr].income * buildings[nr].level);
            }
            else if (nr == 1)
            {
                staty.Set_Data("Crystal_Mine", buildings[nr].level);
                staty.Set_Data("Income_Crystal", buildings[nr].income * buildings[nr].level);
            }
            else if (nr == 2)
            {
                staty.Set_Data("Deuter_Sintetizer", buildings[nr].level);
                staty.Set_Data("Income_Deuter", buildings[nr].income * buildings[nr].level);
            }
            else if (nr == 3)
            {
                staty.Set_Data("Laboratory", buildings[nr].level);
            }
            else if (nr == 4)
            {
                staty.Set_Data("Hangar", buildings[nr].level);
            }
            else if (nr == 5)
            {
                staty.Set_Data("Metal_Store", buildings[nr].level);
                staty.Set_Data("Capacity_Metal", staty.Get_Data_From("Capacity_Metal") + buildings[nr].capacity);
            }
            else if (nr == 6)
            {
                staty.Set_Data("Crystal_Store", buildings[nr].level);
                staty.Set_Data("Capacity_Crystal", staty.Get_Data_From("Capacity_Crystal") + buildings[nr].capacity);
            }
            else if (nr == 7)
            {
                staty.Set_Data("Deuter_Store", buildings[nr].level);
                staty.Set_Data("Capacity_Deuter", staty.Get_Data_From("Capacity_Deuter")+buildings[nr].capacity);
            }

            Set_Free_Field(-1);
            Set_Properties_Down(nr);

        }
        else if ((staty.Get_Data_From("Free_Field") >= 1) && (staty.Get_Data_From("Metal") < Cost(buildings[nr].metal, buildings[nr].factor, buildings[nr].level)) || (staty.Get_Data_From("Crystal") < Cost(buildings[nr].crystal, buildings[nr].factor, buildings[nr].level)) || (staty.Get_Data_From("Deuter") < Cost(buildings[nr].deuter, buildings[nr].factor, buildings[nr].level)))
        {
            GUIPlanetOperations.Turn_On_Ads("resources");
            Earn_Information(nr);
        }
    }

    public void Buybuildings8()
    {
        if (staty.Get_Data_From("Metal") >= Cost(buildings[8].metal, buildings[8].factor, buildings[8].level) && (staty.Get_Data_From("Crystal") >= Cost(buildings[8].crystal, buildings[8].factor, buildings[8].level)) && (staty.Get_Data_From("Deuter") >= Cost(buildings[8].deuter, buildings[8].factor, buildings[8].level)))
        {
            Set_Properties_Up(8);
            staty.Set_Data("Terraformer", buildings[8].level);
            Set_Free_Field(15);
            Set_Properties_Down(8);

        }
        else if ((staty.Get_Data_From("Metal") < Cost(buildings[8].metal, buildings[8].factor, buildings[8].level)) || (staty.Get_Data_From("Crystal") < Cost(buildings[8].crystal, buildings[8].factor, buildings[8].level)) || (staty.Get_Data_From("Deuter") < Cost(buildings[8].deuter, buildings[8].factor, buildings[8].level)))
        {
            GUIPlanetOperations.Turn_On_Ads("resources");
            Earn_Information(8);
        }
    }
    // Update is called once per frame
    private void LateUpdate () {
        Check_buildings();
		Check_buttons();
	}

	public void Info_buildings(int nr){
        GUIPlanetOperations.Subject_Information((int)Cost(buildings[nr].metal, buildings[nr].factor, buildings[nr].level),
        (int)Cost(buildings[nr].crystal, buildings[nr].factor, buildings[nr].level),
        (int)Cost(buildings[nr].deuter, buildings[nr].factor, buildings[nr].level), 0,
        buildings[nr].name + " (" + buildings[nr].level.ToString() + ")",
        buildings[nr].description,
        SpriteBuildings[nr]);
	}

    private float Cost(float cost, float factor, float level)
    {
         return (int)(cost * (Mathf.Exp(Mathf.Log(factor) * level)));
    }
}
