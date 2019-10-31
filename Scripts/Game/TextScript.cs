using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    private statystyki stats;
    private Animator animator;
    internal bool check;
    private Text txtPowerUP;

    public AudioClip powerup;

    private void Start()
    {
        stats = GameObject.Find("spaceship").GetComponent<statystyki>();
        animator = GetComponent<Animator>();
        txtPowerUP = GameObject.Find("txtPowerUp").GetComponent<Text>();
    }

    private void Update()
    {
        if ((stats.Get_Distance() == 150 || stats.Get_Distance() == 500) && !check)
        {
            StartAnimations("speed up");
        }
        if ((stats.Get_Comets() == 50 || stats.Get_Comets() == 150) && !check)
        {
            StartAnimations("fast reload");
        }
    }

    private void SetText(string text)
    {
        txtPowerUP.text = text;
    }

    private void StartAnimations(string text)
    {
        SetText(text);
        animator.SetBool("check", true);
        check = animator.GetBool("check");
        StartCoroutine(Throw());
        AudioSource.PlayClipAtPoint(powerup, stats.transform.position);
    }

    private IEnumerator Throw()
    {
        yield return new WaitForSeconds(GetComponent<Animation>().clip.length);
        animator.SetBool("check", false);
    }
}
