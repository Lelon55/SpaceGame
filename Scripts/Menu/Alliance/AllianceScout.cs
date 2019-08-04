using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllianceScout : MonoBehaviour {

    private statystyki stats;
    private GUIPlanetOperations GUIPlanetOperations;
    private GUIOperations GUIoper;
    private ScoutProposition ScoutProposition;

    private int ID;

    [SerializeField] private AllianceStats AllianceStats;
    [SerializeField] private GUIOverview GUIOverview;

    [SerializeField] private GameObject PropositionsList;
    [SerializeField] private Image Sprite_MemberShip;
    [SerializeField] private Text TxtDescription;
    [SerializeField] private GameObject[] Buttons;
    [SerializeField] private int page;
    private const int cost = 1; // 0 = przywitanie, 1 szuka, 2 wyniki wyszukania

    private void Start()
    {
        page = 0;
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        ScoutProposition = GetComponent<ScoutProposition>();
        GUIoper = GameObject.Find("Interface").GetComponent<GUIOperations>();
        GUIPlanetOperations = GameObject.Find("Interface").GetComponent<GUIPlanetOperations>();
        Work();
    }

    private void RandomID(int id)
    {
        ID = id;
    }

    private void Cost()
    {
        stats.Set_Data("Alliance_Antymatery", stats.Get_Data_From("Alliance_Antymatery") - cost);
    }

    #region ActionForButtons
    public void Search()
    {
        if (stats.Get_Data_From("Alliance_Antymatery") >= cost)
        {
            if (AllianceStats.CompareMemberToLength())//if true do it 
            {
                page = 1;
                //Cost();
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

    public void InfoShips()
    {
        GUIPlanetOperations.Subject_Information(0, 0, 0, 0, ScoutProposition.GetName(ID), ScoutProposition.GetDescription(ID), ScoutProposition.GetSpriteShip(ID));
    }

    public void AddMember()
    {
        if (stats.Get_String_Data_From("GUID") != "")
        {
            if (AllianceStats.CompareMemberToLength())//if true do it 
            {
                //dodaje czlonka do bazy z zapisem guid gracza
            }
            else
            {
                GUIOverview.View_CanvasMessage("Not enough space for new member!");
            }
        }
        else
        {
            stats.Set_String_Data("GUID", System.Guid.NewGuid().ToString());
            GUIOverview.View_CanvasMessage("Registered account. Try again!");
        }
    }

    #endregion

    private string Get_AdmiralName()
    {
        return stats.Get_String_Data_From("Admiral_Name");
    }

    private void SetMemberID()
    {
        stats.Set_Data("MemberID", stats.Get_Data_From("MemberID") + 1);
    }

    private int GetMemberID()
    {
        return stats.Get_Data_From("MemberID");
    }

    private void GetSprite_MemberShip(int id)
    {
        Sprite_MemberShip.sprite = ScoutProposition.GetSpriteShip(id);
    }

    private void FoundThem()
    {
        RandomID(ScoutProposition.GetShipsMaxRange());
        FindShip(ID);
    }

    private void FindShip(int id)
    {
        ScoutProposition.GetName(id);
        ScoutProposition.SetPoint();
        ScoutProposition.GetPoint();
        ScoutProposition.GetLife(id);
        ScoutProposition.GetSpeedShip(id);
        ScoutProposition.GetMaxLasers(id);
        GetSprite_MemberShip(id);
    }

    private void Work()
    {
        if (page == 0)
        {
            TxtDescription.text = "Hi " + Get_AdmiralName() + "!\nI am a scout. I help in finding members for the alliance. Are you interested in this?";
            Buttons[0].SetActive(true);  //search
            Buttons[1].SetActive(false); //add
            Buttons[2].SetActive(false); //info
            PropositionsList.SetActive(false);
        }
        else if (page == 1)
        {
            TxtDescription.text = "I found them!";
            Buttons[0].SetActive(false); //search
            Buttons[1].SetActive(true); //add
            Buttons[2].SetActive(true); //info
            PropositionsList.SetActive(true);
        }
    }
	
	private void LateUpdate () {
        Work();
	}
}
