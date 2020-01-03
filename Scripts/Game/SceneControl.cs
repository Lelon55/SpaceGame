using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneControl : MonoBehaviour
{
    private int max_scen, min_scen;

    public GameObject[] scena;
    private int nrScene;
    public bool enemy;
    private statystyki staty;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            staty = GameObject.Find("spaceship").GetComponent<statystyki>();
            if (staty.LoadedScene <= 0)
            {
                SetRangeScene(0, 7);
            }
            else if (staty.LoadedScene >= 1 && staty.LoadedScene <= 4)
            {
                SetRangeScene(0, 12);
            }
            else if (staty.LoadedScene == 5)
            { // scenes with enemies
                SetRangeScene(16, 18);
            }
            else if (staty.LoadedScene >= 6 && enemy == true)
            { // scenes without enemies
                SetRangeScene(0, 16);
            }
            else if (staty.LoadedScene >= 6 && enemy == false)
            {
                SetRangeScene(0, 18);
            }
        }
    }

    private void SetRangeScene(int min, int max)
    {
        max_scen = max;
        min_scen = min;
    }

    private int GetMaxRangeScene()
    {
        return max_scen;
    }

    private int GetMinRangeScene()
    {
        return min_scen;
    }

    private void OnTriggerEnter2D(Collider2D generator)
    {
        if (generator.gameObject.tag == "Player")
        {
            nrScene = Random.Range(GetMinRangeScene(), GetMaxRangeScene());
            Vector2 scenePosition = new Vector2(3.5f, transform.position.y + 15.75f);
            Instantiate(scena[nrScene], scenePosition, scena[nrScene].transform.rotation);
            Debug.Log("Loaded" + nrScene);
        }
    }
}
