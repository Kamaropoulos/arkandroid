using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    private GameObject ui;

    private void Start() {
        ui = GameObject.Find("UI");
    }

    public void UpdateText() {
        // Update Level Name
        string level = GameObject.Find("GameController").GetComponent<GameController>().GetCurrentLevelName();
        ui.transform.Find("CanvasGameOver").Find("LevelTextGameOver").GetComponent<Text>().text = level;
        ui.transform.Find("CanvasLevelCleared").Find("LevelTextLevelCleared").GetComponent<Text>().text = level;
        ui.transform.Find("CanvasScore").Find("LevelTextScore").GetComponent<Text>().text = level;

        // Update Score
        string score = "Score: " + GameObject.Find("GameController").GetComponent<GameController>().GetScore();
        ui.transform.Find("CanvasScore").Find("ScoreTextScore").GetComponent<Text>().text = score;
        ui.transform.Find("CanvasGameOver").Find("ScoreTextGameOver").GetComponent<Text>().text = score;
        ui.transform.Find("CanvasLevelCleared").Find("ScoreTextLevelCleared").GetComponent<Text>().text = score;

    }

    public void TryAgain() {
        GameObject.Find("GameController").GetComponent<GameController>().RestartGame();
        UpdateText();
    }

    public void NextLevel() {
        GameObject.Find("GameController").GetComponent<GameController>().NextLevel();
        UpdateText();
    }

    public void Quit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
#else
         Application.Quit();
#endif
    }
}