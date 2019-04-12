using UnityEngine;
using System.Collections;

public class Destroy_enemy_ship : MonoBehaviour {

	public int life = 5;
	public Sprite undamaged, close_to_destruction, destroyed;
	private SpriteRenderer ship;
	public GameObject blockade;

	private statystyki staty;
    private bool dropped = false;

    public GameObject Explosion;

    private void Start(){
		staty = GameObject.Find("spaceship").GetComponent<statystyki>();
        ship = GetComponent<SpriteRenderer>();
        undamaged = ship.sprite;
	}
    private void Generate_explosion()
    {
        Vector3 Explosion_vector = new Vector2(transform.position.x, transform.position.y);
        Instantiate(Explosion, Explosion_vector, transform.rotation);
    }
    private void LateUpdate(){
        if (life == 5)
        {
            ship.sprite = undamaged;
        }
        else if (life == 2)
        {
            ship.sprite = close_to_destruction;
        }
        else if (life == 1)
        {
            ship.sprite = destroyed;
        }
    }
    private void Destroy_ship()
    {
        if (life < 1)
        {
            Destroy(gameObject);
            Destroy(blockade);
            Generate_explosion();
            Drop_from_enemy();
        }
    }

    private int Get_Damage()//2 more damage, 1 normal damage
    {
        if(staty.more_damage == 1)
        {
            return 2;
        }
        return 1;
    }
    private void OnCollisionEnter2D(Collision2D niszczenie)
    {
        if (niszczenie.gameObject.tag == "Pocisk" && life >= 1)
        {
            life -= Get_Damage();
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
            //Debug.Log("NISZCZENIE STATKU");
        }

    }

}
