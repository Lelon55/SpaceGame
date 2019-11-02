using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin_enemy_laser : MonoBehaviour
{
    public Sprite[] lasers;
    public SpriteRenderer bullet;

    private void Start()
    {
        bullet.sprite = lasers[PlayerPrefs.GetInt("nr_enemy_laser")];
    }
}
