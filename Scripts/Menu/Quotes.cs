using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;

public class Quotes : MonoBehaviour
{
    private Animation anim;
    [SerializeField] private string name_scene;
    private int nr_quotes;
    public Text quotes, author;

    private class List_Quotes
    {
        public string quotes, author;

        public List_Quotes(string q, string a)
        {
            this.quotes = q;
            this.author = a;
        }
    }

    private List<List_Quotes> cytaty = new List<List_Quotes>();

    private void Start()
    {
        Time.timeScale = 1;
        anim = GetComponent<Animation>();
        anim.Play();
        StartCoroutine(Throw());

        cytaty.Add(new List_Quotes("life would be tragic if it weren't funny", "stephen hawking"));
        cytaty.Add(new List_Quotes("we are all now connected by the internet, like neurons in a giant brain", "stephen hawking"));
        cytaty.Add(new List_Quotes("intelligence is the ability to adapt to change", "stephen hawking"));
        cytaty.Add(new List_Quotes("that's one small step for a man, one giant leap for mankind", "neil armstrong"));
        cytaty.Add(new List_Quotes("the universe is under no obligation to make sense to you", "neil degrasse tyson"));

        nr_quotes = Random.Range(0, 5);
        quotes.text = cytaty[nr_quotes].quotes;
        author.text = cytaty[nr_quotes].author;
    }

    private IEnumerator Throw()
    {
        yield return new WaitForSeconds(anim.clip.length);
        SceneManager.LoadScene(name_scene);
    }
}