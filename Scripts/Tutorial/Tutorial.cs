using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour {
    [SerializeField] private TextScript textScript;
    [SerializeField] private string txtMission;

    public int nr_mission = 0 ;
    [SerializeField] private int value_comets;
    [SerializeField] private float value_bullets;
    [SerializeField] private bool value_wall;

    [SerializeField] private bool _done = false;
    [SerializeField] internal bool wall = false; 

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
            textScript.ShowText(txtMission, textScript.TurnText(), textScript.powerup, staty.transform.position);
            textScript.StartAnimations();
            staty.mission += 1;
        }
    }

    private void FixedUpdate()
    {
        CheckMission(staty.mission, value_bullets, value_comets, value_wall);
    }

    private void CheckMission(int _nr_mission, float _value_bullets, int _value_comets, bool _wall)
    {
        if (nr_mission == _nr_mission && _done == false && Generate_bullet.MinBullets >= _value_bullets && staty.Get_Comets() >= _value_comets && _wall == wall)
        {
            Success();
        }
    }

    private string ReturnText()
    {
        if (nr_mission >= 6)
        {
            return "tutorial complete";
        }
        return "mission complete";
    }

    private void Success()
    {
        textScript.ShowText(ReturnText(), SkipTutorial(), textScript.powerup, staty.transform.position);
        textScript.StartAnimations();
        StartCoroutine(SkipTutorial());
        _done = true;
    }

    private IEnumerator SkipTutorial()
    {
        yield return new WaitForSeconds(textScript.GetComponent<Animation>().clip.length + 1f);
        textScript.animator.SetBool("check", false);
        staty.Set_Data("on_off_shot", 0);
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
                staty.Set_Data("ticks", staty.ticks);
                PlayerPrefs.Save();
            }
        }
    }
}