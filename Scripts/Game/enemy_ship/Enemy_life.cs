using UnityEngine;
using System.Collections;

public class Enemy_life : MonoBehaviour
{
    public Destroy_enemy_ship destroy;

    private void Start()
    {
        //destroy = GameObject.Find("enemy_spaceship").GetComponent<Destroy_enemy_ship>();
    }

    private void LateUpdate()
    {
        Aktulanypasek(0.2f * destroy.life);
    }

    private void Aktulanypasek(float size)
    {
        transform.localScale = new Vector2(size, 0.2f);
    }
}
