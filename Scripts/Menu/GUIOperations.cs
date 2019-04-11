using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GUIOperations : MonoBehaviour {

    private GUIOverview GUIOverview;
    [SerializeField] internal GameObject RewardLvlUp;

    private void Start()
    {
        GUIOverview = GameObject.Find("Interface").GetComponent<GUIOverview>();
    }

    public void BtnClose(Canvas Canvas)
    {
        if (GUIOverview.page > 0)
        {
            Canvas.enabled = false;
            RewardLvlUp.SetActive(false);
        }
    }
    public void BtnOpen(Canvas Canvas)//open pop up canvas at planet
    {
        Canvas.enabled = true;
    }

    internal Color Set_Color(float value1, float value2)
    {
        //checking which number is greater
        if (value1 >= value2)
        {
            return new Color(255f, 0f, 0f, 255f);
        }
        return new Color(255f, 255f, 255f, 255f);
    }

    internal bool Open_Canvas(int nr, int page) //open canvas which is correct with currently page
    {
        return nr == page; //if correct return true if not correct return false
    }


    

}
