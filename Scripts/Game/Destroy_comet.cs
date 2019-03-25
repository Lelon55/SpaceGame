using UnityEngine;
using System.Collections;

public class Destroy_comet : MonoBehaviour {

    public int life = 0;
    public Sprite podniszczone;
	public Sprite niezniszczone;
	private SpriteRenderer Comet;

    public GameObject Explosion;
    private float x;

    private void Start()
    {
        Comet = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        x += Time.deltaTime * 300;
        transform.rotation = Quaternion.Euler(0, 0, x);
    }
    private void LateUpdate()
    {
        if (life == 2)
        {
            Comet.sprite = niezniszczone;
        }
        else if (life == 1)
        {
            Comet.sprite = podniszczone;
        }
    }
    private void Generate_explosion()
    {
        Vector2 Explosion_vector = new Vector2(transform.position.x, transform.position.y);
        Instantiate(Explosion, Explosion_vector, transform.rotation);
    }
    private void Destroy_comets()
    {
        if (life < 1)
        {
            Destroy(gameObject);
            Debug.Log("Niszczenie komety po uderzeniu");
            Generate_explosion();
        }
    }

    private void OnCollisionEnter2D(Collision2D niszczenie)
    {
        if (niszczenie.gameObject.tag == "Pocisk" && life >= 1)
        {
            life -= 1;
			Destroy_comets();
        }
        else if (niszczenie.gameObject.tag == "Player")
        {
			life = 0;
            Destroy_comets();
        }
    } 
}
