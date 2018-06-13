using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelInfo {
    public string resourceName;
    public string levelName;

    public LevelInfo(string resourceName, string levelName) {
        this.resourceName = resourceName;
        this.levelName = levelName;
    }
}

public class GameController : MonoBehaviour {

    public GameObject gridController;

    public static Dictionary<int, LevelInfo> levels;
    public int currentLevel = 1;

    private int lives = 3;
    public int points = 0;
    public GameObject[] lifeSprites = new GameObject[3];
    private Text scoreText;

    private GameObject ui;
    private UIController uiController;

	// Use this for initialization
	void Start () {
        scoreText = GameObject.Find("ScoreTextScore").GetComponent<Text>();

        ui = GameObject.Find("UI");
        uiController = ui.GetComponent<UIController>();

        levels = new Dictionary<int, LevelInfo>();

        // Load level names from resources
        TextAsset levelsRaw = Resources.Load("Levels") as TextAsset;

        // Split into lines
        string[] linesRaw = levelsRaw.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        for (int i = 1; i <= Int32.Parse(linesRaw[0]); i++) {
            string[] data = linesRaw[i].Split(',');
            levels.Add(i, new LevelInfo(data[0], data[1]));
        }

        uiController.UpdateText();

        gridController.GetComponent<GridController>().LoadLevel(levels[currentLevel].resourceName);
    }
	
	// Update is called once per frame
	void Update () {
        // Cheat
        if (Input.GetKeyDown("c"))
            gridController.GetComponent<GridController>().RemoveAllButOne();
    }

    public void NextLevel() {
        currentLevel++;

        gridController.GetComponent<GridController>().ResetGrid();

        ResetBallAndPaddle();

        // Clear Screen
        GameObject.Find("BallAndPaddle").transform.Find("paddle").Find("ball").gameObject.SetActive(true);
        GameObject.Find("BallAndPaddle").transform.Find("paddle").gameObject.SetActive(true);
        GameObject.Find("Bottom").transform.Find("bottomCover").gameObject.SetActive(true);

        ui.transform.Find("CanvasGameOver").gameObject.SetActive(false);
        ui.transform.Find("CanvasLevelCleared").gameObject.SetActive(false);
        ui.transform.Find("CanvasScore").gameObject.SetActive(true);
        ui.transform.Find("CanvasGameCleared").gameObject.SetActive(false);
        
        lives = 3;

        GameObject.Find("bottomCover").transform.Find("life1").gameObject.SetActive(true);
        GameObject.Find("bottomCover").transform.Find("life2").gameObject.SetActive(true);
        GameObject.Find("bottomCover").transform.Find("life3").gameObject.SetActive(true);

        gridController.GetComponent<GridController>().LoadLevel(levels[currentLevel].resourceName);
    }

    public void RestartGame() {
        currentLevel = 1;
        gridController.GetComponent<GridController>().ResetGrid();

        ResetBallAndPaddle();

        // Clear Screen
        GameObject.Find("BallAndPaddle").transform.Find("paddle").Find("ball").gameObject.SetActive(true);
        GameObject.Find("BallAndPaddle").transform.Find("paddle").gameObject.SetActive(true);
        GameObject.Find("Bottom").transform.Find("bottomCover").gameObject.SetActive(true);

        ui.transform.Find("CanvasScore").gameObject.SetActive(true);

        points = 0;

        lives = 3;

        GameObject.Find("bottomCover").transform.Find("life1").gameObject.SetActive(true);
        GameObject.Find("bottomCover").transform.Find("life2").gameObject.SetActive(true);
        GameObject.Find("bottomCover").transform.Find("life3").gameObject.SetActive(true);

        ui.transform.Find("CanvasGameOver").gameObject.SetActive(false);
        ui.transform.Find("CanvasLevelCleared").gameObject.SetActive(false);
        ui.transform.Find("CanvasScore").gameObject.SetActive(true);
        ui.transform.Find("CanvasGameCleared").gameObject.SetActive(false);

        gridController.GetComponent<GridController>().LoadLevel(levels[currentLevel].resourceName);
    }

    public void EmptyLevel() {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("block");
        for (int i = 0; i < blocks.Length; i++) {
            Destroy(blocks[i]);
        }
    }

    public void ResetBallAndPaddle() {
        GameObject.Find("BallAndPaddle").transform.Find("paddle").Find("ball").GetComponent<BallController>().Reset();
        GameObject.Find("BallAndPaddle").transform.Find("paddle").GetComponent<PaddleController>().Reset();
    }

    public void LostLife() {
        lives--;
        UpdateLives();
        if (lives < 0) {
            GameOver();
        }
    }

    public void AddPoints(int points) {
        this.points += points;
        uiController.UpdateText();
        CheckClear();
    }

    public int GetScore() {
        return points;
    }

    public string GetCurrentLevelName() {
        return levels[currentLevel].levelName;
    }

    void CheckClear() {
        Debug.Log(gridController.GetComponent<GridController>().GetBlocksLeft());
        if (gridController.GetComponent<GridController>().GetBlocksLeft() == 0) {
            Clear();
        }
    }

    private void UpdateLives() {
        Debug.Log("Updating Lives: " + lives);
        switch (lives) {
            case 2:
                lifeSprites[2].active = false;
                break;
            case 1:
                lifeSprites[1].active = false;
                break;
            case 0:
                lifeSprites[0].active = false;
                break;
        }
    }

    private void Clear() {
        gridController.GetComponent<GridController>().ResetGrid();

        // Clear Screen
        GameObject.Find("ball").SetActive(false);
        GameObject.Find("paddle").SetActive(false);
        GameObject.Find("bottomCover").SetActive(false);

        if (currentLevel + 1 > levels.Count) {
            // Display Game Cleared
            ui.transform.Find("CanvasGameOver").gameObject.SetActive(false);
            ui.transform.Find("CanvasLevelCleared").gameObject.SetActive(false);
            ui.transform.Find("CanvasScore").gameObject.SetActive(false);
            ui.transform.Find("CanvasGameCleared").gameObject.SetActive(true);
        } else {
            // Display Level Cleared
            ui.transform.Find("CanvasGameOver").gameObject.SetActive(false);
            ui.transform.Find("CanvasLevelCleared").gameObject.SetActive(true);
            ui.transform.Find("CanvasScore").gameObject.SetActive(false);
            ui.transform.Find("CanvasGameCleared").gameObject.SetActive(false);
        }
    }

    private void GameOver() {
        currentLevel = 1;

        gridController.GetComponent<GridController>().ResetGrid();

        // Clear Screen
        GameObject.Find("ball").SetActive(false);
        GameObject.Find("paddle").SetActive(false);
        GameObject.Find("bottomCover").SetActive(false);

        // Display Game Over
        ui.transform.Find("CanvasLevelCleared").gameObject.SetActive(false);
        ui.transform.Find("CanvasScore").gameObject.SetActive(false);
        ui.transform.Find("CanvasGameOver").gameObject.SetActive(true);
        ui.transform.Find("CanvasGameCleared").gameObject.SetActive(false);
    }
}