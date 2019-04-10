using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GUIMerchant : MonoBehaviour {
    private class List_boxes
    {
        public int id, how_much, cost;
        public string resource;


        public List_boxes(int i, string r, int h_w, int c)
        {
            this.id = i;
            this.resource = r;
            this.how_much = h_w;
            this.cost = c;

        }
    }
    private List<List_boxes> boxes = new List<List_boxes>();
    private statystyki staty;
    private GUIOverview GUIOverview;

    // Use this for initialization
    private void Start () {
        GUIOverview = GameObject.Find("Interface").GetComponent<GUIOverview>();
        staty = GameObject.Find("Scripts").GetComponent<statystyki>();
        boxes.Add(new List_boxes(1, "Metal", 250, 10));
        boxes.Add(new List_boxes(2, "Crystal", 250, 10));
        boxes.Add(new List_boxes(3, "Deuter", 250, 10));
        boxes.Add(new List_boxes(4, "Metal", 1000, 30));
        boxes.Add(new List_boxes(5, "Crystal", 1000, 30));
        boxes.Add(new List_boxes(6, "Deuter", 1000, 30));
    }
	
    public void BtnBuyBox(int number)
    {
        if (staty.Get_Data_From("Antymatery") >= boxes[number].cost)
        {
            staty.Set_Data(boxes[number].resource, staty.Get_Data_From(boxes[number].resource) + boxes[number].how_much);
            staty.Change_Antymatery(-boxes[number].cost);
            PlayerPrefs.Save();
            GUIOverview.CanvasMessage.enabled = true;
            GUIOverview.textMessage.text = "Bought "+ boxes[number].how_much+" "+ boxes[number].resource;
            GUIOverview.audiosource_sound_message.PlayOneShot(GUIOverview.sound_message, 0.7F);
        }
        else
        {
            GUIOverview.CanvasMessage.enabled = true;
            GUIOverview.textMessage.text = "Don't enough antymateries";
            GUIOverview.audiosource_sound_message.PlayOneShot(GUIOverview.sound_message, 0.7F);
        }
    }

}
