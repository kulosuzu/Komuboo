﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour {


    public void Restart()
    {
        SceneManager.LoadScene("MainGame"); 
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
