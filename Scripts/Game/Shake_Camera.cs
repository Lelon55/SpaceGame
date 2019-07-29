using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake_Camera : MonoBehaviour
{
    private Animator shake_animator;

    private void Start()
    {
        shake_animator = GetComponent<Animator>();
    }

    internal void ShakeCamera()
    {
        shake_animator.SetTrigger("shake");
    }
}
