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

    private void Start () {
        staty = GameObject.Find("spaceship").GetComponent<statystyki>();
        Generate_bullet = GameObject.Find("shot").GetComponent<Generate_bullet>();
        staty.Set_Data("on_off_shot", 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Show_text(txtMission);
            staty.mission += 1;
        }
    }

    private void FixedUpdate () {
        Check_Mission(staty.mission, value_bullets, value_comets, value_wall);
	}

    private void Show_text(string _txtMission)
    {
        TxtMission.text = _txtMission;
        anim.SetBool("check", true);
        StartCoroutine(Turn_Text());
    }

    private void Check_Mission(int _nr_mission, float _value_bullets, int _value_comets, bool _wall)
    {
        if (nr_mission == _nr_mission && _done == false && Generate_bullet.min_bullets >= _value_bullets && staty.Get_Comets() >= _value_comets && _wall == wall)
        {
           Success();
        }
    }

    private void Success()
    {
        if (nr_mission < 6)
        {
            Show_text("mission complete");
        }
        else if (nr_mission == 6)
        {
            Show_text("tutorial complete");
        }
        _done = true;
        StartCoroutine(Skip_Tutorial());
        AudioSource.PlayClipAtPoint(powerup, staty.transform.position);
    }

    private IEnumerator Turn_Text()
    {
        yield return new WaitForSeconds(animacja.clip.length);
        anim.SetBool("check", false);
        staty.Set_Data("on_off_shot", 1);
    }

    private IEnumerator Skip_Tutorial()
    {
        yield return new WaitForSeconds(animacja.clip.length + 1f);
        anim.SetBool("check", false);
        staty.Set_Data("ticks", staty.ticks);
        staty.Set_Data("on_off_shot", 0);
        PlayerPrefs.Save();
        Destroy(gameObject, 0.2f);
        Next_Scene();
    }

    private void Next_Scene()
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
