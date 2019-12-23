using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllianceScout : MonoBehaviour
{
    private statystyki stats;
    private GUIPlanetOperations GUIPlanetOperations;
    [SerializeField] private XmlOperations xmlOperations;

    private GUIOperations GUIoper;
    private ScoutProposition ScoutProposition;
    private MembersList MembersList;

    private int MemberID;

    [SerializeField] private GUIOverview GUIOverview;

    [SerializeField] private GameObject PropositionsList;
    [SerializeField] internal Image Sprite_MemberShip;
    [SerializeField] private Text TxtDescription;
    [SerializeField] private GameObject[] Buttons;
    [SerializeField] private int page; // 0 = przywitanie, 1 szuka, 2 wyniki wyszukania
    private const int cost = 1; 

    private void Start()
    {
        SetPageID(0);
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        ScoutProposition = GetComponent<ScoutProposition>();
        MembersList = GetComponent<MembersList>();
        GUIoper = GameObject.Find("Interface").GetComponent<GUIOperations>();
        GUIPlanetOperations = GameObject.Find("Interface").GetComponent<GUIPlanetOperations>();
        Work();
    }

    private int GetPageID()
    {
        return page;
    }

    private void SetPageID(int value)
    {
        page = value;
    }

    private void RandomID(int id)
    {
        MemberID = id;
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
            if (MembersList.CompareMemberToLength())//if true do it 
            {
                SetPageID(1);
                Cost();
                FindMember();
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
        SetPageID(0);
    }

    public void InfoShips()
    {
        GUIPlanetOperations.Subject_Information(0, 0, 0, 0, ScoutProposition.GetName(MemberID), ScoutProposition.GetDescription(MemberID), ScoutProposition.GetSpriteShip(MemberID));
    }

    public void AddMember()
    {
        if (MembersList.CompareMemberToLength())
        {
            SetMembers(MemberID);
            stats.Set_Data("MemberID", GetIDToAddMember());
            BtnLeaveIt();
            MembersList.ReloadScene();
        }
        else
        {
            GUIOverview.View_CanvasMessage("Not enough space for new member!");
        }
    }

    #endregion

    private string GetAdmiralName()
    {
        return stats.Get_String_Data_From("Admiral_Name");
    }

    private int GetIDToAddMember()
    {
        return stats.Get_Data_From("MemberID") + 1;
    }

    private void GetSprite_MemberShip(int id)
    {
        Sprite_MemberShip.sprite = ScoutProposition.GetSpriteShip(id);
    }

    private void FindMember()
    {
        RandomID(ScoutProposition.GetShipsMaxRange());
        FindShip(MemberID);
    }

    private void FindShip(int id)
    {
        ScoutProposition.GetName(id);
        ScoutProposition.SetPoint();
        ScoutProposition.GetPoint();
        ScoutProposition.GetLife(id);
        ScoutProposition.GetSteer(id);
        ScoutProposition.GetMaxLasers(id);
        GetSprite_MemberShip(id);
    }

    private void SetMembers(int id)
    {
        if (stats.Get_Data_From("MemberID") <= 0)
        {
            xmlOperations.CreateXMLFile("Allies.xml", GetIDToAddMember(), id, ScoutProposition.GetName(id), ScoutProposition.GetPoint(), ScoutProposition.GetLife(id), ScoutProposition.GetSteer(id), ScoutProposition.GetMaxLasers(id));
        }
        else
        {
            xmlOperations.AddAlly("Allies.xml", GetIDToAddMember(), id, ScoutProposition.GetName(id), ScoutProposition.GetPoint(), ScoutProposition.GetLife(id), ScoutProposition.GetSteer(id), ScoutProposition.GetMaxLasers(id));
        }

    }

    private void ShowDescription(string value)
    {
        TxtDescription.text = value;
    }

    private void SetButtonsVisibility(bool value1, bool value2, bool value3, bool value4)
    {
        Buttons[0].SetActive(value1);  //search
        Buttons[1].SetActive(value2); //add
        Buttons[2].SetActive(value3); //info
        PropositionsList.SetActive(value4);
    }

    private void Work()
    {
        if (GetPageID() == 0)
        {
            ShowDescription("Hi " + GetAdmiralName() + "!\nI am a scout. I help in finding members for the alliance. Are you interested in this?");
            SetButtonsVisibility(true, false, false, false);
        }
        else if (GetPageID() == 1)
        {
            ShowDescription("I found them!");
            SetButtonsVisibility(false, true, true, true);
        }
    }

    private void LateUpdate()
    {
        Work();
    }
}
