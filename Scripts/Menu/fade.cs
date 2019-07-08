using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class fade : MonoBehaviour {
	private Animation anim;
	[SerializeField] private string name_scene;

	private void Start(){
		Time.timeScale = 1;
		anim = GetComponent<Animation>();
		anim.Play();
		StartCoroutine(Throw());
	}

	private IEnumerator Throw()
	{
		yield return new WaitForSeconds(anim.clip.length);
        SceneManager.LoadScene(name_scene);
    }

}