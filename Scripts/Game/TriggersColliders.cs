using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TriggersColliders : MonoBehaviour
{
    private GUIGame menu;
    private ControlCamera controlCamera;

    public AudioClip Defeat, PowerUp;
    public GameObject ExploredMoons;
    private GUIOperations GUIOperations;
    private Skins Skins;

    private void Start()
    {
        menu = GameObject.Find("Main Camera").GetComponent<GUIGame>();
        controlCamera = GameObject.Find("Main Camera").GetComponent<ControlCamera>();
        GUIOperations = GameObject.Find("spaceship").GetComponent<GUIOperations>();
        Skins = GameObject.Find("spaceship").GetComponent<Skins>();
    }

    private void SetVisibilityShip(bool value)
    {
        GetComponent<SpriteRenderer>().enabled = value;
    }

    private IEnumerator SetPage()
    {
        yield return new WaitForSeconds(0.1f);
        menu.page = 2;
    }

    private int GetDrop()
    {
        return Random.Range(2, 6 + (menu.staty.Get_Data_From("Mining Technology") * 2));
    }

    private void OnTriggerEnter2D(Collider2D playerek)
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            if (playerek.gameObject.tag == "Scena" || playerek.gameObject.tag == "Magnetic_storm")
            {
                menu.staty.LoadedScene += 1;
            }
            if (playerek.gameObject.tag == "Moon" || playerek.gameObject.tag == "Tesla")
            {
                AddResourcesFromDropMoon();
                GUIOperations.Generate(transform.position.x, transform.position.y + 3.5f, transform.rotation, ExploredMoons);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D playerek)
    {
        if ((playerek.gameObject.tag == "Pocisk_wroga" || playerek.gameObject.tag == "kometa") && menu.staty.immortal == 1)
        {
            menu.staty.immortal = 0;
            controlCamera.Shake();
            Handheld.Vibrate();
            Debug.Log("Wykryto kolizje. Usuwam niesmiertelnosc");
        }
        else if (playerek.gameObject.tag == "Pocisk_wroga" && menu.staty.immortal == 0)
        {
            menu.staty.Life -= 1;
            controlCamera.Shake();
            GameOver();
        }
        else if (playerek.gameObject.tag == "kometa" && menu.staty.immortal == 0)
        {
            menu.staty.Life -= menu.staty.Life;
            controlCamera.Shake();
            GameOver();
        }
    }

    private void GameOver()
    {
        if (menu.staty.Life == 0)
        {
            SetVisibilityShip(false);
            Skins.ParticleOff();
            AudioSource.PlayClipAtPoint(Defeat, transform.position);
            StartCoroutine(SetPage());
            StopCoroutine(SetPage());
            Handheld.Vibrate();
            PlayerPrefs.Save();
        }
    }

    private void AddResourcesFromDropMoon()
    {
        menu.staty.AddDroppedResources(GetDrop(), GetDrop(), GetDrop(), "Explored_Moons");
    }
}
	