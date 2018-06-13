using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    private IEnumerator ActivateScene(Scene scene) {
        yield return new WaitForEndOfFrame();
        SceneManager.SetActiveScene(scene);
    }

    public void RestartGame() {
        SceneManager.LoadScene("Level", LoadSceneMode.Single);

        //StartCoroutine(ActivateScene(SceneManager.GetSceneByBuildIndex(1)));
        //SceneManager.UnloadSceneAsync(2);
    }
}