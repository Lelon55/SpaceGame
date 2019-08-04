using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlShip : MonoBehaviour
{
    [SerializeField] private Tutorial[] tutorial;
    public Vector2 Steer = new Vector2(10, 0); //predkosc statku dla rigidbody
    private Vector2 movement;
    internal Rigidbody2D gravity_ship;
    internal float gravity_bullet = -1.0f;
    private statystyki staty;
    private SetCountdown SetCountdown;

    private void Start()
    {
        staty = GameObject.Find("spaceship").GetComponent<statystyki>();
        SetCountdown = GameObject.Find("Main Camera").GetComponent<SetCountdown>();
        gravity_ship = GetComponent<Rigidbody2D>();
        SetShipSettings();

    }

    private void SetSteer()
    {
        if (staty.Get_Data_From("Life") == 1 && staty.Get_String_Data_From("Ship_Name") != "Light Hunter")
        {
            Steer.x = 7;
        }
        else
        {
            Steer.x = staty.Get_Data_From("Speed_Ship");
        }
    }

    private Vector2 SetCollider()
    {
        if (staty.Get_String_Data_From("Ship_Name") != "Balcon Triple Heavy")
        {
            return new Vector2(3f, 2.3f);
        }
        return new Vector2(1.5f, 2f);
    }

    private void SetShipSettings()
    {
        GetComponent<BoxCollider2D>().size = SetCollider();
        SetSteer();
    }

    private void SetDirection(float input, float left, float right)
    {
        movement = new Vector2(Steer.x * input, 0);

        if (Input.GetKey("left"))
        {
            movement.x = Steer.x * left;
        }
        if (Input.GetKey("right"))
        {
            movement.x = Steer.x * right;
        }
    }

    private void OnTriggerStay2D(Collider2D playerek)
    {
        if (playerek.gameObject.tag == "Scena")
        {
            SetDirection(Input.acceleration.x, -0.2f, 0.2f);
        }
        else if (playerek.gameObject.tag == "Magnetic_storm")
        {
            SetDirection(-Input.acceleration.x, 0.2f, -0.2f);
        }
    }

    private void OnCollisionEnter2D(Collision2D mission_tutorial)
    {
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            if (mission_tutorial.gameObject.tag == "sciana" && (staty.mission == 1 || staty.mission == 4))
            {
                tutorial[staty.mission - 1].wall = true;
            }
        }
    }

    private void SetGravity(float gravityShip, float gravityBullet)
    {
        gravity_ship.gravityScale = gravityShip;
        gravity_bullet = gravityBullet;
    }

    private void SetSpeedShipBullet()
    {
        if (SetCountdown.doCountdown)
        {
            switch (staty.Get_Distance())
            {
                case 150:
                    SetGravity(-15f, -2f);
                    break;
                case 500:
                    SetGravity(-20f, -2.5f);
                    break;
                case 0:
                    SetGravity(-10f, -1f);
                    break;
            }
        }
    }

    private void Update()
    {
        gravity_ship.velocity = movement;
        SetSpeedShipBullet();
        SetSteer();
    }
}
