using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockController : MonoBehaviour {

    public Sprite[] sprites;
    public Dictionary<string, Sprite> spritesDictionary = new Dictionary<string, Sprite>();

    public int lives = 1;

    // Use this for initialization
    void Start() {
        UpdateSpritesDictionary();
    }

    void OnTriggerExit2D(Collider2D col) {
        lives--;
        if (lives < 1) {
            Destroy(this.gameObject);
        }
    }

    public void Initialize(string color, int lives) {
        UpdateSpritesDictionary();
        SetColor(color);
        this.lives = lives;
    }

    void SetColor(string color) {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = spritesDictionary[color];
    }

    void UpdateSpritesDictionary() {
        foreach (Sprite sprite in sprites) {
            if (!spritesDictionary.ContainsKey(sprite.name)) {
                spritesDictionary.Add(sprite.name, sprite);
            }
        }
    }

}