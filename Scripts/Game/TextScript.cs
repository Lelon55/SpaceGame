using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    private statystyki stats;
    private Animation animations;
    private Animator animator;
    internal bool check;
    private Text txtPowerUP;

    public AudioClip powerup;

    private void Start()
    {
        stats = GameObject.Find("spaceship").GetComponent<statystyki>();
        animations = GetComponent<Animation>();
        animator = GetComponent<Animator>();
        txtPowerUP = GameObject.Find("txtPowerUp").GetComponent<Text>();
    }

    private void Start_animations()
    {
        animator.SetBool("check", true);
        check = animator.GetBool("check");
        StartCoroutine(Throw());
        AudioSource.PlayClipAtPoint(powerup, stats.transform.position);
    }

    private void Update()
    {
        if ((stats.Get_Distance() == 150 || stats.Get_Distance() == 500) && !check)
        {
            Set_Text("speed up");
        }
        if ((stats.Get_Comets() == 50 || stats.Get_Comets() == 150) && !check)
        {
            Set_Text("fast reload");
        }
    }

    private void Set_Text(string text)
    {
        txtPowerUP.text = text;
        Start_animations();
    }

    private IEnumerator Throw()
    {
        yield return new WaitForSeconds(animations.clip.length);
        animator.SetBool("check", false);
    }
}
