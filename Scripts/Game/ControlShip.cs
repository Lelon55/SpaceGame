using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlShip : MonoBehaviour
{
    [SerializeField] private Tutorial[] tutorial;

    private Vector2 movement;
    internal Rigidbody2D gravityShip;
    private float gravityBullet;
    private statystyki staty;
    private SetCountdown SetCountdown;

    private void Start()
    {
        staty = GameObject.Find("spaceship").GetComponent<statystyki>();
        SetCountdown = GameObject.Find("Main Camera").GetComponent<SetCountdown>();
        gravityShip = GetComponent<Rigidbody2D>();
        SetShipSettings();
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
    }

    private Vector2 SetSteer(float x, float y)
    {
        return new Vector2(x, y);
    }

    private Vector2 GetSteer()
    {
        if (staty.Get_Data_From("Life") == 1 && staty.Get_String_Data_From("Ship_Name") != "Light Hunter")
        {
            return SetSteer(7, 0);
        }
        return SetSteer(staty.Get_Data_From("Speed_Ship"), 0);
    }

    private void SetDirection(float input, float left, float right)
    {
        movement = new Vector2(GetSteer().x * input, 0);

        if (Input.GetKey("left"))
        {
            movement.x = GetSteer().x * left;
        }
        if (Input.GetKey("right"))
        {
            movement.x = GetSteer().x * right;
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

    private void SetGravityShip(float value)
    {
        gravityShip.gravityScale = value;
    }

    internal float GetGravityBullet()
    {
        return gravityBullet;
    }

    private void SetGravityBullet(float GravityBullet)
    {
        gravityBullet = GravityBullet;
    }

    private void SetSpeedShipBullet()
    {
        if (SetCountdown.doCountdown)
        {
            switch (staty.Get_Distance())
            {
                case 150:
                    SetGravityShip(-15f);
                    SetGravityBullet(-2f);
                    break;
                case 500:
                    SetGravityShip(-20f);
                    SetGravityBullet(-2.5f);
                    break;
                case 0:
                    SetGravityShip(-10f);
                    SetGravityBullet(-1f);
                    break;
            }
        }
    }

    private void ShipMove()
    {
        gravityShip.velocity = movement;
    }

    private void Update()
    {
        ShipMove();
        SetSpeedShipBullet();
        GetSteer();
    }
}
