﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour {

    [Range(1, 20)]
    public int speed = 5;
    public float moveStep = 0.5f;
    private Rigidbody2D rigidbody = null;

    private Vector3 startPosition;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        float moveXY = Input.GetAxis("Horizontal");
        float x = moveXY * speed * moveStep;
        rigidbody.velocity = new Vector2(x, 0);
	}

    public void Reset() {
        transform.position = startPosition;
    }
}