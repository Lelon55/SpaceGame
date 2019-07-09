using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GUILasers : MonoBehaviour {
    private class List_lasers
    {
        public int id, price, require_lvl, haveornothave;
        public string name;

        public List_lasers(int i, string n, int p, int rl, int hoh)
        {
            this.id = i;
            this.name = n;
            this.price = p;
			this.require_lvl = rl;
            this.haveornothave = hoh;
        }
    }

    private List<List_lasers> lasers = new List<List_lasers>();
    private statystyki staty;
    public Text[] text_button;
	public GameObject[] laserss;
    public Text textDebugLasers;
    private GUIPlanetOperations GUIPlanetOperations;

    private void Start () {
        staty = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIPlanetOperations = GameObject.Find("Interface").GetComponent<GUIPlanetOperations>();
        lasers.Add(new List_lasers(1, "RED", 5, 0, 1));
        lasers.Add(new List_lasers(2, "GREEN", 5, 1, 0));
        lasers.Add(new List_lasers(3, "BLUE", 5, 1, 0));
        lasers.Add(new List_lasers(4, "WHITE", 5, 2, 0));
        lasers.Add(new List_lasers(5, "PURPLE", 5, 2, 0));
        lasers.Add(new List_lasers(6, "WHITE-RED", 5, 2, 0));
        lasers.Add(new List_lasers(7, "GREEN-DARK GREEN", 5, 3, 0));
    }

    private void Check_laser()
    {
        for (int ilosc = 0; ilosc < lasers.Count(); ilosc++)
        {
            lasers[ilosc].haveornothave = GUIPlanetOperations.Check_HasItem(lasers[ilosc].id, staty.Get_Data_From("Laser"));
            GUIPlanetOperations.Check_buttons(lasers[ilosc].haveornothave, text_button, ilosc, staty.Get_Data_From("Antymatery"), lasers[ilosc].price);
        }
    }

	public void BuyLasers(int nr)
    {
        if (lasers[nr].haveornothave == 0)
        {
            if (staty.Get_Data_From("Antymatery") >= lasers[nr].price)
            {
                lasers[nr].haveornothave = 1;
                staty.Change_Antymatery(-lasers[nr].price);
                staty.Set_Data("Laser", lasers[nr].id);
                PlayerPrefs.Save();
                textDebugLasers.text = "BOUGHT: " + lasers[nr].name;

            }
            else if (staty.Get_Data_From("Antymatery") < lasers[nr].price)
            {
                GUIPlanetOperations.Turn_On_Ads("antymatery");
                textDebugLasers.text = "EARN: " + lasers[nr].name;
            }
        } 
    }

    private void LateUpdate () {
        Check_laser();
        GUIPlanetOperations.View_Subject(laserss, "Laser Technology");
    }
}
