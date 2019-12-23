using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    private statystyki stats;
    internal Animator animator;
    internal bool check;
    private Text textToShow;

    public AudioClip powerup;

    private void Start()
    {
        stats = GameObject.Find("spaceship").GetComponent<statystyki>();
        animator = GetComponent<Animator>();
        textToShow = GetComponent<Text>();
    }

    private void Update()
    {
        if ((stats.Get_Distance() == 150 || stats.Get_Distance() == 500) && !check)
        {
            ShowText("speed up", TurnText(), powerup, stats.transform.position);
            StartAnimations();
        }
        if ((stats.Get_Comets() == 50 || stats.Get_Comets() == 150) && !check)
        {
            ShowText("fast reload", TurnText(), powerup, stats.transform.position);
            StartAnimations();
        }
    }

    internal void SetText(string value)
    {
        textToShow.text = value;
    }

    internal void ShowText(string text, IEnumerator iEnumerator, AudioClip clip, Vector3 position)
    {
        SetText(text);
        StartCoroutine(iEnumerator);
        AudioSource.PlayClipAtPoint(clip, position);
    }

    internal void StartAnimations()
    {
        animator.SetBool("check", true);
        check = animator.GetBool("check");
    }

    internal IEnumerator TurnText()
    {
        yield return new WaitForSeconds(GetComponent<Animation>().clip.length);
        animator.SetBool("check", false);
        stats.Set_Data("on_off_shot", 1);
    }
}
