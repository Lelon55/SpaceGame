using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetCountdown : MonoBehaviour {

    private float countdown = 3f;
    internal bool doCountdown;
    public Canvas CanvasCountdown;
    public Text TextCountdown;

    private void Update()
    {
        DoCountdown();
    }

    private void DoCountdown()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            if (countdown > 1f)
            {
                CanvasCountdown.enabled = true;
                countdown -= Time.deltaTime;
                TextCountdown.text = countdown.ToString("N0");
            }
            else if (countdown <= 1f)
            {
                CanvasCountdown.enabled = false;
                countdown = 0f;
                doCountdown = true;
            }
        }

    }
}
