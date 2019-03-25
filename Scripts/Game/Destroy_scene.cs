using UnityEngine;
using System.Collections;

public class Destroy_scene : MonoBehaviour {

	private void OnTriggerExit2D(Collider2D collider){
		if (collider.gameObject.tag == "Player") {
			Destroy(gameObject, 3f);
			Debug.Log ("Niszczenie sceny");
		}
	}
}
