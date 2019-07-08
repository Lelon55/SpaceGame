using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllianceScout : MonoBehaviour {

    private statystyki stats;
    [SerializeField] private AllianceStats AllianceStats;
    [SerializeField] private GUIOverview GUIOverview;

    [SerializeField] private GameObject propositions_list;
    [SerializeField] private Sprite[] Sprite_Ships;
    [SerializeField] private Image Sprite_MemberShip;
    [SerializeField] private Text TxtDescription, TxtMemberID, TxtPoint;
    [SerializeField] private Button BtnSearch, BtnMemberOption;
    [SerializeField] private int page, cost; // 0 = przywitanie, 1 szuka, 2 wyniki wyszukania

    public void Search()
    {
        if (stats.Get_Data_From("Antymatery") >= cost)
        {
            if (AllianceStats.CompareMemberToLength())//if true do it 
            {
                page = 1;
                //SetMemberID(); //tylko przy dodawaniu do listy
                FoundThem();
            }
            else
            {
                GUIOverview.View_CanvasMessage("Not enough space for new member!");
            }
        }
        else
        {
            GUIOverview.View_CanvasMessage("Not enough antymatery!");
        }

    }

    public void BtnLeaveIt()
    {
        page = 0;
    }

    private void SetMemberID()
    {
        stats.Set_Data("MemberID", stats.Get_Data_From("MemberID") + 1);
    }

    private void GetMemberID()
    {
        TxtMemberID.text = "ID: " +stats.Get_Data_From("MemberID");
    }

    private void GetPoint()
    {
        switch (stats.Get_Data_From("Scout"))
        {
            case 1:
                TxtPoint.text = Random.Range(1, 10).ToString();
                break;
            case 2:
                TxtPoint.text = Random.Range(1, 50).ToString();
                break;
            case 3:
                TxtPoint.text = Random.Range(1, 100).ToString();
                break;
        }
    }

    private void GetSprite_MemberShip()
    {
        Sprite_MemberShip.sprite = Sprite_Ships[Random.Range(0, 9)];
    }

    private string Get_AdmiralName()
    {
        return stats.Get_String_Data_From("Admiral_Name");
    }

    private void FoundThem()
    {
        GetSprite_MemberShip();
        GetMemberID();
        GetPoint();
    }

    private void Work()
    {
        if (page == 0)
        {
            TxtDescription.text = "Hi " + Get_AdmiralName() + "!\nI am a scout. I help in finding members for the alliance. Are you interested in this?";
            BtnSearch.enabled = true;
            propositions_list.SetActive(false);
        }
        else if (page == 1)
        {
            TxtDescription.text = "I found them!";
            BtnSearch.enabled = false;
            propositions_list.SetActive(true);
        }
    }

	private void Start () {
        page = 0;
        cost = 1;
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        Work();
    }
	
	private void LateUpdate () {
        Work();
	}
}
