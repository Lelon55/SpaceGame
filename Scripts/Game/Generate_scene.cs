using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Generate_scene : MonoBehaviour {
	public int max_scen; //ilosc scen do wczytania na danej scenie UNITY
	public GameObject[] scena;
	private int nr;
	public int min_scen = 0; // odkad, dokad. Zakres Random RAnge.
    public bool enemy;
	private statystyki staty;

	private void Start(){
		if (SceneManager.GetActiveScene().name != "Menu") {
			staty = GameObject.Find("spaceship").GetComponent<statystyki>();
			if (staty.ilosc_wczytanych_scen <= 0) {
				max_scen = 7;//7
                min_scen = 0;
			} 
			else if (staty.ilosc_wczytanych_scen >= 1 && staty.ilosc_wczytanych_scen <= 4) { 
				max_scen = 12;//12
                min_scen = 0; 
			}
            else if (staty.ilosc_wczytanych_scen == 5){ // scene with enemy
                max_scen = 22;
                min_scen = 16; 
            }
            else if (staty.ilosc_wczytanych_scen >= 6 && enemy == true)
            { // wszystkie bez wrogow
                max_scen = 16;
                min_scen = 0;
            }
            else if (staty.ilosc_wczytanych_scen >= 6 && enemy == false)
            { 
                max_scen = 22;
                min_scen = 0;
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D generator)
    {
        if (generator.gameObject.tag == "Player")
        {
            nr = Random.Range(min_scen, max_scen);
            Vector2 pozycja_sceny = new Vector2(3.5f, transform.position.y + 15.75f);
            Instantiate(scena[nr], pozycja_sceny, scena[nr].transform.rotation);
            Debug.Log("Wczytano" + nr);
        }
    }
}
