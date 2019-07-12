using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIStructures : MonoBehaviour
{
    private class List_Structures
    {
        public int id, antymatery, bonus, level;
        public string name, description;

        public List_Structures(int i, string n, int a, int l, int b, string d)
        {
            this.id = i;
            this.name = n;
            this.antymatery = a;
            this.level = l;
            this.bonus = b;
            this.description = d;
        }
    }

    private List<List_Structures> structures = new List<List_Structures>();
    private statystyki stats;
    private GUIPlanetOperations GUIPlanetOperations;

    public Text[] text_button;
    public Text textDebugStructures;
    public Sprite[] SpriteStructures;

    private void Start()
    {
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIPlanetOperations = GameObject.Find("Interface").GetComponent<GUIPlanetOperations>();
        structures.Add(new List_Structures(1, "Base", 10, 0, 1, "Base gives more place +1 for max recruited admirals."));
        structures.Add(new List_Structures(2, "Scout", 100, 0, 1, "Ten budynek umozliwia wyszukiwanie nowych admiralow. Kazdy poziom zwieksza +1 listy proponowanych admiralow."));
        Check_buttons();
    }

    private void Check_structures() //nadpisuje poziomy
    {
        structures[0].level = stats.Get_Data_From("Base");
        structures[1].level = stats.Get_Data_From("Scout");
    }

    private void Check_buttons()
    {
        for (int ilosc = 0; ilosc < structures.Count; ilosc++)
        {
            if (structures[ilosc].level < 3)
            {
                if (stats.Get_Data_From("Alliance_Antymatery") >= (structures[ilosc].antymatery * (structures[ilosc].level + 1)))
                {
                    text_button[ilosc].text = "BUY " + "(" + (structures[ilosc].level + 1) + ")";
                }
                else
                {
                    text_button[ilosc].text = "EARN " + "(" + (structures[ilosc].level + 1) + ")";
                }
            }
            else
            {
                text_button[ilosc].text = "MAX LVL";
            }
        }
    }

    private void Set_Properties_Up(int nr)
    {
        stats.Set_Data("Alliance_Antymatery", stats.Get_Data_From("Alliance_Antymatery") - structures[nr].antymatery * (structures[nr].level + 1));
        structures[nr].level += 1;
    }

    private void Set_Structures(int nr)
    {
        switch (structures[nr].name)
        {
            case "Base":
                stats.Set_Data("Base", structures[nr].level);
                break;
            case "Scout":
                stats.Set_Data("Scout", structures[nr].level);
                break;
        }
        PlayerPrefs.Save();
        Show_Information(nr, "Bought!");
        GUIPlanetOperations.PlaySound_Complete();
    }

    public void BtnBuy(int nr)
    {
        if (structures[nr].level <= 2)
        {
            if (stats.Get_Data_From("Alliance_Antymatery") >= (structures[nr].antymatery * (structures[nr].level + 1)))
            {
                Set_Properties_Up(nr);
                Set_Structures(nr);
            }
            else if (stats.Get_Data_From("Alliance_Antymatery") < (structures[nr].antymatery * (structures[nr].level + 1)))
            {
                GUIPlanetOperations.Turn_On_Ads("antymatery");
                Show_Information(nr, "Earn!");
            }
        }
        else if (structures[nr].level == 3)
        {
            Show_Information(nr, "MAX LVL!");
        }
    }

    private void LateUpdate()
    {
        Check_structures();
        Check_buttons();
    }

    private void Show_Information(int nr, string description)
    {
        GUIPlanetOperations.Subject_Information(0, 0, 0, Cost(structures[nr].antymatery, structures[nr].level),
        structures[nr].name + " (" + structures[nr].level.ToString() + ")", description, SpriteStructures[nr]);
    }

    public void Info_structures(int nr)
    {
        GUIPlanetOperations.Subject_Information(0, 0, 0, Cost(structures[nr].antymatery, structures[nr].level),
        structures[nr].name + " (" + structures[nr].level.ToString() + ")", structures[nr].description, SpriteStructures[nr]);
    }

    private int Cost(int cost, int level)
    {
        return cost*(level+1);
    }
}

