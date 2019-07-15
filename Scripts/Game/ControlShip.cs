using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlShip : MonoBehaviour
{
    [SerializeField] private Tutorial[] tutorial;
    public Vector2 speed = new Vector2(10, 0); //predkosc statku dla rigidbody
    private Vector2 movement;
    private Rigidbody2D gravity_ship;
    private statystyki staty;

    private void Start()
    {
        staty = GameObject.Find("spaceship").GetComponent<statystyki>();
        gravity_ship = GetComponent<Rigidbody2D>();
        speed = new Vector2(PlayerPrefs.GetInt("Speed_Ship"), 0);
    }

    private void OnTriggerStay2D(Collider2D playerek)
    {
        if (playerek.gameObject.tag == "Scena")
        {
            float inputX = Input.acceleration.x;

            movement = new Vector2(
            speed.x * inputX, 0);

            if (Input.GetKey("left"))
            {
                movement.x = speed.x * -0.2f;
            }
            if (Input.GetKey("right"))
            {
                movement.x = speed.x * 0.2f;
            }
        }

        else if (playerek.gameObject.tag == "Magnetic_storm")
        {
            float inputX = -Input.acceleration.x;

            movement = new Vector2(
            speed.x * inputX, 0);

            if (Input.GetKey("left"))
            {
                movement.x = speed.x * 0.2f;
            }
            if (Input.GetKey("right"))
            {
                movement.x = speed.x * -0.2f;
            }
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

    private void Update()
    {
        gravity_ship.velocity = movement;
    }
}
