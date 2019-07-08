using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GUIResources : MonoBehaviour {
	private statystyki stats;
    private Operations operations = new Operations();

    public Text[] txtIncomeResource, txtActuallyResource;
	public Text textIncomeResources;

    public Image[] PanelResource;

	// Use this for initialization
	private void Start () {
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
    }

    private void Info_ResourcesPanel ()
    {
        PanelResource[0].rectTransform.sizeDelta = new Vector2(150f * operations.Change_result(stats.Get_Data_From("Metal"), stats.Get_Data_From("Capacity_Metal")), 30f);
        PanelResource[1].rectTransform.sizeDelta = new Vector2(150f * operations.Change_result(stats.Get_Data_From("Crystal"), stats.Get_Data_From("Capacity_Crystal")), 30f);
        PanelResource[2].rectTransform.sizeDelta = new Vector2(150f * operations.Change_result(stats.Get_Data_From("Deuter"), stats.Get_Data_From("Capacity_Deuter")), 30f);
    }
	
	private void Print_Resources()
    {
        txtIncomeResource[0].text = stats.Get_Income("Income_Metal").ToString("N0") + "/30sec";
        txtIncomeResource[1].text = stats.Get_Income("Income_Crystal").ToString("N0") + "/30sec";
        txtIncomeResource[2].text = stats.Get_Income("Income_Deuter").ToString("N0") + "/30sec";

        txtActuallyResource[0].text = stats.Get_Data_From("Metal").ToString("N0") + " / " + stats.Get_Data_From("Capacity_Metal");
        txtActuallyResource[1].text = stats.Get_Data_From("Crystal").ToString("N0") + " / " + stats.Get_Data_From("Capacity_Crystal");
        txtActuallyResource[2].text = stats.Get_Data_From("Deuter").ToString("N0") + " / " + stats.Get_Data_From("Capacity_Deuter");

        textIncomeResources.text = "INCOME: " + stats.ticks;
    }
	private void LateUpdate () {
        Info_ResourcesPanel();
        Print_Resources();
    }

}
