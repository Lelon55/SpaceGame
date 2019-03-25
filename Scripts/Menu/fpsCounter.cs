using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fpsCounter : MonoBehaviour {

    public Text Txt_fpsCounter;
    float frames, fps, deltaTime;
	// Use this for initialization
	void Start () {
        Txt_fpsCounter = gameObject.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float msec = deltaTime * 1000.0f;
        fps = 1.0f / deltaTime;
        string text1 = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        Txt_fpsCounter.text = text1;
    }
}
