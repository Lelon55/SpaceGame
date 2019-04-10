using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GUILasers : MonoBehaviour {
    private class list_lasers
    {
        public int id, price, require_lvl, haveornothave;
        public string name;

        public list_lasers(int i, string n, int p, int rl, int hoh)
        {
            this.id = i;
            this.name = n;
            this.price = p;
			this.require_lvl = rl;
            this.haveornothave = hoh;
        }
    }
    private List<list_lasers> lasers = new List<list_lasers>();

    private statystyki staty;
    public Button[] buttony;
    public Text[] text_button;
	public GameObject[] laserss;
    public Text textDebugLasers;
    private Ads Ads;
    private GUIPlanetOperations GUIPlanetOperations;

    // Use this for initialization
    void Start () {
        Ads = GameObject.Find("Scripts").GetComponent<Ads>();
        staty = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIPlanetOperations = GameObject.Find("Interface").GetComponent<GUIPlanetOperations>();
        lasers.Add(new list_lasers(1, "RED", 5, 0, 1));
        lasers.Add(new list_lasers(2, "GREEN", 5, 1, 0));
        lasers.Add(new list_lasers(3, "BLUE", 5, 1, 0));
        lasers.Add(new list_lasers(4, "WHITE", 5, 2, 0));
        lasers.Add(new list_lasers(5, "PURPLE", 5, 2, 0));
        lasers.Add(new list_lasers(6, "WHITE-RED", 5, 2, 0));
        lasers.Add(new list_lasers(7, "GREEN-DARK GREEN", 5, 3, 0));
        Check_buttons();
        View_Lasers();
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
    private void View_Lasers()
    { //wyswietl lasery zgodne z poziomem
        for (int ilosc = 0; ilosc < laserss.Count(); ilosc++)
        {
            laserss[ilosc].SetActive(GUIPlanetOperations.Check_Levels("Laser_Technology", ilosc));
        }
    }
    private void View_Available_Laser(int nr, string text, Color color)
    {
        text_button[nr].text = text;
        text_button[nr].color = color;
    }
    private void Check_buttons() // zmienia tylko nazwy w tekscie
    {
        for (int ilosc = 0; ilosc < lasers.Count(); ilosc++)
        {
            if (lasers[ilosc].haveornothave == 1)
            {
                View_Available_Laser(ilosc, "USING", new Color(.105f, .375f, .105f, 255f));
            }
            else if (lasers[ilosc].haveornothave == 0 && staty.Get_Data_From("Antymatery") >= lasers[ilosc].price)
            { 
                View_Available_Laser(ilosc, "CHANGE", new Color(255f, 255f, 255f, 255f));
            }
            else if (lasers[ilosc].haveornothave == 0 && staty.Get_Data_From("Antymatery") < lasers[ilosc].price)
            { 
                View_Available_Laser(ilosc, "EARN", new Color(255f, 255f, 255f, 255f));
            }
        }
    }
	public void BuyLasers(int nr)
    {
        if (lasers[nr].haveornothave == 0 && staty.Get_Data_From("Antymatery") >= lasers[nr].price)
        {//zmien
            lasers[nr].haveornothave = 1;
            staty.Change_Antymatery(-lasers[nr].price);
            staty.Set_Data("Laser", lasers[nr].id);
            PlayerPrefs.Save();
            textDebugLasers.text = "BOUGHT: " + lasers[nr].name;

        }
        else if (lasers[nr].haveornothave == 0 && staty.Get_Data_From("Antymatery") < lasers[nr].price)
        {
            if (Ads.pokazane == false)
            {
                Ads.Show_to_earn("antymatery");
                Ads.pokazane = false;
            }
            textDebugLasers.text = "EARN: " + lasers[nr].name;
        }
    }
    // Update is called once per frame
    private void LateUpdate () {
        Check_buttons();
        Check_laser();
        View_Lasers();
    }
}
