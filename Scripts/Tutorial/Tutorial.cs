using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour {
    [SerializeField] private Text TxtMission;
    [SerializeField] private string txtMission;

    public int nr_mission = 0 ;
    [SerializeField] private int value_comets;
    [SerializeField] private float value_bullets;
    [SerializeField] private bool value_wall;

    [SerializeField] private bool _done = false;
    [SerializeField] internal bool wall = false; 

    public Animation animacja;
    public Animator anim;
    public AudioClip powerup;

    private Generate_bullet Generate_bullet;
    private statystyki staty;

    private void Start()
    {
        staty = GameObject.Find("spaceship").GetComponent<statystyki>();
        Generate_bullet = GameObject.Find("shot").GetComponent<Generate_bullet>();
        staty.Set_Data("on_off_shot", 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ShowText(txtMission);
            staty.mission += 1;
        }
    }

    private void FixedUpdate()
    {
        CheckMission(staty.mission, value_bullets, value_comets, value_wall);
    }

    private void ShowText(string _txtMission)
    {
        TxtMission.text = _txtMission;
        anim.SetBool("check", true);
        StartCoroutine(TurnText());
        AudioSource.PlayClipAtPoint(powerup, staty.transform.position);
    }

    private string ReturnText()
    {
        if (nr_mission >= 6)
        {
            return "tutorial complete";
        }
        return "mission complete";
    }

    private void CheckMission(int _nr_mission, float _value_bullets, int _value_comets, bool _wall)
    {
        if (nr_mission == _nr_mission && _done == false && Generate_bullet.MinBullets >= _value_bullets && staty.Get_Comets() >= _value_comets && _wall == wall)
        {
            Success();
        }
    }

    private void Success()
    {
        ShowText(ReturnText());
        _done = true;
        StartCoroutine(SkipTutorial());
        AudioSource.PlayClipAtPoint(powerup, staty.transform.position);
    }

    private IEnumerator TurnText()
    {
        yield return new WaitForSeconds(animacja.clip.length);
        anim.SetBool("check", false);
        staty.Set_Data("on_off_shot", 1);
    }

    private IEnumerator SkipTutorial()
    {
        yield return new WaitForSeconds(animacja.clip.length + 1f);
        anim.SetBool("check", false);
        staty.Set_Data("ticks", staty.ticks);
        staty.Set_Data("on_off_shot", 0);
        PlayerPrefs.Save();
        Destroy(gameObject, 0.2f);
        NextScene();
    }

    private void NextScene()
    {
        if (staty.Get_Data_From("first_tutorial") == 0)
        {
            staty.Set_Data("first_tutorial", 1);
        }
        else if (staty.Get_Data_From("first_tutorial") >= 1)
        {
            if (nr_mission == 6)
            {
                SceneManager.LoadScene("Planet");
            }
        }
    }
}
