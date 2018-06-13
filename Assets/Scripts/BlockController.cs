using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockController : MonoBehaviour {

    public Sprite[] sprites;
    public Dictionary<string, Sprite> spritesDictionary = new Dictionary<string, Sprite>();
    private GameController game;

    public int lives = 1;
    public int currentLives = 1;
    
    void Start() {
        game = GameObject.Find("GameController").GetComponent<GameController>();
        UpdateSpritesDictionary();
    }

    void OnTriggerExit2D(Collider2D col) {
        currentLives--;
        if (currentLives < 1) {
            game.AddPoints(lives * 25);
            Destroy(this.gameObject);
        }
    }

    public void Initialize(string color, int lives) {
        UpdateSpritesDictionary();
        SetColor(color);
        this.lives = lives;
        this.currentLives = lives;
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