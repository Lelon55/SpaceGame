using UnityEngine;
using System.Collections;

public class Player_life : MonoBehaviour {

	private statystyki staty;

	// Use this for initialization
	private void Start () {
        staty = GameObject.Find("spaceship").GetComponent<statystyki>();
    }
	
	// Update is called once per frame
	private void Update () {
        Current_Life(0.2f * staty.Life);
	}
	
	private void Current_Life(float size){
		transform.localScale = new Vector3 (size, 0.2f, 0f);
	}

    
}
