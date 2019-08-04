using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Destroy_bullet : MonoBehaviour
{
    private statystyki staty;

    public AudioClip antymatery_sound;
    public GameObject point, antymatery;
    public TextScript textScript;

    private Shake_Camera shake;
    private GUIOperations GUIOperations;

    private void Start()
    {
        staty = GameObject.Find("spaceship").GetComponent<statystyki>();
        shake = GameObject.Find("Main Camera").GetComponent<Shake_Camera>();
        GUIOperations = GameObject.Find("spaceship").GetComponent<GUIOperations>();
    }

    private int GetDrop()
    {
        return Random.Range(0, 5 + (staty.Get_Data_From("Mining Technology") * 2) + ReturnMaxDropResources());
    }

    private void GenerateResources()
    {
        staty.Add_Dropped_Metal(GetDrop());
        staty.Add_Dropped_Crystal(GetDrop());
        staty.Add_Dropped_Deuter(GetDrop());
    }

    private int LuckyAntymatery()
    {
        return Random.Range(1, 100);
    }

    private void OnCollisionEnter2D(Collision2D destroy)
    {
        if (destroy.gameObject.tag == "kometa")
        {
            staty.Add_Comets(1);
            if (SceneManager.GetActiveScene().name == "Game")
            {
                if (LuckyAntymatery() < staty.Get_Chance_Drop())
                {//np 5% szansy na drop z antymaterii
                    AudioSource.PlayClipAtPoint(antymatery_sound, transform.position);
                    staty.Add_Dropped_Antymatery(1);
                    GUIOperations.Generate(transform.position.x + 1f, transform.position.y, transform.rotation, antymatery);
                }
                GenerateResources();
                textScript.check = false;//po kolizji przywraca false, aby sie nie powtarzala animacja
            }
            GUIOperations.Generate(transform.position.x + 0f, transform.position.y, transform.rotation, point);
            Destroy(gameObject); 
        }
        else if (destroy.gameObject.tag == "Enemy" || destroy.gameObject.tag == "Pocisk_wroga")
        {
            Destroy(gameObject, 0.01f);
            shake.ShakeCamera();
        }
    }

    private void OnTriggerEnter2D(Collider2D destroy)
    {
        if (destroy.gameObject.tag == "Niszcz_pocisk")
        {
            Destroy(gameObject, 0.01f);
        }
    }

    private int ReturnMaxDropResources()
    {
        if (staty.more_resource == 1)
        {
            return 5;
        }
        return 0;
    }
}
