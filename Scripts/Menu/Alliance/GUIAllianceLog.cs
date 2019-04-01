using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GUIAllianceLog : MonoBehaviour {//docelowo laczone z baza danych

    private class List_Logs
    {
        public string entry;

        public List_Logs(string e)
        {
            this.entry = e;
        }
    }
    private List<List_Logs> Logs = new List<List_Logs>();
    public Text entry_text;
    private void Start () {
        Logs.Add(new List_Logs("Alliance was created by Lelon"));
    }

	private void Update () {
        View_Entries();
        
	}
    internal void Add_New_Entry(string entry_txt)
    {
        Logs.Add(new List_Logs(entry_txt));
    }

    private void View_Entries()
    {
        for(int i=0; i<=Logs.Count; i++)
        {
            entry_text.text = Logs[i].entry + "\n";
        }
    }
}
