using UnityEngine;
using System.Collections;

public class Generate_bullet : MonoBehaviour {

	public GameObject shot;
	public AudioClip[] laser_sound;
	private int nr;
    private float cooling = 2.5f;
    public float max_bullets;
    public float min_bullets = 0.0f; 

	private Vector2 laser_position;

	private statystyki staty;
	private Rigidbody2D gravity;

    private void Start()
    {
        laser_position = new Vector2(0f, 0.5f);
        staty = GameObject.Find("spaceship").GetComponent<statystyki>();
        gravity = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        max_bullets = staty.Get_Float_Data_From("Max_Lasers");
        gravity.gravityScale = staty.gravity_bullet;
        if (min_bullets <= 0f)
        {
            StopCoroutine("Start_Count");
            min_bullets = 0f;
        }
        else
        {
            StartCoroutine("Start_Count");
        }

        if (staty.Get_Comets() == 50)
        {//skrocimy czas do 1.5f
            cooling = 2.0f;
        }
        else if (staty.Get_Comets() == 150)
        {//skrocimy czas do 1.0f
            cooling = 1.5f;
        }
    }

	private IEnumerator Start_Count()
	{
		while (true) {
			yield return new WaitForSeconds (cooling); // czas Tick
            min_bullets--; //odejmuje wszystko po 2sek
		}

	}

	public void Shoot_bullet() {
       
        if (min_bullets >= max_bullets)
        {
            //Debug.Log("daj odpoczac");
        }
        else if (min_bullets <= max_bullets)
        {
            min_bullets++;
            Vector2 bullet_position = new Vector2(staty.transform.position.x + laser_position.x + 0.02f, staty.transform.position.y + laser_position.y);
            nr = Random.Range(0, 3);
            Instantiate(shot, bullet_position, shot.transform.rotation);
            AudioSource.PlayClipAtPoint(laser_sound[nr], staty.transform.position);
            //Debug.Log("wystrzelono: " + nr + "liczba: " + min_bullets);
        }
	}

}
