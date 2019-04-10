using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GUIShips : MonoBehaviour {

    private class List_ships
    {
        public int id, life, ch_drop, price, speed_ship, haveornothave;
        public string name, description;
        public double chance_drop; //szansa dropu anty z komet
		public float consumption, max_lasers;

        public List_ships(int i, string n, string de, int l, double d, int di, int p, float c, int s_s, float m_l, int hoh)
        {
            this.id = i;
            this.name = n;
            this.description = de;
            this.life = l;
            this.chance_drop = d;
            this.ch_drop = di;
            this.price = p;
			this.consumption = c;
            this.speed_ship = s_s;
            this.max_lasers = m_l;
            this.haveornothave = hoh;
        }
    }
    private List<List_ships> ships = new List<List_ships>();

    public Sprite[] SpriteShips;
    public Button[] buttony;
    public GameObject[] go_ships;
    public Text[] text_button;
    public Text textDebugShips;
    
    private statystyki staty;
    private Ads Ads;
    private GUIPlanetOperations GUIPlanetOperations;

    // Use this for initialization
    private void Start()
    {
        staty = GameObject.Find("Scripts").GetComponent<statystyki>();
        Ads = GameObject.Find("Scripts").GetComponent<Ads>();
        GUIPlanetOperations = GameObject.Find("Interface").GetComponent<GUIPlanetOperations>();
        ships.Add(new List_ships(1, "Light Hunter", "Life: 1 \nChance drop antymatery: 5% \nConsumption: 5 deuter \nSteer: 9 \nMax lasers: 3", 1, 0.05, 5, 10, 5.0f, 9, 3.0f, 1)); // (id, nazwa, opis, zycie, sznasadropu w double, szansadropu w int, koszt, posiadanie)
        ships.Add(new List_ships(2, "Light Hunter", "Life: 1 \nChance drop antymatery: 5% \nConsumption: 5 deuter \nSteer: 9 \nMax lasers: 3", 1, 0.05, 5, 10, 5.0f, 9, 3.0f, 0));
        ships.Add(new List_ships(3, "Light Hunter", "Life: 1 \nChance drop antymatery: 5% \nConsumption: 5 deuter \nSteer: 9 \nMax lasers: 3", 1, 0.05, 5, 10, 5.0f, 9, 3.0f, 0));
        ships.Add(new List_ships(4, "Heavy Hunter", "Life: 2 \nChance drop antymatery: 10% \nConsumption: 10 deuter \nSteer: 11 \nMax lasers: 4", 2, 0.10, 10, 20, 10f, 11, 4.0f, 0));
        ships.Add(new List_ships(5, "Heavy Hunter", "Life: 2 \nChance drop antymatery: 10% \nConsumption: 10 deuter \nSteer: 11 \nMax lasers: 4", 2, 0.10, 10, 20, 10f, 11, 4.0f, 0));
        ships.Add(new List_ships(6, "Heavy Hunter", "Life: 2 \nChance drop antymatery: 10% \nConsumption: 10 deuter \nSteer: 11 \nMax lasers: 4", 2, 0.10, 10, 20, 10f, 11, 4.0f, 0));
        ships.Add(new List_ships(7, "Crusher", "Life: 3 \nChance drop antymatery: 15% \nConsumption: 15 deuter \nSteer: 14 \nMax lasers: 5", 3, 0.15, 15, 30, 15f, 14, 5.0f, 0));
        ships.Add(new List_ships(8, "Crusher", "Life: 3 \nChance drop antymatery: 15% \nConsumption: 15 deuter \nSteer: 14 \nMax lasers: 5", 3, 0.15, 15, 30, 15f, 14, 5.0f, 0));
        ships.Add(new List_ships(9, "Crusher", "Life: 3 \nChance drop antymatery: 15% \nConsumption: 15 deuter \nSteer: 14 \nMax lasers: 5", 3, 0.15, 15, 30, 15f, 14, 5.0f, 0));
        ships.Add(new List_ships(10, "Balcon Triple Heavy", "Life: 1 \nChance drop antymatery: 5% \nConsumption: 5 deuter \nSteer: 9 \nMax lasers: 3", 1, 0.05, 5, 10, 5f, 9, 3.0f, 0));
        Check_ship();
        View_Ships();
    }

    private void Check_ship()
    {
        for (int ilosc = 0; ilosc < ships.Count(); ilosc++)
        {
            if (ships[ilosc].id == staty.Get_Data_From("Ship_Id"))
            {
                ships[ilosc].haveornothave = 1;

            }
            else if (ships[ilosc].id != staty.Get_Data_From("Ship_Id"))
            {
                ships[ilosc].haveornothave = 0;
            }
        }
    }
    private void View_Ships()
    {
        for (int ilosc = 0; ilosc < go_ships.Count(); ilosc++)
        {
            go_ships[ilosc].SetActive(GUIPlanetOperations.Check_Levels("Hangar", ilosc));
        }
    }
    private void View_Available_Ship(int nr, string text, Color color)
    {
        text_button[nr].text = text;
        text_button[nr].color = color;
    }
    private void Check_buttons()
    {
        for (int ilosc = 0; ilosc < ships.Count(); ilosc++)
        {
            if (ships[ilosc].haveornothave == 1)
            {
                View_Available_Ship(ilosc, "USING", new Color(.105f, .375f, .105f, 255f));
            }
            else if (ships[ilosc].haveornothave == 0 && staty.Get_Data_From("Antymatery") >= ships[ilosc].price)
            {
                View_Available_Ship(ilosc, "CHANGE", new Color(255f, 255f, 255f, 255f));
            }
            else if (ships[ilosc].haveornothave == 0 && staty.Get_Data_From("Antymatery") < ships[ilosc].price)
            {
                View_Available_Ship(ilosc, "EARN", new Color(255f, 255f, 255f, 255f));
            }
        }
    }
    public void BuyShips(int nr)
    {
        if (ships[nr].haveornothave == 0 && staty.Get_Data_From("Antymatery") >= ships[nr].price)
        {//zmien
            ships[nr].haveornothave = 1;
            staty.Change_Antymatery(-ships[nr].price);
            staty.Set_Data("Ship_Id", ships[nr].id);
            staty.Set_Data("Life", ships[nr].life);
            staty.Set_Data("Ch_Drop", ships[nr].ch_drop);
            staty.Set_Float_Data("Consumption", ships[nr].consumption);
            staty.Set_Data("Bought_Ships", staty.Get_Data_From("Bought_Ships") + 1);
            staty.Set_Data("Speed_Ship", ships[nr].speed_ship);
            staty.Set_Float_Data("Max_Lasers", ships[nr].max_lasers);
            staty.Set_String_Data("Ship_Name", ships[nr].name);
            PlayerPrefs.Save();
            textDebugShips.text = "BOUGHT: " + ships[nr].name;
        }
        else if (ships[nr].haveornothave == 0 && staty.Get_Data_From("Antymatery") < ships[nr].price)
        { //zarob po kliknieciu wywola reklame

            if (Ads.pokazane == false)
            {
                Ads.Show_to_earn("antymatery");
                Ads.pokazane = false;

            }
            textDebugShips.text = "EARN: " + ships[nr].name;
        }
    }

    // Update is called once per frame
    private void LateUpdate () {
        Check_buttons();
        Check_ship();
        View_Ships();
    }

    public void Info_ships(int nr)
    {
        GUIPlanetOperations.Subject_Information(0, 0, 0, ships[nr].price, ships[nr].name, ships[nr].description, SpriteShips[nr]);
    }
}
