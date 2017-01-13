﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EnvironmentPillar : MonoBehaviour {

    private BoxCollider spawnarea;
    [SerializeField]
    [Range(1, 10)]
    private int debrisCount;

    [SerializeField]
    private GameObject[] debris;

    [SerializeField]
    [Range(1, 5)]
    private int healthPoints;

    // Use this for initialization
    void Start ()
    {
        spawnarea = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (healthPoints == 0)
        {
            dropDebris();

            Destroy(gameObject);
        }
	}

    private void dropDebris()
    {
        if (debris.Length == 0)
        {
            return;
        }

        for (int i = 0; i < debrisCount; i++)
        {
            GameObject drop = debris[UnityEngine.Random.Range(0, debris.Length - 1)];
            Bounds b = spawnarea.bounds;

            Instantiate(drop, new Vector3(UnityEngine.Random.Range(b.min.x,b.max.x), UnityEngine.Random.Range(b.min.y, b.max.y), UnityEngine.Random.Range(b.min.z, b.max.z)), Quaternion.identity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            healthPoints--;
        }
    }
}
