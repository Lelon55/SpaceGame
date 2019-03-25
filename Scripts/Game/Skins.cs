using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Skins : MonoBehaviour {

	private statystyki staty;
    public SpriteRenderer aura;
    public Sprite[] auras;
	[Tooltip("Ilosc Particli tego nie zmieniamy")]
	public ParticleSystem[] ps;

	public SpriteRenderer statek;
	public SpriteRenderer laser;
	private bool done = false;
	public Sprite[] skin_laseru;

	public Sprite[] skin_statku;
    public Sprite[] podniszczony_statek;
    public Sprite[] podniszczony_bardziej;

	public GameObject czastki_statek1; //particle do statku 1
	public GameObject czastki_statek2; //particle do statku 2
	public GameObject czastki_statek3; //particle do statku 3

    private void Start()
    {
        staty = GameObject.Find("spaceship").GetComponent<statystyki>();
    }
    private void LateUpdate() {
    if (SceneManager.GetActiveScene().name != "Menu")
        {
            Check_immortal();
            Check_Sprite_Ship();
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            if (staty.Get_Distance() >= 150)
            {
                if (staty.Get_Data_From("Ship_Id") == 1 || staty.Get_Data_From("Ship_Id") == 4 || staty.Get_Data_From("Ship_Id") == 5)
                {
                    ps = czastki_statek1.GetComponentsInChildren<ParticleSystem>();
                    czastki_statek1.SetActive(true); // zalaczam i sie rozwija
                }
                else if (staty.Get_Data_From("Ship_Id") == 2 || staty.Get_Data_From("Ship_Id") == 6 || staty.Get_Data_From("Ship_Id") == 7)
                {
                    ps = czastki_statek2.GetComponentsInChildren<ParticleSystem>();
                    czastki_statek2.SetActive(true); // zalaczam i sie rozwija
                }
                else if (staty.Get_Data_From("Ship_Id") == 3 || staty.Get_Data_From("Ship_Id") == 8 || staty.Get_Data_From("Ship_Id") == 9)
                {
                    ps = czastki_statek3.GetComponentsInChildren<ParticleSystem>();
                    czastki_statek3.SetActive(true); // zalaczam i sie rozwija
                }
            }
            else if (staty.Get_Distance() < 150)
            {
                czastki_statek1.SetActive(false);
                czastki_statek2.SetActive(false);
                czastki_statek3.SetActive(false);
            }
        }
    }
    private void Check_immortal()
    {
        if (staty.immortal == 1)
        {
            if (staty.Get_String_Data_From("Ship_Name") == "Light Hunter")
            {
                aura.sprite = auras[1];
            }
            else if (staty.Get_String_Data_From("Ship_Name") == "Heavy Hunter")
            {
                aura.sprite = auras[2];
            }
            else if (staty.Get_String_Data_From("Ship_Name") == "Crusher")
            {
                aura.sprite = auras[3];
            }
            else if (staty.Get_String_Data_From("Ship_Name") == "Balcon Triple Heavy")
            {
                aura.sprite = auras[4];
            }
        }
        else if (staty.immortal == 0)
        {
            aura.sprite = auras[0];
        }
    }
    private void Check_Sprite_Ship()
    {
        if (done == false)
        {
            done = true;

            laser.sprite = skin_laseru[staty.Get_Data_From("Laser")];
            Debug.Log("Wczytano laser nr: " + staty.Get_Data_From("Laser"));

            statek.sprite = skin_statku[staty.Get_Data_From("Ship_Id")];
            Debug.Log("Wczytano statek nr: " + staty.Get_Data_From("Ship_Id"));
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pocisk_wroga")
        {
            if (staty.Life == 1)
            {
                statek.sprite = podniszczony_bardziej[staty.Get_Data_From("Ship_Id")];
                Debug.Log("Wczytano podniszczony bardziej statek nr: " + staty.Get_Data_From("Ship_Id"));
            }
            else if (staty.Life == 2)
            {
                statek.sprite = podniszczony_statek[staty.Get_Data_From("Ship_Id")];
                Debug.Log("Wczytano podniszczony statek nr: " + staty.Get_Data_From("Ship_Id"));
            }
            else if (staty.Life >= 3)
            {
                statek.sprite = skin_statku[staty.Get_Data_From("Ship_Id")];
                Debug.Log("Wczytano statek nr: " + staty.Get_Data_From("Ship_Id"));
            }
        }
    }
}


