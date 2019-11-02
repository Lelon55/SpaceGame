using UnityEngine;
using System.Collections;

public class Destroy_comet : MonoBehaviour
{
    public int life = 0;
    public Sprite podniszczone, niezniszczone;
    private SpriteRenderer Comet;
    private GUIOperations GUIOperations;

    public GameObject Explosion;
    private float rotation_z;

    private void Start()
    {
        Comet = GetComponent<SpriteRenderer>();
        GUIOperations = GameObject.Find("spaceship").GetComponent<GUIOperations>();
    }

    private void Update()
    {
        rotation_z += Time.deltaTime * 300;
        transform.rotation = Quaternion.Euler(0, 0, rotation_z);
    }

    private void LateUpdate()
    {
        Comet.sprite = ShowComet();
    }

    private Sprite ShowComet()
    {
        if (life >= 2)
        {
            return niezniszczone;
        }
        return podniszczone;
    }

    private void DestroyComets()
    {
        if (life < 1)
        {
            Destroy(gameObject);
            GUIOperations.Generate(transform.position.x, transform.position.y, transform.rotation, Explosion);
        }
    }

    private void OnCollisionEnter2D(Collision2D destroy)
    {
        if (destroy.gameObject.tag == "Pocisk" && life >= 1)
        {
            life -= 1;
            DestroyComets();
        }
        else if (destroy.gameObject.tag == "Player")
        {
            life = 0;
            DestroyComets();
        }
    }
}
