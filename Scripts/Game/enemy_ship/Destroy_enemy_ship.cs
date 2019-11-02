using UnityEngine;
using System.Collections;

public class Destroy_enemy_ship : MonoBehaviour
{
    public int life = 5;
    public Sprite undamaged, close_to_destruction, destroyed;
    private SpriteRenderer ship;
    public GameObject blockade;

    private statystyki staty;
    private bool dropped = false;

    public GameObject Explosion;

    private void Start()
    {
        staty = GameObject.Find("spaceship").GetComponent<statystyki>();
        ship = GetComponent<SpriteRenderer>();
        undamaged = ship.sprite;
    }

    private void GenerateExplosion()
    {
        Vector3 Explosion_vector = new Vector2(transform.position.x, transform.position.y);
        Instantiate(Explosion, Explosion_vector, transform.rotation);
    }

    private void LateUpdate()
    {
        switch (life)
        {
            case 5:
                ship.sprite = undamaged;
                break;
            case 2:
                ship.sprite = close_to_destruction;
                break;
            case 1:
                ship.sprite = destroyed;
                break;
        }
    }

    private void DestroyShip()
    {
        if (life < 1)
        {
            Destroy(gameObject);
            Destroy(blockade);
            GenerateExplosion();
            DropFromEnemy();
        }
    }

    private int GetDamage()//2 more damage, 1 normal damage
    {
        if (staty.more_damage == 1)
        {
            return 2;
        }
        return 1;
    }

    private void OnCollisionEnter2D(Collision2D niszczenie)
    {
        if (niszczenie.gameObject.tag == "Pocisk" && life >= 1)
        {
            life -= GetDamage();
            DestroyShip();
        }
    }

    private void DropFromEnemy()
    {
        if (!dropped)
        {
            staty.AddDroppedResources(10, 10, 10, "Destroyed_Enemy_Ships");
            dropped = true;
        }
    }
}
