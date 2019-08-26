using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MembersList : MonoBehaviour
{
    [SerializeField] private XmlOperations xmlOperations;
    [SerializeField] private ScoutProposition scoutProposition;
    private GUIPlanetOperations GUIPlanetOperations;
    private ScoutProposition ScoutProposition;
    private statystyki stats;

    private Button button;

    #region toGeneration
    [SerializeField] private GameObject Container;
    [SerializeField] private Image spriteAllyShip;
    [SerializeField] private Button[] optionsAllyShip;
    #endregion

    public GameObject MemberList;
    public RectTransform ContainerMemberList;
    public Canvas ShowInfoAllyID, ReloadMessage;

    private void Start()
    {
        GUIPlanetOperations = GameObject.Find("Interface").GetComponent<GUIPlanetOperations>();
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        ScoutProposition = GetComponent<ScoutProposition>();
        LoadAllies();
    }

    private float CountToGetContainerHeight()
    {
        return 110f * xmlOperations.CountItems("Allies.xml", "Ally");
    }

    private float CountToGetElementHeight(int elementID)
    {
        return 110f - (110f * elementID);
    }

    private void SetHeightContainer()
    {
        ContainerMemberList.sizeDelta = new Vector2(330f, CountToGetContainerHeight());
    }

    private string GetDescription(int memberID)
    {
        return "Point: " + GetAllyData("Point", memberID) + "\nLife: " + GetAllyData("Life", memberID) + "\nSteer: " + GetAllyData("Steer", memberID) + "\nMax lasers: " + GetAllyData("MaxLasers", memberID);
    }

    private void ShowInformation(int memberID, int id)
    {
        GUIPlanetOperations.Subject_Information(0, 0, 0, 0, GetAllyData("Name", memberID), GetDescription(memberID), scoutProposition.GetSpriteShip(xmlOperations.AllyID[id].Value));
    }

    private void DeleteMember(int id)
    {
        xmlOperations.DeleteAlly("Allies.xml", id);
    }

    internal void ReloadScene()
    {
        GUIPlanetOperations.GUIoper.BtnOpen(ReloadMessage);
    }

    private void GenerateImage(int id, GameObject parent)
    {
        Image imageAlly = Instantiate(spriteAllyShip, MemberList.transform.position, MemberList.transform.rotation) as Image;
        imageAlly.transform.SetParent(parent.transform);
        imageAlly.transform.localPosition = new Vector3(-110f, 0f, 0f);

        imageAlly.sprite = scoutProposition.GetSpriteShip(xmlOperations.AllyID[id].Value);
    }

    private void InstantiateButtons(Button toInstantiante, GameObject parent, Vector3 position)
    {
        button = Instantiate(toInstantiante, MemberList.transform.position, MemberList.transform.rotation) as Button;
        button.transform.SetParent(parent.transform);
        button.transform.localPosition = position;
    }

    private void GenerateBtnFight(int memberID, GameObject parent)
    {
        InstantiateButtons(optionsAllyShip[0], parent, new Vector3(0f, 25f, 0f));
    }

    private void GenerateBtnInfo(int memberID, int id, GameObject parent)
    {
        InstantiateButtons(optionsAllyShip[1], parent, new Vector3(110f, 25f, 0f));
        button.onClick.AddListener(delegate () { ShowInformation(memberID, id); });
    }

    private void GenerateBtnDelete(int memberID, GameObject parent)
    {
        InstantiateButtons(optionsAllyShip[2], parent, new Vector3(55f, -20f, 0f));
        button.onClick.AddListener(delegate () { this.DeleteMember(memberID); });
        button.onClick.AddListener(delegate () { this.ReloadScene(); });
    }

    private void LoadAllies()
    {
        if (stats.Get_Data_From("MemberID") >= 1)
        {
            LoadAllyData();
            SetHeightContainer();
            for (int i = 0; i < xmlOperations.CountItems("Allies.xml", "Ally"); i++)
            {
                GameObject allyContainer = Instantiate(Container, MemberList.transform.position, MemberList.transform.rotation) as GameObject;
                allyContainer.transform.SetParent(MemberList.transform);
                allyContainer.transform.localPosition = new Vector3(ContainerMemberList.transform.localPosition.x, CountToGetElementHeight(i), ContainerMemberList.transform.localPosition.y);

                GenerateImage(i, allyContainer);
                GenerateBtnFight(xmlOperations.AllyMemberID[i].Value, allyContainer);
                GenerateBtnInfo(xmlOperations.AllyMemberID[i].Value, i, allyContainer);
                GenerateBtnDelete(xmlOperations.AllyMemberID[i].Value, allyContainer);
            }
        }
    }

    private string GetAllyData(string withTag, int memberID)
    {
        return xmlOperations.LoadAllyData("Allies.xml", withTag, memberID);
    }

    private void LoadAllyData()
    {
        xmlOperations.LoadAllyID("Allies.xml");
        xmlOperations.LoadAllyMemberID("Allies.xml");
    }
}
