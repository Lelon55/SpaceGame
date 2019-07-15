using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;

public class Quotes : MonoBehaviour {
	private Animation anim;
	[SerializeField] private string name_scene;
	private int nr_quotes;
    public Text quotes, author;

    private class List_Quotes
    {
        public int id;
        public string quotes, author;

        public List_Quotes(int i, string q, string a)
        {
            this.id = i;
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

        cytaty.Add(new List_Quotes(1, "life would be tragic if it weren't funny", "stephen Hawking"));
        cytaty.Add(new List_Quotes(2, "we are all now connected by the internet, like neurons in a giant brain", "stephen hawking"));
        cytaty.Add(new List_Quotes(3, "intelligence is the ability to adapt to change", "stephen hawking"));
        cytaty.Add(new List_Quotes(4, "that's one small step for a man, one giant leap for mankind", "neil armstrong"));
        cytaty.Add(new List_Quotes(5, "the universe is under no obligation to make sense to you", "neil degrasse tyson"));

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