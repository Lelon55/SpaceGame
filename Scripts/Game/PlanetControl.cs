using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetControl : MonoBehaviour {

    public GameObject Earth;
    private float rotation_y;

    private GUIOverview GUIOverview;

    private void Start()
    {
        GUIOverview = GameObject.Find("Interface").GetComponent<GUIOverview>();
    }

    private void MovePlanet()
    {
        if (GUIOverview.page == 1)
        {
            Earth.SetActive(true);
            rotation_y += Time.deltaTime * 60;
            Earth.transform.rotation = Quaternion.Euler(0, Time.deltaTime * 60, -30f);
        }
        else
        {
            Earth.SetActive(false);
        }
    }

    private void Update()
    {
        MovePlanet();
    }
}
