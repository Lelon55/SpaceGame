using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllianceStats : MonoBehaviour {

    public Text[] AllianceData;

    private statystyki stats;
    [SerializeField] private XmlOperations xmlOperations;


    private void Start()
    {
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
    }

    private int MembersLength() // 1 lvl of base = 1 member
    {
        return stats.Get_Data_From("Space Base");
    }

    private int CountMembers()
    {
        if (stats.Get_Data_From("MemberID") >= 1)
        {
            return xmlOperations.CountItems("Allies.xml", "Ally");
        }
        return 0;
    }

    private void ResetMembers()
    {
        if(CountMembers() <= 0)
        {
            stats.Set_Data("MemberID", 0);
        }
    }

    internal bool CompareMemberToLength()
    {
        return MembersLength() > CountMembers();
    }

    private void LateUpdate()
    {
        ResetMembers();
        AllianceData[0].text = "Members: " + CountMembers() + "/" + MembersLength();
        AllianceData[1].text = stats.Get_Data_From("Alliance_Antymatery").ToString();
    }
}
