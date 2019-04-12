using UnityEngine;
using System.Collections;

public class Generate_enemy_bullet : MonoBehaviour {

    public Vector2 speed = new Vector2(10f, 0f); //predkosc statku dla rigidbody
    private Vector2 movement;
    public Rigidbody2D gravity;
    private Transform player;
    public Transform enemy;

    private float min_cooling;
	private float cooling;

    private bool moving = false;
    public float reaction_movement; //max 0.2 min 0.01

	public GameObject bullet_ship;
	private int nr_laser;
	public AudioClip[] Sound_laser;

	private void Start(){
        player = GameObject.Find("spaceship").GetComponent<Transform>();
		nr_laser = Random.Range(0, 7);
        min_cooling = Random.Range(0.7f, 1.7f);
		PlayerPrefs.SetInt("nr_enemy_laser", nr_laser);
    }

	private IEnumerator Count()
	{
		while (true) {
			yield return new WaitForSeconds (cooling); // czas Tick
            cooling = Random.Range(min_cooling, min_cooling+1f);
            Generate_bullet(Random.Range(0, 3)); //generuje co iles wczytanych sekund
            StopCoroutine("Count");
        }
	}

    public void Generate_bullet(int nr)
    {
        if (moving == false)
        {
            Vector2 pozycja_pocisku = new Vector2(transform.position.x - 0.02f, transform.position.y - 0.5f);
            Instantiate(bullet_ship, pozycja_pocisku, bullet_ship.transform.rotation);
            AudioSource.PlayClipAtPoint(Sound_laser[nr], transform.position);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine("Count");
            Movement_enemy();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //brak strzalu, ale ma szukac wroga od ściany do ściany
        StopCoroutine("Count");
    }
    
    private void Movement_enemy()
    {
        movement = new Vector2(speed.x, speed.y);

        if (enemy.position.x > player.position.x + 0.3f)
        {
            movement.x = speed.x * -reaction_movement;
            moving = true;

        }
        else if (enemy.position.x < player.position.x - 0.3f)
        {
            movement.x = speed.x * reaction_movement;
            moving = true;
        }
        else if (enemy.position.x == player.position.x || enemy.position.x <= player.position.x + 0.2f || enemy.position.x >= player.position.x - 0.2f)
        {
            movement.x = 0f;
            moving = false;
        }
        gravity.velocity = movement;
    }

}
