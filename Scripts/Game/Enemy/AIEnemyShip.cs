using UnityEngine;
using System.Collections;

public class AIEnemyShip : MonoBehaviour
{
    private int shipID;
    private int life;
    [SerializeField] private Sprite[] undamaged, closeToDestruction, destroyed;
    private SpriteRenderer ship;
    private SpriteRenderer healthBar;

    public GameObject blockade;

    private statystyki staty;
    private bool dropped = false;

    public GameObject Explosion;

    private void Start()
    {
        staty = GameObject.Find("spaceship").GetComponent<statystyki>();
        healthBar = GameObject.Find("enemy_life").GetComponent<SpriteRenderer>();
        ship = GetComponent<SpriteRenderer>();
        SetEnemyID();
        SetEnemyLife();
        SetEnemySkin(undamaged[GetEnemySkin()]);
    }

    private void SetEnemyID()
    {
        shipID = Random.Range(0, undamaged.Length);
    }

    private int GetEnemySkin()
    {
        return shipID;
    }

    private void SetEnemySkin(Sprite skin)
    {
        ship.sprite = skin;
    }

    private void CheckEnemySkin()
    {
        switch (life)
        {
            case 5:
                SetEnemySkin(undamaged[GetEnemySkin()]);
                break;
            case 2:
                SetEnemySkin(closeToDestruction[GetEnemySkin()]);
                break;
            case 1:
                SetEnemySkin(destroyed[GetEnemySkin()]);
                break;
        }
    }

    private void SetEnemyLife()
    {
        life = Random.Range(3, 5);
    }

    internal int GetEnemyLife()
    {
        return life;
    }

    private void GenerateExplosion()
    {
        Vector3 explosionVector = new Vector2(transform.position.x, transform.position.y);
        Instantiate(Explosion, explosionVector, transform.rotation);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pocisk" && life >= 1)
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

    private void LateUpdate()
    {
        staty.ShowCurrentlyHealthBar(healthBar, 0.2f * GetEnemyLife());
        CheckEnemySkin();
    }
}
