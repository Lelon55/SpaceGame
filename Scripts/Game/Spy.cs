using UnityEngine;
using System.Collections;

public class Spy : MonoBehaviour {

	public Transform spaceship;
    public float Height; //7f niszczarka
    public Transform Object;

    private void FixedUpdate(){
        Spy_Player();
	}

    private void Spy_Player(){
        transform.position = new Vector3(spaceship.transform.position.x, Object.transform.position.y + Height, 0f);
    }

}
