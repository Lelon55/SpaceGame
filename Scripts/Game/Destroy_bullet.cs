using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Destroy_bullet : MonoBehaviour
{
    private statystyki staty;
    private int luck;

    public AudioClip antymatery_sound;
    public GameObject point, antymatery;
    public TextScript textScript;

    private int max_drop_resources = 0;

    private Shake_Camera shake;
    private void Start()
    {
        staty = GameObject.Find("spaceship").GetComponent<statystyki>();
        shake = GameObject.Find("Main Camera").GetComponent<Shake_Camera>();
        Bonus();
    }

    private void Generate_point()
    {
        Vector2 point_vector = new Vector2(transform.position.x, transform.position.y);
        Instantiate(point, point_vector, transform.rotation);
    }

    private void Generate_antymatery()
    {
        Vector2 antymatery_vector = new Vector2(transform.position.x+1f, transform.position.y);
        Instantiate(antymatery, antymatery_vector, transform.rotation);
    }

    private int GetDrop()
    {
        return Random.Range(0, (5 + ((staty.Get_Data_From("Mining Technology") * 2) + max_drop_resources)));
    }

    private void Generate_resources(){
        staty.Add_Dropped_Metal(GetDrop());
        staty.Add_Dropped_Crystal(GetDrop());
        staty.Add_Dropped_Deuter(GetDrop());
    }

    private void OnCollisionEnter2D(Collision2D destroy)
    {
        if (destroy.gameObject.tag == "kometa")
        {
            staty.Add_Comets(1);
            if (SceneManager.GetActiveScene().name == "Game")
            {
                luck = Random.Range(1, 100);
                if (luck < staty.Get_Chance_Drop())
                {//np 5% szansy na drop z antymaterii
                    AudioSource.PlayClipAtPoint(antymatery_sound, transform.position);
                    staty.Add_Dropped_Antymatery(1);
                    Generate_antymatery();
                }
                Generate_resources();
            }
            Generate_point();
            Destroy(gameObject);
            textScript.check = false;//po kolizji przywraca false, aby sie nie powtarzala animacja
            //Debug.Log("Niszczenie pocisku po uderzeniu");
        }
        else if (destroy.gameObject.tag == "Enemy" || destroy.gameObject.tag == "Pocisk_wroga")
        {
            Destroy(gameObject, 0.01f);
            shake.ShakeCamera();
        }
    }

    private void OnTriggerEnter2D(Collider2D destroy)
    {
        if (destroy.gameObject.tag == "Niszcz_pocisk"){
            Destroy(gameObject, 0.01f);
        }
    }

    private void Bonus()
    {
        if(staty.more_resource == 1)
        {
            max_drop_resources = 5;
        }
    }

}
