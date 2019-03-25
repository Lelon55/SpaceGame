using UnityEngine;
using System.Collections;

public class Destroy_enemy_ship : MonoBehaviour {

	public int life = 5;
	public Sprite podniszczone;
	public Sprite podniszczone_bardziej;
	public Sprite niezniszczone;
	public SpriteRenderer statek;
	public GameObject blokada;

	private statystyki staty;
    private bool dropped = false;

    public GameObject Explosion;

    private void Start(){
		//blokada = GameObject.Find("blokada");
		staty = GameObject.Find("spaceship").GetComponent<statystyki>();
		statek = GetComponent<SpriteRenderer>();
        niezniszczone = statek.sprite;
	}
    private void Generate_explosion()
    {
        Vector3 Explosion_vector = new Vector2(transform.position.x, transform.position.y);
        Instantiate(Explosion, Explosion_vector, transform.rotation);
    }
    private void LateUpdate(){
        if (life == 5)
        {
            statek.sprite = niezniszczone;
        }
        else if (life == 2)
        {
            statek.sprite = podniszczone;
        }
        else if (life == 1)
        {
            statek.sprite = podniszczone_bardziej;
        }
    }
    private void Destroy_ship()
    {
        if (life < 1)
        {
            staty.Set_Data("Destroyed_Enemy_Ships", staty.Get_Data_From("Destroyed_Enemy_Ships") + 1);
            Destroy(gameObject);
            Destroy(blokada);
            Generate_explosion();
            Drop_from_enemy();
        }
    }
    private void OnCollisionEnter2D(Collision2D niszczenie)
    {
        if (niszczenie.gameObject.tag == "Pocisk" && life >= 1 && staty.more_damage == 0)
        {
            life -= 1;
            Destroy_ship();
        }
        else if (niszczenie.gameObject.tag == "Pocisk" && life >= 1 && staty.more_damage == 1)
        {
            life -= 2;
            Destroy_ship();
        }
    }
    private void Drop_from_enemy()
    {
        if (dropped == false)
        {
            staty.Add_Dropped_Metal(10);
            staty.Add_Dropped_Crystal(10);
            staty.Add_Dropped_Deuter(10);
            staty.Set_Data("Destroyed_Enemy_Ships", staty.Get_Data_From("Destroyed_Enemy_Ships") + 1);
            dropped = true;
            Debug.Log("NISZCZENIE STATKU");
        }

    }

}
