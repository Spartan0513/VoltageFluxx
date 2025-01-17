﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeMenu : MonoBehaviour
{
    private void Start()
    {
        if (PersistentGUI.Instance == null)
        {
            SceneManager.LoadSceneAsync("TopBar", LoadSceneMode.Additive);
        }
        else
        {
            PersistentGUI.Instance.gameObject.SetActive(true);
        }
    }
}
