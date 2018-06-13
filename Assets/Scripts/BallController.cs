using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public GameObject gameController;

    private float force = 200f;
    private Rigidbody2D rigidbody = null;
    private bool started = false;

    private GameObject paddle;
    private Vector3 startPosition;

    private GameController game;

    // Use this for initialization
    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        game = gameController.GetComponent<GameController>();
        paddle = transform.parent.gameObject;
        startPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (!started) {
            transform.position = new Vector3(paddle.transform.position.x, transform.position.y, transform.position.z);
        }
		if(!started && Input.GetKeyUp(KeyCode.Space)) {
            transform.SetParent(null);
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
            rigidbody.gravityScale = 0;
            rigidbody.AddForce(new Vector2(force, force));
            started = true;
        }
	}

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name == "WallBottom") {
            game.LostLife();
            paddle.GetComponent<PaddleController>().Reset();
            Reset();
        }
    }

    public void Reset() {
        started = false;
        rigidbody.velocity = Vector3.zero;
        transform.position = startPosition;
        transform.parent = paddle.transform;
    }
}