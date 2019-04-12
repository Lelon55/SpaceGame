using UnityEngine;
using System.Collections;

public class Destroy_comet : MonoBehaviour {

    public int life = 0;
    public Sprite podniszczone, niezniszczone;
	private SpriteRenderer Comet;

    public GameObject Explosion;
    private float rotation_z;

    private void Start()
    {
        Comet = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        rotation_z += Time.deltaTime * 300;
        transform.rotation = Quaternion.Euler(0, 0, rotation_z);
    }
    private void LateUpdate()
    {
        Comet.sprite = Show_Comet();
    }

    private Sprite Show_Comet()
    {
        if (life >= 2)
        {
            return niezniszczone;
        }
        return podniszczone;
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
            Generate_explosion();
            //Debug.Log("Niszczenie komety po uderzeniu");
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
