using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Blockade : MonoBehaviour {
	public Text tekst;
	public string txt_mission;
	
	public int mission;

	public bool check_mission = false; //true zrobione
	private bool check = false;
    private bool during = false;
	
	public Animation animacja;
	public Animator anim;
	public AudioClip powerup;

	private Generate_bullet Generate_bullet;
    private statystyki staty;
	public GameObject imgPhone;

    private void Start()
    {
        staty = GameObject.Find("spaceship").GetComponent<statystyki>();
        Generate_bullet = GameObject.Find("shot").GetComponent<Generate_bullet>();
        PlayerPrefs.SetInt("on_off_shot", 0);
        imgPhone.SetActive(false);
    }

    private void FixedUpdate()
    {
        Missions();
    }

    private void OnCollisionEnter2D(Collision2D stopowanie)
    {
        if (stopowanie.gameObject.tag == "Player")
        {
            if (mission == 1 || mission == 4)
            {
                imgPhone.SetActive(true);
            }
            anim.SetBool("check", true);
            tekst.text = txt_mission;
            check_mission = false;
            during = true;
            Debug.Log("Stopuje");
            StartCoroutine(Throw());
            AudioSource.PlayClipAtPoint(powerup, staty.transform.position);
        }
    }

	private void Missions(){
        if (mission == 1 || mission == 4)
        {
            if (check_mission == true && check == false && during == true)
            {
                Success();
                imgPhone.SetActive(false);
            }
        }
		if (mission == 6) {
			if((check_mission == false && check == false && during == true) && Generate_bullet.min_bullets == 4.0f){
                Success();
			}
		}
		if (mission == 5) {
			if((check_mission == false && check == false && during == true) && staty.Get_Comets() >= 3){
                Success();
			}
		}
		if (mission == 3) {
			if((check_mission == false && check == false && during == true) && staty.Get_Comets() == 1){
                Success();
			}
		}
		if (mission == 2) {
			if((check_mission == false && check == false && during == true) && Generate_bullet.min_bullets == 1.0f){
                Success();
			}
		}
	}

    private void Success()
    {
        if (mission < 6)
        {
            tekst.text = "mission complete";
        }
        else if (mission == 6)
        {
            tekst.text = "tutorial complete";
        }
        anim.SetBool("check", true);
        Debug.Log("mission complete" + mission);
        StartCoroutine(Skip());
        AudioSource.PlayClipAtPoint(powerup, staty.transform.position);
        check_mission = false; //ustawiam bo takto sie duplikuje sie wykonanie misji
        check = true;
        during = false;
    }
	private IEnumerator Throw()
	{
		yield return new WaitForSeconds(animacja.clip.length);
		anim.SetBool ("check", false);
        PlayerPrefs.SetInt("on_off_shot", 1);

    }
	private IEnumerator Skip()
	{
		yield return new WaitForSeconds(animacja.clip.length+1f);
		anim.SetBool ("check", false);
		PlayerPrefs.SetInt("ticks", staty.ticks); 
		PlayerPrefs.Save();
        Destroy(gameObject, 0.2f);
		PlayerPrefs.SetInt("on_off_shot", 0);
        if (PlayerPrefs.GetInt("first_tutorial") == 0)
        {
            PlayerPrefs.SetInt("first_tutorial", 1);
        }else if (PlayerPrefs.GetInt("first_tutorial") >= 1)
        {
            if (mission == 6)
            {
                SceneManager.LoadScene("Planet");
            }
        }
        

    }
}
