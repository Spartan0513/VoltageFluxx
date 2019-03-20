﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Anim : MonoBehaviour
{
    private Transform playerPos;
    private float distance;
    private Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(playerPos.position, transform.position);
        myAnim.SetFloat("Distance", distance);
    }
}
