using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;

public class statystyki : MonoBehaviour {

    internal int Life;
    internal int ticks;
    internal int mission;
    public int LoadedScene = 0;

    #region Gameplay Variables
    private int droppedMetal;
    private int droppedCrystal;
    private int droppedDeuter;
    private int droppedAntymatery;
    private int comets;
    #endregion
    #region Bonus Variables
    internal int immortal = 0;
    internal int free_exploration = 0;
    internal int more_damage = 0;
    internal int more_resource = 0;
    internal int more_antymateries = 0;
    #endregion

    private void Start()
    {
        Time.timeScale = 1;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        BonusFromResearch();
        StartCoroutine("Add_Resources");
        ticks = Get_Data_From("ticks");
        Life = Get_Life();
        NewGame();
    }

    private void NewGame()
    {
        if (Get_Data_From("New_game") <= 0)
        {
            PlayerPrefs.SetInt("Ship_Id", 0);
            PlayerPrefs.SetInt("Laser", 0);
            PlayerPrefs.SetString("Ship_Name", "Light Hunter");
            PlayerPrefs.SetInt("Life", 1);
            PlayerPrefs.SetInt("Ch_Drop", 5);
            PlayerPrefs.SetFloat("Consumption", 5.0f);
            PlayerPrefs.SetInt("Speed_Ship", 9);
            PlayerPrefs.SetFloat("Max_Lasers", 3.0f);
            PlayerPrefs.SetInt("Metal", 100);
            PlayerPrefs.SetInt("Crystal", 100);
            PlayerPrefs.SetInt("Deuter", 100);
            PlayerPrefs.SetInt("Spent_Resources", 0);
            PlayerPrefs.SetInt("Antymatery", 1);
            PlayerPrefs.SetInt("ticks", 1);
            PlayerPrefs.SetInt("Capacity_Metal", 250);
            PlayerPrefs.SetInt("Capacity_Crystal", 250);
            PlayerPrefs.SetInt("Capacity_Deuter", 250);
            PlayerPrefs.SetInt("Free_Field", 30);
            PlayerPrefs.SetInt("Quest", 0);
            PlayerPrefs.SetInt("Exp", 0);
            PlayerPrefs.SetInt("Level", 1);
            PlayerPrefs.SetInt("Wins", 0);
            PlayerPrefs.SetInt("Loses", 0);
            PlayerPrefs.SetString("Admiral_Avatar", "http://www.owiki.de/images/2/28/Flottenadmiral.PNG");
            PlayerPrefs.SetInt("Collected_Antymatery", 0);
            PlayerPrefs.SetString("Planet_Name", "set planet name");
            PlayerPrefs.SetString("Admiral_Name", "set admiral name");
            PlayerPrefs.SetString("Alliance_Name", "no alliance");
            PlayerPrefs.SetString("Alliance_Tag", "no tag");
            PlayerPrefs.SetInt("MemberID", 0);
            PlayerPrefs.SetInt("Alliance_Antymatery", 0);
            PlayerPrefs.SetString("message_on_start", "false");
            PlayerPrefs.SetString("sound_option", "false");
            PlayerPrefs.SetInt("first_tutorial", 0); //jesli 0 tzn, ze nie wlaczono tutorialu
            PlayerPrefs.SetInt("New_game", 1);
            PlayerPrefs.GetString("Alliance_Avatar", "http://www.owiki.de/images/2/28/Flottenadmiral.PNG");
        }
    }

    #region Gameplay methods
    internal int Get_Distance()
    {
        return (int)transform.position.y;
    }
    internal int Get_Comets()
    {
        return comets;
    }
    internal int Get_Score()
    {
        return Get_Distance() + Get_Comets();
    }
    internal void Add_Comets(int value)
    {
        comets += value;
    }
    internal int Get_Dropped_Metal()
    {
        return droppedMetal;
    }
    internal void Add_Dropped_Metal(int value)
    {
        droppedMetal += value;
    }
    internal int Get_Dropped_Crystal()
    {
        return droppedCrystal;
    }
    internal void Add_Dropped_Crystal(int value)
    {
        droppedCrystal += value;
    }
    internal int Get_Dropped_Deuter()
    {
        return droppedDeuter;
    }
    internal void Add_Dropped_Deuter(int value)
    {
        droppedDeuter += value;
    }
    internal int Get_Dropped_Antymatery()
    {
        return droppedAntymatery;
    }
    internal void Add_Dropped_Antymatery(int value)
    {
        droppedAntymatery += value;
    }
    internal void Change_Antymatery(int value_antymatery)
    {
        PlayerPrefs.SetInt("Antymatery", Get_Data_From("Antymatery") + value_antymatery);
    }

    internal void AddDroppedResources(int metal, int crystal, int deuter, string whereData)
    {
        Add_Dropped_Metal(metal);
        Add_Dropped_Crystal(crystal);
        Add_Dropped_Deuter(deuter);
        Set_Data(whereData, Get_Data_From(whereData) + 1);
    }

    internal void AddDroppedResources(int metal, int crystal, int deuter)
    {
        Add_Dropped_Metal(metal);
        Add_Dropped_Crystal(crystal);
        Add_Dropped_Deuter(deuter);
    }
    private void SetResources(string name, string capacity, string income)
    {
        if (Get_Data_From(name) < Get_Data_From(capacity))
        {
            Set_Data(name, Get_Data_From(name) + Get_Income(income));
        }
    }
    internal int Get_Points()
    {
        return Get_Data_From("Spent_Resources") / 1000; //1point = 1000 spent resources
    }
    internal int Get_Income(string name)
    {
        return 1 + Get_Data_From(name);
    }
    private void CheckRecords(int value, string name_record)
    {
        if (PlayerPrefs.GetInt(name_record) <= value)
        {
            PlayerPrefs.SetInt(name_record, value);
        }
    }
    #endregion
    #region Get Datas
    internal int Get_Data_From(string name)
    {
        return PlayerPrefs.GetInt(name);
    }
    internal string Get_String_Data_From(string name)
    {
        return PlayerPrefs.GetString(name);
    }
    internal float Get_Float_Data_From(string name)
    {
        return PlayerPrefs.GetFloat(name);
    }
    internal void Set_Data(string name, int value)
    {
        PlayerPrefs.SetInt(name, value);
    }
    internal void Set_String_Data(string name, string value)
    {
        PlayerPrefs.SetString(name, value);
    }
    internal void Set_Float_Data(string name, float value)
    {
        PlayerPrefs.SetFloat(name, value);
    }

    internal int Count_Exp_To_Next_Level()
    {
        return 15 * (Get_Data_From("Level") + 1);
    }
    internal int Get_Chance_Drop()
    {
        return Get_Data_From("Ch_Drop") + Get_Data_From("Antymatery Technology");
    }
    internal int Get_Life()
    {
        return Get_Data_From("Life") + Get_Data_From("Shield");
    }
    internal float Get_Consumption()
    {
        return Get_Float_Data_From("Consumption") + Get_Float_Data_From("Combustion");
    }
    #endregion

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            CheckRecords(Get_Score(), "Player_Record");
            CheckRecords(Get_Comets(), "Comets_Record");
        }
    }

    private IEnumerator Add_Resources()
    {
        yield return new WaitForSeconds(1.0f);
        ticks += 1;

        if (ticks >= 31)
        {//default 31
            SetResources("Metal", "Capacity_Metal", "Income_Metal");
            SetResources("Crystal", "Capacity_Crystal", "Income_Crystal");
            SetResources("Deuter", "Capacity_Deuter", "Income_Deuter");
            ticks = 1;
        }
        StopCoroutine("Add_Resources");
        StartCoroutine("Add_Resources");
    }

    private int SetBonus(string name)
    {
        if (Get_Data_From(name) == 3 || Get_Float_Data_From(name) == 3f)
        {
            return 1;
        }
        return 0;
    }

    private void BonusFromResearch()
    {
        immortal = SetBonus("Shield");
        free_exploration = SetBonus("Combustion");
        more_damage = SetBonus("Laser Technology");
        more_resource = SetBonus("Mining Technology");
        more_antymateries = SetBonus("Antymatery Technology");
    }

    internal int LoadLuckyForConsumption()
    {
        if (free_exploration == 1)
        {
            return Random.Range(1, 100);
        }
        return 0;
    }

    internal int LuckyConsumption()
    {
        if (LoadLuckyForConsumption() < 90)
        {
            return (int)Get_Consumption();
        }
        return 0;
    }

    internal void ShowCurrentlyHealthBar(SpriteRenderer healthBar, float size)
    {
        healthBar.transform.localScale = new Vector2(size, 0.2f);
    }

    internal int GetDamage()
    {
        if (more_damage == 1)
        {
            return 2;
        }
        return 1;
    }

    internal int AddToMaxDropResources()
    {
        if (more_resource == 1)
        {
            return 5;
        }
        return 0;
    }
}