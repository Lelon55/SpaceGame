using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GUIResources : MonoBehaviour {
	private statystyki stats;
    private Operations operations = new Operations();

    public Text textIncomeMetal, textIncomeCrystal, textIncomeDeuter;
	public Text textActualMetal, textActualCrystal, textActualDeuter;
	public Text textIncomeResources;
	
	public Image panelMMetal, panelMCrystal, panelMDeuter;

	// Use this for initialization
	private void Start () {
        stats = GameObject.Find("Scripts").GetComponent<statystyki>();
    }

    private void Info_ResourcesPanel ()
    {
        panelMMetal.rectTransform.sizeDelta = new Vector2(150f * operations.Change_result(stats.Get_Data_From("Metal"), stats.Get_Data_From("Capacity_Metal")), 30f);
        panelMCrystal.rectTransform.sizeDelta = new Vector2(150f * operations.Change_result(stats.Get_Data_From("Crystal"), stats.Get_Data_From("Capacity_Crystal")), 30f);
        panelMDeuter.rectTransform.sizeDelta = new Vector2(150f * operations.Change_result(stats.Get_Data_From("Deuter"), stats.Get_Data_From("Capacity_Deuter")), 30f);
    }
	
	private void Print_Resources()
    {
        textIncomeMetal.text = stats.Get_Income("Income_Metal").ToString("N0") + "/30sec";
        textIncomeCrystal.text = stats.Get_Income("Income_Crystal").ToString("N0") + "/30sec";
        textIncomeDeuter.text = stats.Get_Income("Income_Deuter").ToString("N0") + "/30sec";

        textActualMetal.text = stats.Get_Data_From("Metal").ToString("N0") + " / " + stats.Get_Data_From("Capacity_Metal");
        textActualCrystal.text = stats.Get_Data_From("Crystal").ToString("N0") + " / " + stats.Get_Data_From("Capacity_Crystal");
        textActualDeuter.text = stats.Get_Data_From("Deuter").ToString("N0") + " / " + stats.Get_Data_From("Capacity_Deuter");

        textIncomeResources.text = "INCOME: " + stats.ticks;
    }
	private void LateUpdate () {
        Info_ResourcesPanel();
        Print_Resources();
    }

}
