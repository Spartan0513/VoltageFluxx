﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEnemy : EnemyController
{
	[SerializeField]
	private bool pushObject = true;
	private bool playerPushed = false;
	private bool playerPulled = false;
	private float windPower = 0.1f;
	private GameObject player;
	private Rigidbody2D playerBody;
	[SerializeField]
	private GameObject field;
	[SerializeField]
	private Vector2 startPos;
	[SerializeField]
	private Vector2 endPos;
	private Vector2 windDiection;
	private Vector2 pullDirection;

    // Start is called before the first frame update
    protected override void ExtraStart()
    {
		player = GameObject.FindGameObjectWithTag("Player");
		playerBody = player.GetComponent<Rigidbody2D>();

		field = this.gameObject.transform.GetChild(1).gameObject;
		startPos = this.gameObject.transform.GetChild(2).transform.position;
		endPos = this.gameObject.transform.GetChild(3).transform.position;
		field.gameObject.GetComponent<Renderer>().enabled = false;
		windDiection = startPos - endPos;
		pullDirection = endPos - startPos;
    }

    // Update is called once per frame
    void Update()
    {
		if(playerPushed == true)
		{
			playerBody.velocity -= windDiection * windPower;
		}
		if(playerPulled == true)
		{
			playerBody.velocity -= pullDirection * windPower;
		}
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player" && pushObject == true)
		{
			playerPushed = true;
		}
		if(other.gameObject.tag == "Player" && pushObject != true)
		{
			playerPulled = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			playerPushed = false;
			playerPulled = false;
		}
	}
}
