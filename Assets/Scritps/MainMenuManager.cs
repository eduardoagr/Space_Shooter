using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager: MonoBehaviour {

    public void LoadScene(int sceneIdex) {

        if (sceneIdex < 3) {
            SceneManager.LoadScene(sceneIdex);
        } else {
            Debug.Log("quitting");
        }
    }

     void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
