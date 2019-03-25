using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class fade : MonoBehaviour {
	public Animation anim;
	public string name_scene;

	void Start(){
		Time.timeScale = 1;
		anim = GetComponent<Animation>();
		anim.Play();
		StartCoroutine(Throw());
	}

	IEnumerator Throw()
	{

		yield return new WaitForSeconds(anim.clip.length);
        SceneManager.LoadScene(name_scene);

    }

}