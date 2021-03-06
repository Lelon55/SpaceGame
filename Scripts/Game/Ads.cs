﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class Ads : MonoBehaviour 
{
    private const string ID = "3283559";
    private const string ads = "rewardedVideo";
    private string type_reward = "resources";
    internal bool pokazane;

	public statystyki staty;

	private void Start()
	{
		Advertisement.Initialize (ID, true);

        StartCoroutine(ShowBannerWhenReady());
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_LEFT);
    }

    private IEnumerator ShowBannerWhenReady()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            while (!Advertisement.IsReady("banner"))
            {
                yield return new WaitForSeconds(0.5f);
            }
            Advertisement.Banner.Show("banner");
        }
        else
        {
            Advertisement.Banner.Hide();
        }
    }

    public void Show_to_earn(string t_reward)
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = Status;
        type_reward = t_reward;
        if (Advertisement.IsReady(ads))
        {
            Advertisement.Show(ads, options);
            pokazane = true;
        }
    }

    private void Status(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Earn(type_reward);
                pokazane = false;
                Debug.Log("Ok. Monetyzacja w toku");
                break;
            case ShowResult.Skipped:
                pokazane = false;
                Debug.Log("Pominięto");
                break;
            case ShowResult.Failed:
                pokazane = false;
                Debug.Log("Nie odtworzono");
                break;
        }
    }

    private void Earn(string t_reward)
    {
        switch (t_reward)
        {
            case "resources":
                staty.Set_Data("Metal", staty.Get_Data_From("Metal") + Random.Range(10, 15 + (staty.Get_Points() * 2)));
                staty.Set_Data("Crystal", staty.Get_Data_From("Crystal") + Random.Range(10, 15 + (staty.Get_Points() * 2)));
                staty.Set_Data("Deuter", staty.Get_Data_From("Deuter") + Random.Range(10, 15 + (staty.Get_Points() * 2)));
                PlayerPrefs.Save();
                break;
            case "antymatery":
                staty.Change_Antymatery(Random.Range(3, 10 + staty.more_antymateries));
                PlayerPrefs.Save();
                break;
            case "game_over":
                Debug.Log("Przegrales");
                break;
        }
    }
}
