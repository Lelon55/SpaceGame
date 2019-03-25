using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour {
    // Use this for initialization

    public InputField Txt_Email;
    public InputField Txt_Password;
    public Text Txt_Message;

    public Canvas CanvasMessage;
    public AudioClip sound_message;
    public AudioSource audiosource_sound_message;

    public Text[] txt_Length;
    private void Start () {
		
	}

    // Update is called once per frame
    private void Update()
    {
        txt_Length[0].text = Txt_Email.text.Length.ToString() + "/" + Txt_Email.characterLimit;
        txt_Length[1].text = Txt_Password.text.Length.ToString() + "/" + Txt_Password.characterLimit;
    }

    private bool CheckEmail()
    {
        return Txt_Email.text.Length >= 8;
    }
    private bool CheckPassword()
    {
        return Txt_Password.text.Length >= 8;
    }
    private void View_CanvasMessage(string text)
    {
        CanvasMessage.enabled = true;
        Txt_Message.text = text;
        audiosource_sound_message.PlayOneShot(sound_message, 0.7F);
    }

    public void BtnLogin()
    {
        if(CheckEmail() == true && CheckPassword() == true)
        {
            //przenies do menu
        }
        else
        {
            View_CanvasMessage("za malo znakow");
        }
        
    }

    public void BtnRegistered()
    {
        if (CheckEmail() == true && CheckPassword() == true)
        {
            View_CanvasMessage("Registered");
        }
        else
        {
            View_CanvasMessage("za malo znakow");
        }
    }

    public void BtnLostPassword()
    {
        if (CheckEmail() == true)
        {
            View_CanvasMessage("Password was sent to your email");
        }
        else
        {
            View_CanvasMessage("za malo znakow");
        }
    }

    public void BtnClose(Canvas Canvas)
    {
        Canvas.enabled = false;
    }

}
