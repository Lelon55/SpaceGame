using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GUIGame : MonoBehaviour
{
    public Canvas[] Canvases;
    public Text[] Score, txt_dropped_resources;

    internal int page = 0; // 0gra, 1PAUZA, 2Gameover
    internal statystyki staty;
    public AudioSource[] stopowanie_tla; //stopuje dzwiek w tle

    private bool changePanel;
    private float ticks = 1f;
    public Animator[] anim;
    private GUIOperations GUIOperations;

    private void Start()
    {
        staty = GameObject.Find("spaceship").GetComponent<statystyki>();
        GUIOperations = GameObject.Find("Interface").GetComponent<GUIOperations>();
    }

    private void LateUpdate()
    {
        Pages();
    }

    private void Update()
    {
        if (page == 2)
        {
            SoundStop();
        }
        GameStop();
        ChangePanel();
    }

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

    private void GameplayDatasToSave()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            staty.Set_Data("Metal", staty.Get_Data_From("Metal") + staty.Get_Dropped_Metal());
            staty.Set_Data("Crystal", staty.Get_Data_From("Crystal") + staty.Get_Dropped_Crystal());
            staty.Set_Data("Deuter", staty.Get_Data_From("Deuter") + staty.Get_Dropped_Deuter());
            staty.Set_Data("Destroyed_Comets", staty.Get_Data_From("Destroyed_Comets") + staty.Get_Comets());
            staty.Change_Antymatery(staty.Get_Dropped_Antymatery());
        }
    }

    public void BtnOpenScene(string name_scene)
    {
        SceneManager.LoadScene(name_scene);
        staty.Set_Data("ticks", staty.ticks);
        GameplayDatasToSave();
        PlayerPrefs.Save();
    }

    public void BtnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        staty.Set_Data("ticks", staty.ticks);
    }

    private void GameStop()
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

    private void SoundStop()
    {
        stopowanie_tla[0].Stop(); //stopuje tlo
        stopowanie_tla[1].Stop(); //stopuje silnik
    }

    private void Pages()
    {
        GameScore();
        GUIOperations.Steer_Canvas(Canvases, page);
    }

    private void ChangePanel()
    {
        ticks += Time.deltaTime;
        if (ticks >= 6.0f)
        { //bo od 1 do 6 jest 5 tickniec
            changePanel = !changePanel;
            anim[0].SetBool("panel", changePanel);
            anim[1].SetBool("panel", changePanel);
            ticks = 1f;
        }
    }
}
