using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GUIMerchant : MonoBehaviour {
    private class ListBoxes
    {
        public int id, how_much, cost;
        public string resource;

        public ListBoxes(int i, string r, int h_w, int c)
        {
            this.id = i;
            this.resource = r;
            this.how_much = h_w;
            this.cost = c;
        }
    }

    private List<ListBoxes> boxes = new List<ListBoxes>();
    private statystyki staty;
    private GUIOverview GUIOverview;

    private void Start()
    {
        GUIOverview = GameObject.Find("Interface").GetComponent<GUIOverview>();
        staty = GameObject.Find("Scripts").GetComponent<statystyki>();
        boxes.Add(new ListBoxes(1, "Metal", 250, 10));
        boxes.Add(new ListBoxes(2, "Crystal", 250, 10));
        boxes.Add(new ListBoxes(3, "Deuter", 250, 10));
        boxes.Add(new ListBoxes(4, "Metal", 1000, 30));
        boxes.Add(new ListBoxes(5, "Crystal", 1000, 30));
        boxes.Add(new ListBoxes(6, "Deuter", 1000, 30));
    }

    public void BtnBuyBox(int number)
    {
        if (staty.Get_Data_From("Antymatery") >= boxes[number].cost)
        {
            staty.Set_Data(boxes[number].resource, staty.Get_Data_From(boxes[number].resource) + boxes[number].how_much);
            staty.Change_Antymatery(-boxes[number].cost);
            GUIOverview.View_CanvasMessage("Bought " + boxes[number].how_much + " " + boxes[number].resource);
            PlayerPrefs.Save();
        }
        else
        {
            GUIOverview.View_CanvasMessage("Don't enough antymateries");
        }
    }
}
