using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIOperations : MonoBehaviour {

    private GUIOverview GUIOverview;
    [SerializeField] internal GameObject RewardLvlUp;
    internal bool connection;
    [SerializeField] private Canvas ConnectionInternet;

    private void Start()
    {
        GUIOverview = GameObject.Find("Interface").GetComponent<GUIOverview>();
    }
    #region Canvas
    public void BtnClose(Canvas Canvas)
    {
        if (SceneManager.GetActiveScene().name == "Planet")
        {
            if (GUIOverview.page > 0)
            {
                Canvas.enabled = false;
                RewardLvlUp.SetActive(false);
            }
        }
        else
        {
            Canvas.enabled = false;
        }
    }

    public void BtnOpen(Canvas Canvas)//open pop up canvas at planet
    {
        Canvas.enabled = true;
    }

    internal bool Open_Canvas(int page, int nr) //open canvas which is correct with currently page
    {
        return page == nr; //if correct return true if not correct return false
    }

    internal void Steer_Canvas(Canvas[] Canvases, int page) //open canvas which is correct with currently page
    {
        for (int ilosc = 0; ilosc < Canvases.Length; ilosc++)
        {
            Canvases[ilosc].enabled = Open_Canvas(page, ilosc);
        }
    }

    public void RefreshScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion

    #region InternetConnection
    public void CheckInternetConnection()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            ConnectionInternet.enabled = true;
            connection = false;
        }
        else
        {
            connection = true;
        }
    }
    #endregion

    #region Game
    internal void Generate(float pos_x, float pos_y, Quaternion rotation, GameObject gameobject)
    {
        Vector2 vector = new Vector2(pos_x, pos_y);
        Instantiate(gameobject, vector, rotation);
    }
    #endregion
}
