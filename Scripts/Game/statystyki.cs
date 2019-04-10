﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;

public class statystyki : MonoBehaviour {

    internal int Life;
    internal int ticks;
    internal int mission;

    internal float gravity_bullet = -1.0f;
    public int ilosc_wczytanych_scen = 0;

    private Rigidbody2D gravity_ship;
    internal ControlShip ControlShip;

    #region Gameplay Variables
    private int Dropped_Metal;
    private int Dropped_Crystal;
    private int Dropped_Deuter;
    private int Dropped_Antymatery;
    private int Comets;
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
        Bonus_from_research();
        StartCoroutine("Add_Resources");
        ticks = Get_Data_From("ticks");
        Life = Get_Life();
        New_game();

        if (SceneManager.GetActiveScene().name != "Planet")
        {
            gravity_ship = GetComponent<Rigidbody2D>();
            ControlShip = GetComponent<ControlShip>();
            ControlShip.speed.x = Get_Data_From("Speed_Ship");
            //zmiana rozmiaru koliderow
            if (Get_String_Data_From("Ship_Name") != "Balcon Triple Heavy")
            {
                GetComponent<BoxCollider2D>().size = new Vector2(3f, 2.3f);
            }
            else
            {
                GetComponent<BoxCollider2D>().size = new Vector2(1.5f, 2f);
            }
        }
    }
    private void New_game()
    {//nowa gra lub pierwsze odpalenie w zaleznosci jak kto rozumie
        if (Get_Data_From("Ship_Id") <= 0)
        { //przed kupnem bierze 1 statek ze statami ze sklepu //mozna podciagnac to co gracz otrzymuje na starcie gry
            PlayerPrefs.SetInt("Ship_Id", 1); //statek
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
            PlayerPrefs.SetInt("Collected_Antymatery", 0);
            PlayerPrefs.SetString("Planet_Name", "set planet name");
            PlayerPrefs.SetString("Admiral_Name", "set admiral name");
            PlayerPrefs.SetString("Alliance_Name", "no alliance");
            PlayerPrefs.SetString("Alliance_Tag", "no tag");
            PlayerPrefs.SetString("message_on_start", "false");
            PlayerPrefs.SetString("sound_option", "false");
            PlayerPrefs.SetInt("first_tutorial", 0); //jesli 0 tzn, ze nie wlaczono tutorialu
        }
        if (Get_Data_From("Laser") <= 0)
        { //przed kupnem bierze 1 laser
            PlayerPrefs.SetInt("Laser", 1);
        }
    }

    #region Gameplay methods
    internal int Get_Score()
    {
        return Get_Distance() + Get_Comets();
    }
    internal int Get_Distance()
    {
        return (int)transform.position.y;
    }
    internal int Get_Comets()
    {
        return Comets;
    }
    internal void Add_Comets(int value_comets)
    {
        Comets += value_comets;
    }
    internal int Get_Dropped_Metal()
    {
        return Dropped_Metal;
    }
    internal void Add_Dropped_Metal(int value)
    {
        Dropped_Metal += value;
    }
    internal int Get_Dropped_Crystal()
    {
        return Dropped_Crystal;
    }
    internal void Add_Dropped_Crystal(int value)
    {
        Dropped_Crystal += value;
    }
    internal int Get_Dropped_Deuter()
    {
        return Dropped_Deuter;
    }
    internal void Add_Dropped_Deuter(int value)
    {
        Dropped_Deuter += value;
    }
    internal int Get_Dropped_Antymatery()
    {
        return Dropped_Antymatery;
    }
    internal void Add_Dropped_Antymatery(int value)
    {
        Dropped_Antymatery += value;
    }
    internal void Change_Antymatery(int value_antymatery)
    {
        PlayerPrefs.SetInt("Antymatery", Get_Data_From("Antymatery") + value_antymatery);
    }
    private void Check_Resources(string name, string capacity, string income)
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
    private void Check_Records(int value, string name_record)
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
        return Get_Data_From("Ch_Drop") + Get_Data_From("Antymatery_Technology");
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

    private void Properties_ship()
    {
        if (Get_Distance() >= 150)
        {
            gravity_ship.gravityScale = -15f;
            gravity_bullet = -2f;
        }
        else if (Get_Distance() >= 500)
        {
            gravity_ship.gravityScale = -20f;
            gravity_bullet = -2.5f;
        }
        else if (Get_Distance() < 150)
        {
            gravity_ship.gravityScale = -10f;
            gravity_bullet = -1.0f;
        }

        if (Get_Data_From("Life") == 1 && (Get_Data_From("Ship_Id") >= 2 && Get_Data_From("Ship_Id") <= 9))
        {
            ControlShip.speed.x = 7;
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "Planet")
        {
            if (SceneManager.GetActiveScene().name == "Game")
            {
                Check_Records(Get_Score(), "Player_Record");
                Check_Records(Get_Comets(), "Comets_Record");
            }
            Properties_ship();
        }
    }
    private IEnumerator Add_Resources()
    {
        yield return new WaitForSeconds(1.0f);
        ticks += 1;

        if (ticks >= 31)
        {//default 31
            Check_Resources("Metal", "Capacity_Metal", "Income_Metal");
            Check_Resources("Crystal", "Capacity_Crystal", "Income_Crystal");
            Check_Resources("Deuter", "Capacity_Deuter", "Income_Deuter");
            ticks = 1;
        }
        StopCoroutine("Add_Resources");
        StartCoroutine("Add_Resources");
    }
    private int Set_Bonus(string name)
    {
        if (Get_Data_From(name) == 3 || Get_Float_Data_From(name) == 3f)
        {
            return 1;
        }
        return 0;
    }
    private void Bonus_from_research()
    {
        immortal = Set_Bonus("Shield");
        free_exploration = Set_Bonus("Combustion");
        more_damage = Set_Bonus("Laser_Technology");
        more_resource = Set_Bonus("Mining_Technology");
        more_antymateries = Set_Bonus("Antymatery_Technology");
    }
}