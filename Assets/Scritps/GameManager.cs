using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager: MonoBehaviour {
    [SerializeField]
    private bool _isGameOver = false;

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true) {

            SceneManager.LoadScene(0);
        }
    }

    internal void GameOver(bool isGameOver) {

        _isGameOver = isGameOver;

    }
}
