using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllianceScout : MonoBehaviour
{
    private statystyki stats;
    private GUIPlanetOperations GUIPlanetOperations;
    private GUIOperations GUIoper;
    [SerializeField] private XmlOperations xmlOperations;
    private ScoutProposition ScoutProposition;
    private MembersList MembersList;

    private int ID;

    [SerializeField] private AllianceStats AllianceStats;
    [SerializeField] private GUIOverview GUIOverview;

    [SerializeField] private GameObject PropositionsList;
    [SerializeField] internal Image Sprite_MemberShip;
    [SerializeField] private Text TxtDescription;
    [SerializeField] private GameObject[] Buttons;
    [SerializeField] private int page;
    private const int cost = 1; // 0 = przywitanie, 1 szuka, 2 wyniki wyszukania

    private void Start()
    {
        page = 0;
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        ScoutProposition = GetComponent<ScoutProposition>();
        MembersList = GetComponent<MembersList>();
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
        page = 0;
    }

    public void InfoShips()
    {
        GUIPlanetOperations.Subject_Information(0, 0, 0, 0, ScoutProposition.GetName(ID), ScoutProposition.GetDescription(ID), ScoutProposition.GetSpriteShip(ID));
    }

    public void AddMember()
    {
        if (AllianceStats.CompareMemberToLength())
        {
            SetMembers(ID);
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

    private string Get_AdmiralName()
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
        FindShip(ID);
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
        if (stats.Get_Data_From("MemberID") < 1)
        {
            xmlOperations.CreateXMLFile("Allies.xml", GetIDToAddMember(), id, ScoutProposition.GetName(id), ScoutProposition.GetPoint(), ScoutProposition.GetLife(id), ScoutProposition.GetSteer(id), ScoutProposition.GetMaxLasers(id));
        }
        else
        {
            xmlOperations.AddAlly("Allies.xml", GetIDToAddMember(), id, ScoutProposition.GetName(id), ScoutProposition.GetPoint(), ScoutProposition.GetLife(id), ScoutProposition.GetSteer(id), ScoutProposition.GetMaxLasers(id));
        }
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

    private void LateUpdate()
    {
        Work();
    }
}
