using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Generate_scene : MonoBehaviour
{
    public int max_scen; //ilosc scen do wczytania na danej scenie UNITY
    public GameObject[] scena;
    private int nr_scene;
    public int min_scen = 0; // odkad, dokad. Zakres Random RAnge.
    public bool enemy;
    private statystyki staty;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            staty = GameObject.Find("spaceship").GetComponent<statystyki>();
            if (staty.LoadedScene <= 0)
            {
                RangeScene(7, 0);
            }
            else if (staty.LoadedScene >= 1 && staty.LoadedScene <= 4)
            {
                RangeScene(12, 0);
            }
            else if (staty.LoadedScene == 5)
            { // scenes with enemies
                RangeScene(22, 16);
            }
            else if (staty.LoadedScene >= 6 && enemy == true)
            { //scenes without enemies
                RangeScene(16, 0);
            }
            else if (staty.LoadedScene >= 6 && enemy == false)
            {
                RangeScene(22, 0);
            }
        }
    }

    private void RangeScene(int max, int min)
    {
        max_scen = max;
        min_scen = min;
    }

    private void OnTriggerEnter2D(Collider2D generator)
    {
        if (generator.gameObject.tag == "Player")
        {
            nr_scene = Random.Range(min_scen, max_scen);
            Vector2 pozycja_sceny = new Vector2(3.5f, transform.position.y + 15.75f);
            Instantiate(scena[nr_scene], pozycja_sceny, scena[nr_scene].transform.rotation);
            Debug.Log("Wczytano" + nr_scene);
        }
    }
}
