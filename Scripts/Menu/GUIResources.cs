using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GUIResources : MonoBehaviour
{
    private statystyki stats;
    private GUIPlanetOperations GUIPlanetOperations;

    public Text[] IncomeResource, ActuallyResource, ResourcesAtPanel;
    public Image[] PanelResource;

    private void Start()
    {
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

    private void SetColourCapacity()
    {
        ResourcesAtPanel[0].color = GUIPlanetOperations.Set_Color(stats.Get_Data_From("Metal"), stats.Get_Data_From("Capacity_Metal"));
        ResourcesAtPanel[1].color = GUIPlanetOperations.Set_Color(stats.Get_Data_From("Crystal"), stats.Get_Data_From("Capacity_Crystal"));
        ResourcesAtPanel[2].color = GUIPlanetOperations.Set_Color(stats.Get_Data_From("Deuter"), stats.Get_Data_From("Capacity_Deuter"));
    }

    private void CheckCapacity()
    {
        ResourcesAtPanel[0].text = stats.Get_Data_From("Metal").ToString("N0");
        ResourcesAtPanel[1].text = stats.Get_Data_From("Crystal").ToString("N0");
        ResourcesAtPanel[2].text = stats.Get_Data_From("Deuter").ToString("N0");
        ResourcesAtPanel[3].text = stats.Get_Data_From("Free_Field").ToString("N0");
        ResourcesAtPanel[4].text = stats.Get_Data_From("Antymatery").ToString("N0");
    }

    private void ResourcesInformation()
    {
        PanelResource[0].rectTransform.sizeDelta = GUIPlanetOperations.CountVector("Metal", "Capacity_Metal", 30f);
        PanelResource[1].rectTransform.sizeDelta = GUIPlanetOperations.CountVector("Crystal", "Capacity_Crystal", 30f);
        PanelResource[2].rectTransform.sizeDelta = GUIPlanetOperations.CountVector("Deuter", "Capacity_Deuter", 30f);
    }

    private void ShowIncome()
    {
        IncomeResource[0].text = ShowIncome("Income_Metal");
        IncomeResource[1].text = ShowIncome("Income_Crystal");
        IncomeResource[2].text = ShowIncome("Income_Deuter");
        IncomeResource[3].text = "INCOME: " + stats.ticks;
    }

    private void FillingOfCapacities()
    {
        ActuallyResource[0].text = ShowFillingOfCapacities("Metal", "Capacity_Metal");
        ActuallyResource[1].text = ShowFillingOfCapacities("Crystal", "Capacity_Crystal");
        ActuallyResource[2].text = ShowFillingOfCapacities("Deuter", "Capacity_Deuter");
    }

    private void LateUpdate()
    {
        SetColourCapacity();
        CheckCapacity();
        ResourcesInformation();
        FillingOfCapacities();
        ShowIncome();
    }
}
