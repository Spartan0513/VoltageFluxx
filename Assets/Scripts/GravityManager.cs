﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameState;

public class GravityManager: MonoBehaviour, IGameState
{
	private const float GRAV_SPEED = 40f;
	private GameStateManager gsManager;
	private e_GAMESTATE state;
    [SerializeField]
    private bool use360 = true;

	void Start ()
	{
		gsManager = GameStateManager.GetInstance();
		gsManager.GameStateSubscribe(this.gameObject);
		state = gsManager.GetGameState();

		Screen.orientation = ScreenOrientation.Landscape;
		Input.gyro.enabled = true;
		Physics.gravity = Vector2.zero;
        use360 = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		CalcGrav();
    }

	void CalcGrav()
	{
		if (state != e_GAMESTATE.MENU)
		{
			if (state == e_GAMESTATE.PLAYING)
			{
				float xx;
				float yy;

				#if UNITY_EDITOR
				xx = Mathf.Sin (Camera.main.transform.localEulerAngles.z*Mathf.Deg2Rad) * GRAV_SPEED;
                yy = Mathf.Cos(-Camera.main.transform.localEulerAngles.z * Mathf.Deg2Rad) * GRAV_SPEED;

               
                if(yy < 0.0f && !use360)
                {
                    yy = 0.0f;
                }
                Physics2D.gravity = new Vector2(xx,-yy);
                
                #else
				
				xx = Input.gyro.gravity.x * GRAV_SPEED;
				yy = Input.gyro.gravity.y * GRAV_SPEED;

                if(yy > 0.0f && !use360)
                {
                    yy = 0.0f;
                }
                Physics2D.gravity = new Vector2 (xx, yy);
				
               #endif
            }
        }
	}

	public void ChangeState(e_GAMESTATE e_state)
	{
		state = e_state;
		//If going from paused to play, enable gravity on all gravity objects
		//If going from play to paused, disable gravity on all gravityObjects and freeze rigidbody.velocity (0,0,0) if object should.

		//Some things not affected by pause? oooo.... That can be interesting - Boom, implemented.
	}

    public void Use360(bool s)
    {
        use360 = s;
    }
}
