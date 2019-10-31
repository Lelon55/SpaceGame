using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GUIShips : MonoBehaviour {

    internal class ListShips
    {
        public int id, life, ch_drop, price, speed_ship;
        public bool haveornothave;
        public string name, description;
        public double chance_drop; //szansa dropu anty z komet
		public float consumption, max_lasers;

        public ListShips(int i, string n, string de, int l, double d, int di, int p, float c, int s_s, float m_l, bool hoh)
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

    internal List<ListShips> ships = new List<ListShips>();
    public Sprite[] SpriteShips;
    public GameObject[] go_ships;
    public Text[] text_button;
    private statystyki staty;
    private GUIPlanetOperations GUIPlanetOperations;

    private void Start()
    {
        staty = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIPlanetOperations = GameObject.Find("Interface").GetComponent<GUIPlanetOperations>();
        ships.Add(new ListShips(0, "Light Hunter", "Life: 1 \nChance drop antymatery: 5% \nConsumption: 5 deuter \nSteer: 9 \nMax lasers: 3", 1, 0.05, 5, 10, 5.0f, 9, 3.0f, true)); // (id, nazwa, opis, zycie, sznasadropu w double, szansadropu w int, koszt, posiadanie)
        ships.Add(new ListShips(1, "Light Hunter", "Life: 1 \nChance drop antymatery: 5% \nConsumption: 5 deuter \nSteer: 9 \nMax lasers: 3", 1, 0.05, 5, 10, 5.0f, 9, 3.0f, false));
        ships.Add(new ListShips(2, "Light Hunter", "Life: 1 \nChance drop antymatery: 5% \nConsumption: 5 deuter \nSteer: 9 \nMax lasers: 3", 1, 0.05, 5, 10, 5.0f, 9, 3.0f, false));
        ships.Add(new ListShips(3, "Heavy Hunter", "Life: 2 \nChance drop antymatery: 10% \nConsumption: 10 deuter \nSteer: 11 \nMax lasers: 4", 2, 0.10, 10, 25, 10f, 11, 4.0f, false));
        ships.Add(new ListShips(4, "Heavy Hunter", "Life: 2 \nChance drop antymatery: 10% \nConsumption: 10 deuter \nSteer: 11 \nMax lasers: 4", 2, 0.10, 10, 25, 10f, 11, 4.0f, false));
        ships.Add(new ListShips(5, "Heavy Hunter", "Life: 2 \nChance drop antymatery: 10% \nConsumption: 10 deuter \nSteer: 11 \nMax lasers: 4", 2, 0.10, 10, 25, 10f, 11, 4.0f, false));
        ships.Add(new ListShips(6, "Crusher", "Life: 3 \nChance drop antymatery: 15% \nConsumption: 15 deuter \nSteer: 14 \nMax lasers: 5", 3, 0.15, 15, 50, 15f, 14, 5.0f, false));
        ships.Add(new ListShips(7, "Crusher", "Life: 3 \nChance drop antymatery: 15% \nConsumption: 15 deuter \nSteer: 14 \nMax lasers: 5", 3, 0.15, 15, 50, 15f, 14, 5.0f, false));
        ships.Add(new ListShips(8, "Crusher", "Life: 3 \nChance drop antymatery: 15% \nConsumption: 15 deuter \nSteer: 14 \nMax lasers: 5", 3, 0.15, 15, 50, 15f, 14, 5.0f, false));
        ships.Add(new ListShips(9, "Balcon Triple Heavy", "Life: 1 \nChance drop antymatery: 5% \nConsumption: 5 deuter \nSteer: 10 \nMax lasers: 3", 1, 0.05, 5, 15, 5f, 10, 3.0f, false));
    }

    private void CheckShip()
    {
        for (int nr = 0; nr < ships.Count(); nr++)
        {
            ships[nr].haveornothave = GUIPlanetOperations.Check_HasItem(ships[nr].id, staty.Get_Data_From("Ship_Id"));
            GUIPlanetOperations.Check_buttons(ships[nr].haveornothave, text_button, nr, staty.Get_Data_From("Antymatery"), ships[nr].price);
        }
    }

    public void BuyShips(int nr)
    {
        if (!ships[nr].haveornothave)
        {
            if (staty.Get_Data_From("Antymatery") >= ships[nr].price)
            {
                ships[nr].haveornothave = true;
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
                GUIPlanetOperations.Subject_Information(0, 0, 0, ships[nr].price, ships[nr].name, "Bought!", SpriteShips[nr]);
            }
            else if (staty.Get_Data_From("Antymatery") < ships[nr].price)
            {
                GUIPlanetOperations.Turn_On_Ads("antymatery");
                GUIPlanetOperations.Subject_Information(0, 0, 0, ships[nr].price, ships[nr].name, "Too Small Antymatery", SpriteShips[nr]);
            }
        }
    }

    private void LateUpdate()
    {
        CheckShip();
        GUIPlanetOperations.View_Subject(go_ships, "Hangar");
    }

    public void Info_ships(int nr)
    {
        GUIPlanetOperations.Subject_Information(0, 0, 0, ships[nr].price, ships[nr].name, ships[nr].description, SpriteShips[nr]);
    }
}
