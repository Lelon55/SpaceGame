using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllianceStats : MonoBehaviour {

    public Text[] AllianceData;

    private int Max_Members;
    private int Members;

    private statystyki stats;


	private void Start () {
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
    }

    private int MembersLength() // 1 lvl of base = 1 member
    {
        return stats.Get_Data_From("Space Base");
    }

    private int CountMembers()
    {
        return 0;
        //wylicz ilosc czlonkow z tabeli
    }

    internal bool CompareMemberToLength() //jesli jest mniejsza ilosc czlonkow niz pojemnosc sojuszu to zwraca true
    {
        return MembersLength() > CountMembers();
    }

	private void LateUpdate () {
        AllianceData[0].text = "Members: "+ CountMembers() + "/" + MembersLength();
        AllianceData[1].text = stats.Get_Data_From("Alliance_Antymatery").ToString();
	}
}
