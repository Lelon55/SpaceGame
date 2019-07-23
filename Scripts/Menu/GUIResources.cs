using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GUIResources : MonoBehaviour {
	private statystyki stats;
    private GUIPlanetOperations GUIPlanetOperations;

    public Text[] txtIncomeResource, txtActuallyResource;
	public Text textIncomeResources;

    public Image[] PanelResource;

	// Use this for initialization
	private void Start () {
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
        GUIPlanetOperations = GameObject.Find("Interface").GetComponent<GUIPlanetOperations>();
    }

    private string ShowIncome(string resource)
    {
        return stats.Get_Income(resource).ToString("N0") + "/30sec";
    }

    private string ShowFillingOfCapacities(string resource, string capacity)
    {
        return stats.Get_Data_From(resource).ToString("N0") + " / " + stats.Get_Data_From(capacity);
    }

    private void Info_ResourcesPanel ()
    {
        PanelResource[0].rectTransform.sizeDelta = GUIPlanetOperations.CountVector("Metal", "Capacity_Metal", 30f);
        PanelResource[1].rectTransform.sizeDelta = GUIPlanetOperations.CountVector("Crystal", "Capacity_Crystal", 30f);
        PanelResource[2].rectTransform.sizeDelta = GUIPlanetOperations.CountVector("Deuter", "Capacity_Deuter", 30f);
    }
	
	private void Print_Resources()
    {
        txtIncomeResource[0].text = ShowIncome("Income_Metal");
        txtIncomeResource[1].text = ShowIncome("Income_Crystal");
        txtIncomeResource[2].text = ShowIncome("Income_Deuter");

        txtActuallyResource[0].text = ShowFillingOfCapacities("Metal", "Capacity_Metal");
        txtActuallyResource[1].text = ShowFillingOfCapacities("Crystal", "Capacity_Crystal");
        txtActuallyResource[2].text = ShowFillingOfCapacities("Deuter", "Capacity_Deuter");

        textIncomeResources.text = "INCOME: " + stats.ticks;
    }

	private void LateUpdate () {
        Info_ResourcesPanel();
        Print_Resources();
    }
}
