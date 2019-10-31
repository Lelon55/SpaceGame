﻿using UnityEngine;
using System.Collections;

public class Destroy_enemy_bullet : MonoBehaviour
{
    private void FixedUpdate()
    {
        Destroy(gameObject, 2.0f);
    }

    private void OnCollisionEnter2D(Collision2D destroy)
    {
        if (destroy.gameObject.tag == "Player" || destroy.gameObject.tag == "Pocisk")
        {
            Destroy(gameObject);
        }
    }
}
