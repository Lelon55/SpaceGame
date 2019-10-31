using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake_Camera : MonoBehaviour
{
    internal void ShakeCamera()
    {
        GetComponent<Animator>().SetTrigger("shake");
    }
}
