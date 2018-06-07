using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    private float force = 200f;
    private Rigidbody2D rigidbody = null;
    private bool started = false;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!started && Input.GetKeyUp(KeyCode.Space)) {
            transform.SetParent(null);
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
            rigidbody.gravityScale = 0;
            rigidbody.AddForce(new Vector2(force, force));
            started = true;
        }
	}
}
