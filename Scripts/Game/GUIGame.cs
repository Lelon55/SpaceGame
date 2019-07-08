using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GUIGame : MonoBehaviour {
    public Canvas[] Canvases;
    public Text[] Score;
    private SpriteRenderer laser_life;

    internal int page; // 1gra, 2PAUZA, 3Gameover
    internal statystyki staty;
    internal Generate_bullet generate;
    public Skins skins;
    public AudioSource[] stopowanie_tla; //stopuje dzwiek w tle

	public GUIStyle strzelam;
	
	private bool change_stats;
    private float ticks = 1;
	public Animator[] anim;
	public Text[] txt_dropped_resources;
    private GUIOperations GUIOperations;

    // Use this for initialization
    private void Start () {
        staty = GameObject.Find("spaceship").GetComponent<statystyki>();
        generate = GameObject.Find("shot").GetComponent<Generate_bullet>();
        GUIOperations = GameObject.Find("Interface").GetComponent<GUIOperations>();
        laser_life = GameObject.Find("laser_life").GetComponent<SpriteRenderer>();
        page = 0;
	}

    private void LateUpdate()
    {
        laser_life.sprite = skins.laser.sprite;
        Current_Laser();
        Pages();
    }

    // Update is called once per frame
    private void Update () {
        Game_stop();
		Change_panel();
    }

    private void Current_Laser()
    {
        laser_life.transform.localScale = new Vector2(0.5f, generate.min_bullets / generate.max_bullets);
    }

    // dane z obszaru gry
    private void GameScore()
    {
        Score[0].text = staty.Get_Score() + "/" + staty.Get_Data_From("Player_Record");
        Score[1].text = staty.Get_Comets() + "/" + staty.Get_Data_From("Comets_Record");
        Score[2].text = staty.Get_Distance().ToString("N0");
		txt_dropped_resources[0].text = staty.Get_Dropped_Metal().ToString();
		txt_dropped_resources[1].text = staty.Get_Dropped_Crystal().ToString();
		txt_dropped_resources[2].text = staty.Get_Dropped_Deuter().ToString();
		txt_dropped_resources[3].text = staty.Get_Dropped_Antymatery().ToString();
        txt_dropped_resources[4].text = staty.Get_Dropped_Metal().ToString();
        txt_dropped_resources[5].text = staty.Get_Dropped_Crystal().ToString();
        txt_dropped_resources[6].text = staty.Get_Dropped_Deuter().ToString();
        txt_dropped_resources[7].text = staty.Get_Dropped_Antymatery().ToString();
    }

    public void BtnPages(int number)
    {
        page = number;
    }

    private void Gameplay_datas_to_save()
    {
        staty.Set_Data("Metal", staty.Get_Data_From("Metal") + staty.Get_Dropped_Metal());
        staty.Set_Data("Crystal", staty.Get_Data_From("Crystal") + staty.Get_Dropped_Crystal());
        staty.Set_Data("Deuter", staty.Get_Data_From("Deuter") + staty.Get_Dropped_Deuter());
        staty.Set_Data("Destroyed_Comets", staty.Get_Data_From("Destroyed_Comets") + staty.Get_Comets());
        staty.Change_Antymatery(staty.Get_Dropped_Antymatery());

    }
    public void BtnOpenScene(string name_scene)
    {
        SceneManager.LoadScene(name_scene);
        staty.Set_Data("ticks", staty.ticks); // to daja gdzies przy zmianie sceny
        Gameplay_datas_to_save();
        PlayerPrefs.Save();
    }
    public void BtnRestart()
    {
        Handheld.Vibrate();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        staty.Set_Data("ticks", staty.ticks); // to daja gdzies przy zmianie sceny
    }
    private void OnGUI()
    {
        if (page == 0)
        {//gra
            if (staty.Get_Data_From("on_off_shot") == 1 && (SceneManager.GetActiveScene().name == "Tutorial"))
            {
                if (GUI.Button(new Rect(0, Screen.height * 0.1f, Screen.width, Screen.height * 0.8f), "", strzelam))
                {
                    Debug.Log("strzelam");
                    generate.Shoot_bullet();
                }
            }
            if (SceneManager.GetActiveScene().name == "Game")
            {
                if (GUI.Button(new Rect(0, Screen.height * 0.1f, Screen.width, Screen.height * 0.8f), "", strzelam))
                {
                    Debug.Log("strzelam");
                    generate.Shoot_bullet();
                }
            }
        }
        else if (page == 2)
        { // GameOver
            Sound_stop();
        }
    }

    private void Game_stop()
    {
        if (page >= 1)
        {
            Time.timeScale = 0;
        }
        else if (page == 0)
        {
            Time.timeScale = 1;
        }

    }
    private void Sound_stop()
    {
        stopowanie_tla[0].Stop(); //stopuje tlo
        stopowanie_tla[1].Stop(); //stopuje silnik
    }
   
    private void Pages()
    {
        GameScore();
        GUIOperations.Steer_Canvas(Canvases, page);
    }
    private void Change_panel()
    {
        ticks += Time.deltaTime;
        if (ticks >= 6.0f)
        { //bo od 1 do 6 jest 5 tickniec
            change_stats = !change_stats;
            anim[0].SetBool("panel", change_stats);
            anim[1].SetBool("panel", change_stats);
            ticks = 1f;
        }
    }
}
