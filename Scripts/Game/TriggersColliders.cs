﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TriggersColliders : MonoBehaviour
{
    private GUIGame menu;
    private SpriteRenderer imgShip;
    private Shake_Camera shake;

    public AudioClip Defeat, PowerUp;
    public GameObject ExploredMoons;
    private GUIOperations GUIOperations;

    private void Start()
    {
        imgShip = GetComponent<SpriteRenderer>();
        menu = GameObject.Find("Main Camera").GetComponent<GUIGame>();
        shake = GameObject.Find("Main Camera").GetComponent<Shake_Camera>();
        GUIOperations = GameObject.Find("spaceship").GetComponent<GUIOperations>();
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
                DropFromMoon();
                GUIOperations.Generate(transform.position.x, transform.position.y + 3.5f, transform.rotation, ExploredMoons);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D playerek)
    {
        if ((playerek.gameObject.tag == "Pocisk_wroga" || playerek.gameObject.tag == "kometa") && menu.staty.immortal == 1)
        {
            menu.staty.immortal = 0;
            shake.ShakeCamera();
            Handheld.Vibrate();
            Debug.Log("Wykryto kolizje. Usuwam niesmiertelnosc");
        }
        else if (playerek.gameObject.tag == "Pocisk_wroga" && menu.staty.immortal == 0)
        {
            menu.staty.Life -= 1;
            shake.ShakeCamera();
            if (menu.staty.Life == 0)
            {
                GameOver();
            }
        }
        else if (playerek.gameObject.tag == "kometa" && menu.staty.immortal == 0)
        {
            menu.staty.Life -= menu.staty.Life;
            if (menu.staty.Life == 0)
            {
                GameOver();
                shake.ShakeCamera();
            }
        }
    }

    private void GameOver()
    {
        imgShip.enabled = false;
        AudioSource.PlayClipAtPoint(Defeat, transform.position);
        StartCoroutine(Count());
        StopCoroutine(Count());
        Handheld.Vibrate();
        Debug.Log("Wykryto kolizje");
        PlayerPrefs.Save();
    }

    private void DropFromMoon()
    {
        menu.staty.Add_Dropped_Metal(GetDrop());
        menu.staty.Add_Dropped_Crystal(GetDrop());
        menu.staty.Add_Dropped_Deuter(GetDrop());
        menu.staty.Set_Data("Explored_Moons", menu.staty.Get_Data_From("Explored_Moons") + 1);
    }
}
	