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
    public Sprite[] SpriteStructures;

    private void Start()
    {
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIPlanetOperations = GameObject.Find("Interface").GetComponent<GUIPlanetOperations>();
        structures.Add(new List_Structures(1, "Space Base", 10, 0, 1, "Space Base gives more place +1 for max recruited admirals."));
        structures.Add(new List_Structures(2, "Scout", 100, 0, 1, "Ten budynek umozliwia wyszukiwanie nowych admiralow. Kazdy poziom zwieksza +1 listy proponowanych admiralow."));
        CheckButtons();
    }

    private void CheckStructures() //narazie max 1lvl
    {
        structures[0].level = stats.Get_Data_From("Space Base");
        structures[1].level = stats.Get_Data_From("Scout");
    }

    private void CheckButtons()
    {
        for (int nr = 0; nr < structures.Count; nr++)
        {
            if (structures[nr].level < 1)
            {
                if (stats.Get_Data_From("Alliance_Antymatery") >= (structures[nr].antymatery * (structures[nr].level + 1)))
                {
                    text_button[nr].text = "BUY " + "(" + (structures[nr].level + 1) + ")";
                }
                else
                {
                    text_button[nr].text = "EARN " + "(" + (structures[nr].level + 1) + ")";
                }
            }
            else
            {
                text_button[nr].text = "MAX LVL";
            }
        }
    }

    private void SetPropertiesUp(int nr)
    {
        stats.Set_Data("Alliance_Antymatery", stats.Get_Data_From("Alliance_Antymatery") - structures[nr].antymatery * (structures[nr].level + 1));
        structures[nr].level += 1;
    }

    private void SetStructures(int nr)
    {
        switch (structures[nr].name)
        {
            case "Space Base":
                stats.Set_Data("Space Base", structures[nr].level);
                break;
            case "Scout":
                stats.Set_Data("Scout", structures[nr].level);
                break;
        }
        PlayerPrefs.Save();
        ShowInformation(nr, "Bought!");
        GUIPlanetOperations.PlaySound_Complete();
    }

    public void BtnBuy(int nr)
    {
        if (structures[nr].level < 1)
        {
            if (stats.Get_Data_From("Alliance_Antymatery") >= (structures[nr].antymatery * (structures[nr].level + 1)))
            {
                SetPropertiesUp(nr);
                SetStructures(nr);
            }
            else if (stats.Get_Data_From("Alliance_Antymatery") < (structures[nr].antymatery * (structures[nr].level + 1)))
            {
                GUIPlanetOperations.Turn_On_Ads("antymatery");
                ShowInformation(nr, "Earn!");
            }
        }
        else if (structures[nr].level >= 2)
        {
            ShowInformation(nr, "MAX LVL!");
        }
    }

    private void LateUpdate()
    {
        CheckStructures();
        CheckButtons();
    }

    private void ShowInformation(int nr, string description)
    {
        GUIPlanetOperations.Subject_Information(0, 0, 0, Cost(structures[nr].antymatery, structures[nr].level),
        structures[nr].name + " (" + structures[nr].level.ToString() + ")", description, SpriteStructures[nr]);
    }

    public void Info_structures(int nr)
    {
        ShowInformation(nr, structures[nr].description);
    }

    private int Cost(int cost, int level)
    {
        return cost * (level + 1);
    }
}

