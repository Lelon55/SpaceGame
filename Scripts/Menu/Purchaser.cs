﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class Purchaser : MonoBehaviour
{
    private GUIOverview GUIOverview;
    private statystyki stats;

    private void Start()
    {
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIOverview = GameObject.Find("Scripts").GetComponent<GUIOverview>();
    }

    private void Collect(int value)
    {
        GUIOverview.View_CanvasMessage("You've bought " + value + " Antymateries.");
        stats.Change_Antymatery(value);
    }

    public void OnPurchasedCompleted(Product product)
    {
        if (product != null)
        {
            switch (product.definition.id)
            {
                case "1":
                    Collect(50);
                    break;
                case "2":
                    Collect(250);
                    break;
                case "3":
                    Collect(1000);
                    break;
                case "4":
                    Collect(10000);
                    break;
                default:
                    Debug.Log("test");
                    break;
            }
        }
    }

}
