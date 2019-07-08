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

    // Use this for initialization
    void Start () {
        staty = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIPlanetOperations = GameObject.Find("Interface").GetComponent<GUIPlanetOperations>();
        lasers.Add(new List_lasers(1, "RED", 5, 0, 1));
        lasers.Add(new List_lasers(2, "GREEN", 5, 1, 0));
        lasers.Add(new List_lasers(3, "BLUE", 5, 1, 0));
        lasers.Add(new List_lasers(4, "WHITE", 5, 2, 0));
        lasers.Add(new List_lasers(5, "PURPLE", 5, 2, 0));
        lasers.Add(new List_lasers(6, "WHITE-RED", 5, 2, 0));
        lasers.Add(new List_lasers(7, "GREEN-DARK GREEN", 5, 3, 0));
        Check_buttons();
        GUIPlanetOperations.View_Subject(laserss, "Laser_Technology");
    }
    private void Check_laser()
    {
        for (int ilosc = 0; ilosc < lasers.Count(); ilosc++)
        {
            if (lasers[ilosc].id == staty.Get_Data_From("Laser"))
            {
                lasers[ilosc].haveornothave = 1;
            }
            else if (lasers[ilosc].id != staty.Get_Data_From("Laser"))
            {
                lasers[ilosc].haveornothave = 0;
            }
        }
    }

    private void Check_buttons() // zmienia tylko nazwy w tekscie
    {
        for (int ilosc = 0; ilosc < lasers.Count(); ilosc++)
        {
            if (lasers[ilosc].haveornothave == 1)
            {
                GUIPlanetOperations.View_Available_Subject(text_button,ilosc, "USING", new Color(.105f, .375f, .105f, 255f));
            }
            else if (lasers[ilosc].haveornothave == 0 && staty.Get_Data_From("Antymatery") >= lasers[ilosc].price)
            {
                GUIPlanetOperations.View_Available_Subject(text_button, ilosc, "CHANGE", new Color(255f, 255f, 255f, 255f));
            }
            else if (lasers[ilosc].haveornothave == 0 && staty.Get_Data_From("Antymatery") < lasers[ilosc].price)
            {
                GUIPlanetOperations.View_Available_Subject(text_button, ilosc, "EARN", new Color(255f, 255f, 255f, 255f));
            }
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
        Check_buttons();
        Check_laser();
        GUIPlanetOperations.View_Subject(laserss, "Laser_Technology");
    }
}
