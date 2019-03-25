using UnityEngine;
using System.Collections;

public class GUILoading : MonoBehaviour {
	public GUIStyle przycisk_ladowania;
	public GUIStyle tekst;
	// Use this for initialization
	void Start () {
		tekst.fontSize=  Mathf.Min(Mathf.FloorToInt(Screen.width * 45/1920), Mathf.FloorToInt(Screen.height * 45/1080));
		przycisk_ladowania.fontSize=  Mathf.Min(Mathf.FloorToInt(Screen.width * 45/1920), Mathf.FloorToInt(Screen.height * 45/1080));

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator ButtonAction (string levelName){
		yield return new WaitForSeconds (0.35f);

		if (levelName != "quit") {
			Application.LoadLevel(levelName);
				}
		}

	void OnGUI(){
		if (Application.CanStreamedLevelBeLoaded ("Game")) {
			if(GUI.Button(new Rect(Screen.width*0.25f, Screen.height*0.25f, Screen.width*0.5f, Screen.height*0.5f), "Play")){
				StartCoroutine("ButtonAction", "Game");
			}else{
				float percentLoaded = Application.GetStreamProgressForLevel(1)*100;
				GUI.Box (new Rect(Screen.width*0.25f, Screen.height*0.25f, Screen.width*0.5f, Screen.height*0.5f), "Loading..." + percentLoaded.ToString ("f0")+"% Loaded");
			}
				}
	}
}
