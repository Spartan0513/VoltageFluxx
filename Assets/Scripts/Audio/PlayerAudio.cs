﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField]
    private float lightImpact = 10;
    [SerializeField]
    private float mediumImpact = 20;
    [SerializeField]
    private float heavyImpact = 30;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Wall")
        {
            Debug.Log("test");
            if(rb.velocity.magnitude > lightImpact)
            {
                //Light Impact
            }
            else if (rb.velocity.magnitude > mediumImpact)
            {
                //Medium Impact
            }
            else if (rb.velocity.magnitude > heavyImpact)
            {
                //Heavy Impact
            }
        }
    }
}
