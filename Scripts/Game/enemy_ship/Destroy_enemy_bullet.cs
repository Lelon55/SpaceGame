using UnityEngine;
using System.Collections;

public class Destroy_enemy_bullet : MonoBehaviour {

	private void FixedUpdate(){
		Destroy(gameObject,2.0f);
	}

    private void OnCollisionEnter2D (Collision2D niszczenie){
		 if (niszczenie.gameObject.tag == "Player" || niszczenie.gameObject.tag == "Pocisk") {
			Destroy(gameObject);
			Debug.Log ("Niszczenie pocisku wroga");
		 }
	}
}
