using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Skins : MonoBehaviour {

	private statystyki staty;
    private bool done = false;

    [Tooltip("Ilosc Particli tego nie zmieniamy")]
	public ParticleSystem[] ps;
    public GameObject[] ParticleShip;
    public SpriteRenderer aura, ship, laser;
    public Sprite[] auras, skin_laseru, skin_statku, podniszczony_ship, podniszczony_bardziej;

    private void Start()
    {
        staty = GameObject.Find("spaceship").GetComponent<statystyki>();
    }

    private void LateUpdate()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            CheckImmortal();
            CheckSpriteShip();
        }
    }

    private void SetParcticle(int number)
    {
        ps = ParticleShip[number].GetComponentsInChildren<ParticleSystem>();
        ParticleShip[number].SetActive(true);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            if (staty.Get_Distance() >= 150)
            {
                switch (staty.Get_String_Data_From("Ship_Name"))
                {
                    case "Light Hunter":
                    case "Balcon Triple Heavy":
                        SetParcticle(0);
                        break;
                    case "Heavy Hunter":
                        SetParcticle(1);
                        break;
                    case "Crusher":
                        SetParcticle(2);
                        break;
                }
            }
            else if (staty.Get_Distance() < 150)
            {
                ParticleShip[0].SetActive(false);
                ParticleShip[1].SetActive(false);
                ParticleShip[2].SetActive(false);
            }
        }
    }

    private void CheckImmortal()
    {
        if (staty.immortal == 1)
        {
            switch (staty.Get_String_Data_From("Ship_Name"))
            {
                case "Light Hunter":
                    aura.sprite = auras[1];
                    break;
                case "Heavy Hunter":
                    aura.sprite = auras[2];
                    break;
                case "Crusher":
                    aura.sprite = auras[3];
                    break;
                case "Balcon Triple Heavy":
                    aura.sprite = auras[4];
                    break;
            }
        }
        else if (staty.immortal == 0)
        {
            aura.sprite = auras[0];
        }
    }

    private void CheckSpriteShip()
    {
        if (!done)
        {
            done = true;
            laser.sprite = skin_laseru[staty.Get_Data_From("Laser")];
            ship.sprite = skin_statku[staty.Get_Data_From("Ship_Id")];
        }
    }

    private Sprite SetCollisionSkin(Sprite[] CollisionSkin, int id)
    {
        return CollisionSkin[id];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pocisk_wroga")
        {
            switch (staty.Life)
            {
                case 1:
                    ship.sprite = SetCollisionSkin(podniszczony_bardziej, staty.Get_Data_From("Ship_Id"));
                    break;
                case 2:
                    ship.sprite = SetCollisionSkin(podniszczony_ship, staty.Get_Data_From("Ship_Id"));
                    break;
                case 3:
                    ship.sprite = SetCollisionSkin(skin_statku, staty.Get_Data_From("Ship_Id"));
                    break;
            }
        }
    }
}