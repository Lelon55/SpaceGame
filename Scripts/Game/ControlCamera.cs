using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamera : MonoBehaviour
{
    internal void Shake()
    {
        GetComponent<Animator>().SetTrigger("shake");
    }
}
