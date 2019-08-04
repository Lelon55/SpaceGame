﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutProposition : MonoBehaviour {

    private statystyki stats;
    private GUIShips GUIShips;

    private int point;
    [SerializeField] private Sprite[] Sprite_Ships;

    private void Start()
    {
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIShips = GameObject.Find("Interface").GetComponent<GUIShips>();
    }

    internal int GetID()
    {
        return stats.Get_Data_From("MemberID");
    }

    internal string GetName(int id)
    {
        return GUIShips.ships[id].name;
    }

    internal string GetDescription(int id)
    {
        return "Point: " + GetPoint() + "\nLife: " + GetLife(id) + "\nSteer: " + GetSpeedShip(id) + "\nMax lasers: " + GetMaxLasers(id);
    }

    internal void SetPoint()
    {
        switch (stats.Get_Data_From("Scout"))
        {
            case 1:
                point = Random.Range(1, 5);
                break;
            case 2:
                point = Random.Range(1, 15);
                break;
            case 3:
                point = Random.Range(1, 30);
                break;
        }
    }

    internal int GetPoint()
    {
        return point;
    }

    internal int GetLife(int id)
    {
        return GUIShips.ships[id].life;
    }

    /*public double GetChanceDrop(int id)
    {
        return GUIShips.ships[id].chance_drop;
    }

    public int GetChDrop(int id)
    {
        return GUIShips.ships[id].ch_drop;
    }*/

    internal int GetSpeedShip(int id)
    {
        return GUIShips.ships[id].speed_ship;
    }

    internal float GetMaxLasers(int id)
    {
        return GUIShips.ships[id].max_lasers;
    }

    internal Sprite GetSpriteShip(int id)
    {
        return Sprite_Ships[id];
    }

    internal int GetShipsMaxRange()
    {
        switch (stats.Get_Data_From("Scout"))
        {
            case 1:
                return Random.Range(0, 2);
            case 2:
                return Random.Range(0, 5);
            case 3:
                return Random.Range(0, 9);
            default:
                return 0;
        }
    }
}
