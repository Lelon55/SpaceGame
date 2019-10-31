using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIStructures : MonoBehaviour
{
    private class ListStructures
    {
        public int id, antymatery, level;
        public string name, description;

        public ListStructures(int i, string n, int a, int l, string d)
        {
            this.id = i;
            this.name = n;
            this.antymatery = a;
            this.level = l;
            this.description = d;
        }
    }

    private List<ListStructures> structures = new List<ListStructures>();
    private statystyki stats;
    private GUIPlanetOperations GUIPlanetOperations;

    public Text[] text_button;
    public Sprite[] SpriteStructures;

    private void Start()
    {
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIPlanetOperations = GameObject.Find("Interface").GetComponent<GUIPlanetOperations>();
        structures.Add(new ListStructures(1, "Space Base", 25, 0, "Space Base gives more place +1 for max recruited admirals."));
        structures.Add(new ListStructures(2, "Scout", 10, 0, "Scout can find the better allies."));
    }

    private void CheckStructures() //narazie max 1lvl
    {
        structures[0].level = stats.Get_Data_From("Space Base");
        structures[1].level = stats.Get_Data_From("Scout");
    }

    private void SetButtons()
    {
        for (int nr = 0; nr < structures.Count; nr++)
        {
            text_button[nr].text = GUIPlanetOperations.CheckLevel(structures[nr].level, 3, Cost(nr));
        }
    }

    private void SetPropertiesUp(int nr)
    {
        stats.Set_Data("Alliance_Antymatery", stats.Get_Data_From("Alliance_Antymatery") - Cost(nr));
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
        if (structures[nr].level < 3)
        {
            if (stats.Get_Data_From("Alliance_Antymatery") >= Cost(nr))
            {
                SetPropertiesUp(nr);
                SetStructures(nr);
            }
            else if (stats.Get_Data_From("Alliance_Antymatery") < Cost(nr))
            {
                GUIPlanetOperations.Turn_On_Ads("antymatery");
                ShowInformation(nr, "Earn!");
            }
        }
        else if (structures[nr].level >= 3)
        {
            ShowInformation(nr, "MAX LVL!");
        }
    }

    private void LateUpdate()
    {
        CheckStructures();
        SetButtons();
    }

    private void ShowInformation(int nr, string description)
    {
        GUIPlanetOperations.Subject_Information(0, 0, 0, Cost(nr),
        structures[nr].name + " (" + structures[nr].level.ToString() + ")", description, SpriteStructures[nr]);
    }

    public void Info_structures(int nr)
    {
        ShowInformation(nr, structures[nr].description);
    }

    private int Cost(int nr)
    {
        return structures[nr].antymatery * (structures[nr].level + 1);
    }
}

