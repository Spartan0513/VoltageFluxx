﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
    public void Back()
    { 
        GetComponent<Button>().onClick.AddListener(LevelManager.GetInstance().ReturnToLevelSelect);
    }
}