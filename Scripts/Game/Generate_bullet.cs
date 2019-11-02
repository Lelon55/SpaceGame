using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Generate_bullet : MonoBehaviour {

	public GameObject shot;
	public AudioClip[] laser_sound;
    private float Cooling = 2.5f;
    public float MaxBullets, MinBullets;
    private SpriteRenderer LaserSkin;

    private Vector2 LaserPosition;
	private statystyki Stats;
    private ControlShip ControlShip;
	private Rigidbody2D Gravity;
    private Skins Skins;
    private SetCountdown Countdown;

    private void Start()
    {
        LaserPosition = new Vector2(0f, 0.5f);
        Stats = GameObject.Find("spaceship").GetComponent<statystyki>();
        Countdown = GameObject.Find("Main Camera").GetComponent<SetCountdown>();
        ControlShip = GameObject.Find("spaceship").GetComponent<ControlShip>();
        Skins = GameObject.Find("spaceship").GetComponent<Skins>();
        LaserSkin = GameObject.Find("laser_life").GetComponent<SpriteRenderer>();
        Gravity = GetComponent<Rigidbody2D>();
    }

    private void ShowLaserConsumption()
    {
        LaserSkin.sprite = Skins.laser.sprite;
        LaserSkin.transform.localScale = new Vector2(0.5f, MinBullets / MaxBullets);
    }

    private void ShortenReloading()
    {
        if (Stats.Get_Comets() == 50)
        {
            Cooling = 2.0f;
        }
        else if (Stats.Get_Comets() == 150)
        {
            Cooling = 1.5f;
        }
    }

    private void CheckBullets()
    {
        if (MinBullets <= 0f)
        {
            StopCoroutine("Start_Count");
            MinBullets = 0f;
        }
        else
        {
            StartCoroutine("Start_Count");
        }
    }

    private void Update()
    {
        MaxBullets = Stats.Get_Float_Data_From("Max_Lasers");
        Gravity.gravityScale = ControlShip.GetGravityBullet();
        CheckBullets();
        ShortenReloading();
    }

    private void LateUpdate()
    {
        ShowLaserConsumption();
    }

    private IEnumerator Start_Count()
    {
        while (true)
        {
            yield return new WaitForSeconds(Cooling); // czas Tick
            MinBullets--; //odejmuje wszystko po 2sek
        }
    }

    private void ShowBullet(int nr)
    {
        if (MinBullets >= MaxBullets)
        {
            //dzwiek przegrzanego dzialka
        }
        else if (MinBullets <= MaxBullets)
        {
            MinBullets++;
            Vector2 bulletPosition = new Vector2(Stats.transform.position.x + LaserPosition.x + 0.02f, Stats.transform.position.y + LaserPosition.y);
            CreateBullet(shot, bulletPosition, laser_sound[nr], Stats.transform.position);
        }
    }

    internal void CreateBullet(GameObject bullet, Vector2 bulletPosition, AudioClip laserSound, Vector3 soundPosition)
    {
        Instantiate(bullet, bulletPosition, bullet.transform.rotation);
        AudioSource.PlayClipAtPoint(laserSound, soundPosition);
    }

    public void ShootBullet()
    {
        if (Countdown.doCountdown && (SceneManager.GetActiveScene().name == "Game"))
        {
            ShowBullet(Random.Range(0, 3));
        }
        if (Countdown.doCountdown && Stats.Get_Data_From("on_off_shot") == 1 && (SceneManager.GetActiveScene().name == "Tutorial"))
        {
            ShowBullet(Random.Range(0, 3));
        }
    }
}
