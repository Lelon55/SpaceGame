using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TriggersColliders : MonoBehaviour
{
    private GUIGame menu;
    private Shake_Camera shake;

    public AudioClip Defeat, PowerUp;
    public GameObject ExploredMoons;
    private GUIOperations GUIOperations;
    private Skins Skins;

    private void Start()
    {
        menu = GameObject.Find("Main Camera").GetComponent<GUIGame>();
        shake = GameObject.Find("Main Camera").GetComponent<Shake_Camera>();
        GUIOperations = GameObject.Find("spaceship").GetComponent<GUIOperations>();
        Skins = GameObject.Find("spaceship").GetComponent<Skins>();
    }

    private void SetVisibilityShip(bool value)
    {
        GetComponent<SpriteRenderer>().enabled = value;
    }

    private IEnumerator Count()
    {
        yield return new WaitForSeconds(0.1f);
        menu.page = 2;
    }

    private int GetDrop()
    {
        return Random.Range(2, 6 + (menu.staty.Get_Data_From("Mining Technology") * 2));
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            if (player.gameObject.tag == "Scena" || player.gameObject.tag == "Magnetic_storm")
            {
                menu.staty.LoadedScene += 1;
            }
            if (player.gameObject.tag == "Moon" || player.gameObject.tag == "Tesla")
            {
                DropFromMoon();
                GUIOperations.Generate(transform.position.x, transform.position.y + 3.5f, transform.rotation, ExploredMoons);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D player)
    {
        if ((player.gameObject.tag == "Pocisk_wroga" || player.gameObject.tag == "kometa") && menu.staty.immortal == 1)
        {
            menu.staty.immortal = 0;
            shake.ShakeCamera();
            Handheld.Vibrate();
            Debug.Log("Wykryto kolizje. Usuwam niesmiertelnosc");
        }
        else if (player.gameObject.tag == "Pocisk_wroga" && menu.staty.immortal == 0)
        {
            menu.staty.Life -= 1;
            shake.ShakeCamera();
            GameOver();
        }
        else if (player.gameObject.tag == "kometa" && menu.staty.immortal == 0)
        {
            menu.staty.Life -= menu.staty.Life;
            shake.ShakeCamera();
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
            StartCoroutine(Count());
            StopCoroutine(Count());
            Handheld.Vibrate();
            PlayerPrefs.Save();
        }
    }

    private void DropFromMoon()
    {
        menu.staty.AddDroppedResources(GetDrop(), GetDrop(), GetDrop(), "ExploredMoons");
    }
}
	