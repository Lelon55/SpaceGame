using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin_enemy_laser : MonoBehaviour
{
    public Sprite[] lasers;
    public SpriteRenderer bullet;
    private int nr_laser;

    private void Start()
    {
        nr_laser = PlayerPrefs.GetInt("nr_enemy_laser");
        bullet.sprite = lasers[nr_laser];
    }
}
