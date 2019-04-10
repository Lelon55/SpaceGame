﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class Purchaser : MonoBehaviour
{
    private GUIOverview GUIOverview;
    private statystyki stats;

    private void Start()
    {
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIOverview = GameObject.Find("Scripts").GetComponent<GUIOverview>();
    }
    private void Set_Options(int value)
    {
        GUIOverview.CanvasMessage.enabled = true;
        GUIOverview.textMessage.text = "You've bought " + value + " Antymateries.";
        stats.Change_Antymatery(value);
        GUIOverview.audiosource_sound_message.PlayOneShot(GUIOverview.sound_message, 0.7F);
    }
    public void OnPurchasedCompleted(Product product)
    {
        if(product != null)
        {
            switch (product.definition.id)
            {
                case "1":
                    Set_Options(50);
                    break;
                case "2":
                    Set_Options(250);
                    break;
                case "3":
                    Set_Options(1000);
                    break;
                case "4":
                    Set_Options(10000);
                    break;
                default:
                    Debug.Log("test");
                    break;

            }
        }
    }

}