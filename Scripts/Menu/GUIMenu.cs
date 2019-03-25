using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GUIMenu : MonoBehaviour {
    private int page = 0; //1menu, 2tworcy, 3politykaprywatnosc

    public Canvas[] Canvases;
    private bool mute_sound;
    public Image BtnSoundOption;
    public Sprite[] ImgBtnSoundOption;
    private GUIOperations GUIOperations;

    private void Start() {
        GUIOperations = GameObject.Find("Interface").GetComponent<GUIOperations>();
        mute_sound = bool.Parse(PlayerPrefs.GetString("sound_option"));
        Sound_mute(mute_sound);
    }
    public void BtnSound()
    {
        mute_sound = !mute_sound;
        Sound_mute(mute_sound);
    }
    public void BtnOpenScene(string name_scene)
    {
        if (name_scene == "Tutorial")
        {
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            PlayerPrefs.SetInt("first_tutorial", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene(name_scene);
        }
    }
    public void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }
    public void BtnPages(int number)
    {
        if (number == 3 && PlayerPrefs.GetInt("first_tutorial") == 0)
        {
            page = number;
        }
        else if (number == 3 && PlayerPrefs.GetInt("first_tutorial") == 1)
        {
            SceneManager.LoadScene("Planet");
        }
        else if (number <= 2 && PlayerPrefs.GetInt("first_tutorial") <= 1)
        {
            page = number;
        }
    }
    private void Steer_Canvases()
    {
        for (int ilosc = 0; ilosc < Canvases.Length; ilosc++)
        {
            Canvases[ilosc].enabled = GUIOperations.Open_Canvas(page, ilosc);
        }
    }

    private void LateUpdate()
    {
        Steer_Canvases();
        Application_Quit();
    }
    private void Set_Sound_Options(float volume, int nr_img, string option)
    {
        AudioListener.volume = volume;
        BtnSoundOption.sprite = ImgBtnSoundOption[nr_img];
        PlayerPrefs.SetString("sound_option", option);
    }
    private void Sound_mute(bool mute)
    {
        if (mute == true)
        {
            Set_Sound_Options(0f, 0, "true");
        }
        else
        {
            Set_Sound_Options(1f, 1, "false");
        }
        PlayerPrefs.Save();
    }
    private void Application_Quit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        } 
    }
}


