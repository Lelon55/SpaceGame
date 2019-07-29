using UnityEngine;
using System.Collections;

public class Player_life : MonoBehaviour
{
    private statystyki staty;

    private void Start()
    {
        staty = GameObject.Find("spaceship").GetComponent<statystyki>();
    }

    private void Update()
    {
        CurrentLife(0.2f * staty.Life);
    }

    private void CurrentLife(float size)
    {
        transform.localScale = new Vector3(size, 0.2f, 0f);
    }
}
