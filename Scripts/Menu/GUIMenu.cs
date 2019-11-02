using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIMenu : MonoBehaviour
{
    private int page = 0; //1menu, 2tworcy, 3politykaprywatnosc

    public Canvas[] Canvases;
    public Image BtnSoundOption;
    public Sprite[] ImgBtnSoundOption;

    private bool muteSound;
    private GUIOperations GUIOperations;

    private void Start()
    {
        GUIOperations = GameObject.Find("Interface").GetComponent<GUIOperations>();
        muteSound = bool.Parse(PlayerPrefs.GetString("sound_option"));
        SoundMute(muteSound);
    }

    public void BtnSound()
    {
        muteSound = !muteSound;
        SoundMute(muteSound);
    }

    public void BtnOpenScene(string scene_name)
    {
        if (scene_name == "Tutorial")
        {
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            PlayerPrefs.SetInt("first_tutorial", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene(scene_name);
        }
    }

    public void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }

    public void BtnGame(int number)
    {
        GUIOperations.CheckInternetConnection();
        if (GUIOperations.connection == true && number == 3)
        {
            if (PlayerPrefs.GetInt("first_tutorial") == 0)
            {
                page = number;
            }
            else if (PlayerPrefs.GetInt("first_tutorial") == 1)
            {
                SceneManager.LoadScene("Planet");
            }
        }
    }

    public void BtnPages(int number)
    {
        page = number;
    }

    private void LateUpdate()
    {
        GUIOperations.Steer_Canvas(Canvases, page);
        ApplicationQuit();
    }

    private void SetSoundOptions(float volume, int nr_img, string option)
    {
        AudioListener.volume = volume;
        BtnSoundOption.sprite = ImgBtnSoundOption[nr_img];
        PlayerPrefs.SetString("sound_option", option);
        PlayerPrefs.Save();
    }

    private void SoundMute(bool mute)
    {
        if (mute)
        {
            SetSoundOptions(0f, 0, "true");
        }
        else
        {
            SetSoundOptions(1f, 1, "false");
        }
    }

    private void ApplicationQuit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}