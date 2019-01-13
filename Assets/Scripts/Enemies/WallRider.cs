﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRider : BaseController
{
    [Header("WallRider")]
    [SerializeField] private float speed = 10f;

    protected override void ExtraStart()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        AttachToNormal();
    }

    private void Update()
    {
        MoveEnemy();
    }

    public void MovementCallback()
    {
        TurnAround();
    }

    private void MoveEnemy()
    {
        transform.Translate(-transform.right * speed * Time.deltaTime);
    }

    private void TurnAround()
    {
        transform.Rotate(new Vector3(0f, 180f, 0f));
        speed *= -1;
    }

    private void AttachToNormal()
    {
        RaycastHit hit;
        Vector3 startPoint = transform.position;

        if (Physics.Raycast(startPoint, -transform.up, out hit, 5f))
        {
            Quaternion surfaceRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            Quaternion newRotation = surfaceRotation * Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up);
            transform.rotation = newRotation;
            transform.position = hit.point;
        }
        else
        {
            Debug.LogError("WallRider failed to find wall to attach to");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            levelManager.RestartLevel();
        }
    }
}
