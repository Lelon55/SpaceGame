using UnityEngine;
using System.Collections;

public class AIEnemyShip : MonoBehaviour
{
    private int shipID, life, immortal;
    private string name;
    [SerializeField] private Sprite[] undamaged, closeToDestruction, destroyed, auras;
    private SpriteRenderer ship, healthBar, aura;

    public GameObject blockade;

    private statystyki staty;
    private bool dropped = false;

    public GameObject Explosion;

    private void Start()
    {
        staty = GameObject.Find("spaceship").GetComponent<statystyki>();
        healthBar = GameObject.Find("enemy_life").GetComponent<SpriteRenderer>();
        ship = GetComponent<SpriteRenderer>();
        aura = GameObject.Find("enemy_aura").GetComponent<SpriteRenderer>();
        SetEnemyID(Random.Range(0, undamaged.Length));
        SetEnemyLife(Random.Range(3, 6));
        SetEnemyImmortal(Random.Range(0, 2));
        SetEnemySkin(undamaged[GetEnemySkin()]);
        CheckEnemySkin();
        CheckEnemyName();
        CheckEnemyImmortalSkin();
        ShowCurrentlyHealthBar();
    }

    private void SetEnemyID(int ID)
    {
        shipID = ID;
    }

    private int GetEnemySkin()
    {
        return shipID;
    }

    private void SetEnemySkin(Sprite skin)
    {
        ship.sprite = skin;
    }

    private void SetEnemyImmortalSkin(Sprite immortalSkin)
    {
        aura.sprite = immortalSkin;
    }

    private void CheckEnemySkin()
    {
        switch (GetEnemyLife())
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

    private void CheckEnemyImmortalSkin()
    {
        if (GetEnemyImmortal() == 1)
        {
            switch (GetEnemyName())
            {
                case "Light Hunter":
                    SetEnemyImmortalSkin(auras[1]);
                    break;
                case "Heavy Hunter":
                    SetEnemyImmortalSkin(auras[2]);
                    break;
                case "Crusher":
                    SetEnemyImmortalSkin(auras[3]);
                    break;
                case "Balcon Triple Heavy":
                    SetEnemyImmortalSkin(auras[4]);
                    break;
            }
        }
        else
        {
            SetEnemyImmortalSkin(auras[0]);
        }
    }

    private void CheckEnemyName()
    {
        if (shipID >= 0 && shipID <= 2)
        {
            SetEnemyName("Light Hunter");
        }
        else if (shipID >= 3 && shipID <= 5)
        {
            SetEnemyName("Heavy Hunter");
        }
        else if (shipID >= 6 && shipID <= 8)
        {
            SetEnemyName("Crusher");
        }
        else if (shipID == 9)
        {
            SetEnemyName("Balcon Triple Heavy");
        }
    }

    private void SetEnemyName(string name)
    {
        this.name = name;
    }

    private string GetEnemyName()
    {
        return name;
    }

    private void SetEnemyLife(int life)
    {
        this.life = life;
    }

    internal int GetEnemyLife()
    {
        return life;
    }

    private void SetEnemyImmortal(int immortal)
    {
        this.immortal = immortal;
        Debug.Log("IMMORTAL: " + immortal);
    }

    private int GetEnemyImmortal()
    {
        return immortal;
    }

    private void GenerateExplosion()
    {
        Vector3 explosionVector = new Vector2(transform.position.x, transform.position.y);
        Instantiate(Explosion, explosionVector, transform.rotation);
    }

    private void DestroyShip()
    {
        if (GetEnemyLife() < 1)
        {
            Destroy(gameObject);
            Destroy(blockade);
            GenerateExplosion();
            DropFromEnemy();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GetEnemyImmortal() == 0)
        {
            if (collision.gameObject.tag == "Pocisk" && GetEnemyLife() >= 1)
            {
                SetEnemyLife(GetEnemyLife() - staty.GetDamage());
                ShowCurrentlyHealthBar();
                CheckEnemySkin();
                DestroyShip();
            }
        }
        else if (GetEnemyImmortal() >= 1)
        {
            if (collision.gameObject.tag == "Pocisk")
            {
                SetEnemyImmortal(0);
                CheckEnemyImmortalSkin();
                ShowCurrentlyHealthBar();
            }
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

    private void ShowCurrentlyHealthBar()
    {
        staty.ShowCurrentlyHealthBar(healthBar, 0.2f * GetEnemyLife());
    }
}
