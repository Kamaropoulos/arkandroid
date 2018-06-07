using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{

    public int lives = 1;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerExit2D(Collider2D col) {
        lives--;
        if (lives < 1) {
            Destroy(this.gameObject);
        }
    }
}