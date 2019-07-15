using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GUILasers : MonoBehaviour {
    private class List_lasers
    {
        public int id, price, require_lvl;
        public bool haveornothave;
        public string name;

        public List_lasers(int i, string n, int p, int rl, bool hoh)
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
        lasers.Add(new List_lasers(0, "RED", 5, 0, true));
        lasers.Add(new List_lasers(1, "GREEN", 5, 1, false));
        lasers.Add(new List_lasers(2, "BLUE", 5, 1, false));
        lasers.Add(new List_lasers(3, "WHITE", 5, 2, false));
        lasers.Add(new List_lasers(4, "PURPLE", 5, 2, false));
        lasers.Add(new List_lasers(5, "WHITE-RED", 5, 2, false));
        lasers.Add(new List_lasers(6, "GREEN-DARK GREEN", 5, 3, false));
    }

    private void Check_laser()
    {
        for (int nr = 0; nr < lasers.Count(); nr++)
        {
            lasers[nr].haveornothave = GUIPlanetOperations.Check_HasItem(lasers[nr].id, staty.Get_Data_From("Laser"));
            GUIPlanetOperations.Check_buttons(lasers[nr].haveornothave, text_button, nr, staty.Get_Data_From("Antymatery"), lasers[nr].price);
        }
    }

	public void BuyLasers(int nr)
    {
        if (!lasers[nr].haveornothave)
        {
            if (staty.Get_Data_From("Antymatery") >= lasers[nr].price)
            {
                lasers[nr].haveornothave = true;
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
